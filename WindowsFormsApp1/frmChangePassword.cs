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
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            Settings.User = ClsUsers.Find(UserID);
            Settings.p = clsPeople.Find(Settings.User.PersonID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Settings.nullableObject();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Settings.User.Password == txbCurrentPassword.Text && (txbNewPassword.Text == txbConfirmPassword.Text))
            {
                Settings.User.Password = txbNewPassword.Text;
                if (Settings.User.Save())
                {
                    MessageBox.Show("Password Changed Successfully");
                }
                else
                    MessageBox.Show("Password Changed Failed");
            }
            else
            {
                MessageBox.Show("Password Changed Failed");

            }
        }

        private void txbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txbCurrentPassword.Text != Settings.User.Password)
            {

                errorProvider1.SetError(txbCurrentPassword,"Password Wrong.");
            }
            else
            {
                errorProvider1.SetError(txbCurrentPassword, "");

            }
        }

        private void txbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txbNewPassword.Text))
            {
                errorProvider1.SetError(txbCurrentPassword, "Please Enter a Password.");

            }
            else
            {
                errorProvider1.SetError(txbCurrentPassword, "");

            }
        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txbConfirmPassword.Text != txbNewPassword.Text)
            {
                errorProvider1.SetError(txbCurrentPassword, "Password Is Not Match");

            }
            else
            {
                errorProvider1.SetError(txbCurrentPassword, "");

            }
        }
    }
}
