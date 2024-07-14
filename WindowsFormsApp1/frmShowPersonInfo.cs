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
    public partial class frmShowPersonInfo : Form
    {
        //private int PersonID;
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            personDetails1.p = clsPeople.Find(PersonID);
        }
    

    }
}
