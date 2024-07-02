using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using AccessBusinessLayer;

namespace WindowsFormsApp1
{
    public partial class FrmLoginScreen : Form
    { 
        
        public FrmLoginScreen()
        {
            InitializeComponent();
        }



     
        private void Showing()
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text)))
            {
                if (Settings.ReadFile(textBox1.Text, textBox2.Text, checkBox1.Checked))
                    Showing();
                else
                    MessageBox.Show("Error!!!!");
            }
        }

        private void FrmLoginScreen_Load(object sender, EventArgs e)
        {
            if (Settings.isRememberMe())
            {
                textBox1.Text = Settings.UserName;
                textBox2.Text = Settings.Password;
                checkBox1.Checked=true;
            }
        }
    }
}
