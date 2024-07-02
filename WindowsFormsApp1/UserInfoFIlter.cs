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
        public DataRow dr ;


        private DataTable dt;


        public UserInfoFIlter()
        {
            InitializeComponent();
            personDetails1.DataBack += Databack;
        }

        private void _Refrech()
        {
            FillComboBox();
            if (Settings.User != null)
            {
                comboBox1.Enabled = false;
                Settings.p = clsPeople.Find(Settings.User.PersonID);
                personDetails1.Refrech();
                textBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }
        private void FillComboBox()
        {
            dt = clsPeople.GetAllPeople();
            if (dt == null)
                return;
            foreach (DataColumn col in dt.Columns)
            {
                comboBox1.Items.Add(col.ToString());
            }
            comboBox1.SelectedIndex = 1;
        }
        private void UserInfoFIlter_Load(object sender, EventArgs e)
        {
            _Refrech();
        }
        private void Databack(DataTable dt)
        {
            DataColumn[] dc = new DataColumn[1];
            dc[0] = dt.Columns["PersonID"];
            dt.PrimaryKey = dc;
            dr = dt.Rows[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox1.Text) || textBox1.Text == "")
            {
   
                personDetails1.FillWithDataTable(Settings.FilterByLike(dt,comboBox1.GetItemText(comboBox1.SelectedItem.ToString()), textBox1.Text));
            }
        }
        private void FF(int PersonID)
        {
            Settings.p = clsPeople.Find(PersonID);
            textBox1.Text = Settings.p.NationalNo;
            personDetails1.Refrech();
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
