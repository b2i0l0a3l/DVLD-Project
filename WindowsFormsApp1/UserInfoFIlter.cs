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
    public partial class UserInfoFIlter : UserControl
    {
        public DataRow PrimaryKey;
        private DataTable dat;

        public int PersonID;
        public UserInfoFIlter()
        {
            InitializeComponent();
            personDetails1.DataBack += Databack;

        }
        private void _Refrech()
        {

            FillComboBox();


        }
        private void FillComboBox()
        {
            dat = clsPeople.GetAllPeople();

            if (dat == null)
                return;

            foreach (DataColumn col in dat.Columns)
            {
                comboBox1.Items.Add(col.ToString());
            }
            comboBox1.SelectedIndex = 1;
        }
        private void UserInfoFIlter_Load(object sender, EventArgs e)
        {
            if(this.PersonID != 0)
            {
                personDetails1.p = clsPeople.Find(PersonID);
                personDetails1.Refrech();
            }
           
            _Refrech();
        }
        private void Databack(DataTable dt)
        {
            DataColumn[] dc = new DataColumn[1];
  
            dc[0] = dt.Columns["PersonID"];
            dt.PrimaryKey = dc;
            PrimaryKey = dt.Rows[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox1.Text))

                personDetails1.FillWithDataTable(Settings.FilterByLike(dat, comboBox1.GetItemText(comboBox1.SelectedItem.ToString()), textBox1.Text));

            else
                MessageBox.Show("Please Enter a Valid Person.");
        }
        private void FF(int PersonID)
        {

            clsPeople p = clsPeople.Find(PersonID);
            textBox1.Text = p.NationalNo;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmAddPerson frm = new frmAddPerson();
            frm.DataBack += FF;
            frm.ShowDialog();
            _Refrech();
        }
    }
}
