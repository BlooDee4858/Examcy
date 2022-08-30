using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class AssignedVarsRepository
    {
        public AssignedVarsRepository()
        {
        }

        public List<AssignedVarsForStudent> getUnsolvedVars(int idStudent)
        {
            List<AssignedVarsForStudent> assignedVars = new List<AssignedVarsForStudent>();
            string sqlExpression = "select AssignedVars.ID, AssignedVars.VarId, AssignedVars.Date, Course.Title  from AssignedVars join Var on Var.ID = AssignedVars.VarId join Course on Course.ID = Var.CourseID where AssignedVars.Time = 0 and StudentID = @id and AssignedVars.isTestVar != 0";

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
                            AssignedVarsForStudent var = new AssignedVarsForStudent();
                            var.Id = reader.GetInt32(0);
                            var.VarId = reader.GetInt32(1);
                            var.Date = reader.GetDateTime(2);
                            var.CourseTitle = reader.GetString(3);
                            assignedVars.Add(var);
                        }
                    }
                }
                connection.Close();
            }
            return assignedVars;
        }

        public List<SolvedTaskInVar> getVarWithAnswers(int idVar, int idStudent)
        {
            List<SolvedTaskInVar> var = new List<SolvedTaskInVar>();

            string sqlExpression = "select * from SolvedTaskInVar where AssignedVarID = @id";

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
                            SolvedTaskInVar task = new SolvedTaskInVar();
                            task.Id = reader.GetInt32(0);
                            task.AssignedVarId = reader.GetInt32(1);
                            task.TaskId = reader.GetInt32(2);
                            task.Answer = reader.GetString(3);
                            var.Add(task);
                        }
                    }
                }
                connection.Close();
            }

            return var;
        }

        public int setVar(int id, int idStudent, DateTime date)
        {
            string sqlExpression = "Insert into AssignedVars(VarID, StudentID, Result, Date, Time, isTestVar) output INSERTED.ID Values (@var, @student, @result, @date, @time, @test)";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //string sqlExpression = String.Format("Insert into AssignedVars(VarID, StudentID, Result, Date, Time, isTestVar) " + "output INSERTED.ID " +
                //                                     "Values ({0}, {1}, '{2}', '{3}', {4}, {5})", idStudent, id, 0, date, 0, 1);
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter varParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                varParam.Value = id;
                command.Parameters.Add(varParam);

                SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                studentParam.Value = idStudent;
                command.Parameters.Add(studentParam);

                SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                dateParam.Value = date;
                command.Parameters.Add(dateParam);

                SqlParameter resultParam = new SqlParameter("@result", System.Data.SqlDbType.Int);
                resultParam.Value = 0;
                command.Parameters.Add(resultParam);

                SqlParameter timeParam = new SqlParameter("@time", System.Data.SqlDbType.Float);
                timeParam.Value = 0;
                command.Parameters.Add(timeParam);

                SqlParameter testParam = new SqlParameter("@test", System.Data.SqlDbType.Int);
                testParam.Value = 1;
                command.Parameters.Add(testParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

                return modified;
            }
        }

        internal bool isSolved(int id)
        {
            bool flag = false;
            string sqlExpression = "select Time from AssignedVars where ID = @id";

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
                            if (reader.GetDouble(0) > 1)
                                flag = true;
                        }
                    }
                }
                connection.Close();
            }
            return flag;
        }

        public int getTestVar(int idS, int idC)
        {
            AssignedVars var = new AssignedVars();
            var.Id = 0;
            string sqlExpression = "select AssignedVars.ID, Date, Time from AssignedVars join Var on Var.ID = AssignedVars.VarId where StudentID = @id and isTestVar = 0 and Var.CourseID = @course";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = idC;
                command.Parameters.Add(courseParam);

                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idS;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {

                            var.Id = reader.GetInt32(0);
                            var.DateTime = reader.GetDateTime(1);
                            var.Time = reader.GetDouble(2);
                        }
                    }
                }
                connection.Close();
            }

            if (var != null && var.Id != 0)
            {
                if ((DateTime.Now - var.DateTime).TotalDays > 7)
                {
                    // new
                    int newid = var.generate(idC);
                    if (newid != 0)
                    {
                        List<int> allStud = new List<int>();
                        sqlExpression = "select AssignedVars.ID from AssignedVars join Var on Var.ID = AssignedVars.VarId where isTestVar = 0 and Var.CourseID = @course";

                        using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                            courseParam.Value = idC;
                            command.Parameters.Add(courseParam);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows) // если есть данные
                                {
                                    while (reader.Read()) // построчно считываем данные
                                    {
                                        allStud.Add(reader.GetInt32(0));
                                    }
                                }
                            }
                            connection.Close();
                        }

                        foreach (var i in allStud)
                        {

                            sqlExpression = "update AssignedVars set VarId = @var, Result = 0, Date = @date, Time = 0 where ID = @id";

                            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                            {
                                connection.Open();

                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                SqlParameter assignedParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                                assignedParam.Value = newid;
                                command.Parameters.Add(assignedParam);

                                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                                idParam.Value = i;
                                command.Parameters.Add(idParam);

                                SqlParameter courseParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                                courseParam.Value = DateTime.Now;
                                command.Parameters.Add(courseParam);

                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
            else
            {
                sqlExpression = "select AssignedVars.ID, Date from AssignedVars join Var on Var.ID = AssignedVars.VarId where isTestVar = 0 and Var.CourseID = @course";

                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                    courseParam.Value = idC;
                    command.Parameters.Add(courseParam);



                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {

                                var.Id = reader.GetInt32(0);
                                var.DateTime = reader.GetDateTime(1);
                                break;
                            }
                        }
                    }
                    connection.Close();
                }

                int newid = 0;
                if (var == null || var.Id == 0)
                {
                    newid = var.generate(idC);
                    if (newid != 0)
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                        {
                            connection.Open();

                            sqlExpression = String.Format("insert into AssignedVars (VarId, StudentID, Result, Date, Time, isTestVar) output INSERTED.ID values (@var, @student, 0, @date, 0, 0)");

                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                            studentParam.Value = idS;
                            command.Parameters.Add(studentParam);

                            SqlParameter idParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                            idParam.Value = newid;
                            command.Parameters.Add(idParam);

                            SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                            dateParam.Value = DateTime.Now;
                            command.Parameters.Add(dateParam);

                            int modified = Convert.ToInt32(command.ExecuteScalar());

                            if (connection.State == System.Data.ConnectionState.Open)
                                connection.Close();
                            return modified;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    int var_ = getVarIdById(var.Id);
                    using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                    {
                        connection.Open();

                        sqlExpression = String.Format("insert into AssignedVars (VarId, StudentID, Result, Date, Time, isTestVar) output INSERTED.ID values (@var, @student, 0, @date, 0, 0)");

                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                        studentParam.Value = idS;
                        command.Parameters.Add(studentParam);

                        SqlParameter idParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                        idParam.Value = var_;
                        command.Parameters.Add(idParam);

                        SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                        dateParam.Value = DateTime.Now;
                        command.Parameters.Add(dateParam);

                        int modified = Convert.ToInt32(command.ExecuteScalar());

                        if (connection.State == System.Data.ConnectionState.Open)
                            connection.Close();
                        return modified;
                    }
                }

            }
            return var.Id;
        }

        public List<TaskInVar> getVarByIdWithTasks(int idVar)
        {
            List<TaskInVar> var = new List<TaskInVar>();

            string sqlExpression = "select *  from TaskInVar where VarID = @id";

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
                            TaskInVar task = new TaskInVar();
                            task.Id = reader.GetInt32(0);
                            task.numberTaskInVar = reader.GetInt32(1);
                            task.TaskId = reader.GetInt32(2);
                            task.VarId = reader.GetInt32(3);
                            var.Add(task);
                        }
                    }
                }
                connection.Close();
            }
            return var;
        }


        public AssignedVars getVarById(int idVar)
        {
            AssignedVars var = new AssignedVars();
            string sqlExpression = "SELECT varId, StudentID, Result, Date, Time FROM AssignedVars where ID = @id";
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
                            var.VarId = reader.GetInt32(0);
                            var.StudentId = reader.GetInt32(1);
                            var.Result = reader.GetInt32(2);
                            var.DateTime = reader.GetDateTime(3);
                            var.Time = reader.GetDouble(4);
                        }
                    }
                }
                connection.Close();
            }
            return var;

        }

        private int convertBall(int ball)
        {
            switch (ball)
            {
                case 1: return 7;
                case 2: return 14;
                case 3: return 20;
                case 4: return 27;
                case 5: return 34;
                case 6: return 40;
                case 7: return 43;
                case 8: return 46;
                case 9: return 48;
                case 10: return 51;
                case 11: return 54;
                case 12: return 56;
                case 13: return 59;
                case 14: return 62;
                case 15: return 64;
                case 16: return 67;
                case 17: return 70;
                case 18: return 72;
                case 19: return 75;
                case 20: return 78;
                case 21: return 80;
                case 22: return 83;
                case 23: return 85;
                case 24: return 88;
                case 25: return 90;
                case 26: return 93;
                case 27: return 95;
                case 28: return 98;
                case 29: return 100;
            }
            return 0;
        }

        public List<SolvedTaskInVar> solvedVar(int idVar, int idStudent, List<string> answers, DateTime start, DateTime end)
        {
            List<SolvedTaskInVar> solvedTasks = new List<SolvedTaskInVar>();

            List<TaskInVar> var = new List<TaskInVar>();
            var = getVarByIdWithTasks(getVarIdById(idVar));
            if (var != null)
            {
                TaskRepository taskRepository = new TaskRepository();
                foreach (var task in var)
                {
                    task.Task = taskRepository.getFullTask(task.TaskId);
                    task.Task.pattern = task.Task.pattern.getTask(task.Task.Number);
                    if (task.Task.pattern != null)
                    {
                        task.Task.pattern.files = taskRepository.getFilesForTask(task.TaskId);
                        if (task.Task.pattern.files != null)
                        {
                            foreach (var f in task.Task.pattern.files)
                            {
                                if (f.Type == "img" || f.Type == "file")
                                    f.Path = f.Path.Substring(7);
                            }
                        }
                        if (task.Task.pattern._code)
                        {
                            foreach (var f in task.Task.pattern.files)
                            {
                                if (f.Type == "code1")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[0] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code2")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[1] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code3")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[2] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code4")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[3] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code5")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[4] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                            }
                        }
                    }
                    task.numberTaskInVar = task.Task.Number; 
                }
                int i = 0;
                int result = 0;
                foreach (var t in var)
                {
                    SolvedTaskInVar s = new SolvedTaskInVar();
                    s.TaskId = t.TaskId;
                    s.Time = 0;
                    s.Task = t.Task;
                    s.Date = end;
                    s.AssignedVarId = idVar;
                    s.Answer = answers[i];
                    i++;
                    if (s.Answer == s.Task.Answer)
                    {
                        if (t.numberTaskInVar == 26 || t.numberTaskInVar == 27)
                        {
                            result = result + 2;
                        }
                        else
                            result++;
                    }
                    solvedTasks.Add(s);
                }
                AssignedVars assignedVars = new AssignedVars();
                assignedVars.Result = convertBall(result);
                assignedVars.DateTime = end;
                var time = end.Subtract(start);
                assignedVars.Time = time.Seconds + time.Minutes * 60 + time.Hours * 60 * 60;

                // обновляем запись
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    string sqlExpression = String.Format("update AssignedVars set AssignedVars.Result = @result, AssignedVars.Date = @date, AssignedVars.Time = @time where ID = @id");

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = idVar;
                    command.Parameters.Add(idParam);

                    SqlParameter rezParam = new SqlParameter("@result", System.Data.SqlDbType.Int);
                    rezParam.Value = assignedVars.Result;
                    command.Parameters.Add(rezParam);

                    SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.DateTime);
                    dateParam.Value = assignedVars.DateTime;
                    command.Parameters.Add(dateParam);

                    SqlParameter timeParam = new SqlParameter("@time", System.Data.SqlDbType.Float);
                    timeParam.Value = assignedVars.Time;
                    command.Parameters.Add(timeParam);

                    command.ExecuteNonQuery();
                    connection.Close();
                }

                // добавляем ответы на задачи
                SolvedTaskInVarRepository solvedTaskInVarRepository = new SolvedTaskInVarRepository();
                foreach (var t in solvedTasks)
                {
                    solvedTaskInVarRepository.addSolvedTaskInVar(idVar, t.TaskId, t.Answer);
                }

                //// добавляем ответы к общим ответам
                //AnswersRepository answersRepository = new AnswersRepository();
                //foreach (var t in solvedTasks)
                //{
                //    answersRepository.addAnswer(idStudent, t.Answer, t.TaskId, assignedVars.DateTime, t.Time);
                //}
            }
            return solvedTasks;
        }


        public List<AssignedVarsForStudent> getSolvedVarForStudent(int idStudent)
        {
            List<AssignedVarsForStudent> vars = new List<AssignedVarsForStudent>();
            string sqlExpression = "select AssignedVars.ID, AssignedVars.VarId, AssignedVars.Date, Course.Title, AssignedVars.Time, AssignedVars.Result from AssignedVars join Var on Var.ID = AssignedVars.VarId join Course on Course.ID = Var.CourseID where AssignedVars.Time != 0 and StudentID = @id and AssignedVars.isTestVar != 0";
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
                            AssignedVarsForStudent var = new AssignedVarsForStudent();
                            var.Id = reader.GetInt32(0);
                            var.VarId = reader.GetInt32(1);
                            var.Date = reader.GetDateTime(2);
                            var.CourseTitle = reader.GetString(3);
                            double t = reader.GetDouble(4);
                            var.result = reader.GetInt32(5);

                            int sec = Convert.ToInt32(t % 60);
                            int min = Convert.ToInt32(t / 60 % 60);
                            int h = Convert.ToInt32(t / 360);
                            var.Time = h.ToString()+":"+min.ToString()+":"+sec.ToString();
                            vars.Add(var);
                        }
                    }
                }
                connection.Close();
            }
            return vars;

        }

        public List<AssignedVarsForTeacherList> getVarsForTeacher(int idTeacher)
        {
            List<AssignedVarsForTeacherList> vars = new List<AssignedVarsForTeacherList>();
            string sqlExpression = "select AssignedVars.ID, AssignedVars.StudentID, Users.LastName, Users.FirstName, AssignedVars.VarID, AssignedVars.Date, Course.ID, Course.Title from AssignedVars join Student on AssignedVars.StudentID= Student.ID join Users on Users.ID = Student.UserID join Var on Var.ID = AssignedVars.VarID join Course on Course.ID=Var.CourseID where Course.TeacherID = @id";
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
                            AssignedVarsForTeacherList var = new AssignedVarsForTeacherList();
                            var.Id = reader.GetInt32(0);
                            var.StudentId = reader.GetInt32(1);
                            var.StudentName = reader.GetString(2) + " " + reader.GetString(3);
                            var.VarId = reader.GetInt32(4);
                            var.Date = reader.GetDateTime(5);
                            var.CourseId = reader.GetInt32(6);
                            var.CourseTitle = reader.GetString(7);
                            vars.Add(var);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return vars;
        }

        public int getVarIdById(int id)
        {
            int var = 0;
            string sqlExpression = "select VarId from AssignedVars where ID = @id";
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
                            var = reader.GetInt32(0);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return var;

        }

        public int delassignedVarById(int id)
        {
            string sqlExpression = "DELETE from AssignedVars WHERE AssignedVars.ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);
                int i = command.ExecuteNonQuery();
                connection.Close();
                return i;
            }
        }

        public int getStudentId(int id)
        {
            int st = 0;
            string sqlExpression = "select StudentID from AssignedVars where ID = @id";
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
                            st = reader.GetInt32(0);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return st;

        }

    }
}
