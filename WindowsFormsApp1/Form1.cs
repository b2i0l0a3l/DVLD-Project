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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private frmPeople frm = new frmPeople();
        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm.MdiParent = this;
            frm.Show();
        }
        
        private    frmUsers frm2 = new frmUsers();
        private void ShowLogin()
        {
            this.Hide();
            FrmLoginScreen frm = new FrmLoginScreen();
            frm.ShowDialog();
        }
        private void singOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLogin();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm2.MdiParent = this;
            frm2.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo();
            frm.Show();
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword();
            frm.ShowDialog();
        }

         frmManageApplicationsType frm3 = new frmManageApplicationsType();


        private void applicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm3.MdiParent = this;
            frm3.Show();
        }
    }
}
