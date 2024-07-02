using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UsAddNewPeople 
    {
       

      

        public event Action<int> OnPersonSaved;


        public virtual void PersonSaved(int PersonID)
        {
            Action<int> Handler = OnPersonSaved;
            if (Handler != null)
            {
                Handler(PersonID);
            }
        }

        private void DeleteImage( string imagePath)
        {
            string filePath = imagePath;

            if (File.Exists(filePath))
            {
                
                    File.Delete(filePath);
                
             
            }
        }
        
        private string GUID()
        {
            return Guid.NewGuid().ToString();
        }
        private string SaveImageTo(string Ext)
        {
           
            string sourceFilePath = pictureBox1.ImageLocation;
            string destinationFilePath = @"C:\DVLD SAVE PEOPLE IMAGE\"+ GUID() + '.'+Ext;
            try
            {
                string destinationDirectory = Path.GetDirectoryName(destinationFilePath);


                if (!Directory.Exists(destinationDirectory+ destinationFilePath))
                {
                    if (Settings.p.ImagePath != "")
                    {
                        DeleteImage(Settings.p.ImagePath);
                    }
                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                }
                else { return sourceFilePath; }
            }
            catch (Exception ex) { return ""; }
            return destinationFilePath;
        }
        private bool IsEmailExists(string Email)
        {
            MessageBox.Show(Email);
            DataTable dt = clsPeople.GetAllPeople();
            foreach(DataRow row in dt.Rows)
            {
                MessageBox.Show(row["Email"].ToString());

                if (Email == row["Email"].ToString())
                {
                    return true;
                }
            }
            return false;
        }       
        
        private void RadioChanged()
        {
            if (linkLabel1.Visible == true)
            {
                return;
            }
            if (rdMale.Checked)
                pictureBox1.Image = imageList1.Images[0];
            else
                pictureBox1.Image = imageList1.Images[1];
        }
        private DateTime GetTime(int birthDay = 18)
        {
            int Year = DateTime.Now.Year - birthDay, Month = DateTime.Now.Month, Day = DateTime.Now.Day;
            return new DateTime(Year, Month, Day);
        }
        private void LoadPersonData()
        {

            if (Settings.p == null)
            {
                return;
            }
            txbFirstName.Text = Settings.p.FirstName;
            txbSecondName.Text = Settings.p.SecondName;
            txbThirdName.Text = Settings.p.ThirdName;
            txbLasttName.Text = Settings.p.LastName;
            txbNationalNo.Text = Settings.p.NationalNo;
            comboBox1.SelectedIndex = Settings.p.Nationality -1;
            dateTimePicker1.Value = Settings.p.DateOfBirth;

            if (Settings.p.Gendor == 0)
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;

            txbPhone.Text = Settings.p.Phone;
            txbEmail.Text = Settings.p.Email;
            txbAddress.Text = Settings.p.Address;

            if (!string.IsNullOrEmpty(Settings.p.ImagePath))
            {
                pictureBox1.Load(Settings.p.ImagePath);
                linkLabel1.Visible = true;
            }
            else
            {
                pictureBox1.ImageLocation = "";
                RadioChanged();
            }
        }

        private void SavePersonDAta()
        {
            Settings.p.Nationality = comboBox1.SelectedIndex + 1;
            Settings.p.NationalNo = txbNationalNo.Text;
            Settings.p.DateOfBirth = dateTimePicker1.Value;
            Settings.p.ThirdName = txbThirdName.Text;
            Settings.p.LastName = txbLasttName.Text;
            Settings.p.FirstName = txbFirstName.Text;
            Settings.p.SecondName = txbSecondName.Text;
            Settings.p.Phone = txbPhone.Text;
            Settings.p.Email = txbEmail.Text;
            Settings.p.Address = txbAddress.Text;

            if ((pictureBox1.ImageLocation != "" && pictureBox1.ImageLocation != null  ) && pictureBox1.Image != null )
            {
                string Ext = pictureBox1.ImageLocation.Split('.')[pictureBox1.ImageLocation.Split('.').Length - 1];
                Settings.p.ImagePath = SaveImageTo(Ext);
            }
            else
                Settings.p.ImagePath = null;



            if (rdFemale.Checked)
                Settings.p.Gendor = 1;
            else
                Settings.p.Gendor = 0;

        }
     


        private void FillBoxWithCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row["CountryName"].ToString());
            }
        }
        private void _RefrechScreen()
        {
            FillBoxWithCountries();
            RadioChanged();
            dateTimePicker1.MaxDate = GetTime();
            comboBox1.SelectedIndex = comboBox1.FindString("Morocco");
        }

    
    }
}
