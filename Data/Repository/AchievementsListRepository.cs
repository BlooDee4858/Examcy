using Examcy.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class AchievementsListRepository
    {

        public List<AchievementsList> GetAllAchievements()
        {
            List<AchievementsList> achievementsLists = new List<AchievementsList>();
            string sqlExpression = "SELECT * FROM AchievementsList";

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
                            AchievementsList achievementsList = new AchievementsList();
                            achievementsList.Id = reader.GetInt32(0);
                            achievementsList.Title = reader.GetString(1);
                            achievementsList.Description = reader.GetString(2);
                            achievementsList.Img = reader.GetString(3);

                            //user.Img = reader.GetString(7);

                            achievementsLists.Add(achievementsList);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return achievementsLists;
        }

        public AchievementsList GetAchievementByID(int id)
        {
            AchievementsList achievementsList = new AchievementsList();
            string sqlExpression = "SELECT * FROM AchievementsList where Id = @id";
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
                            achievementsList.Id = reader.GetInt32(0);
                            achievementsList.Title = reader.GetString(1);
                            achievementsList.Description = reader.GetString(2);
                            achievementsList.Img = reader.GetString(3);
                        }
                    }
                }
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return achievementsList;
        }







    }
}