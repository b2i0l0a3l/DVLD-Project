using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessBusinessLayer
{
    public partial class clsPeople
    {
        enum enMode { Add , Update}
        private enMode _Mode;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public byte Gendor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Nationality { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public string Error { get; set; }
        private bool _AddNewPerson()
        {
            string Error = "";
            this.PersonID = clsPeopleData.AddNewPerson(ref Error,this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.Gendor, this.DateOfBirth, this.Nationality, this.Phone, this.Email, this.Address, this.ImagePath, this.NationalNo);
            this.Error = Error;
            return PersonID != -1;
        }
        private clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName,
          string ThirdName, string LastName, byte Gendor, DateTime DateOfBirth, int Nationality, string Phone, string Email, string ImagePath,string Address)
        {
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gendor = Gendor;
            this.DateOfBirth = DateOfBirth;
            this.Nationality = Nationality;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            this.PersonID = PersonID;
            this.Address = Address;
            _Mode = enMode.Update;
        }
        public clsPeople()
        {
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gendor = 3;
            this.DateOfBirth = DateTime.Now;
            this.Nationality = -1;
            this.Phone = "";
            this.Email = "";
            this.NationalNo = "";
            this.ImagePath = "";
            this.PersonID = -1;
            _Mode = enMode.Add;
    }
        private bool _UpdatePersonInfo()
        {
            return clsPeopleData.UpdatePersonData(this.PersonID, this.NationalNo, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.Gendor, this.DateOfBirth, this.Nationality, this.Phone, this.Email, this.Address, this.ImagePath);
        }
      
        
    }
}
