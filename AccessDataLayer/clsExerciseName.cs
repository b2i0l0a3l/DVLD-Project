using AccessBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDataLayer
{
    public class clsExerciseName
    {

        public static int AddNewExerciseName(string ExName)
        {
            int Num = -1;
            SqlConnection connection = new SqlConnection(AccessDataSettings.ConnectionString);
            string Query = @"INSERT INTO ExerciseNames (Name)
                            SELECT 'Push Ups'
                            WHERE not EXISTS (
                                SELECT 1 FROM ExerciseNames
                                WHERE Name = 'Push Ups'
                            ); 
                        select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.Add("@Name", ExName);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if(Result != null && int.TryParse(Result.ToString() , out int re))
                {
                    Num = re;
                }
            }catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return Num;
        }
    }
}
