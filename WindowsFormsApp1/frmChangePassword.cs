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
        private ClsUsers User;
        private clsPeople p;
        enum enMode { Upper , Down }
        private enMode Mode;

        public frmChangePassword()
        {
            InitializeComponent();
            personDetails1.p = Settings.p;
            userControl11.User = Settings.User;

            Mode = enMode.Upper;
        }
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            this.User = ClsUsers.Find(UserID);
            this.p = clsPeople.Find(User.PersonID);
            personDetails1.p = p;
            userControl11.User = User;
            Mode = enMode.Down;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(Mode == enMode.Upper)
            {
                if (Settings.User == null)
                {
                    return;
                }

                if (Settings.User.Password == txbCurrentPassword.Text && (txbNewPassword.Text == txbConfirmPassword.Text))
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
            else
            {
                if (User == null)
                {
                    return;
                }

                if (User.Password == txbCurrentPassword.Text && (txbNewPassword.Text == txbConfirmPassword.Text))
                {
                    User.Password = txbNewPassword.Text;
                    if (User.Save())
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
