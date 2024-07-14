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
    public partial class frmManageApplicationsType : Form
    {
        public frmManageApplicationsType()
        {
            InitializeComponent();
        }

        private void Refrech()
        {
            dataGridView1.DataSource = clsApplicationType.GetAllApplicationTypes();
        }
        private void frmManageApplicationsType_Load(object sender, EventArgs e)
        {
            Refrech();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
