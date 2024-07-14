using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AccessBusinessLayer
{
    public class Settings
    {
        private static string path = @"C:\Users\bilal\OneDrive\C#\LoginUser.txt";
        public static string UserName = "";
        public static string Password = "";

        public static ClsUsers User;
        public static clsPeople p;


        //DataView
        public static DataView FilterByBoolean (DataTable dt, string Col, string Value)
        {
            DataView dv = dt.DefaultView;
            if (Value == "All")
            {
                return   ClsUsers.GetAllUsers().DefaultView;
            }
            dv.RowFilter = $"{Col} = '{Value}'";
               return dv;
        }
        public static DataView FilterByLike(DataTable dt, string Col, string Value)
        {
            DataView dv = dt.DefaultView;
            if(Value == "")
            {
                return dt.DefaultView;
            }
           
            if (int.TryParse(Value.ToString(),out int i))
             {
                dv.RowFilter = $"{Col} = '{Value}'";
                return dv;
            }

            dv.RowFilter = $"{Col} Like '%{Value}%'";
            return dv;
        }
   


        //File
        private static void WriteInFile(string Txt)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(Txt.TrimEnd());
                sw.Close();
            }
        }
        
        public static void AppendTextToFile(string UserName,string Password , bool IsActive, string PersonID,string Seperator = "#")
        {
            string Active = "";
            if (!File.Exists(path))
            {
                return;
            }
            if (IsActive)
                Active = "Active";
            else
                Active = "NotActive";
            string Record = Environment.NewLine + UserName + Seperator + Password + Seperator + Active + Seperator + PersonID + Seperator;
            File.AppendAllText(path, Record, Encoding.UTF8);
        }
        public static bool isRememberMe()
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                string[] arr = { };
                while ((s = sr.ReadLine()) != null)
                {
                    arr = s.Split('#');
                    if (arr[4] == "Remember")
                    {
                        sr.Close();
                    
                        UserName = arr[0];
                        Password =  arr[1];
                        return true;
                    }
                }
                sr.Close();
                return false;
            }
        }
        public static bool ReadFile(string UserName,string Password,bool IsActive)
        {
            bool isDone = false;
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    string[] arr;
                    string ss = "";
                    

                    while ((s = sr.ReadLine()) != null)
                    {
                        arr = s.Split('#');

                    //check if ther any Remember 
                            if(!(arr[0] == UserName && arr[1] == Password) && arr[4] == "Remember")
                        {
                            arr[4] = "";
                        }
                      
                        if (arr[0] == UserName && arr[1] == Password && arr[2] == "Active")
                        {
                            p = clsPeople.Find(int.Parse(arr[3]));
                            
                        //check if Checkbox is active and Remember not found
                            if (IsActive && arr[4] == "")
                                arr[4] = "Remember";
                        //check if Checkbox not Active  
                        if (!IsActive)
                                arr[4] = "";

                        User = ClsUsers.Find(int.Parse(arr[3]));
                        if(User != null)
                            p = clsPeople.Find(User.PersonID);

                            isDone = true;
                        }
                        ss += string.Join("#", arr) + Environment.NewLine;
                    }


                    sr.Close();
                    WriteInFile(ss);
                }
                return isDone;
                }
    }
    }
