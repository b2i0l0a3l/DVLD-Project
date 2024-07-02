using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDataLayer
{
    public class clsCountryData
    {
        public static string GetCountryByName(int CountryID)
        {
            string Name = "";
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = "select CountryName from Countries where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                object reader = command.ExecuteScalar();
                if (reader != null)
                {
                    Name = reader.ToString();
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return Name;
        } 
        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = "select * from Countries";
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
            catch (Exception ex) { }
            finally { connection.Close(); }
            return dt;

        }
    }
}
