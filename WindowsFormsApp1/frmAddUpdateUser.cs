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

        public frmAddUpdateUser()
        {
            InitializeComponent();
        }
        public frmAddUpdateUser(int PersonID)
        {
            InitializeComponent();

            Settings.User = ClsUsers.Find(PersonID);


            label2.Text = Settings.User.UserID.ToString();
            txbUserName.Text = Settings.User.UserName;
            txbConfirmPassword.Text = Settings.User.Password;
            txbPasswod.Text = Settings.User.Password;
            checkBox1.Checked = Settings.User.IsActive;

            
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
            Settings.nullableObject();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Settings.User == null)
            {
                return;
            }

            Settings.User.UserName = txbUserName.Text;
            Settings.User.IsActive = checkBox1.Checked;
            Settings.User.Password = txbPasswod.Text;
            Settings.User.PersonID = (int) userInfoFIlter1.dr["PersonID"];

            if (Settings.User.Save())
            {
                MessageBox.Show("User Data Saved.");
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
