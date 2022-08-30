using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class TypesRepository
    {
        public Types getTypeById(int id)
        {
            Types type = new Types();
            string sqlExpression = "SELECT * FROM Types where ID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            type.Id = reader.GetInt32(0);
                            type.Title = reader.GetString(1);
                        }
                    }
                }
                connection.Close();
            }
            return type;
        }

        public List<Types> getAllTypes()
        {
            List<Types> typesList = new List<Types>();
            
            string sqlExpression = "SELECT * FROM Types";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                //Types type = new Types();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Types type = new Types();
                            type.Id = reader.GetInt32(0);
                            type.Title = reader.GetString(1);
                            type.numInEGE = reader.GetInt32(2);
                            typesList.Add(type);
                        }
                    }
                }
                connection.Close();
            }
            return typesList;
        }
        

    }
}
