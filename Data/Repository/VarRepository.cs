using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class VarRepository
    {
        public VarRepository()
        {
        }

        // Получаем список всех вариантов для курса
        public List<Var> getAllVarsForCourse(int idCourse)
        {
            List<Var> vars = new List<Var>();
            string sqlExpression = "select * from Var where CourseID = @id";

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
                            Var var = new Var();
                            var.Id = reader.GetInt32(0);
                            var.CourseId = reader.GetInt32(1);
                            vars.Add(var);
                        }
                    }
                }
                connection.Close();
            }
            return vars;
        }

        // Получаем вариант по id
        public Var getVarByTd(int idVar)
        {
            Var var = new Var();
            string sqlExpression = "select * from Var where ID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idVar;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            var.Id = reader.GetInt32(0);
                            var.CourseId = reader.GetInt32(1);
                        }
                    }
                }
                connection.Close();
            }
            return var;
        }

        // Получаем список решенных студентами вариантов для курса
        public List<SolvedVarForTeacherList> getAllSolvedVarForCourse(int idCourse)
        {
            List<SolvedVarForTeacherList> vars = new List<SolvedVarForTeacherList>();
            string sqlExpression = "select AssignedVars.ID, AssignedVars.VarId, AssignedVars.Result, AssignedVars.StudentID, AssignedVars.Date, AssignedVars.Time, Users.LastName, Users.FirstName  from AssignedVars join Student on Student.ID = AssignedVars.StudentID join Users on Student.UserID = Users.ID join Var on AssignedVars.VarId = Var.ID where AssignedVars.Time != 0 and AssignedVars.isTestVar != 0 and Var.CourseID = @id";

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
                            SolvedVarForTeacherList var = new SolvedVarForTeacherList();

                            var.Id = reader.GetInt32(0);
                            var.CourseId = idCourse;
                            var.VarId = reader.GetInt32(1);
                            var.mark = reader.GetInt32(2);
                            var.StudentId = reader.GetInt32(3);
                            var.Date = reader.GetDateTime(4);
                            int time = Convert.ToInt32(reader.GetDouble(5));
                            var.StudentName = reader.GetString(6) + " " + reader.GetString(7);

                            int sec = time % 60;
                            int min = (time / 60) % 60;
                            int h = time / 360;
                            var.time = h + ":" + min + ":" + sec;
                            vars.Add(var);
                        }
                    }
                }
                connection.Close();
            }
            return vars;

        }

        // Получаем список назначенных, но не решенных, вариантов для курса
        public List<AssignedVarForTeacherList> getAllAssignedVarForCourse(int idCourse)
        {
            List<AssignedVarForTeacherList> vars = new List<AssignedVarForTeacherList>();
            string sqlExpression = "select AssignedVars.ID, AssignedVars.VarId, AssignedVars.StudentID, AssignedVars.Date, Users.FirstName, Users.LastName  from AssignedVars join Student on Student.ID = AssignedVars.StudentID join Users on Student.UserID = Users.ID join Var on AssignedVars.VarId = Var.ID where AssignedVars.Time = 0 and AssignedVars.isTestVar != 0 and Var.CourseID = @id";

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
                            AssignedVarForTeacherList var = new AssignedVarForTeacherList();

                            var.Id = reader.GetInt32(0);
                            var.CourseId = idCourse;
                            var.VarId = reader.GetInt32(1);
                            var.StudentId = reader.GetInt32(2);
                            var.Date = reader.GetDateTime(3);
                            var.StudentName = reader.GetString(4) + " " + reader.GetString(5);
                            vars.Add(var);
                        }
                    }
                }
                connection.Close();
            }
            return vars;
        }

        public int addVar(int courseId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //string sqlExpression = String.Format("Insert into Answers(StudentID,TaskID,Answer,Date,Time) " + "output INSERTED.ID " +
                //                                     "Values ({0}, {1}, '{2}', '{3}', {4})", StudentID, TaskID, Answer, Date, Time);

                string sqlExpression = String.Format("Insert into Var(CourseID) output INSERTED.ID Values(@course)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = courseId;
                command.Parameters.Add(courseParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }

        public int addTaskInVar(int id, GenerateTask generateTask)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("Insert into TaskInVar(TaskID, VarID, NumberTaskInVar) output INSERTED.ID Values(@task, @var, @num)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter varParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                varParam.Value = id;
                command.Parameters.Add(varParam);

                SqlParameter taskParam = new SqlParameter("@task", System.Data.SqlDbType.Int);
                taskParam.Value = generateTask.Id;
                command.Parameters.Add(taskParam);

                SqlParameter numParam = new SqlParameter("@num", System.Data.SqlDbType.Int);
                numParam.Value = generateTask.Num;
                command.Parameters.Add(numParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }

        public void deleteVar(int id)
        {
            List<int> SolvedTaskInVar = new List<int>();
            // получаем список решенных заданий из варианта
            string sqlExpression = "select SolvedTaskInVar.ID from TaskInVar join SolvedTaskInVar on SolvedTaskInVar.TaskID = TaskInVar.ID WHERE TaskInVar.VarID = @id";
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
                            SolvedTaskInVar.Add(reader.GetInt32(0));
                        }
                    }
                }

            }

            sqlExpression = "DELETE from SolvedTaskInVar WHERE VarID = @id";

            foreach (var task in SolvedTaskInVar)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = task;
                    command.Parameters.Add(idParam);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            sqlExpression = "DELETE from AssignedVars WHERE AssignedVars.VarId = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                connection.Close();
            }

            sqlExpression = "DELETE from TaskInVar WHERE TaskInVar.VarID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                connection.Close();
            }

            sqlExpression = "DELETE from Var WHERE Var.ID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int addTaskInVar(int varId, int taskId, int i)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("Insert into TaskInVar(TaskID, VarID, NumberTaskInVar) output INSERTED.ID Values(@task, @var, @num)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter varParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                varParam.Value = varId;
                command.Parameters.Add(varParam);

                SqlParameter taskParam = new SqlParameter("@task", System.Data.SqlDbType.Int);
                taskParam.Value = taskId;
                command.Parameters.Add(taskParam);

                SqlParameter numParam = new SqlParameter("@num", System.Data.SqlDbType.Int);
                numParam.Value = i;
                command.Parameters.Add(numParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }
    }
}
