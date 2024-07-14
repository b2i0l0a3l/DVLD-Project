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
        private DataTable dt;
        public frmUsers()
        {
            InitializeComponent();
        }

        private void fillComboBox(DataTable dt)
        {
            if (dt == null)
                return;
            comboBox1.Items.Add("None");
            foreach (DataColumn dc in dt.Columns)
            {
                comboBox1.Items.Add(dc.ColumnName);
            }
        }
        private void _Refrech()
        {
            dt = ClsUsers.GetAllUsers();
            dataGridView1.DataSource = dt;
            label2.Text = (dataGridView1.RowCount -1).ToString();
            fillComboBox(dt);
            comboBox1.SelectedIndex = 0;


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
            frmChangePassword frm = new frmChangePassword( (int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refrech();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";

            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = ClsUsers.GetAllUsers();
                comboBox2.Visible = false;
                textBox1.Visible = false;
                return;
            }

            if (comboBox1.SelectedIndex == 4) 
            {
                comboBox2.Visible = true;
                textBox1.Visible = false;
            }
            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = Settings.FilterByBoolean(dt, "IsActive", "All");
                    break;
                case 1:
                    dataGridView1.DataSource = Settings.FilterByBoolean(dt, "IsActive", "true");
                    break;
                case 2:
                    dataGridView1.DataSource = Settings.FilterByBoolean(dt, "IsActive","false");
                    break;
            }
        }
        private void Filter(string Value)
        {
            dataGridView1.DataSource = Settings.FilterByLike(dt, comboBox1.SelectedItem.ToString(), Value);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "PersonID" || comboBox1.SelectedItem.ToString() == "UserID" && !int.TryParse(textBox1.Text.ToString(), out int i)){
                textBox1.Text = "";
                return;
            }
            Filter(textBox1.Text);
        }
    }
}
