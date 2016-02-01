using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmaranthineServer
{
    public partial class ClientView : Form
    {
        String username;
        Thread refreshThread = null;

        public ClientView(String _username, ClientDetails _clientDetails)
        {
            InitializeComponent();
            this.Text = _username;
            username = _username;
            clientDetails = _clientDetails;

            if (File.Exists(username + ".txt"))
            {
                displayInUserDisplay(File.ReadAllText(username + ".txt"));
            }
            else
            {
                File.Create(username + ".txt");
            }
            fileSystemWatcher1.Path = Path.GetDirectoryName(Application.ExecutablePath);
            refreshThread = new Thread(new ThreadStart(this.refresh));
            refreshThread.Start();
        }
     
        public void displayInUserDisplay(String message)
        {
            txtb_Display.AppendText(message);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            if (File.Exists("Archived_" + username + ".txt"))
            {
                String logcontents = File.ReadAllText(username + ".txt");
                File.AppendAllText("Archived_" + username + ".txt", logcontents);
                File.Delete(username + ".txt");
            }
            else
                File.Move(username + ".txt", "Archived_" + username + ".txt");
            txtb_Display.Clear();
        }

        public void refresh()
        {
            try
            {
                while (true)
                {
                    txtb_ClientDetailsViewer.Text = "Username: " + clientDetails.userName + Environment.NewLine + "Password: " + clientDetails.password + Environment.NewLine + "Messages:" + Environment.NewLine;
                    foreach (MessageContainer message in clientDetails.messages)
                    {
                        txtb_ClientDetailsViewer.Text += message.message + Environment.NewLine;
                    }
                    Thread.Sleep(250);
                }
            }
            catch (Exception)
            {

            }
        }

        private void btn_DisconnectClient_Click(object sender, EventArgs e)
        {
            try
            {
                displayInUserDisplay("Closing connection with " + username);
                //clientDetails.messages.Add("Server Administrator>You have been logged out...");     Try and implement
                clientDetails.clientSocket.Close();
            }
            catch (Exception ex)
            {
                displayInUserDisplay("Exception in closing client Socket " + username + ".txt with message " + ex.Message);
            }
            btn_DisconnectClient.Enabled = false;
        }

        private void ClientView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                clientDetails.clientView = null;
                clientDetails.label.BackColor = Color.Transparent;
                fileSystemWatcher1.Dispose();
                if (refreshThread != null && refreshThread.IsAlive)
                    refreshThread.Abort();
            }
            catch (Exception)
            {

            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.Name == username + ".txt")
                    txtb_Display.AppendText(File.ReadAllText(username + ".txt"));
            }
            catch (Exception ex)
            {
                displayInUserDisplay("Exception in reading " + username + ".txt with message " + ex.Message);
            }
        }
    }
}
