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
    public partial class UsPersonDetails : UserControl
    {


        public delegate void Handler(DataTable PersonID);
        public event Handler DataBack;
        public clsPeople p;

        public UsPersonDetails()
        {
            InitializeComponent();
        }
        private void fillTable()
        {
            if (p == null)
               return ;

            DataTable dt = new DataTable();
            dt.Columns.Add("PersonID",typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("SecondName", typeof(string));
            dt.Columns.Add("ThirdName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("NationalNo", typeof(string));
            dt.Columns.Add("DateOfBirth", typeof(DateTime));
            dt.Columns.Add("Gendor", typeof(byte));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("ImagePath", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Nationality", typeof(string));
            dt.Rows.Add(p.PersonID, p.FirstName,p.SecondName,p.ThirdName,p.LastName,p.NationalNo,
                p.DateOfBirth,p.Gendor,p.Phone,p.ImagePath,p.Email,p.Address,p.Nationality);
           DataBack?.Invoke(dt);
        }
        public void loadData()
        {
            if (p == null)
                return;

            lblFirstName.Text = p.FirstName;
            lblThirdName.Text = p.ThirdName;
            lblLastName.Text = p.LastName;
            lblSeconName.Text = p.SecondName;
            lblNationalNo.Text = p.NationalNo;
            lblDateOfBirth.Text = p.DateOfBirth.ToString();
            if (p.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";
            lblPhone.Text = p.Phone;
            if (p.ImagePath != null)
                pictureBox1.ImageLocation = p.ImagePath;


            lblEmail.Text = p.Email;
            lblAddress.Text = p.Address;
            lblCountry.Text = clsCountry.GetCountryName(p.Nationality);

            fillTable();
        }

        public void FillWithDataTable(DataView dv)
        {
            if(dv == null || dv.Count <= 0)
            {
                MessageBox.Show("User Not Found");
                return;
            }
            lblNationalNo.Text = dv[0][1].ToString();
            lblFirstName.Text = dv[0][2].ToString();
            lblSeconName.Text = dv[0][3].ToString();
            lblThirdName.Text = dv[0][4].ToString();
            lblLastName.Text = dv[0][5].ToString();
            lblDateOfBirth.Text = dv[0][6].ToString();
            lblCountry.Text = dv[0][7].ToString();


            lblPhone.Text = dv[0][8].ToString();
            lblEmail.Text = dv[0][9].ToString();
            lblGendor.Text = dv[0][10].ToString();
            lblAddress.Text = dv[0][11].ToString();

            if (dv[0][12].ToString() != null)
                pictureBox1.ImageLocation = dv[0][12].ToString();


            DataBack?.Invoke(dv.ToTable());

        }

        public void Refrech()
        {
            loadData();
        }
        private void PersonDetails_Load(object sender, EventArgs e)
        {
            Refrech();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(p == null)
            {
                return;
            }
            frmAddPerson frm = new frmAddPerson(p.PersonID);
            frm.ShowDialog();
            Refrech();
        }
    }
}
