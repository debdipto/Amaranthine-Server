using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace AmaranthineServer
{
    public class ClientDetails
    {
        public String ClientID;
        public String ClientName;
        public TcpClient clientSocket;
        public List<MessageContainer> messages;
        public List<String> Files;
        public String userName;
        public String password;
        public Label label;
        public ClientView clientView;
        public Server server;
        public ClientDetails(String _ClientID, TcpClient _clientSocket)
        {
            ClientID = _ClientID;
            clientSocket = _clientSocket;
            messages = new List<MessageContainer>();
            ClientName = "";
            userName = ClientID.Split(':')[0];
            password = ClientID.Split(':')[1];
            label = new Label();
            label.AutoSize = true;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.Width = 216;
            label.MinimumSize = new System.Drawing.Size(205, 20);
            label.Margin = new Padding(0, 0, 0, 5);
            label.Click += label_Click;
            label.MouseEnter += label_MouseEnter;
            label.MouseLeave += label_MouseLeave;
            clientView = null;
            server = null;
        }

        void label_MouseLeave(object sender, EventArgs e)
        {
            label.BackColor = Color.Transparent;
        }

        void label_MouseEnter(object sender, EventArgs e)
        {
            label.BackColor = Color.Orange;
        }

        void label_Click(object sender, EventArgs e)
        {
            if (clientView == null)
            {
                clientView = new ClientView(userName, this);
                label.BackColor = Color.Green;
                clientView.Show();
            }
        }

        public void updateClientName(String _ClientName)
        {
            ClientName = _ClientName;
        }
    }
}