using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessBusinessLayer
{
    public class Settings
    {
        private static string path = @"C:\Users\bilal\OneDrive\C#\LoginUser.txt";
        public static string UserName = "";
        public static string Password = "";
        public static ClsUsers User;
        public static clsPeople p;

        public static void nullableObject()
        {
            User = null;
            p = null;
        }
        public static DataView FilterByLike(DataTable dt, string Col, string Value)
        {
            DataView dv = dt.DefaultView;
        
            if(int.TryParse(Value.ToString(),out int i))
             {
                dv.RowFilter = $"{Col} = '{Value}'";
                return dv;
            }

            dv.RowFilter = $"{Col} Like '%{Value}%'";
            return dv;
        }
   

        private static void WriteInFile(string Txt)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(Txt.TrimEnd());
                sw.Close();
            }
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
                    if (arr[3] == "Remember" )
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
                    string[] arr = { };
                    string ss = "";
                    

                    while ((s = sr.ReadLine()) != null)
                    {
                        arr = s.Split('#');
                        if (!(arr[0] == UserName && arr[1] == Password) && arr[2] == "Active" && arr[3] == "Remember")
                        {
                            arr[3] = "";
                        }
                        if (arr[0] == UserName && arr[1] == Password && arr[2] == "Active")
                        {

                            if (IsActive && arr[3] == "")
                            {
                                arr[3] = "Remember";
                                
                            }

                            if (!IsActive)
                                arr[3] = "";

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
