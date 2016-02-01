using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmaranthineServer
{
    public partial class frm_Main : Form
    {
        private bool serverStatus;
        ParallelListener parallel;
        Thread thread = null;
        public string exitcode;

        public frm_Main()
        {
            InitializeComponent();
            if (File.Exists("ServerLog.txt"))
            {
                String contents = File.ReadAllText("ServerLog.txt");
                markup(contents);
            }

            resetServer();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                switch(args[1])                    
                {
                    case "-AutoOn":
                        {
                            displayLine("Turning Server ON");
                            turnServerON();
                        }
                        break;
                    default:
                        {
                            displayLine("[Exception] Invalid input parameter, please consult documentation...");
                        }
                        break;
                }
            }
        }        

        public void displayLineSettings(String message)
        {
            txtb_ServerSettings.Text += message+Environment.NewLine;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turnServerOFF();
            Dispose();
        }

        public void addNewUser(Label name)
        {
            flow_ListOfClients.Controls.Add(name);
        }

        public void removeUser(Label name)
        {
            flow_ListOfClients.Controls.Remove(name);
        }

        private void serverPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Settings settingsObject = new frm_Settings();
            settingsObject.Show();
        }

        private void aboutAmaranthineServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            displaySettings();
        }

        public void displayLine(String message, bool addDate = true)
        {
            DateTime dt = DateTime.Now;
            int position = rtxtb_ServerDisplay.Text.Length;
            String timestamp = String.Empty;
            if (addDate)
                timestamp = "[" + dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " " + dt.TimeOfDay + "] ";

            rtxtb_ServerDisplay.AppendText("\r\n" + timestamp + message);
            int timestampLength = 0;
            if (addDate)
                timestampLength = timestamp.Length;
            else
                timestampLength = message.IndexOf("]") + 2;

            rtxtb_ServerDisplay.Select(position, timestampLength);

            rtxtb_ServerDisplay.SelectionColor = Color.Wheat;
            rtxtb_ServerDisplay.Select(position + timestampLength, message.Length + 1);
            rtxtb_ServerDisplay.SelectionColor = Color.Azure;

            if (message.Contains("[Exception]"))
            {
                if (addDate)
                    rtxtb_ServerDisplay.Select(position + timestampLength + message.IndexOf("[Exception]"), "[Exception]".Length + 1);
                else
                    rtxtb_ServerDisplay.Select(position + message.IndexOf("[Exception]"), "[Exception]".Length + 1);
                rtxtb_ServerDisplay.SelectionColor = Color.Red;
            }
            if (message.Contains("[AUTHENTICATION PHASE]"))
            {
                if (addDate)
                    rtxtb_ServerDisplay.Select(position + timestampLength + message.IndexOf("[AUTHENTICATION PHASE]"), "[AUTHENTICATION PHASE]".Length + 1);
                else
                    rtxtb_ServerDisplay.Select(position + message.IndexOf("[AUTHENTICATION PHASE]"), "[AUTHENTICATION PHASE]".Length + 1);
                rtxtb_ServerDisplay.SelectionColor = Color.Coral;
            }
            if (message.Contains("[Chatting]"))
            {
                if (addDate)
                    rtxtb_ServerDisplay.Select(position + timestampLength + message.IndexOf("[Chatting]"), "[Chatting]".Length + 1);
                else
                    rtxtb_ServerDisplay.Select(position + message.IndexOf("[Chatting]"), "[Chatting]".Length + 1);
                rtxtb_ServerDisplay.SelectionColor = Color.GreenYellow;
            }
            if (message.Contains("[Writing]"))
            {
                if (addDate)
                    rtxtb_ServerDisplay.Select(position + timestampLength + message.IndexOf("[Writing]"), "[Writing]".Length + 1);
                else
                    rtxtb_ServerDisplay.Select(position + message.IndexOf("[Writing]"), "[Writing]".Length + 1);
                rtxtb_ServerDisplay.SelectionColor = Color.Khaki;
            }
            if (addDate)
                File.AppendAllText("ServerLog.txt", Environment.NewLine + timestamp + message);

            if (rtxtb_ServerDisplay.Text.Length>Convert.ToInt32(AmaranthineServer.Properties.Settings.Default.LogCharLength))
                ArchiveLog();
        }

        private void markup(String contents)
        {
            String[] lines = contents.Split(Environment.NewLine.ToCharArray());
            foreach (String line in lines)
            {
                if (line.Trim() != String.Empty)
                    displayLine(line.Trim(), false);
            }
        }

        public void displayLine(String message, String file)
        {
            DateTime dt = DateTime.Now;
            String timestamp = "[" + dt.Date + " " + dt.TimeOfDay + "] ";

            File.AppendAllText(file, Environment.NewLine + timestamp + " " + message);
        }

        private void displaySettings()
        {
            txtb_ServerSettings.Text = String.Empty;
            foreach (System.Configuration.SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                String value;
                if (Properties.Settings.Default[currentProperty.Name].ToString() == String.Empty)
                    value = "Empty";
                else
                    value = Properties.Settings.Default[currentProperty.Name].ToString();

                displayLineSettings(currentProperty.Name + " currently set to " + value);
            }
        }

        private void resetServer()
        {
            try
            {
                exitcode = "exit";
                serverStatus = false;
                btn_ServerOnOff.Text = "Turn Server On";

                if (thread != null)
                {
                    parallel.serverSocket.Stop();
                    if (parallel.clientSocket != null)
                        parallel.clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                displayLine(ex.Message);
            }
        }

        private void turnServerON()
        {
            try
            {
                if (Convert.ToInt32(AmaranthineServer.Properties.Settings.Default.Port) < 10)
                {
                    MessageBox.Show("Listening port is not specified, please enter a value more than 10" + Environment.NewLine +
                        "Navigate to Settings->Server Properties to set port and retry");
                }

                exitcode = "";
                serverStatus = true;
                btn_ServerOnOff.Text = "Turn Server Off";

                displayLine("Server started");

                if (File.Exists("ServerLog.txt"))
                {
                    String contents = File.ReadAllText("ServerLog.txt");
                    rtxtb_ServerDisplay.Text = String.Empty;
                    markup(contents);
                }

                startListening();
            }
            catch (Exception ex)
            {
                displayLine(ex.Message);
            }
        }

        private void turnServerOFF()
        {
            if (exitcode != "exit")
            {
                resetServer();

                displayLine("Server shutdown");
            }
        }

        private void btn_ServerOnOff_Click(object sender, EventArgs e)
        {
            if (!serverStatus)
            {
                turnServerON();
            }
            else
            {
                turnServerOFF();
            }
        }

        private void startListening()
        {
            parallel = new ParallelListener(this);
            thread = new Thread(new ThreadStart(parallel.listen));
            thread.Start();
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            turnServerOFF();
            Dispose();
        }

        private void btn_ArchiveLog_Click(object sender, EventArgs e)
        {
            ArchiveLog();
        }

        private void ArchiveLog()
        {
            rtxtb_ServerDisplay.Text = String.Empty;
            if (File.Exists("ArchivedServerLog.txt"))
            {
                String logcontents = File.ReadAllText("ServerLog.txt");
                File.AppendAllText("ArchivedServerLog.txt", logcontents);
                File.Delete("ServerLog.txt");
            }
            else
                File.Move("ServerLog.txt", "ArchivedServerLog.txt");
        }

        private void btn_ClearWatcherLog_Click(object sender, EventArgs e)
        {
            txtb_WatcherLogs.Text = String.Empty;
        }
    }
}
