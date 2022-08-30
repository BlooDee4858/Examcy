using Examcy.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class SessionsRepository
    {

        public void InsertUserInSessions(User user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();


                string sqlExpression = String.Format("Insert into Sessions(UserID,RoleID,Date) " +
                                                     "Values({0},{1},GETDATE())", user.Id, user.Role);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

            }

        }
        public int GetActiveUserId()
        {
            int id = 1;
            string sqlExpression = "SELECT top(1)* FROM Sessions";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            id = reader.GetInt32(0);
                        }
                    }
                }
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return id;
        }

        public void DeleteFromSession(int id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();


                string sqlExpression = String.Format("Delete from Sessions where UserID = {0}",id);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

            }

        }

        public void ClearSession()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();


                string sqlExpression = String.Format("Truncate table Sessions");

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

            }

        }





    }
}
