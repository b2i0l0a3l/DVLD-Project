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
        private    frmUsers frm2 = new frmUsers();
        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm.MdiParent = this;
            frm.Show();
        }

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
    }
}
