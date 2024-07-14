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
    public partial class frmAddPerson : Form
    {
        public delegate void handler(int PersonID);
        public event handler DataBack;

        public frmAddPerson(int PersonID)
        {
            InitializeComponent();
            label1.Text = "Update Person";
            lblPersonID.Text = PersonID.ToString();
            addNewPeople1.p = clsPeople.Find(PersonID);
        }
        public frmAddPerson()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewPeople1_OnPersonSaved(int obj)
        {
            lblPersonID.Text = obj.ToString();
            label1.Text = "Update Person";
            DataBack?.Invoke(obj);
        }
    }
}
