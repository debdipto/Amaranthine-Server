using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmaranthineServer
{
    public partial class frm_Settings : Form
    {
        private List<UserCtrl_SettingsEntry> listOfSettings;
        public frm_Settings()
        {
            InitializeComponent();
            loadSettings();
        }

        private void loadSettings()
        {
            listOfSettings = new List<UserCtrl_SettingsEntry>();

            foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                UserCtrl_SettingsEntry newSettingsEntry = new UserCtrl_SettingsEntry(currentProperty.Name, Properties.Settings.Default[currentProperty.Name].ToString());
                listOfSettings.Add(newSettingsEntry);                
            }
            listOfSettings = listOfSettings.OrderBy(o => o.name).ToList();

            foreach(UserCtrl_SettingsEntry settings in listOfSettings)
            {
                flow_SettingsDisplay.Controls.Add(settings);
            }
        }

        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            foreach(UserCtrl_SettingsEntry UserSettings in listOfSettings )
            {
                try
                {
                    Properties.Settings.Default[UserSettings.name] = UserSettings.returnValue();
                    Properties.Settings.Default.Save();
                }
                catch(Exception)
                {
                    MessageBox.Show("Unable to save the value for "+ UserSettings.name);
                }
            }
            MessageBox.Show("Settings saved");
        }
    }
}
