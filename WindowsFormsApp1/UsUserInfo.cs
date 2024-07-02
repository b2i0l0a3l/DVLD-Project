using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UsUserInfo : UserControl
    {
        public UsUserInfo()
        {
            InitializeComponent();
        }

        private void UsUserInfo_Load(object sender, EventArgs e)
        {
            if(Settings.User != null)
            {
                label2.Text = Settings.User.UserID.ToString() ;
                label4.Text= Settings.User.UserName ;
                label6.Text = Settings.User.IsActive.ToString();
            }
        }
    }
}
