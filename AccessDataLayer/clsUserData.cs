using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDataLayer
{
    public class clsUserData
    {
        
        public static bool GetUserInfo(int UserID,ref string Password, ref int PersonID, ref string UserName, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"SELECT * from Users
             where UserID = @UserID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    Password =   (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    IsActive = (bool)reader["IsActive"];
               
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
        public static bool DeleteUser(int UserID)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"Delete from Users where UserID = @UserID
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return AffectedRows != -1;

        }
        public static int AddNewUser( int PersonID,  string UserName,
         bool IsActive,string Password)
        {

            int UserID = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"
                            INSERT INTO Users(PersonID,UserName,IsActive,Password)
                            values(@PersonID,@UserName,@IsActive,@Password)
                        select SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@IsActive", IsActive);
        
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int PersonIDD))
                {
                    UserID = PersonIDD;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return UserID;

        }
        public static bool UpdateUser(int UserID, string UserName, string Password,
        bool IsActive)
        {

            int affectedRows = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"Update Users
                            set UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive
                        where UserID = @UserID
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);



            try
            {
                connection.Open();
                object Result = command.ExecuteNonQuery();
                if (Result != null )
                {
                    affectedRows = (int)Result;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return affectedRows != -1;

        }
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"SELECT  Users.UserID,Users.PersonID, People.FirstName + ' ' +People.SecondName + ' '+  People.ThirdName + ' ' +  People.LastName as FullName,Users.IsActive,Users.UserName
            FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID";
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
            }
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;
        }

    }
}
