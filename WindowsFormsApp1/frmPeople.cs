using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmPeople : Form
    {
         public frmPeople()
        {
            InitializeComponent();
        }
        private void _RefrechScreenData()
        {
            dataGridView1.DataSource = clsPeople.GetAllPeople();
            comboBox2.SelectedIndex = 0;
        }
        private void People_Load(object sender, EventArgs e)
        {
            _RefrechScreenData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddPerson frmAddPeople = new frmAddPerson();
            frmAddPeople.ShowDialog();
            _RefrechScreenData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsPeople.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Person Deleted Successfully.");
            }
            else
            {
                MessageBox.Show("Person Delete failed.");

            }

            _RefrechScreenData();
        }


        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddPerson frmAddPeople = new frmAddPerson();
            frmAddPeople.ShowDialog();
            _RefrechScreenData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddPerson frmAddPeople = new frmAddPerson( (int)dataGridView1.CurrentRow.Cells[0].Value );
            frmAddPeople.ShowDialog();
            _RefrechScreenData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex != 0)
            {
                    textBox1.Visible = true;

            }

            else
            {
                textBox1.Visible = false;
                _RefrechScreenData();

            }

        }

        private void Filter(string value)
        {
            dataGridView1.DataSource = Settings.FilterByLike(clsPeople.GetAllPeople(), comboBox2.SelectedItem.ToString(), value);
        }
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedItem.ToString() == "PersonID" && !int.TryParse(textBox1.Text.ToString(), out int i))
            {
                textBox1.Text = "";
                return;
            }

            Filter(textBox1.Text);
            
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefrechScreenData();
        }
    }
}
