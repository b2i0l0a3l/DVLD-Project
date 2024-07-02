using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessBusinessLayer;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class UsAddNewPeople : UserControl
    {

      
        public UsAddNewPeople()
        {
            InitializeComponent();
        }
        

        


        private void AddNewPeople_Load(object sender, EventArgs e)
        {
             _RefrechScreen();

                LoadPersonData();
        }



        private void llblImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Image";
            openFileDialog1.DefaultExt = ".Jpg";
            openFileDialog1.Filter = "JPG File (*.jpg)|*.jpg| PNG File (*.png)|*.png|All File (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName.ToString());
                pictureBox1.Tag = openFileDialog1.FileName.ToString();
                linkLabel1.Visible = true;
            }
        }

      
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = "";
            if (rdMale.Checked)
                pictureBox1.Image = imageList1.Images[0];
            else
                pictureBox1.Image = imageList1.Images[1];

            linkLabel1.Visible = false;
            
        }

        

   

     

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Settings.p == null)
            {
                Settings.p = new clsPeople();
            }

            SavePersonDAta();

            if (Settings.p.Save())
            {
                MessageBox.Show("Person Saved Successfully.");

                if(OnPersonSaved != null)
                    PersonSaved(Settings.p.PersonID);
                
            }

            else
            {

                MessageBox.Show("Person Save failed.");

            }
        }

     
        //text Box Validating
        private void txbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (clsPeople.FindByNationalNo(txbNationalNo.Text))
            {
                e.Cancel = false;
                txbNationalNo.Focus();

                errorProvider1.SetError(txbNationalNo, "Nationality No is Exists before.");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbNationalNo, "");
            }
        }
        private void txbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbFirstName.Text))
            {
                e.Cancel = false;
                txbFirstName.Focus();
                errorProvider1.SetError(txbFirstName, "Please Enter Your Name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbFirstName, "");

            }
        }

        private void txbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbPhone.Text) || txbPhone.Text.Length < 6 )
            {
                e.Cancel = true;
                txbPhone.Focus();
                errorProvider1.SetError(txbPhone, "Please Enter A valid Phone number");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbPhone, "");
            }
        }

        private void txbPhone_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txbPhone.Text.ToString(), out int i))
            {
                txbPhone.Text = "";
            }

        }

        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!(txbEmail.Text == "") && IsEmailExists(txbEmail.Text))
            {
                e.Cancel = true;
                txbEmail.Focus();
                errorProvider1.SetError(txbEmail, "This Email Exists Before.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbEmail, "");

            }
        }

        private void OnRadioChanged(object sender, EventArgs e)
        {
            RadioChanged();
        }
    }

}
