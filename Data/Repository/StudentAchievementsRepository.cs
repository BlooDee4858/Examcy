using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class StudentAchievementsRepository
    {
        public StudentAchievementsRepository()
        {
        }

        public List<AchievementsList> AllAchivs()
        {
            List<AchievementsList> achives = new List<AchievementsList>();
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
                            AchievementsList achiv = new AchievementsList();
                            achiv.Id = reader.GetInt32(0);
                            
                            achiv.Title = reader.GetString(1);
                            achiv.Description = reader.GetString(2);
                            achiv.Img = reader.GetString(3);
                            if ((achiv.Id != 14 && achiv.Id != 15 && achiv.Id != 16 && achiv.Id != 17) && (achiv.Id != 19 && achiv.Id != 20 && achiv.Id != 21 && achiv.Id != 22) && (achiv.Id != 24 && achiv.Id != 25 && achiv.Id != 26 && achiv.Id != 27) && (achiv.Id != 29 && achiv.Id != 30 && achiv.Id != 31 && achiv.Id != 32) && (achiv.Id != 35 && achiv.Id != 36 && achiv.Id != 37 && achiv.Id != 38))
                                achives.Add(achiv);
                        }
                    }
                }
                connection.Close();
            }
            return achives;
        }

        public List<AchievementsList> getMyAchives(int idStudent)
        {
            List<AchievementsList> myAchives = new List<AchievementsList>();
            string sqlExpression = "select  AchievementsList.ID, AchievementsList.Title, AchievementsList.Description, AchievementsList.img  from AchievementsList left join StudentAchievements on AchievementsList.ID = StudentAchievements.AchiveID where StudentAchievements.StudentID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idStudent;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            AchievementsList achiv = new AchievementsList();
                            achiv.Id = reader.GetInt32(0);
                            achiv.Title = reader.GetString(1);
                            achiv.Description = reader.GetString(2);
                            achiv.Img = reader.GetString(3);
                            myAchives.Add(achiv);
                        }
                    }
                }
                connection.Close();
            }
            return myAchives;
        }        
    }
}
