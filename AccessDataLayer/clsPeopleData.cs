using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AccessDataLayer
{
    public class clsPeopleData
    {
        public static void GetPersonInfoByNationalNo()
        {

        }
        public static bool UpdatePersonData(int PersonID, string NationalNo, string FirstName, string SecondName,
         string ThirdName, string LastName, byte Gendor, DateTime DateOfBirth, int NationalCountryID,
         string Phone, string Email, string Address, string ImagePath)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"Update People
                            set FirstName = @FirstName,
                                NationalNo = @NationalNo,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName,
                                DateOfBirth = @DateOfBirth,
                                Gendor=@Gendor,
                                NationalityCountryID = @NationalityCountryID,
                                Address=@Address,
                                Phone = @Phone,
                                Email = @Email,
                                ImagePath = @ImagePath
                                Where PersonID = @PersonID
                                                ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalCountryID);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }catch (Exception ex) { }
            finally { connection.Close(); }
            return AffectedRows != -1;
        }
        public static bool GetPersonInfo(int PersonID,ref string NationalNo ,ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref byte Gendor, ref DateTime DateOfBirth, ref int NationalCountryID,
        ref string Phone, ref string Email, ref string Address,ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"SELECT * from People
             where PersonID = @PersonID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Gendor = (byte)reader["Gendor"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    NationalCountryID = (int)reader["NationalityCountryID"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    Address = (string)reader["Address"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool DeletePerson(int PersonID)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"Delete from People where PersonID = @PersonID
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
               AffectedRows = command.ExecuteNonQuery();   
            }catch(Exception ex) { }
            finally { connection.Close(); }
            return AffectedRows != -1;

        }
        public static int AddNewPerson(ref string Errors, string FirstName,  string SecondName,
         string ThirdName,  string LastName,  byte Gendor,  DateTime DateOfBirth,  int Nationality,
          string Phone,  string Email,  string Address, string ImagePath,string NationalNo)
        {
           
            int affectedRows = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"INSERT INTO People(NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,NationalityCountryID,Address,Phone,Email,ImagePath)
                            values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,
                              @NationalCountryID,@Address,@Phone,@Email,@ImagePath)
                            SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalCountryID", Nationality);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Address", Address);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if(Result != null && int.TryParse(Result.ToString(),out int PersonIDD))
                {
                    affectedRows = PersonIDD;
                }
            }catch (Exception ex) { Errors = ex.Message.ToString(); }
            finally { connection.Close(); }
            return affectedRows;

        }
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"
            SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName,
            People.DateOfBirth, Countries.CountryName, People.Phone, People.Email, Gendor=
            case
	            when People.Gendor = 0 then 'Male'
	            when People.Gendor = 1 then 'Female'
            end,Address,ImagePath
            FROM   Countries INNER JOIN
                         People ON Countries.CountryID = People.NationalityCountryID;
";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
               

                reader.Close();
            }catch(Exception ex)
            {
             }
            finally { connection.Close(); }
            return dt;
        }

        
        public static bool  GetPersonByNationalNo( string NationalNo)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"select * from People where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    IsFound = true;
                                }
                else
                    IsFound = false;

            }
            catch (Exception ex)
            {
                IsFound = false;

            }
            finally { connection.Close(); }
            return IsFound;
        }

    }
}
