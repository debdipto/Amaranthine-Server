using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AmaranthineServer
{
    public class Server
    {
        private frm_Main frm_Main;
        public System.Net.Sockets.TcpClient clientSocket;
        public Boolean needsUserList;         
        public ObservableCollection<ClientDetails> listOfClients;
        ParallelListener dispatcher;
        public ClientDetails ClientID;
        Object locker = new Object();
        private bool authenticated = false;
        String userName = "";
        String password = "";
        String deviceType = "";
        String c = Char.ConvertFromUtf32(30);

        byte[] transient = null;

        public Server(frm_Main frm_Main, System.Net.Sockets.TcpClient clientSocket, ObservableCollection<ClientDetails> listOfClients, ParallelListener _dispatcher)
        {
            needsUserList = false;
            this.frm_Main = frm_Main;
            this.clientSocket = clientSocket;
            this.listOfClients = listOfClients;
            dispatcher = _dispatcher;
        }       

        private void readByteArrayFromClient(int length)
        {
            try
            {
                NetworkStream networkStream = clientSocket.GetStream();
                int numberOfBytesRead = 0;
                MemoryStream receivedData = new MemoryStream();
                byte[] buffer = new byte[1024];
                do
                {
                    int bytesread = networkStream.Read(buffer, 0, buffer.Length);
                    numberOfBytesRead += bytesread;
                    if (numberOfBytesRead > 0)
                        receivedData.Write(buffer, 0, bytesread);
                }
                while (numberOfBytesRead < length);

                transient = null;
                transient = receivedData.ToArray();
            }
            catch (Exception ex)
            {
                displayInMainForm(ex.Message + " Thrown while reading from client");
                displayInMainForm(ex.Message + " Thrown while reading from client", userName + ".txt");
            }
        }

        private string readFromClient()
        {
            try
            {
                Monitor.Enter(locker);
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                networkStream.Flush();
                transient = null;
                transient = bytesFrom;

                String dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                if (dataFromClient.Contains("$$"))
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.LastIndexOf("$$"));

                if (dataFromClient.Contains(c))
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.LastIndexOf(c));

                Monitor.Exit(locker);
                return dataFromClient;
            }
            catch (Exception ex)
            {
                displayInMainForm("[ReadFunction] [Exception] " + ex.StackTrace + " Thrown while reading from client");
                displayInMainForm("[ReadFunction] [Exception] " + ex.StackTrace + " Thrown while reading from client", userName + ".txt");
                Monitor.Exit(locker);

                throw ex;
            }
        }
        public bool writeToClient(String serverResponse, TcpClient tcpClient, bool display = true)
        {
            bool returnFlag = false;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.Flush();
                serverResponse += Environment.NewLine;
                if (display)
                {
                    displayInMainForm("[Writing] " + serverResponse + " To " + userName);
                    displayInMainForm("[Writing] " + serverResponse + " To " + userName, userName + ".txt");
                }
                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
                returnFlag = true;
            }
            catch (Exception ex)
            {
                displayInMainForm(ex.Message + " Thrown while writing to client");
                displayInMainForm(ex.Message + " Thrown while writing to client", userName + ".txt");
                returnFlag = false;
            }
            return returnFlag;
        }

        public bool writeToClient(String serverResponse, bool display = true)
        {
            bool returnFlag = false;
            Monitor.Enter(locker);
            try
            {
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Flush();
                serverResponse += Environment.NewLine;
                if (display)
                {
                    displayInMainForm("[Writing] " + serverResponse + " To " + userName);
                    displayInMainForm("[Writing] " + serverResponse + " To " + userName, userName + ".txt");
                }
                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
                Monitor.Exit(locker);
                returnFlag = true;
            }
            catch (Exception ex)
            {
                displayInMainForm(ex.Message + " Thrown while writing to client");
                displayInMainForm(ex.Message + " Thrown while writing to client", userName + ".txt");
                returnFlag = false;
            }

            return returnFlag;
        }

        private void ImageWriteToClient()
        {
            NetworkStream networkStream = clientSocket.GetStream();
            ImageConverter converter = new ImageConverter();
            byte[] sendBytes = (byte[])converter.ConvertTo(AmaranthineServer.Properties.Resources.server, typeof(byte[]));

            writeToClient(sendBytes.Length.ToString());

            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }

        public bool Authentication(String username, String password)
        {
            try
            {
                OleDbConnection connect = new OleDbConnection();
                connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Optix.accdb;Persist Security Info=False;";
                connect.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM Userdetails";

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String tempUsername = reader["Username"].ToString();
                    String tempPassword = reader["Password"].ToString();

                    if (username == tempUsername && password == tempPassword)
                    {
                        deviceType = reader["devicetype"].ToString();
                        if (reader["clientlist"].ToString()=="Yes")
                            needsUserList = true;
                        else
                            needsUserList = false;

                        connect.Close();

                        return true;
                    }
                }

                connect.Close();
            }
            catch (Exception ex)
            {
                displayInMainForm("[AUTHENTICATION PHASE] [Exception] " + ex.StackTrace);
            }
            return false;
        }

        public void createDatabase(String username)
        {
            try
            {
                OleDbConnection connect = new OleDbConnection();
                connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Optix.accdb;Persist Security Info=False;";
                connect.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = connect;
                command.CommandText = "CREATE TABLE " + username + " (cur_timestamp TEXT, fromUser TEXT, toUser TEXT, chat TEXT);";

                OleDbDataReader reader = command.ExecuteReader();
                connect.Close();
            }
            catch (Exception ex)
            {
                displayInMainForm("[AUTHENTICATION PHASE] [Exception] " + ex.Message);
            }
        }

        private void logChat(String targetUsername, String fromUser, String toUser, string message)
        {
            try
            {
                DateTime dt = DateTime.Now;

                OleDbConnection connect = new OleDbConnection();
                connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Optix.accdb;Persist Security Info=False;";
                connect.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = connect;
                command.CommandText = "INSERT INTO " + targetUsername + " (cur_timestamp, fromUser, toUser, chat) VALUES ('" + dt.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture) + "','" + fromUser + "','" + toUser + "','" + message + "');";
                displayInMainForm("[Chatting] with " + toUser + " [Message]: " + message);
                OleDbDataReader reader = command.ExecuteReader();
                connect.Close();
            }
            catch (Exception ex)
            {
                displayInMainForm("[Chatting] [Exception] " + ex.Message);
            }
        }

        public void main()
        {
            string _ClientID = "";

            try
            {
                //Oculus backward compatible
                _ClientID = readFromClient();
                ClientID = new ClientDetails(_ClientID, clientSocket);

                userName = ClientID.userName;
                password = ClientID.password;
                displayInMainForm("[AUTHENTICATION PHASE] Client with User ID: " + userName + " requesting authentication");

                if (!Authentication(userName, password))
                {
                    writeToClient("OculusMessage:Authentication Failed$$");
                    displayInMainForm("[AUTHENTICATION PHASE] Client with User ID: " + userName + " Failed authentication");
                    return;
                }
                createDatabase(userName);
                writeToClient("OculusMessage:Logged In$$");
                ClientID.label.Text = ClientID.userName;
                displayInMainForm("[AUTHENTICATION PHASE] Client with User ID: " + ClientID.userName + " Logged in");
                displayInMainForm("[AUTHENTICATION PHASE] Client with User ID: " + ClientID.userName + " Logged in", userName + ".txt");
                addNewUser(ClientID.label);
                ClientID.server = this;
                authenticated = true;
                dispatcher.addClientToList(ClientID);
            }
            catch (Exception e)
            {
                displayInMainForm("[AUTHENTICATION PHASE] " + userName + " Disconnected due to " + e.Message);
                displayInMainForm("[AUTHENTICATION PHASE] " + userName + " Disconnected due to " + e.Message, userName + ".txt");
                return;
            }

            //Amaranthine compatible only

            try
            {
                while (true)
                {
                    String rawData = readFromClient();
                    String[] splitData = rawData.Split(c.ToCharArray());
                    //displayInMainForm("[rawdata] "+rawData);
                    foreach (String partJSON in splitData)
                    {
                        if (partJSON.Trim() == String.Empty)
                            continue;

                        dynamic dataFromClient = JsonConvert.DeserializeObject(partJSON);
                        if (dataFromClient.Action != (int)Mnemonics.Actions.datafromothers && dataFromClient.Action != (int)Mnemonics.Actions.clientlist)
                        {
                            displayInMainForm(rawData);
                            displayInMainForm(rawData, userName + ".txt");
                        }
                        int actionMnemonic = dataFromClient.Action;

                        switch (actionMnemonic)
                        {
                            case (int)Mnemonics.Actions.clientlist:
                                {
                                    //clientlist
                                    getClientList();
                                }
                                break;
                            case (int)Mnemonics.Actions.heartBeat:
                                {
                                    //disconnection test
                                    writeToClient(Mnemonics.Actions.heartBeat.ToString());
                                }
                                break;
                            //datafromothers
                            case (int)Mnemonics.Actions.datafromothers:
                                {
                                    dataFromOthers();
                                }
                                break;

                            //senddatanow:<from>:<to>:<message>
                            case (int)Mnemonics.Actions.senddatanow:
                                {
                                    sendDataNow(dataFromClient);
                                }
                                break;

                            //senddata:<from>:<to>:<message>
                            case (int)Mnemonics.Actions.senddata:
                                {//Parallel .foreach https://msdn.microsoft.com/en-us/library/dd460720%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                                    foreach (ClientDetails client in listOfClients)
                                    {
                                        if (client.userName == dataFromClient.TargetUsername)
                                        {
                                            client.messages.Add(new MessageContainer(dataFromClient.SourceUsername, dataFromClient.Message));
                                            logChat(userName, userName, client.userName, dataFromClient.Message);
                                            logChat(client.userName, userName, client.userName, dataFromClient.Message);
                                            //serverResponse += "OculusMessage:Message sent to client";
                                        }
                                    }
                                    //writeToClient(serverResponse);

                                }
                                break;
                            case (int)Mnemonics.Actions.deviceType:
                                {
                                    writeToClient(getDeviceType((String)dataFromClient.TargetUsername));
                                }
                                break;

                            case (int)Mnemonics.Actions.extract:
                                {
                                    insertIntoWarehouse(dataFromClient[1]);
                                    writeToClient("OculusMessage:Accepted");
                                }
                                break;
                            case (int)Mnemonics.Actions.SaveDiaryContent:
                                {
                                    writeToClient("OculusMessage:LengthReceived");
                                    saveDiaryContents(dataFromClient[1], Convert.ToInt32(dataFromClient[2]));
                                    writeToClient("OculusMessage:Accepted");
                                }
                                break;
                            default:
                                {
                                    displayInMainForm("Unknown mnemonic " + dataFromClient[0] + " from client " + ClientID.userName);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                displayInMainForm(userName + " Disconnected due to " + ex.Message);
                displayInMainForm(userName + " Disconnected due to " + ex.StackTrace, userName + ".txt");
                clientSocket.Close();
                dispatcher.removeClientFromList(ClientID);
                removeUser(ClientID.label);
            }
        }

        private void sendDataNow(dynamic dataFromClient)
        {       
            bool usageFlag = false;
            string JSONServerResponse = String.Empty;
            try
            {
                foreach (ClientDetails client in listOfClients)
                {
                    if (client.userName == (String)dataFromClient.TargetUsername)
                    {
                        usageFlag = true;

                        ReplyPayload<MessageContainer> serverResponse = new ReplyPayload<MessageContainer>();
                        serverResponse.source = (String)dataFromClient.SourceUsername;
                        serverResponse.Action = Mnemonics.Actions.senddatanow;
                        serverResponse.status = (int)Mnemonics.replyStatus.success;
                        serverResponse.reply = new MessageContainer[1];
                        serverResponse.reply[0] = new MessageContainer();
                        serverResponse.reply[0].username = (String)dataFromClient.SourceUsername;
                        serverResponse.reply[0].message = (String)dataFromClient.Message;
                        JSONServerResponse = JsonConvert.SerializeObject(serverResponse);

                        bool replyType= writeToClient(JSONServerResponse, client.clientSocket);
                        if (replyType)
                        {
                            displayInMainForm("[Chatting] " + JSONServerResponse);

                            logChat(userName, userName, client.userName, (String)dataFromClient.Message);

                            ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                            serverResponseToSender.Action = Mnemonics.Actions.serverReply;
                            serverResponseToSender.source = "AmaranthineServer";
                            serverResponseToSender.status = (int)Mnemonics.replyStatus.success;
                            serverResponseToSender.reply = new String[1];
                            serverResponseToSender.reply[0] = "Message sent to client";
                            JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                            writeToClient(JSONServerResponse, false);
                            displayInMainForm("[Chatting] " + JSONServerResponse);
                        }
                        else
                        {
                            dispatcher.removeClientFromList(client);
                            removeUser(client.label);
                        }
                    }
                }

                if (!usageFlag)
                {
                    ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                    serverResponseToSender.source = "AmaranthineServer";
                    serverResponseToSender.status = (int)Mnemonics.replyStatus.failure;
                    serverResponseToSender.reply = new String[1];
                    serverResponseToSender.reply[0] = "Message not sent to client";
                    JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                    writeToClient(JSONServerResponse, false);
                    displayInMainForm("[Chatting] [Exception] " + JSONServerResponse);
                }
            }
            catch (Exception e)
            {
                displayInMainForm("[Chatting] [Exception] " + e.Message);

                displayInMainForm("[Chatting] [Exception] " +e.StackTrace);

                ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                serverResponseToSender.Action = Mnemonics.Actions.senddatanow;
                serverResponseToSender.source = "AmaranthineServer";
                serverResponseToSender.status = (int)Mnemonics.replyStatus.internalFailure;
                serverResponseToSender.reply = new String[1];
                serverResponseToSender.reply[0] = "Message not sent to client due to internal Exception";
                JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                writeToClient(JSONServerResponse, false);
            }
        }

        public void dataFromOthers()
        {
            ReplyPayload<MessageContainer> serverResponse = new ReplyPayload<MessageContainer>();
            serverResponse.source = "AmaranthineServer";
            serverResponse.Action = Mnemonics.Actions.datafromothers;

            serverResponse.reply = new MessageContainer[ClientID.messages.Count];
            int i = 0;
            foreach (MessageContainer messages in ClientID.messages)
            {
                serverResponse.reply[i] = messages;
                i++;
            }

            string JSONServerResponse = String.Empty;

            serverResponse.status = (int)Mnemonics.replyStatus.success;
            JSONServerResponse = JsonConvert.SerializeObject(serverResponse);
            writeToClient(JSONServerResponse, false);

            ClientID.messages.Clear();
        }

        public void getClientList()
        {
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

                displayInMainForm("[ClientList] "+ JSONServerResponse);

                writeToClient(JSONServerResponse, false);
            }
            catch (Exception e)
            {
                displayInMainForm("[ClientList] [Exception] " + e.Message);

                displayInMainForm("[ClientList] [Exception] " + e.StackTrace);

                ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                serverResponseToSender.Action = Mnemonics.Actions.clientlist;
                serverResponseToSender.source = "AmaranthineServer";
                serverResponseToSender.status = (int)Mnemonics.replyStatus.internalFailure;
                serverResponseToSender.reply = new String[1];
                serverResponseToSender.reply[0] = "Unable to get clientlist due to internal Exception";
                String JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                writeToClient(JSONServerResponse, false);
            }
        }

        private string getDeviceType(string username)
        {
            String userDeviceType = String.Empty;
            String JSONServerResponse = String.Empty;
            bool usageFlag = false;

            try
            {
                OleDbConnection connect = new OleDbConnection();
                connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Optix.accdb;Persist Security Info=False;";
                connect.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM Userdetails";

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String tempUsername = reader["Username"].ToString();
                    if (tempUsername == username)
                    {
                        userDeviceType = reader["devicetype"].ToString();

                        ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                        serverResponseToSender.Action = Mnemonics.Actions.deviceType;
                        serverResponseToSender.source = "AmaranthineServer";
                        serverResponseToSender.status = (int)Mnemonics.replyStatus.success;
                        serverResponseToSender.reply = new String[1];
                        serverResponseToSender.reply[0] = userDeviceType;
                        JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                        displayInMainForm("[DeviceType] " + JSONServerResponse);

                        usageFlag = true;
                    }
                }

                if (!usageFlag)
                {
                    ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                    serverResponseToSender.Action = Mnemonics.Actions.deviceType;
                    serverResponseToSender.source = "AmaranthineServer";
                    serverResponseToSender.status = (int)Mnemonics.replyStatus.failure;
                    serverResponseToSender.reply = new String[1];
                    serverResponseToSender.reply[0] = "Unable to get deviceType as user not found.";
                    JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                    writeToClient(JSONServerResponse, false);
                    displayInMainForm("[DeviceType] [Exception] " + JSONServerResponse);
                }
            }
            catch (Exception ex)
            {
                displayInMainForm("[Device Type] [Exception] " + ex.Message);

                displayInMainForm("[Device Type] [Exception] " + ex.StackTrace);

                ReplyPayload<String> serverResponseToSender = new ReplyPayload<String>();
                serverResponseToSender.Action = Mnemonics.Actions.deviceType;
                serverResponseToSender.source = "AmaranthineServer";
                serverResponseToSender.status = (int)Mnemonics.replyStatus.internalFailure;
                serverResponseToSender.reply = new String[1];
                serverResponseToSender.reply[0] = "Unable to get deviceType due to internal exception";
                JSONServerResponse = JsonConvert.SerializeObject(serverResponseToSender);
                writeToClient(JSONServerResponse, false);
            }

            return JSONServerResponse;
        }

        private void saveDiaryContents(String diaryfilename, int length)
        {
            displayInMainForm("Reading file contents..");

            readByteArrayFromClient(length);

            displayInMainForm("File contents read..");

            byte[] encryptedContents = transient;

            if (!Directory.Exists(userName))
            {
                Directory.CreateDirectory(userName);
            }
            displayInMainForm("Saving file..");
            File.WriteAllBytes(userName + "\\" + diaryfilename, encryptedContents);
            displayInMainForm("File saved..");
        }

        private void insertIntoWarehouse(string insertData)
        {
            String[] lines = insertData.Split(Environment.NewLine.ToCharArray());
            foreach (String line in lines)
            {
                String[] columns = line.Split(',');
                String newLine = String.Empty;
                foreach (String column in columns)
                {
                    if (column.Trim() != String.Empty)
                        newLine += column.ToUpper() + ",";
                }

                File.WriteAllText(userName + "_warehouse.txt", newLine);
            }
        }

        public void addNewUser(Label newUser)
        {
            try
            {
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        frm_Main.addNewUser(newUser);
                    });
                }
                else
                {
                    frm_Main.addNewUser(newUser);
                }
            }
            catch (Exception)
            { }
        }

        public void removeUser(Label newUser)
        {
            try
            {
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        frm_Main.removeUser(newUser);
                    });
                }
                else
                {
                    frm_Main.removeUser(newUser);
                }
            }
            catch (Exception)
            { }
        }

        public void displayInMainForm(String message, String filename)
        {
            try
            {
                if (authenticated)
                    message = "[" + userName + "] " + message;
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        frm_Main.displayLine(message, filename);
                    });
                }
                else
                {
                    frm_Main.displayLine(message, filename);
                }
            }
            catch (Exception)
            { }
        }


        public void displayInMainForm(String message)
        {
            try
            {
                if (authenticated)
                    message = "[" + userName + "] " + message;
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

        public void addClientToList(ClientDetails client)
        {
            try
            {                
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        dispatcher.addClientToList(client);
                    });
                }
                else
                {
                    dispatcher.addClientToList(client);
                }
            }
            catch (Exception)
            { }
        }

        public void removeClientFromList(ClientDetails client)
        {
            try
            {
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        dispatcher.removeClientFromList(client);
                    });
                }
                else
                {
                    dispatcher.removeClientFromList(client);
                }
            }
            catch (Exception)
            { }
        }
    }
}