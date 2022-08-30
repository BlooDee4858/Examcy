using Examcy.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class RatingRepository
    {
        public RatingRepository()
        {
        }
        public List<Rating> AllRating(int IdCourse)
        {
            List<Rating> ratings = new List<Rating>();
            string sqlExpression = "exec Rating @CourseID";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //connection.OpenAsync();
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@CourseID", System.Data.SqlDbType.Int);
                idParam.Value = IdCourse;
                command.Parameters.Add(idParam);

                //SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        int i = 1;
                        while (reader.Read()) // построчно считываем данные
                        {
                            Rating rating = new Rating();
                            rating.numberAllRairing = i;
                            rating.numberVisitorsRairing = i;
                            rating.FirstName = reader.GetString(0);
                            rating.LastName= reader.GetString(1);
                            rating.ExpAll = reader.GetInt32(2);
                            rating.ExpTestVar = reader.GetInt32(3);
                            rating.Visitors = reader.GetInt32(4);
                            i++;

                            ratings.Add(rating);
                        }
                    }
                }
                connection.Close();
            }
            return ratings;
        }

        public List<Rating> TestRating(int IdCourse)
        {
            List<Rating> ratings = new List<Rating>();
            string sqlExpression = "exec TestRating @CourseID";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //connection.OpenAsync();
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@CourseID", System.Data.SqlDbType.Int);
                idParam.Value = IdCourse;
                command.Parameters.Add(idParam);

                //SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        int i = 1;
                        while (reader.Read()) // построчно считываем данные
                        {
                            Rating rating = new Rating();
                            rating.numberTestRairing = i;
                            rating.FirstName = reader.GetString(0);
                            rating.LastName = reader.GetString(1);
                            rating.ExpAll = reader.GetInt32(2);
                            rating.ExpTestVar = reader.GetInt32(3);
                            rating.Visitors = reader.GetInt32(4);
                            i++;

                            ratings.Add(rating);
                        }
                    }
                }
                connection.Close();
            }
            return ratings;
        }
        //public User GetUserById(int id)
        //{
        //    User user = new User();
        //    string sqlExpression = "SELECT * FROM Users where Id = @id";
        //    using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
        //    {
        //        connection.OpenAsync();

        //        SqlCommand command = new SqlCommand(sqlExpression, connection);
        //        SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
        //        idParam.Value = id;
        //        command.Parameters.Add(idParam);

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            if (reader.HasRows) // если есть данные
        //            {
        //                while (reader.Read()) // построчно считываем данные
        //                {
        //                    user.Id = reader.GetInt32(0);
        //                    user.Login = reader.GetString(1);
        //                    user.Password = reader.GetString(2);
        //                    user.RoleId = reader.GetInt32(3);
        //                    user.LastDate = reader.GetDateTime(4);
        //                    user.FirstName = reader.GetString(5);
        //                    user.LastName = reader.GetString(6);
        //                    //user.Img = reader.GetString(7);
        //                }
        //            }
        //        }
        //    }
        //    return user;
        //}

        //public string getLogin(int idUser)
        //{
        //    string login = string.Empty;
        //    string sqlExpression = "SELECT * FROM Users where ID = @id";
        //    using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
        //    {
        //        connection.OpenAsync();

        //        SqlCommand command = new SqlCommand(sqlExpression, connection);
        //        SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
        //        idParam.Value = idUser;
        //        command.Parameters.Add(idParam);

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            if (reader.HasRows) // если есть данные
        //            {
        //                while (reader.Read()) // построчно считываем данные
        //                {
        //                    login = reader.GetString(1);
        //                }
        //            }
        //        }
        //    }
        //    return login;
        //}

        //public void changeInfoAboutUser(User user)
        //{
        //    // обновление таблиц Users
        //}

    }
}
