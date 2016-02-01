using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AmaranthineServer
{
    public class ParallelListener
    {
        private frm_Main frm_Main;
        public TcpListener serverSocket = null;
        public TcpClient clientSocket = null;
        public volatile ObservableCollection<ClientDetails> listOfClients = new ObservableCollection<ClientDetails>();

        public ParallelListener(frm_Main frm_Main)
        {
            this.frm_Main = frm_Main;
            listOfClients.CollectionChanged += ListOfClients_CollectionChanged;
        }

        public void addClientToList(ClientDetails ClientID)
        {
            listOfClients.Add(ClientID);
            updateClientListToAll();
        }

        public void removeClientFromList(ClientDetails ClientID)
        {
            listOfClients.Remove(ClientID);
            updateClientListToAll();
        }

        private void ListOfClients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //updateClientListToAll();
        }

        public void writeToClient(ClientDetails client, String serverResponse, bool display=true)
        {
            try
            {
                NetworkStream networkStream = client.clientSocket.GetStream();
                networkStream.Flush();
                serverResponse += Environment.NewLine;
                if (display)
                {
                    displayInMainForm("[Writing] " + serverResponse + " To " + client.userName);
                }
                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
            catch (Exception ex)
            {
                displayInMainForm(ex.Message + " Thrown while writing to client");
            }
        }

        public void updateClientListToAll()
        {
            displayInMainForm("[Amaranthine Server] Pushing changed Client list to all "+listOfClients.Count.ToString());
            String clients = String.Empty;

            try
            {
                int i = 0;
                string JSONServerResponse = String.Empty;

                ReplyPayload<String> serverResponse = new ReplyPayload<String>();
                serverResponse.Action = Mnemonics.Actions.clientlist;
                serverResponse.source = "AmaranthineServer";
                serverResponse.reply = new String[listOfClients.Count];
                foreach (ClientDetails client in listOfClients)
                {
                    serverResponse.reply[i] = client.userName;
                    i++;
                }
                serverResponse.status = (int)Mnemonics.replyStatus.success;
                JSONServerResponse = JsonConvert.SerializeObject(serverResponse);

                displayInMainForm("[ClientList] " + JSONServerResponse);
                foreach (ClientDetails client in listOfClients)
                {
                    if(client.server.needsUserList)
                        writeToClient(client,JSONServerResponse);
                }
            }
            catch (Exception e)
            {
                displayInMainForm("[Exception] " + e.StackTrace);
            }
            displayInMainForm("[Amaranthine Server]" + clients);
        }

        public void displayInMainForm(String message)
        {
            try
            {
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        frm_Main.displayLine(message);
                    });
                }
                else
                {
                    frm_Main.displayLine(message);
                }
            }
            catch (Exception)
            { }
        }

        public void listen()
        {
            String host = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            String ServerIP = "";
            foreach (IPAddress local in localIPs)
            {
                if (local.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (local.ToString().IndexOf("192.168.") != -1)
                    {
                        displayInMainForm("Server IP: " + local.ToString());
                        ServerIP = local.ToString();
                    }
                }
            }

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            serverSocket = new TcpListener(IPAddress.Any,Convert.ToInt32(AmaranthineServer.Properties.Settings.Default.Port));

            clientSocket = default(TcpClient);
            displayInMainForm("Listening Started");

            serverSocket.Start();
            displayInMainForm("Accepting connections from client");

            List<Thread> listOfServers = new List<Thread>();
            Sentinel sentinel = new Sentinel(this);
            Thread sentinelThread = new Thread(new ThreadStart(sentinel.monitorClients));
            sentinelThread.Start();
            while (frm_Main.exitcode != "exit")
            {
                try
                {
                    clientSocket = serverSocket.AcceptTcpClient();

                    Server newServerObj = new Server(frm_Main, clientSocket, listOfClients,this);
                    Thread newServer = new Thread(new ThreadStart(newServerObj.main));
                    listOfServers.Add(newServer);
                    newServer.Start();
                }
                catch (Exception)
                { }
            }

            try
            {
                if (sentinelThread.IsAlive)
                    sentinelThread.Abort();
                clientSocket.Close();
                serverSocket.Stop();

                foreach (ClientDetails client in listOfClients)
                {
                    try
                    {
                        client.clientSocket.Close();
                    }
                    catch (Exception)
                    { }
                }
                listOfClients.Clear();
                foreach (Thread obj in listOfServers)
                    obj.Abort();
            }
            catch (Exception)
            { }

            displayInMainForm("Listening Stopped");
        }
    } 
}