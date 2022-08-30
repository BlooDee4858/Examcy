using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.ViewModels.Admin;
using Examcy.ViewModels.Teacher;
using Examcy.ViewModels.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationCon _db;

        public UserRepository()
        {

        }

        public UserRepository(ApplicationCon db)
        {
            _db = db;
        }
       
       
        public List<User> AllUsers()
        {
            List<User> users = new List<User>();
            string sqlExpression = "SELECT * FROM Users";

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
                            User user = new User();
                            user.Id = reader.GetInt32(0);
                            user.Login = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            user.Role = (Domain.Enum.Role)reader.GetInt32(3);
                            user.LastDate = reader.GetDateTime(4);
                            user.FirstName = reader.GetString(5);
                            user.LastName = reader.GetString(6);
                            //user.Img = reader.GetString(7);

                            users.Add(user);
                        }
                    }
                }
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return users;
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public async System.Threading.Tasks.Task Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }


        public int GetStudentIdByUserId(int id)
        {
            int i = 0;
            string sqlExpression = "SELECT ID FROM Student where UserID = @id";
            try
            {
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
            }
            catch (Exception ex)
            {
                return 0;
            }
            return i;
        }


        public TSetsViewModel GetInfoForSets(int id)
        {
            TSetsViewModel user = new TSetsViewModel();
            string sqlExpression = "select Login, Password, FirstName, LastName from Users where ID = @id";
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
                            user.Login = reader.GetString(0);
                            user.Password = reader.GetString(1);
                            user.FirstName = reader.GetString(2);
                            user.LastName = reader.GetString(3);
                        }
                    }
                }
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return user;
        }


        public User GetUserById(int id)
        {
            User user = new User();
            string sqlExpression = "SELECT * FROM Users where Id = @id";
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
                            user.Id = reader.GetInt32(0);
                            user.Login = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            user.Role = (Domain.Enum.Role)reader.GetInt32(3);
                            user.LastDate = reader.GetDateTime(4);
                            user.FirstName = reader.GetString(5);
                            user.LastName = reader.GetString(6);
                            //user.Img = reader.GetString(7);
                        }
                    }
                }
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return user;
        }

        

        public string getLogin(int idUser)
        {
            string login = string.Empty;
            string sqlExpression = "SELECT * FROM Users where ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = idUser;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            login = reader.GetString(1);
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return login;
        }

        public void UpdateUser(User user, bool flag)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();
                string sqlExpression;
                if(flag)
                    sqlExpression = String.Format("update Users set Login = @log, Password = @pass, FirstName = @fname, LastName = @lname where ID = @id");
                else
                    sqlExpression = String.Format("update Users set Login = @log, FirstName = @fname, LastName = @lname where ID = @id");



                //string sqlExpression = String.Format("Update Users " +
                //                                     "Set " +
                //                                     (user.Login is not null ? "Login = N'{0}'" : "") +
                //                                     (user.Password is not null ? (user.Login is not null ? "," : "") + "Password = N'{1}'" : "") +
                //                                     (user.FirstName is not null ? (user.Login is not null || user.Password is not null ? "," : "") + "FirstName = N'{2}'" : "") +
                //                                     (user.LastName is not null ? (user.Login is not null || user.Password is not null || user.FirstName is not null ? "," : "") + "LastName = N'{3}'" : "") +
                //                                     (user.Img is not null ? (user.Login is not null || user.Password is not null || user.FirstName is not null || user.LastName is not null ? "," : "") + "img = '{4}'" : "") +
                //                                     " Where Users.ID = {5}", user.Login, user.Password, user.FirstName, user.LastName, user.Img, user.Id); 

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = user.Id;
                command.Parameters.Add(idParam);

                if (flag)
                {
                    SqlParameter passParam = new SqlParameter("@pass", System.Data.SqlDbType.NVarChar);
                    passParam.Value = user.Password;
                    passParam.Size = 150;
                    command.Parameters.Add(passParam);
                }

                SqlParameter loginParam = new SqlParameter("@log", System.Data.SqlDbType.NVarChar);
                loginParam.Value = user.Login;
                loginParam.Size = 50;
                command.Parameters.Add(loginParam);

                SqlParameter fnameParam = new SqlParameter("@fname", System.Data.SqlDbType.NVarChar);
                fnameParam.Value = user.FirstName;
                fnameParam.Size = 50;
                command.Parameters.Add(fnameParam);

                SqlParameter lnameParam = new SqlParameter("@lname", System.Data.SqlDbType.NVarChar);
                lnameParam.Value = user.LastName;
                lnameParam.Size = 50;
                command.Parameters.Add(lnameParam);

                command.ExecuteNonQuery();

                connection.Close();
            }

        }


        public void addUser(User user)
        {
            SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/");

                connection.Open();

                string sqlExpression = "Insert into Users(Login, Password, RoleID, FirstName, LastName) Values(" +
                    "'" + user.Login + "'" + "," + "'" + user.Password + "'" + "," + user.Role + "," + "N'" + user.FirstName + "'" + "," + "N'" + user.LastName + "'" + ")";

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
        }
        public void addStudent(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/");

            connection.Open();

            string sqlExpression = String.Format("Insert Into Student(UserID) Values({0})",id);
            SqlCommand command = new SqlCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public void DeleteUser(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/");

            connection.Open();

            string sqlExpression = String.Format("Delete from users where id = {0}", id);
            SqlCommand command = new SqlCommand(sqlExpression, connection);

            command.ExecuteNonQuery();

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public int GetRoleByLogin(string login)
        {
            int role = 2;
            SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/");

            connection.Open();

            string sqlExpression = String.Format("Select Role from Users where login = N'{0}'", login);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        role = reader.GetInt32(0);
                    }
                }
            }
                       
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            return role;
        }

        public List<UserStudentViewModel> GetAllUserInGroup(int id)
        {
            List<UserStudentViewModel> u = new List<UserStudentViewModel>();
            List<int> studentId = new List<int>();
            UserRepository user = new UserRepository();
            List<User> AllUsers = new List<User>();
            List<User> OutUsers = new List<User>();
            string sqlExpression = String.Format("SELECT UserID,u.Login,u.FirstName,u.LastName,sg.id FROM StudentGroup sg join Student s on s.ID = sg.StudentId join Users u on u.Id = s.UserID where GroupID = {0}", id);

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
                           UserStudentViewModel student = new UserStudentViewModel();
                            student.UserID = reader.GetInt32(0);
                            student.UserLogin = reader.GetString(1);
                            student.FirstName = reader.GetString(2);
                            student.LastName = reader.GetString(3);
                            student.StudentGroupID = reader.GetInt32(4);

                            u.Add(student);
                        }
                    }
                }


                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return u;
        }
        public void DeleteUserFromGroup(int id)
        {
            string sqlExpression = String.Format("Delete from StudentGroup Where ID = {0}", id);

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        



        public int GetUserIdByLogin(string login)
        {
            SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/");

            connection.Open();
            int id = 0;
            string sqlExpression = String.Format("Select Id from Users where login = N'{0}'", login);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            return id;
        }



        public int GetUserIdByStudentId(int id)
        {
            int i = 0;
            string sqlExpression = "SELECT UserID FROM Student where ID = @id";
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

        public int GetTeacherIdByUserId(int id)
        {
            int i = 0;
            string sqlExpression = "SELECT ID FROM Users where UserID = @id";
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

    }
}
