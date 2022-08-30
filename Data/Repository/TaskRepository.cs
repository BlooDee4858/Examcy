using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class TaskRepository
    {        
        public int getIdVarByTaskInVarId(int id)
        {
            int var = 0;
            string sqlExpression = "select VarID from TaskInVar where ID = @id";

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
                connection.Close();
            }
            return var;
        }

        public bool updateTaskFromVar(int oldId, int newTask)
        {
            string sqlExpression = String.Format("update TaskInVar set TaskID = @task where ID = @id");
            bool flag = false;
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {

                connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            idParam.Value = oldId;
            command.Parameters.Add(idParam);

            SqlParameter textParam = new SqlParameter("@task", System.Data.SqlDbType.Int);
            textParam.Value = newTask;
            command.Parameters.Add(textParam);

                if (command.ExecuteNonQuery() == 0)
                {
                }
                else
                    flag = true;
                connection.Close();
            }
            return flag;
        }

        public Models.Task getSolvedTask(int id)
        {
            List<int> idS = new List<int>();
            string sqlExpression = "select distinct Task.ThemeID from Answers join Task on Task.ID = Answers.TaskID where Answers.StudentID = @id";

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
                            idS.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }

            if (idS == null || idS.Count == 0)
                return null;

            List<int> tasks = new List<int>();

            foreach (var i in idS)
            {
                sqlExpression = "select ID from Task where ThemeID = @id";

                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = i;
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

            }
            Random rand = new Random();
            Models.Task task = new Models.Task();
            task = getTaskByIdWithTheme(tasks[rand.Next(0, tasks.Count - 1)]);

            return task;
        }


        public Models.Task getUnSolvedTask(int id)
        {
            List<int> idCourses = new List<int>();

            string sqlExpression = "select GroupCourse.CourseID from StudentGroup join GroupCourse on StudentGroup.GroupID = GroupCourse.ID where StudentGroup.StudentID = @id";

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
                            idCourses.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }

            if(idCourses == null)
            {
                return null;
            }
            if(idCourses != null && idCourses.Count == 0)
            {
                return null;
            }

            List<int> idThemes = new List<int>();

            sqlExpression = "select distinct Task.ThemeID from Task join Theme on Theme.ID = Task.ThemeID where Theme.CourseID = @id";

            foreach (int idC in idCourses)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = idC;
                    command.Parameters.Add(idParam);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                idThemes.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    connection.Close();
                }
            }

            if (idThemes == null || idThemes.Count == 0)
                return null;

            sqlExpression = "select distinct Task.ThemeID from Answers join Task on Task.ID = Answers.TaskID where Answers.StudentID = @id";

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
                            int a = reader.GetInt32(0);
                            if(idThemes.Contains(a))
                                idThemes.Remove(a);
                        }
                    }
                }
                connection.Close();
            }

            if (idThemes == null || idThemes.Count == 0)
            {
                return null;
            }

            List<int> tasks = new List<int>();

            foreach (var i in idThemes)
            {
                sqlExpression = "select ID from Task where ThemeID = @id";

                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = i;
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

            }
            if (tasks == null || tasks.Count == 0)
                return null;

            Random rand = new Random();
            Models.Task task = new Models.Task();
            if (tasks.Count > 1)
                task = getTaskByIdWithTheme(tasks[rand.Next(0, tasks.Count - 1)]);
            else
                task = getTaskByIdWithTheme(tasks[0]);

            return task;
        }


        public List<int> getAllTaskIdSForNumAndCourse(int num, int courseId)
        {
            List<int> idS = new List<int>();
            string sqlExpression = "select Task.ID from Task join Theme on Theme.ID = Task.ThemeID join Types on Theme.TypeID = Types.ID where Theme.CourseID = @course and Types.TaskNumber = @num";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter numParam = new SqlParameter("@num", System.Data.SqlDbType.Int);
                numParam.Value = num;
                command.Parameters.Add(numParam);

                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = courseId;
                command.Parameters.Add(courseParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            idS.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }
            return idS;


        }

        public int getNumByTaskInVarId(int id)
        {
            int num = 0;
            string sqlExpression = "select Types.TaskNumber from TaskInVar join Task on TaskInVar.TaskID = Task.ID join Theme on Theme.ID = Task.ThemeID join Types on Types.ID = Theme.TypeID where TaskInVar.ID = @id";

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
                            num = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return num;

        }

        public FullTask getFullTask(int id)
        {
            FullTask task = new FullTask();
            string sqlExpression = "select Task.ID, Task.Text, Task.Answer, Task.ThemeID, Task.Solution, Theme.Title, Theme.CourseID, Types.TaskNumber from Task join Theme on Theme.ID = Task.ThemeID join Types on Theme.TypeID = Types.ID where Task.ID = @id";

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
                            task.Id = reader.GetInt32(0);
                            task.Text = reader.GetString(1);
                            task.Answer = reader.GetString(2);
                            task.ThemeId = reader.GetInt32(3);
                            task.Solution = reader.GetString(4);
                            task.ThemeTitle = reader.GetString(5);
                            task.CourseId = reader.GetInt32(6);
                            task.Number = reader.GetInt32(7);
                            //task.countAnswer = reader.GetInt32(8);
                        }
                    }
                }
                connection.Close();
            }
            return task;
        }

        public List<TaskContent> getFilesForTask(int id)
        {
            List<TaskContent> contents = new List<TaskContent>();
            string sqlExpression = "select ID, Path, Type from TaskContent where TaskID = @id";

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
                            TaskContent content = new TaskContent();
                            content.Id = reader.GetInt32(0);
                            content.Path = reader.GetString(1);
                            content.Type = reader.GetString(2);
                            contents.Add(content);
                        }
                    }
                }
                connection.Close();
            }
            return contents;

        }

        internal List<TaskInVar> getAllTaskInVar(int id)
        {
            // id = VarId
            List<TaskInVar> tasks = new List<TaskInVar>();
            string sqlExpression = "select TaskInVar.ID, TaskInVar.NumberTaskInVar, Task.ID, Task.Text, Task.Answer, Task.ThemeID, Task.Solution, Theme.Title, Types.TaskNumber from TaskInVar join Task on TaskInVar.TaskID = Task.ID join Theme on Theme.ID = Task.ThemeID join Types on Theme.TypeID = Types.ID where TaskInVar.VarId = @id";

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
                            TaskInVar task = new TaskInVar();
                            task.Id = reader.GetInt32(0);
                            task.numberTaskInVar = reader.GetInt32(1);
                            task.TaskId = reader.GetInt32(2);
                            task.Task.Text = reader.GetString(3);
                            task.Task.Answer = reader.GetString(4);
                            task.Task.ThemeId = reader.GetInt32(5);
                            task.Task.Solution = reader.GetString(6);
                            task.Task.ThemeTitle = reader.GetString(7);
                            task.Task.Number = reader.GetInt32(8);
                            tasks.Add(task);
                        }
                    }
                }
                connection.Close();
            }
            return tasks;

        }

        public Models.Task getTaskById(int id)
        {
            Models.Task task = new Models.Task();
            string sqlExpression = "SELECT * FROM Task where ID = @id";

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
                            task.Id = reader.GetInt32(0);
                            task.Text = reader.GetString(1);
                            task.Answer = reader.GetString(2);
                            task.ThemeId = reader.GetInt32(3);
                        }
                    }
                }
                connection.Close();
            }
            return task;
        }


        public Models.Task getTaskByIdWithTheme(int id)
        {
            Models.Task task = new Models.Task();
            string sqlExpression = "SELECT Task.ID, Task.Text, Theme.Title FROM Task join Theme on Theme.ID = Task.ThemeID where Task.ID = @id";

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
                            task.Id = reader.GetInt32(0);
                            task.Text = reader.GetString(1);
                            task.Title = reader.GetString(2);
                        }
                    }
                }
                connection.Close();
            }
            return task;
        }


        public void updateTaskContent(int taskId, string myPath, string v)
        {
            throw new NotImplementedException();
        }

        public bool IsAssignedTask(int id)
        {
            int count = 0;
            string sqlExpression = "select count(*) from AssignedTasks join Task on Task.ID = AssignedTasks.TaskID where Task.ID = @id";

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
                            count = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            if (count != 0)
                return false;
            return true;
        }

        public List<string> getCodePath(int taskId)
        {
            List<string> codePath = new List<string>();
            string sqlExpression = "select Path from TaskContent WHERE TaskContent.TaskID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = taskId;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            codePath.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return codePath;
        }

        public bool delTaskContent(int taskId, string type)
        {
            string sqlExpression = "DELETE from TaskContent WHERE TaskContent.TaskID = @id and Type = @type";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = taskId;
                command.Parameters.Add(idParam);
                SqlParameter typeParam = new SqlParameter("@type", System.Data.SqlDbType.NVarChar);
                typeParam.Size = 50;
                typeParam.Value = type;
                command.Parameters.Add(typeParam);

                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        internal bool deleteTask(int id)
        {
            string sqlExpression = "DELETE from AssignedTasks WHERE AssignedTasks.TaskID = @id";
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

            sqlExpression = "DELETE from Answers WHERE Answers.TaskID = @id";
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

            sqlExpression = "DELETE from SolvedTaskInVar WHERE SolvedTaskInVar.TaskID = @id";
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

            sqlExpression = "DELETE from TaskContent WHERE TaskContent.TaskID = @id";
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

            int newTask = getNewTask(id);
            if (newTask != 0)
            {
                sqlExpression = "update TaskInVar set TaskID = @new where TaskID = @id";
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = id;
                    command.Parameters.Add(idParam);

                    SqlParameter newParam = new SqlParameter("@new", System.Data.SqlDbType.Int);
                    newParam.Value = newTask;
                    command.Parameters.Add(newParam);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                int varId = 0;
                sqlExpression = "select VarID from TaskInVar WHERE TaskID = @id";
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
                                varId = reader.GetInt32(0);
                            }
                        }
                    }
                    connection.Close();
                }

                sqlExpression = "DELETE from TaskInVar WHERE TaskID = @id";
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

                sqlExpression = "DELETE from AssignedVars WHERE VarId = @id";
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = varId;
                    command.Parameters.Add(idParam);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                sqlExpression = "DELETE from Var WHERE ID = @id";
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idParam.Value = varId;
                    command.Parameters.Add(idParam);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            sqlExpression = "DELETE from Task WHERE ID = @id";
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


            return true;
        }

        private int getNewTask(int id)
        {
            int i = 0;
            string sqlExpression = "select Task.ID from Task join Theme on Theme.ID = Task.ThemeID join Types on Types.ID = Theme.TypeID where Types.TaskNumber = (select Types.TaskNumber from Task join Theme on Theme.ID = Task.ThemeID join Types on Types.ID = Theme.TypeID where Task.ID = @id)";
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
                            i = reader.GetInt32(0);
                            break;
                        }
                    }
                }
                connection.Close();
            }
            return i;
        }

        public bool IsTaskInVar(int id)
        {
            string sqlExpression = "select count(*) from TaskInVar WHERE TaskInVar.TaskID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);
                if (command.ExecuteNonQuery() != 0)
                {
                    return false;
                }
                connection.Close();
            }
            return true;
        }

        public void updateTask(FullTask task)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("update Task set Task.Text = @text, Task.Solution = @solution, Task.Answer = @answer where ID = @id");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = task.Id;
                command.Parameters.Add(idParam);

                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Size = 3000;
                textParam.Value = task.Text;
                command.Parameters.Add(textParam);

                SqlParameter solParam = new SqlParameter("@solution", System.Data.SqlDbType.NVarChar);
                solParam.Size = 3000;
                solParam.Value = task.Solution;
                command.Parameters.Add(solParam);

                SqlParameter answParam = new SqlParameter("@answer", System.Data.SqlDbType.NVarChar);
                answParam.Size = 3000;
                answParam.Value = task.Answer;
                command.Parameters.Add(answParam);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Models.Task> getAllTaskForTheme(int idTheme)
        {
            List<Models.Task> tasks = new List<Models.Task>();
            string sqlExpression = "SELECT * FROM Task where ThemeID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idTheme;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Models.Task task = new Models.Task();
                            task.Id = reader.GetInt32(0);
                            task.Text = reader.GetString(1);
                            task.Answer = reader.GetString(2);
                            task.ThemeId = reader.GetInt32(3);
                            tasks.Add(task);
                        }
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public int getThemeIdForTask(int idTask)
        {
            int idTheme = 0;
            string sqlExpression = "select ThemeID from Task where ID = @id";

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
                            idTheme = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return idTheme;

        }

        // список заданий по теме, которые студент не решал
        public List<Models.Task> getTasksForTheme(int idTheme, List<int> myTasks)
        {
            List<Models.Task> tasks = new List<Models.Task>();
            string sqlExpression = "select *  from [Task] where ThemeID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idTheme;
                command.Parameters.Add(idParam);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            int a = reader.GetInt32(0);
                            bool flag = false;
                            foreach (var my in myTasks)
                            {
                                if (my == a)
                                    flag = true;
                            }
                            if (!flag)
                            {
                                Models.Task task = new Models.Task();
                                task.Id = reader.GetInt32(0);
                                task.Text = reader.GetString(1);
                                task.Answer = reader.GetString(2);
                                task.ThemeId = reader.GetInt32(3);
                                tasks.Add(task);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return tasks;

        }

        internal int addTask(Models.Task task)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {

                string sqlExpression = String.Format("Insert into Task(Text, Answer, ThemeID, Solution) output INSERTED.ID Values(@text, @answer, @themeId, @solution)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Value = task.Text;
                textParam.Size = 3000;
                command.Parameters.Add(textParam);

                SqlParameter answerParam = new SqlParameter("@answer", System.Data.SqlDbType.NVarChar);
                answerParam.Value = task.Answer;
                answerParam.Size = 3000;
                command.Parameters.Add(answerParam);

                SqlParameter themeParam = new SqlParameter("@themeId", System.Data.SqlDbType.Int);
                themeParam.Value = task.ThemeId;
                command.Parameters.Add(themeParam);

                SqlParameter solutionParam = new SqlParameter("@solution", System.Data.SqlDbType.NVarChar);
                solutionParam.Value = task.Solution;
                solutionParam.Size = 3000;
                command.Parameters.Add(solutionParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }

        public int addTask(FullTask task)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {

                string sqlExpression = String.Format("Insert into Task(Text, Answer, ThemeID, Solution) output INSERTED.ID Values(@text, @answer, @themeId, @solution)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Value = task.Text;
                textParam.Size = 3000;
                command.Parameters.Add(textParam);

                SqlParameter answerParam = new SqlParameter("@answer", System.Data.SqlDbType.NVarChar);
                answerParam.Value = task.Answer;
                answerParam.Size = 3000;
                command.Parameters.Add(answerParam);

                SqlParameter themeParam = new SqlParameter("@themeId", System.Data.SqlDbType.Int);
                themeParam.Value = task.ThemeId;
                command.Parameters.Add(themeParam);

                SqlParameter solutionParam = new SqlParameter("@solution", System.Data.SqlDbType.NVarChar);
                solutionParam.Value = task.Solution;
                solutionParam.Size = 3000;
                command.Parameters.Add(solutionParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }

        }

        public int getCountTasks(int idCourse)
        {
            int count = 0;
            string sqlExpression = "select count(*) from Task join Theme on Task.ThemeID = Theme.ID where Theme.CourseID = @id";
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


        public List<TaskForList> getAllTaskForCourse(int idCourse)
        {
            List<TaskForList> tasks = new List<TaskForList>();
            string sqlExpression = "select Task.ID, Theme.Title, Theme.TypeID from Task join Theme on Theme.ID = Task.ThemeID where Theme.CourseID = @id order by Theme.TypeID";
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
                            TaskForList task = new TaskForList();
                            task.Id = reader.GetInt32(0);
                            task.Theme = reader.GetString(1);
                            task.NumInEGE = reader.GetInt32(2);
                            task.CourseId = idCourse;
                            tasks.Add(task);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }


        public List<AnswersForList> getAllSolvedTaskForCourse(int idCourse)
        {
            List<AnswersForList> tasks = new List<AnswersForList>();
            string sqlExpression = "select Answers.ID, Answers.TaskID, Users.LastName, Users.FirstName, Answers.Date, Answers.Time, Answers.Answer, Task.Answer, Theme.Title from Answers join Student on Answers.StudentID = Student.ID join Users on Student.UserID = Users.ID join Task on Answers.TaskID = Task.ID join Theme on Task.ThemeID = Theme.ID where Theme.CourseID = @id";
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
                            AnswersForList task = new AnswersForList();
                            task.Id = reader.GetInt32(0);

                            task.TaskId = reader.GetInt32(1);
                            task.StudentName = reader.GetString(2) + " " + reader.GetString(3);
                            task.Date = reader.GetDateTime(4);
                            int time = Convert.ToInt32(reader.GetDouble(5));
                            string ans = reader.GetString(6);
                            string ansTask = reader.GetString(7);
                            if (ans == ansTask)
                                task.Correct = "Верно";
                            else
                                task.Correct = "Неверно";
                            task.Theme = reader.GetString(8);
                            task.CourseId = idCourse;

                            int sec = time % 60;
                            int min = (time / 60) % 60;
                            int h = time / 360;
                            task.Time = h + ":" + min + ":" + sec;

                            tasks.Add(task);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }

        public List<GenerateTask> getAllTaskForGenerate(int idTheme)
        {
            List<GenerateTask> tasks = new List<GenerateTask>();
            string sqlExpression = "select Task.ID, Types.TaskNumber from Task join Theme on Theme.ID = Task.ThemeID join Types on Types.ID = Theme.TypeID where Theme.ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idTheme;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            GenerateTask task = new GenerateTask();
                            task.Id = reader.GetInt32(0);
                            task.Num = reader.GetInt32(1);
                            tasks.Add(task);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }

        public List<AssignedTaskForTeacherList> getAssignedTasksForCourse(int idCourse)
        {
            List<AssignedTaskForTeacherList> tasks = new List<AssignedTaskForTeacherList>();
            string sqlExpression = "select AssignedTasks.ID, AssignedTasks.StudentID, Users.LastName, Users.FirstName, AssignedTasks.TaskID, AssignedTasks.Date, Theme.Title, Theme.ID from AssignedTasks join Student on AssignedTasks.StudentID = Student.ID join Users on Student.UserID = Users.ID join Task on AssignedTasks.TaskID = Task.ID join Theme on Task.ThemeID = Theme.ID where Theme.CourseID = @id";
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
                            AssignedTaskForTeacherList assignedTasks = new AssignedTaskForTeacherList();
                            assignedTasks.Id = reader.GetInt32(0);
                            assignedTasks.StudentId = reader.GetInt32(1);
                            assignedTasks.StudentName = reader.GetString(2) + " " + reader.GetString(3);
                            assignedTasks.TaskId = reader.GetInt32(4);
                            assignedTasks.Date = reader.GetDateTime(5);
                            assignedTasks.Theme = reader.GetString(6);
                            assignedTasks.ThemeId = reader.GetInt32(7);
                            assignedTasks.CourseId = idCourse;
                            tasks.Add(assignedTasks);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return tasks;
        }



        public void addTaskContent(int taskId, string myPath, string type)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {

                string sqlExpression = String.Format("Insert into TaskContent(TaskID, Path, Type) Values(@taskId, @path, @type)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter pathParam = new SqlParameter("@path", System.Data.SqlDbType.NVarChar);
                pathParam.Value = myPath;
                pathParam.Size = 300;
                command.Parameters.Add(pathParam);

                SqlParameter idParam = new SqlParameter("@taskId", System.Data.SqlDbType.Int);
                idParam.Value = taskId;
                command.Parameters.Add(idParam);

                SqlParameter typeParam = new SqlParameter("@type", System.Data.SqlDbType.NVarChar);
                typeParam.Value = type;
                typeParam.Size = 50;
                command.Parameters.Add(typeParam);

                command.ExecuteNonQuery();

                connection.Close();
            }


        }
    }
}
