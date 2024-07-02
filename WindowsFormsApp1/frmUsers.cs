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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private void _Refrech()
        {
            Settings.nullableObject();
            dataGridView1.DataSource = ClsUsers.GetAllUsers();

        }
        private void frmUsers_Load(object sender, EventArgs e)
        {
            _Refrech();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            _Refrech();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            _Refrech();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ClsUsers.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value)){
                MessageBox.Show("User Deleted Successfully.");
            }
            else
            {
                MessageBox.Show("User Delete Failed.");
            }
            _Refrech();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refrech();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refrech();
        }
    }
}
