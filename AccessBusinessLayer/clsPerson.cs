using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccessBusinessLayer
{
    public partial class clsPeople
    {
    
        public static bool FindByNationalNo(string NationalNo)
        {
            return clsPeopleData.GetPersonByNationalNo(NationalNo);
        }

        public static clsPeople Find(int PersonID)
        {
            string FirstName = "",NationalNo = "" ,SecondName = "", ThirdName = "", LastName = "", Address = "",ImagePath = "", Phone = "", Email = "";
            int  NationalCountryID = -1;
            byte Gendor = 3;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleData.GetPersonInfo( PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref Gendor, ref DateOfBirth, ref NationalCountryID, ref Phone, ref Email, ref Address, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName,
                ThirdName, LastName, Gendor, DateOfBirth, NationalCountryID, Phone, Email,ImagePath,Address);
            }
            return null;
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();

        }

       

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    {
                        return _UpdatePersonInfo();
                    }
            }
            return false;
        }
    }
}
