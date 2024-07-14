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
        public ClsUsers User;
        public UsUserInfo()
        {
            InitializeComponent();
        }

        private void UsUserInfo_Load(object sender, EventArgs e)
        {
            if(User != null)
            {
                label2.Text = User.UserID.ToString() ;
                label4.Text= User.UserName ;
                label6.Text = User.IsActive.ToString();
            }
        }
    }
}
