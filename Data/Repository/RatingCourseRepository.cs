using Examcy.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class RatingCourseRepository
    {
        public RatingCourseRepository()
        {
        }
        public List<Course> AllRatingCourse(int IdStudent)
        {
            List<Course> raitingcourses = new List<Course>();
            string sqlExpression = "select t1.ID ,t1.Title ,t1.TeacherID ,t1.Description ,t1.IsOpen from [dbo].[Course] t1 inner join [dbo].[StudentCourse] t2 on t1.ID=t2.CourseID where t2.StudentID=@StudentID";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //connection.OpenAsync();
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@StudentID", System.Data.SqlDbType.Int);
                idParam.Value = IdStudent;
                command.Parameters.Add(idParam);

                //SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Course course = new Course();
                            course.Id = reader.GetInt32(0);
                            course.Title = reader.GetString(1);
                            course.TeacherId = reader.GetInt32(2);
                            course.Description = reader.GetString(3);
                            course.IsOpen = reader.GetBoolean(4);
                            raitingcourses.Add(course);
                        }
                    }
                }
                connection.Close();
            }
            return raitingcourses;
        }

    }
}
