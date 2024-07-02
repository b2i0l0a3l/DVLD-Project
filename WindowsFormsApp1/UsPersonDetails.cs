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

        public UsPersonDetails()
        {
            InitializeComponent();
        }

        public void loadData()
        {
            if (Settings.p == null)
            {
                return;
            }
            lblFirstName.Text = Settings.p.FirstName;
            lblThirdName.Text = Settings.p.ThirdName;
            lblLastName.Text = Settings.p.LastName;
            lblSeconName.Text = Settings.p.SecondName;
            lblNationalNo.Text = Settings.p.NationalNo;
            lblDateOfBirth.Text = Settings.p.DateOfBirth.ToString();
            if (Settings.p.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";
            lblPhone.Text = Settings.p.Phone;
            if (Settings.p.ImagePath != null)
                pictureBox1.ImageLocation = Settings.p.ImagePath;


            lblEmail.Text = Settings.p.Email;
            lblCountry.Text = clsCountry.GetCountryName(Settings.p.Nationality);
            lblAddress.Text = Settings.p.Address;
        }
        private void FillPersonData(DataView dv)
        {
            Settings.p = clsPeople.Find((int)dv[0][0]);
            Settings.User = new ClsUsers();
        }
        public void FillWithDataTable(DataView dv)
        {
            FillPersonData(dv);

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
            if(Settings.p == null)
            {
                return;
            }
            frmAddPerson frm = new frmAddPerson(Settings.p.PersonID);

            
            frm.ShowDialog();
            Refrech();
        }
    }
}
