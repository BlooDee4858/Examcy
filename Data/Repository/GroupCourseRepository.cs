using Examcy.Data.Models;
using Examcy.ViewModels.Admin;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class GroupCourseRepository
    {
        public GroupCourseRepository()
        {
        }

        public List<GroupCourse> AllGroups()
        {
            List<GroupCourse> groups = new List<GroupCourse>();
            string sqlExpression = "SELECT * FROM GroupCourse";

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
                            GroupCourse group = new GroupCourse();
                            group.Id = reader.GetInt32(0);
                            group.Title = reader.GetString(1);
                            group.CourseId = reader.GetInt32(2);
                            group.IsCommonGroup = reader.GetBoolean(3);
                            groups.Add(group);
                        }
                    }
                }
                connection.Close();
            }
            return groups;
        }

        public List<GroupCourseViewModel> AllGroupsAdmin()
        {
            List<GroupCourseViewModel> groups = new List<GroupCourseViewModel>();
            string sqlExpression = "Select g.ID,g.Title,c.Title from GroupCourse g join Course c on c.ID = g.CourseID";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            GroupCourseViewModel group = new GroupCourseViewModel();
                            group.GroupID = reader.GetInt32(0);
                            group.Title= reader.GetString(1);
                            group.CourseName = reader.GetString(2);
                            groups.Add(group);
                        }
                    }
                }
                connection.Close();
            }
            return groups;
        }

        // список id курсов, которые есть у студента
        public List<int> getIdsCoursesForStudent(int idStudent)
        {
            List<int> courses = new List<int>();
            string sqlExpression = "select GroupCourse.CourseID from StudentGroup left join GroupCourse on StudentGroup.GroupID = GroupCourse.ID where StudentGroup.StudentID = @id";

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
                            courses.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }
            return courses;
        }


        public void DeleteGroup(int id)
        {
            string sqlExpression = String.Format("Delete from GroupCourse where id = {0}", id);
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();  
                connection.Close();
            }
        }

        // получаем id курса к которому относится данная группа
        public int getIdCourseForGroup(int idGroup)
        {
            int courseId = 0;
            string sqlExpression = "select GroupCourse.CourseID from GroupCourse where GroupCourse.ID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idGroup;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            courseId = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return courseId;
        }


        // получаем id общей группы для курса
        public int getIdCommonGroupForCourse(int idCourse)
        {
            int groupId = 0;
            string sqlExpression = "select GroupCourse.ID from GroupCourse where GroupCourse.CourseID = @id and GroupCourse.IsCommonGroup = 1";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idCourse;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            groupId = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return groupId;
        }

        public List<GroupCourse> GetAllGroupForCourse(int id)
        {
            List<GroupCourse> groups = new List<GroupCourse>();
            string sqlExpression = "select * from GroupCourse where CourseID = @id";
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
                            GroupCourse g = new GroupCourse();
                            g.Id = reader.GetInt32(0);
                            g.Title = reader.GetString(1);
                            g.CourseId = reader.GetInt32(2);
                            g.IsCommonGroup = reader.GetBoolean(3);
                            groups.Add(g);
                        }
                    }
                }
                connection.Close();
            }
            return groups;
        }

        public List<StudentForAssign> GetAllStudentForGroup(int id)
        {
            List<StudentForAssign> students = new List<StudentForAssign>();
            string sqlExpression = "select Student.ID, Users.FirstName, Users.LastName from Student join StudentGroup on StudentGroup.StudentID = Student.ID join Users on Users.ID = Student.UserID where StudentGroup.GroupID = @id";
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
                            StudentForAssign g = new StudentForAssign();
                            g.Id = reader.GetInt32(0);
                            g.FirstName = reader.GetString(1);
                            g.LastName = reader.GetString(2);
                            g.GroupId = id;
                            students.Add(g);
                        }
                    }
                }
                connection.Close();
            }
            return students;
        }
        public List<int> GetAllStudentIdsForGroup(int id)
        {
            List<int> students = new List<int>();
            string sqlExpression = "select Student.ID from Student join StudentGroup on StudentGroup.StudentID = Student.ID where StudentGroup.GroupID = @id";
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
                            students.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }
            return students;
        }

    }
}


