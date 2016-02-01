using Newtonsoft.Json;
using OculusClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmaranthineServer
{
    internal class Sentinel
    {
        private ParallelListener parallelListener;

        public Sentinel(ParallelListener parallelListener)
        {
            this.parallelListener = parallelListener;
        }

        public void monitorClients()
        {
            while (true)
            {
                List<ClientDetails> garbageList = new List<ClientDetails>();
                try
                {
                    foreach (ClientDetails clients in parallelListener.listOfClients)
                    {
                        try
                        {
                            if (clients != null)
                            {
                                if (clients.server != null)
                                {
                                    NetworkStream networkStream = clients.clientSocket.GetStream();
                                    networkStream.Flush();

                                    IMPacket askForClientList = new IMPacket();
                                    askForClientList.Action = Mnemonics.Actions.heartBeat;
                                    String JSONServerResponse = JsonConvert.SerializeObject(askForClientList);

                                    String serverResponse = JSONServerResponse + Environment.NewLine;
                                    byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                                    networkStream.Flush();
                                }
                                else
                                {
                                    garbageList.Add(clients);
                                }
                            }

                            foreach (ClientDetails client in garbageList)
                            {
                                parallelListener.listOfClients.Remove(client);
                            }
                        }
                        catch (Exception e)
                        {
                            clients.server.clientSocket.Close();
                            clients.server.listOfClients.Remove(clients.server.ClientID);
                            clients.server.removeUser(clients.label);

                            garbageList.Add(clients);
                        }
                    }
                }
                catch (Exception e)
                {

                }

                Thread.Sleep(5000);
            }
        }
    }
}