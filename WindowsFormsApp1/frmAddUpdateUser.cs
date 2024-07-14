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
    public partial class frmAddUpdateUser : Form
    {
        private clsPeople p;
        private ClsUsers User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            User = ClsUsers.Find(UserID);


            label2.Text = User.UserID.ToString();
            txbUserName.Text = User.UserName;
            txbConfirmPassword.Text = User.Password;
            txbPasswod.Text = User.Password;
            checkBox1.Checked = User.IsActive;
            userInfoFIlter1.PersonID = User.PersonID;

            userInfoFIlter1.Enabled = false;
            
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            btnNext.Visible = false;
            button2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool CheckIfEverythingGood()
        {
            if (string.IsNullOrEmpty(txbUserName.Text) || string.IsNullOrEmpty(txbConfirmPassword.Text) || string.IsNullOrEmpty(txbPasswod.Text))
                return true;
            return false;
        }
        private bool Validating(ClsUsers User)
        {
            return txbUserName.Text.Trim() == User.UserName && txbPasswod.Text.Trim() == User.Password && checkBox1.Checked == User.IsActive;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isADD = false;
            if (CheckIfEverythingGood())
            {
                MessageBox.Show("Please Enter Your information");
                return;
            }

            if(User == null)
            {
                User = new ClsUsers();
                isADD = true;
            }
            //Check if information changed
            if (Validating(User))
            {
                return;
            }

            User.UserName = txbUserName.Text;
            User.IsActive = checkBox1.Checked;
            User.Password = txbPasswod.Text;
            User.PersonID = (int) userInfoFIlter1.PrimaryKey["PersonID"];

            if(isADD)
                Settings.AppendTextToFile(txbUserName.Text, txbPasswod.Text, checkBox1.Checked, User.UserID.ToString());

            if (User.Save())
            {
                MessageBox.Show("User Data Saved.");
                
                userInfoFIlter1.Enabled = false;
            }
            else
                MessageBox.Show("User Data Save Failed.");

        }

        //validating
        private void txbPasswod_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbPasswod.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbPasswod, "Enter Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbPasswod, "");

            }
        }
        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbPasswod.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbPasswod, "Enter Password");
            }
            else if (txbPasswod.Text != txbConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txbConfirmPassword, "The password does not match The password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbPasswod, "");

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 1)
            {
                btnNext.Visible = false;
                button2.Visible = true;
            }
        }
    }
}
