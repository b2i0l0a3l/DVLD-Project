using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessBusinessLayer
{
    public class ClsUsers
    {
        enum enMode{ADD,UPDATE}
        private enMode _Mode;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string UserName {  get; set; }

        public ClsUsers() 
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.IsActive = false;
            _Mode = enMode.ADD;
        }
        private ClsUsers(int userID, int personID, bool isActive, string password, string userName)
        {
            _Mode = enMode.UPDATE;
            UserID = userID;
            PersonID = personID;
            IsActive = isActive;
            Password = password;
            UserName = userName;
        }

        public static ClsUsers Find(int UserID)
        {
            string Password = "", UserName = "";
            int PersonID = -1;
            bool isActive = false;
            if (clsUserData.GetUserInfo(UserID, ref Password, ref PersonID, ref UserName, ref isActive))
            {
                return new ClsUsers(UserID, PersonID, isActive, Password, UserName);
            }
            return null;
        }
        
        private bool UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName,this.Password, this.IsActive);
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        private bool AddNewUser()
        {
            this.UserID =  clsUserData.AddNewUser(this.PersonID, this.UserName, this.IsActive,this.Password);
            return this.UserID != -1;
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.ADD:
                    if(AddNewUser())
                    {
                        _Mode =enMode.UPDATE;
                        return true;
                    }else
                    { return false; }
                case enMode.UPDATE:
                    if (UpdateUser())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }
    }
}
