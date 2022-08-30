using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class AnswersRepository
    {
        public AnswersRepository()
        {
        }

        public List<AnswersForList> getAllAnswersForStudent(int idStudent)
        {
            List<AnswersForList> tasks = new List<AnswersForList>();
            string sqlExpression = "select Answers.ID, Answers.TaskID, Answers.Date, Answers.Time, Answers.Answer, Task.Answer, Theme.Title, Course.Title from Answers join Task on Answers.TaskID = Task.ID join Theme on Task.ThemeID = Theme.ID join Course on Course.ID = Theme.CourseID where Answers.StudentID = @id";

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
                            AnswersForList task = new AnswersForList();
                            task.Id = reader.GetInt32(0);

                            task.TaskId = reader.GetInt32(1);
                            task.Date = reader.GetDateTime(2);
                            int time = Convert.ToInt32(reader.GetDouble(3));
                            string ans = reader.GetString(4);
                            string ansTask = reader.GetString(5);
                            if (ans == ansTask)
                                task.Correct = "Верно";
                            else
                                task.Correct = "Неверно";
                            task.Theme = reader.GetString(6);
                            task.CourseTitle = reader.GetString(7);

                            int sec = time % 60;
                            int min = (time / 60) % 60;
                            int h = time / 60;
                            task.Time = h + ":" + min + ":" + sec;
                            tasks.Add(task);
                        }
                    }
                }
                connection.Close();
            }
            return tasks;
        }


        public List<int> getAllTaskForStudent(int idStudent)
        {
            List<int> tasks = new List<int>();
            string sqlExpression = "select TaskId from Answers where StudentID = @id";

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
                            tasks.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public int addAnswer(int StudentID, string Answer, int TaskID, DateTime Date, double Time)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //string sqlExpression = String.Format("Insert into Answers(StudentID,TaskID,Answer,Date,Time) " + "output INSERTED.ID " +
                //                                     "Values ({0}, {1}, '{2}', '{3}', {4})", StudentID, TaskID, Answer, Date, Time);

                string sqlExpression = String.Format("Insert into Answers(StudentID, TaskID, Answer, Date, Time) output INSERTED.ID Values(@studentID, @taskID, @answer, @date, @time)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter studentParam = new SqlParameter("@studentID", System.Data.SqlDbType.Int);
                studentParam.Value = StudentID;
                command.Parameters.Add(studentParam);

                SqlParameter taskParam = new SqlParameter("@taskID", System.Data.SqlDbType.Int);
                taskParam.Value = TaskID;
                command.Parameters.Add(taskParam);

                SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.DateTime);
                dateParam.Value = Date;
                command.Parameters.Add(dateParam);

                SqlParameter timeParam = new SqlParameter("@time", System.Data.SqlDbType.Float);
                timeParam.Value = Time;
                command.Parameters.Add(timeParam);

                string ans = Answer.Trim();

                SqlParameter ansParam = new SqlParameter("@answer", System.Data.SqlDbType.NVarChar);
                ansParam.Size = 3000;
                ansParam.Value = ans;
                command.Parameters.Add(ansParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }

        public ViewAnswer getAnswerById(int idAnswer)
        {
            ViewAnswer answer = new ViewAnswer();
            string sqlExpression = "select Answers.ID, Answers.StudentID, Answers.TaskID, Answers.Answer, Answers.Date, Answers.Time, Users.LastName, Users.FirstName from Answers join Student on Answers.StudentID = Student.ID join Users on Users.ID = Student.UserID where Answers.ID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idAnswer;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            answer.Id = reader.GetInt32(0);
                            answer.StudentId = reader.GetInt32(1);
                            answer.TaskId = reader.GetInt32(2);
                            answer.Answer = reader.GetString(3);
                            answer.Date = reader.GetDateTime(4);

                            int time = Convert.ToInt32(reader.GetDouble(5));
                            int sec = time % 60;
                            int min = time / 60 % 60;
                            int h = time / 60;
                            answer.Time = h + ":" + min + ":" + sec;
                            answer.StudentName = reader.GetString(6) + " " + reader.GetString(7);
                        }
                    }
                }
                connection.Close();
            }

            return answer;
        }



        public int getCountAnswers(int idCourse)
        {
            int count = 0;
            string sqlExpression = "select count(*) from Answers join Task on Answers.TaskID = Task.ID join Theme on Task.ThemeID = Theme.ID where Theme.CourseID = @id";
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

        public void addExp(int taskID, string answer, int idS)
        {

            int exp = 0, expweek = 0;
            string ans = "";
            string sqlExpression = "select Exp, ExpWeek from student where ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idS;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            exp = reader.GetInt32(0);
                            expweek = reader.GetInt32(1);
                        }
                    }
                }
                connection.Close();
            }

            sqlExpression = "select Answer from Task where ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = taskID;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            ans = reader.GetString(0);
                        }
                    }

                    connection.Close();
                }
            }

            answer = answer.Trim();
            if (answer == ans)
            {
                exp += 5;
                expweek += 5;

                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    //string sqlExpression = String.Format("Insert into Answers(StudentID,TaskID,Answer,Date,Time) " + "output INSERTED.ID " +
                    //                                     "Values ({0}, {1}, '{2}', '{3}', {4})", StudentID, TaskID, Answer, Date, Time);

                    sqlExpression = String.Format("update student set Exp = @exp, ExpWeek = @expweek where ID = @id");

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter studentParam = new SqlParameter("@exp", System.Data.SqlDbType.Int);
                    studentParam.Value = exp;
                    command.Parameters.Add(studentParam);

                    SqlParameter taskParam = new SqlParameter("@expweek", System.Data.SqlDbType.Int);
                    taskParam.Value = expweek;
                    command.Parameters.Add(taskParam);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = idS;
                    command.Parameters.Add(idParam);

                    command.ExecuteScalar();

                    connection.Close();

                }
            }
        }
    }

}
