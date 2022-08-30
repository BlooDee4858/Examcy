using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class AssignedTasksRepository
    {
        public AssignedTasksRepository()
        {
        }

        public List<AssignedTasks> getAllTasksAssignedTasks()
        {
            List<AssignedTasks> tasks = new List<AssignedTasks>();
            string sqlExpression = "select * from AssignedTasks";
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
                            AssignedTasks assignedTasks = new AssignedTasks();
                            assignedTasks.Id = reader.GetInt32(0);
                            assignedTasks.StudentId = reader.GetInt32(1);
                            assignedTasks.TaskId = reader.GetInt32(2);
                            assignedTasks.Date = reader.GetDateTime(3);
                            tasks.Add(assignedTasks);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }

        // возвращает список назначенных заданий для студента
        public List<AssignedTaskForStudentList> getTasksForStudent(int idStudent)
        {
            List<AssignedTaskForStudentList> tasks = new List<AssignedTaskForStudentList>();
            string sqlExpression = "select AssignedTasks.ID, Task.ID, AssignedTasks.Date, Theme.Title, Course.Title from AssignedTasks join Task on Task.ID = AssignedTasks.TaskID join Theme on Task.ThemeID = Theme.ID join Course on Course.ID = Theme.CourseID where AssignedTasks.StudentID = @id";
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
                            AssignedTaskForStudentList assignedTasks = new AssignedTaskForStudentList();
                            assignedTasks.Id = reader.GetInt32(0);
                            assignedTasks.StudentId = idStudent;
                            assignedTasks.TaskId = reader.GetInt32(1);
                            assignedTasks.Date = reader.GetDateTime(2);
                            assignedTasks.Theme = reader.GetString(3);
                            assignedTasks.CourseTitle = reader.GetString(4);
                            tasks.Add(assignedTasks);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }



        public List<AssignedTaskForTeacherList> getTasksForTeacher(int idTeacher)
        {
            List<AssignedTaskForTeacherList> tasks = new List<AssignedTaskForTeacherList>();
            string sqlExpression = "select AssignedTasks.ID, AssignedTasks.StudentID, Users.LastName, Users.FirstName, AssignedTasks.TaskID, AssignedTasks.Date, Theme.Title, Theme.ID, Course.ID, Course.Title  from AssignedTasks join Student on AssignedTasks.StudentID = Student.ID join Users on Student.UserID = Users.ID join Task on AssignedTasks.TaskID = Task.ID join Theme on Task.ThemeID = Theme.ID join Course on Course.ID = Theme.CourseID where Course.TeacherID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idTeacher;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            AssignedTaskForTeacherList assignedTasks = new AssignedTaskForTeacherList();
                            assignedTasks.Id = reader.GetInt32(0);
                            assignedTasks.StudentId = reader.GetInt32(1);
                            assignedTasks.StudentName = reader.GetString(2) + " " + reader.GetString(3);
                            assignedTasks.TaskId = reader.GetInt32(4);
                            assignedTasks.Date = reader.GetDateTime(5);
                            assignedTasks.Theme = reader.GetString(6);
                            assignedTasks.ThemeId = reader.GetInt32(7);
                            assignedTasks.CourseId = reader.GetInt32(8);
                            assignedTasks.CourseTitle = reader.GetString(9);
                            tasks.Add(assignedTasks);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }


        public AssignedTasks getTaskById(int idTask)
        {
            AssignedTasks assignedTasks = new AssignedTasks();
            string sqlExpression = "select * from AssignedTasks where AssignedTasks.ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idTask;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            assignedTasks.Id = reader.GetInt32(0);
                            assignedTasks.StudentId = reader.GetInt32(1);
                            assignedTasks.TaskId = reader.GetInt32(2);
                            assignedTasks.Date = reader.GetDateTime(3);
                        }
                    }
                }
                connection.Close();
            }
            return assignedTasks;
        }


        public int getCountAssignTask(int idCourse)
        {
            int count = 0;
            string sqlExpression = "select count(*) from AssignedTasks join Task on AssignedTasks.TaskID = Task.ID join Theme on Task.ThemeID = Theme.ID where Theme.CourseID = @id";
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
                            count = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return count;
        }


        public int delassignedTaskById(int idTask)
        {
            string sqlExpression = "DELETE from AssignedTasks WHERE AssignedTasks.ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idTask;
                command.Parameters.Add(idParam);
                int i = command.ExecuteNonQuery();
                connection.Close();
                return i;
            }
        }

        public int assignTaskForStudent(int studentId, int taskId, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {

                string sqlExpression = String.Format("Insert into AssignedTasks(StudentID, TaskID, Date) output INSERTED.ID Values(@studentID, @taskID, @date)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter studentParam = new SqlParameter("@studentID", System.Data.SqlDbType.Int);
                studentParam.Value = studentId;
                command.Parameters.Add(studentParam);

                SqlParameter taskParam = new SqlParameter("@taskID", System.Data.SqlDbType.Int);
                taskParam.Value = taskId;
                command.Parameters.Add(taskParam);

                SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.DateTime);
                dateParam.Value = date;
                command.Parameters.Add(dateParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }
    }
}

