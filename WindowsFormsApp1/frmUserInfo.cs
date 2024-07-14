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
    public partial class frmUserInfo : Form
    {
        public frmUserInfo()
        {
            InitializeComponent();
            if (Settings.p != null)
                usPersonDetails1.p = Settings.p;
            if (Settings.User != null)
                usUserInfo1.User = Settings.User;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
           

        }
    }
}
