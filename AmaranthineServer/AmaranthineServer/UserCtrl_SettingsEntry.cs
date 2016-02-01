using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmaranthineServer
{
    public partial class UserCtrl_SettingsEntry : UserControl
    {
        public String name;
        public UserCtrl_SettingsEntry(String _name,String value)
        {
            InitializeComponent();
            lbl_SettingsName.Text = _name;
            name = _name;
            if (name.IndexOf('e') == 0)
                txtb_SettingsValue.PasswordChar = '*';

            txtb_SettingsValue.Text = value;

            
        }

        public String returnValue()
        {
            return txtb_SettingsValue.Text;
        }

        private void UserCtrl_SettingsEntry_Load(object sender, EventArgs e)
        {

        }
    }
}
