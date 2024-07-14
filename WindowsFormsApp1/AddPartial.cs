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

        public clsPeople p;
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
        private string SaveImageTo(string Ext,ref clsPeople p)
        {
           
            string sourceFilePath = pictureBox1.ImageLocation;
            string destinationFilePath = @"C:\DVLD SAVE PEOPLE IMAGE\"+ GUID() + '.'+Ext;
            try
            {
                string destinationDirectory = Path.GetDirectoryName(destinationFilePath);


                if (!Directory.Exists(destinationDirectory+ destinationFilePath))
                {
                    if (p.ImagePath != "")
                    {
                        DeleteImage(p.ImagePath);
                    }
                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                }
                else { return sourceFilePath; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return destinationFilePath;
        }
        private bool IsEmailExists(string Email)
        {
            DataTable dt = clsPeople.GetAllPeople();
            foreach(DataRow row in dt.Rows)
            {
                if (Email.Trim() == row["Email"].ToString())
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

        
            txbFirstName.Text = p.FirstName;
            txbSecondName.Text = p.SecondName;
            txbThirdName.Text = p.ThirdName;
            txbLasttName.Text = p.LastName;
            txbNationalNo.Text = p.NationalNo;
            comboBox1.SelectedIndex = p.Nationality -1;
            dateTimePicker1.Value = p.DateOfBirth;

            if (p.Gendor == 0)
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;

            txbPhone.Text = p.Phone;
            txbEmail.Text = p.Email;
            txbAddress.Text = p.Address;

            if (!string.IsNullOrEmpty(p.ImagePath))
            {
                if (!File.Exists(p.ImagePath))
                {
                    return;
                }
                pictureBox1.Load(p.ImagePath);
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
            
            p.Nationality = comboBox1.SelectedIndex + 1;
            p.NationalNo = txbNationalNo.Text;
            p.DateOfBirth = dateTimePicker1.Value;
            p.ThirdName = txbThirdName.Text;
            p.LastName = txbLasttName.Text;
            p.FirstName = txbFirstName.Text;
            p.SecondName = txbSecondName.Text;
            p.Phone = txbPhone.Text;
            p.Email = txbEmail.Text;
            p.Address = txbAddress.Text;

            if ((pictureBox1.ImageLocation != "" && pictureBox1.ImageLocation != null  ) && pictureBox1.Image != null )
            {
                string Ext = pictureBox1.ImageLocation.Split('.')[pictureBox1.ImageLocation.Split('.').Length - 1];
                p.ImagePath = SaveImageTo(Ext,ref p);
            }
            else
                p.ImagePath = null;



            if (rdFemale.Checked)
                p.Gendor = 1;
            else
                p.Gendor = 0;

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
            if(p!= null)
                LoadPersonData();
        }

    
    }
}
