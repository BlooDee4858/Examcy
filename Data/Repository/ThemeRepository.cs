using Examcy.Data.Models;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class ThemeRepository
    {
        public ThemeRepository()
        {
        }

        public List<Theme> AllThemes()
        {
            List<Theme> themes = new List<Theme>();
            string sqlExpression = "SELECT ID, Title, Text, OtherText, CourseID, TypeID FROM Theme";

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
                            Theme theme = new Theme();
                            theme.Id = reader.GetInt32(0);
                            theme.Title = reader.GetString(1);
                            theme.Text = reader.GetString(2);
                            theme.OtherText = reader.GetString(3);
                            theme.CourseId = reader.GetInt32(4);
                            theme.TypeId = reader.GetInt32(5);
                            themes.Add(theme);
                        }
                    }
                }
                connection.Close();
            }
            return themes;
        }
        
        public int getCourseByThemeId(int id)
        {
            int i = 0;
            string sqlExpression = "SELECT CourseID FROM Theme where ID = @id";

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
                        }
                    }
                }
                connection.Close();
            }
            return i;
        }

        public List<ThemeForCreating> getAllThemesForCreatingTheme()
        {
            List<ThemeForCreating> themes = new List<ThemeForCreating>();
            string sqlExpression = "SELECT ID, Title, TaskNumber FROM Types";

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
                            ThemeForCreating theme = new ThemeForCreating();
                            theme.Id = reader.GetInt32(0);
                            theme.Title = reader.GetString(1);
                            theme.number = reader.GetInt32(2);
                            themes.Add(theme);
                        }
                    }
                }
                connection.Close();
            }
            return themes;
        }

        public string getTypeTitle(int themeId)
        {
            string text = "";
            string sqlExpression = "SELECT Title FROM Types where ID = @id";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = themeId;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            text = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            return text;
        }

        public int addTheme(Theme theme)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {

                string sqlExpression = String.Format("Insert into Theme(Title, Text, OtherText, CourseID, TypeID, Path) output INSERTED.ID Values (N'{0}', N'{1}', N'{2}', {3}, {4}, N'{5}')", theme.Title, theme.Text, theme.OtherText, theme.CourseId, theme.TypeId, theme.Path);


                //string sqlExpression = String.Format("Insert into Theme(Title, Text, OtherText, CourseID, TypeID, Path) output INSERTED.ID Values(@title, @text, @otherText, @courseID, @typeID, @path)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                //SqlParameter courseParam = new SqlParameter("@courseID", System.Data.SqlDbType.Int);
                //courseParam.Value = theme.CourseId;
                //command.Parameters.Add(courseParam);

                //SqlParameter typeParam = new SqlParameter("@typeID", System.Data.SqlDbType.Int);
                //typeParam.Value = theme.TypeId;
                //command.Parameters.Add(typeParam);

                //SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.NVarChar);
                //titleParam.Size = 150;
                //titleParam.Value = theme.Title;
                //command.Parameters.Add(titleParam);

                //SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                //textParam.Size = 3000;
                //textParam.Value = theme.Text;
                //command.Parameters.Add(textParam);


                //SqlParameter otherParam = new SqlParameter("@otherText", System.Data.SqlDbType.NVarChar);
                //otherParam.Size = 3000;
                //otherParam.Value = theme.OtherText;
                //command.Parameters.Add(otherParam);

                //SqlParameter pathParam = new SqlParameter("@path", System.Data.SqlDbType.NVarChar);
                //pathParam.Size = 200;
                //pathParam.Value = theme.Path;
                //command.Parameters.Add(pathParam);



                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }

        }

        public int UpdateTheme(Theme theme, int themeId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //string sqlExpression = String.Format("update Theme set (Title, Text, OtherText, CourseID, TypeID, Path) Values (N'{0}', N'{1}', N'{2}', {3}, {4}, N'{5}') where Theme.ID = @themeId", theme.Title, theme.Text, theme.OtherText, theme.CourseId, theme.TypeId, theme.Path);
                //string sqlExpression = String.Format("update Task set Task.Text = @text, Task.Solution = @solution, Task.Answer = @answer where ID = @id");
                string sqlExpression = String.Format("Update Theme set Theme.Title = @title, Theme.Text = @text, Theme.OtherText = @otherText, Theme.CourseID = @courseID, Theme.TypeID = @typeID, Theme.Path = @Path where ID = @themeid");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@themeId", System.Data.SqlDbType.Int);
                idParam.Value = theme.Id;
                command.Parameters.Add(idParam);

                SqlParameter courseParam = new SqlParameter("@courseID", System.Data.SqlDbType.Int);
                courseParam.Value = theme.CourseId;
                command.Parameters.Add(courseParam);

                SqlParameter typeParam = new SqlParameter("@typeID", System.Data.SqlDbType.Int);
                typeParam.Value = theme.TypeId;
                command.Parameters.Add(typeParam);

                SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.NVarChar);
                titleParam.Size = 150;
                titleParam.Value = theme.Title;
                command.Parameters.Add(titleParam);

                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Size = 3000;
                textParam.Value = theme.Text;
                command.Parameters.Add(textParam);

                SqlParameter otherParam = new SqlParameter("@otherText", System.Data.SqlDbType.NVarChar);
                otherParam.Size = 3000;
                otherParam.Value = theme.OtherText;
                command.Parameters.Add(otherParam);

                SqlParameter pathParam = new SqlParameter("@path", System.Data.SqlDbType.NVarChar);
                pathParam.Size = 200;
                pathParam.Value = theme.Path;
                command.Parameters.Add(pathParam);



                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }

        }

        public Theme getThemeById(int id)
        {
            Theme theme = new Theme();
            string sqlExpression = "SELECT ID, Title, Text, OtherText, CourseID, TypeID, Path FROM Theme where ID = @id";

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
                            theme.Id = reader.GetInt32(0);
                            theme.Title = reader.GetString(1);
                            theme.Text = reader.GetString(2);
                            theme.OtherText = reader.GetString(3);
                            theme.CourseId = reader.GetInt32(4);
                            theme.TypeId = reader.GetInt32(5);
                            theme.Path = reader.GetString(6);
                        }
                    }
                }
                connection.Close();
            }
            return theme;
        }

        public List<Theme> getAllThemesForCourse(int idCourse)
        {
            List<Theme> themes = new List<Theme>();
            string sqlExpression = "select ID, Title, Text, OtherText, CourseID, TypeID from Theme where CourseID = @id";

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
                            Theme theme = new Theme();
                            theme.Id = reader.GetInt32(0);
                            theme.Title = reader.GetString(1);
                            theme.Text = reader.GetString(2);
                            theme.OtherText = reader.GetString(3);
                            theme.CourseId = reader.GetInt32(4);
                            theme.TypeId = reader.GetInt32(5);
                            themes.Add(theme);
                        }
                    }
                }
                connection.Close();
            }
            return themes;
        }

        public void addPatternForTheme(int themeId, int countFiles, int countImg, int countTable, string tableSize)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("update Theme set Theme.imageCount = @imgC, Theme.fileCount = @fileC, Theme.tableCount = @tableC, Theme.tableSize = @size where ID = @id");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = themeId;
                command.Parameters.Add(idParam);

                SqlParameter tSizeParam = new SqlParameter("@size", System.Data.SqlDbType.NVarChar);
                tSizeParam.Size = 100;
                tSizeParam.Value = tableSize;
                command.Parameters.Add(tSizeParam);

                SqlParameter cFileParam = new SqlParameter("@fileC", System.Data.SqlDbType.Int);
                cFileParam.Value = countFiles;
                command.Parameters.Add(cFileParam);

                SqlParameter cImgParam = new SqlParameter("@imgC", System.Data.SqlDbType.Int);
                cImgParam.Value = countImg;
                command.Parameters.Add(cImgParam);


                SqlParameter cTableParam = new SqlParameter("@tableC", System.Data.SqlDbType.Int);
                cTableParam.Value = countTable;
                command.Parameters.Add(cTableParam);

                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        internal ForCreating getInfoForCreating(int idTheme)
        {
            ForCreating info = new ForCreating();
            string sqlExpression = "select imageCount, fileCount, tableCount, tableSize, Title from Theme where ID = @id";

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
                            info.countImg = reader.GetInt32(0);
                            info.countFile = reader.GetInt32(1);
                            info.countTable = reader.GetInt32(2);
                            info.tableSize = reader.GetString(3);
                            info.title = reader.GetString(4);
                        }
                    }
                }
                connection.Close();
            }
            return info;

        }

        public int addTheme(int courseId, string theme, int i, int type)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                //string sqlExpression = String.Format("Insert into Answers(StudentID,TaskID,Answer,Date,Time) " + "output INSERTED.ID " +
                //                                     "Values ({0}, {1}, '{2}', '{3}', {4})", StudentID, TaskID, Answer, Date, Time);

                string sqlExpression = String.Format("Insert into Theme(Title, Text, OtherText, CourseID, TypeID, Path) output INSERTED.ID Values(@title, @text, @otherText, @courseID, @typeID, @path)");

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@courseID", System.Data.SqlDbType.Int);
                courseParam.Value = courseId;
                command.Parameters.Add(courseParam);

                SqlParameter typeParam = new SqlParameter("@typeID", System.Data.SqlDbType.Int);
                typeParam.Value = type;
                command.Parameters.Add(typeParam);

                SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.NVarChar);
                titleParam.Size = 150;
                titleParam.Value = theme;
                command.Parameters.Add(titleParam);

                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Size = 3000;
                textParam.Value = "Теория к этой теме пока не добавлена";
                command.Parameters.Add(textParam);


                SqlParameter otherParam = new SqlParameter("@otherText", System.Data.SqlDbType.NVarChar);
                otherParam.Size = 3000;
                otherParam.Value = "Дополнительной информации к данной теме нет.";
                command.Parameters.Add(otherParam);

                SqlParameter pathParam = new SqlParameter("@path", System.Data.SqlDbType.NVarChar);
                pathParam.Size = 200;
                pathParam.Value = "Нет файлов";
                command.Parameters.Add(pathParam);



                int modified = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return modified;
            }
        }

        public int getTypeByNum(int i)
        {
            int type = 0;
            string sqlExpression = "SELECT ID FROM Types where TaskNumber = @id";

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
                            type = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return type;

        }

        public int getNumByTheme(int id)
        {
            int type = 0;
            string sqlExpression = "select Types.TaskNumber from Theme join Types on Types.ID = Theme.TypeID where Theme.ID = @id";

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
                            type = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return type;
        }

    }
}
