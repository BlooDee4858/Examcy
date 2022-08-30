using Examcy.Data.Models;
using Examcy.ViewModels;
using Examcy.ViewModels.Admin;
using Microsoft.Data.SqlClient;

namespace Examcy.Data.Repository
{
    public class CourseRepository
    {
        public CourseRepository()
        {
        }
        
        public List<Course> getAllCourseForTeacher(int idTeacher)
        {
            List<Course> courses = new List<Course>();
            string sqlExpression = "SELECT * FROM Course where TeacherID = @id";

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
                            Course course = new Course();
                            course.Id = reader.GetInt32(0);
                            course.Title = reader.GetString(1);
                            course.TeacherId = reader.GetInt32(2);
                            course.Description = reader.GetString(3);
                            course.IsOpen = reader.GetBoolean(4);
                            courses.Add(course);
                        }
                    }
                }
                connection.Close();
            }
            return courses;
        }

        internal int addNewVar(List<int> var)
        {
            throw new NotImplementedException();
        }

        internal List<int> getTaskForGenerating(int idC, int i)
        {
            List<int> ids = new List<int>();
            string sqlExpression = "select Task.ID from Task join Theme on Task.ThemeID = Theme.ID join Types on Types.ID = Theme.TypeID where Theme.CourseID = @course and Types.TaskNumber = @task";

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = idC;
                command.Parameters.Add(courseParam);

                SqlParameter idParam = new SqlParameter("@task", System.Data.SqlDbType.Int);
                idParam.Value = i;
                command.Parameters.Add(idParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            ids.Add(reader.GetInt32(0));
                        }
                    }
                }
                connection.Close();
            }
            return ids;
        }

        public List<UserCourseViewModel> GetAllCourseAdmin()
        {
            List<UserCourseViewModel> output = new List<UserCourseViewModel>();
            string sqlExpression = "select c.ID,c.Title,c.Description, u.LastName from Course c join Users u on u.ID = c.TeacherID";
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
                            UserCourseViewModel u = new UserCourseViewModel();
                            u.CourseID = reader.GetInt32(0);
                            u.Title = reader.GetString(1);
                            u.Description = reader.GetString(2);
                            u.LastName = reader.GetString(3);
                            output.Add(u);
                        }
                    }
                }
                connection.Close();
            }
            return output;
        }

        public List<GroupForList> getAllGroupForCourse(int id)
        {
            List<GroupForList> groups = new List<GroupForList>();
            string sqlExpression = "select ID, Title, isCommonGroup from GroupCourse where CourseID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            GroupForList group = new GroupForList();
                            group.Id = reader.GetInt32(0);
                            group.Name = reader.GetString(1);
                            group.isCommon = reader.GetBoolean(2);
                            group.CourseId = id;
                            groups.Add(group);
                        }
                    }
                }
                connection.Close();
            }
            return groups;
        }

        public List<StudentForGroup> getAllStudentFromMyCourse(int id)
        {
            List<StudentForGroup> students = new List<StudentForGroup>();
            string sqlExpression = "select StudentGroup.ID, StudentGroup.StudentID, Users.LastName, Users.FirstName from StudentGroup join GroupCourse on StudentGroup.GroupID = GroupCourse.ID join Student on Student.ID = StudentGroup.StudentID join Users on Users.ID = Student.UserID where GroupCourse.CourseID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            StudentForGroup student = new StudentForGroup();
                            student.Id = reader.GetInt32(0);
                            student.StudentId = reader.GetInt32(1);
                            student.StudentName = reader.GetString(2) + " " + reader.GetString(3);
                            students.Add(student);
                        }
                    }
                }
                connection.Close();
            }
            return students;
        }

        internal List<StudentForGroup> getAllStuddentsForGroup(int id)
        {
            List<StudentForGroup> students = new List<StudentForGroup>();
            string sqlExpression = "select StudentGroup.ID, StudentGroup.StudentID, Users.LastName, Users.FirstName from StudentGroup join Student on Student.ID = StudentGroup.StudentID join Users on Users.ID = Student.UserID where StudentGroup.GroupID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            StudentForGroup student = new StudentForGroup();
                            student.Id = reader.GetInt32(0);
                            student.StudentId = reader.GetInt32(1);
                            student.StudentName = reader.GetString(2) + " " + reader.GetString(3);
                            students.Add(student);
                        }
                    }
                }
                connection.Close();
            }
            return students;
        }

        internal GroupCourse getGroup(int id)
        {
            GroupCourse group = new GroupCourse();
            string sqlExpression = "select Title, CourseID, IsCommonGroup from GroupCourse where ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {

                            group.Id = id;
                            group.Title = reader.GetString(0);
                            group.CourseId = reader.GetInt32(1);
                            group.IsCommonGroup = reader.GetBoolean(2); 
                        }
                    }
                }
                connection.Close();
            }
            return group;

        }

        internal void addStudentIntoGroup(int student, int groupId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("insert into StudentGroup(StudentID, GroupID) Values (@student, @group)");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter titleParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                titleParam.Value = student;
                command.Parameters.Add(titleParam);

                SqlParameter courseParam = new SqlParameter("@group", System.Data.SqlDbType.Int);
                courseParam.Value = groupId;
                command.Parameters.Add(courseParam);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }


        internal void deleteStudentFromGroupAndCourse(int student)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("select StudentID from StudentGroup where ID = @id");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = student;
                command.Parameters.Add(courseParam);

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
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("delete StudentGroup where ID = @id");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = student;
                command.Parameters.Add(courseParam);

                command.ExecuteNonQuery();

                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("delete StudentCourse where StudentID = @id");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                command.ExecuteNonQuery();

                connection.Close();
            }

        }

        internal int deleteStudentFromGroup(int student)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("select StudentID from StudentGroup where ID = @id");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = student;
                command.Parameters.Add(courseParam);

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
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("delete StudentGroup where ID = @id");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = student;
                command.Parameters.Add(courseParam);

                command.ExecuteNonQuery();

                connection.Close();
            }
            return id;
        }

        internal int addGroup(string title, int course)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("insert into GroupCourse(Title, CourseID, IsCommonGroup) output INSERTED.ID Values (@title, @course, 0)");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.NVarChar);
                titleParam.Size = 150;
                titleParam.Value = title;
                command.Parameters.Add(titleParam);

                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = course;
                command.Parameters.Add(courseParam);

                int modified = Convert.ToInt32(command.ExecuteScalar());
                
                connection.Close();
                return modified;
            }
        }

        internal void updateGroup(string title, int group)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("update GroupCourse set Title = @title where ID = @id");
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.NVarChar);
                titleParam.Size = 150;
                titleParam.Value = title;
                command.Parameters.Add(titleParam);

                SqlParameter groupParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                groupParam.Value = group;
                command.Parameters.Add(groupParam);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }


        public void addNewStudentToCourse(int StudentID, int CourseID)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                string sqlExpression = String.Format("insert into StudentCourse (StudentID, CourseID, BeginingDate, CountOfDay) values (@student, @course, @date, 0)");

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                studentParam.Value = StudentID;
                command.Parameters.Add(studentParam);

                SqlParameter idParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                idParam.Value = CourseID;
                command.Parameters.Add(idParam);

                SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                dateParam.Value = DateTime.Now;
                command.Parameters.Add(dateParam);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

            }
        }


        public List<StudentForGroup> getAllStudentNotFromThisCourse(int id, List<int> onThisCourse)
        {
            List<StudentForGroup> students = new List<StudentForGroup>();
            string sqlExpression = "select Student.ID, Users.LastName, Users.FirstName from Student join Users on Users.ID = Student.UserID";
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
                            StudentForGroup student = new StudentForGroup();
                            student.Id = reader.GetInt32(0);
                            student.StudentName = reader.GetString(1) + " " + reader.GetString(2);
                            if (!onThisCourse.Contains(student.Id))
                            {
                                students.Add(student);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return students;
        }

        public List<StudentForGroup> getAllStudentForGroup(int id)
        {
            List<StudentForGroup> students = new List<StudentForGroup>();
            string sqlExpression = "select StudentGroup.ID, Users.LastName, Users.FirstName from StudentGroup join Student on Student.ID = StudentGroup.StudentID join Users on Users.ID = Student.UserID where StudentGroup.GroupID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            StudentForGroup student = new StudentForGroup();
                            student.Id = reader.GetInt32(0);
                            student.StudentName = reader.GetString(1)  + " "+ reader.GetString(2);
                            student.groupId = id;
                            students.Add(student);
                        }
                    }
                }
                connection.Close();
            }
            return students;
        }


        public Review getReview(int idS, int idCourse)
        {
            Review review = new Review();
            int count = 0;
            string sqlExpression = "select count(*) from Review where CourseID = @course and StudentID = @student";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = idCourse;
                command.Parameters.Add(courseParam);

                SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                studentParam.Value = idS;
                command.Parameters.Add(studentParam);

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

            if (count == 0)
            {
                review.Id = 0;

                sqlExpression = "select count(distinct Task.ThemeID) from Answers join Task on Task.ID = Answers.TaskID join Theme on Theme.ID = Task.ThemeID where Answers.StudentID = @student and Theme.CourseID = @course";
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                    courseParam.Value = idCourse;
                    command.Parameters.Add(courseParam);

                    SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                    studentParam.Value = idS;
                    command.Parameters.Add(studentParam);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                review.lost = 27 - reader.GetInt32(0);
                            }
                        }
                    }
                    connection.Close();
                }
            }

            else
            {
                sqlExpression = "select ID from Review where CourseID = @course and StudentID = @student";
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                    courseParam.Value = idCourse;
                    command.Parameters.Add(courseParam);

                    SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                    studentParam.Value = idS;
                    command.Parameters.Add(studentParam);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                review.Id = reader.GetInt32(0);
                            }
                        }
                    }
                    connection.Close();
                }
            }

            return review;
        }

        public void updateReview(int id, string text)
        {
            string sqlExpression = "update Review set Text = @text where ID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = id;
                command.Parameters.Add(idParam);

                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Size = 3000;
                textParam.Value = text;
                command.Parameters.Add(textParam);

                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        internal int addReview(int idS, int id)
        {
            int modified = 0;
            string sqlExpression = "insert into Review (CourseID, StudentID, Text) output INSERTED.ID values (@course, @student, @text)";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = id;
                command.Parameters.Add(courseParam);

                SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                studentParam.Value = idS;
                command.Parameters.Add(studentParam);

                SqlParameter textParam = new SqlParameter("@text", System.Data.SqlDbType.NVarChar);
                textParam.Size = 3000;
                textParam.Value = " ";
                command.Parameters.Add(textParam);

                modified = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return modified;
        }

        public Review getReviewById(int id)
        {
            Review review = new Review();
            string sqlExpression = "select CourseID, Text from Review where ID = @id";
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
                            review.courseId = reader.GetInt32(0);
                            review.text = reader.GetString(1);
                            review.Id = id;
                        }
                    }
                }
                connection.Close();
            }
            return review;
        }

        internal bool checkingExistingTask(int num, int idCourse)
        {
            bool flag = false;
            string sqlExpression = "select count(*) from Task join Theme on Task.ThemeID = Theme.ID join Types on Theme.TypeID = Types.ID where Theme.CourseID = @course and Types.TaskNumber = @num";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter courseParam = new SqlParameter("@course", System.Data.SqlDbType.Int);
                courseParam.Value = idCourse;
                command.Parameters.Add(courseParam);

                SqlParameter numParam = new SqlParameter("@num", System.Data.SqlDbType.Int);
                numParam.Value = num;
                command.Parameters.Add(numParam);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            if (reader.GetInt32(0) != 0)
                                flag = true;
                        }
                    }
                }
                connection.Close();
            }

            return flag;
        }

        public int getCountStudent(int id)
        {
            int count = 0;
            string sqlExpression = "select count(*) from groupCourse join StudentGroup on groupCourse.ID = StudentGroup.groupID where groupCourse.CourseID = @id";
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

            return count;
        }

        public InitViewModel getInitInfo()
        {
            InitViewModel viewModel = new InitViewModel();
            string sqlExpression = "select count(*) from Course";
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
                            viewModel.countCourse = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }

            sqlExpression = "select count(*) from Student";
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
                            viewModel.countSt = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }

            sqlExpression = "select count(*) from Review";
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
                            viewModel.countFeedback = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }

            return viewModel;
        }

        public int getCourseByTaskInVarID(int id)
        {
            int courseId = 0;
            string sqlExpression = "select Theme.CourseID from TaskInVar join Task on TaskInVar.TaskID = Task.ID join Theme on Theme.ID = Task.ThemeID where TaskInVar.ID = @id";
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
                            courseId = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return courseId;
        }

        public List<CourseForList> getAllCourseForListForTeacher(int idTeacher)
        {
            List<CourseForList> courses = new List<CourseForList>();
            string sqlExpression = "SELECT ID, Title FROM Course where TeacherID = @id";

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
                            CourseForList course = new CourseForList();
                            course.Id = reader.GetInt32(0);
                            course.Title = reader.GetString(1);
                            courses.Add(course);
                        }
                    }
                }
                connection.Close();
            }
            return courses;
        }


        public List<Course> AllCourses()
        {
            List<Course> courses = new List<Course>();
            string sqlExpression = "SELECT * FROM Course";

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
                            Course course = new Course();
                            course.Id = reader.GetInt32(0);
                            course.Title = reader.GetString(1);
                            course.TeacherId = reader.GetInt32(2);
                            course.Description = reader.GetString(3);
                            course.IsOpen = reader.GetBoolean(4);
                            courses.Add(course);
                        }
                    }
                }
                connection.Close();
            }
            return courses;
        }

        public Course getCourseById(int idCourse)
        {
            Course course = new Course();
            string sqlExpression = "SELECT * FROM Course where ID = @id";
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
                            course.Id = reader.GetInt32(0);
                            course.Title = reader.GetString(1);
                            course.TeacherId = reader.GetInt32(2);
                            course.Description = reader.GetString(3);
                            course.IsOpen = reader.GetBoolean(4);
                        }
                    }
                }
                connection.Close();
            }
            return course;
        }


        public bool checkingTheTopicForTheCourse(int idTheme, int idTask)
        {
            int courseId = 0;
            string sqlExpression = "select Theme.CourseID from Task join Theme on Theme.ID = Task.ThemeID where Task.ID = @id";
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
                           courseId = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }

            sqlExpression = "select count(*) from Theme join Course on Theme.CourseID = Course.ID where Theme.ID = @idTheme and Course.ID = @idCourse";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idThemeParam = new SqlParameter("@idTheme", System.Data.SqlDbType.Int);
                idThemeParam.Value = idTheme;
                command.Parameters.Add(idThemeParam);

                SqlParameter idCourseParam = new SqlParameter("@idCourse", System.Data.SqlDbType.Int);
                idCourseParam.Value = courseId;
                command.Parameters.Add(idCourseParam);


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

            if (courseId != 0)
                return true;

            return false;
        }

        public bool checkingAccessToTheCourseByVarId(int idVar, int idTeacher)
        {
            bool flag = false;
            string sqlExpression = "select Course.TeacherID from Var join Course on Course.ID = Var.CourseID where Var.ID = @id";
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
                            if (idTeacher == reader.GetInt32(0))
                                flag = true;
                        }
                    }
                }
                connection.Close();
            }
            return flag;
        }

        public bool checkingAccessToTheCourseByTaskId(int idTask, int idTeacher)
        {
            bool flag = false;
            string sqlExpression = "select Course.TeacherID from Task join Theme on Theme.ID = Task.ThemeID join Course on Course.ID = Theme.CourseID where Task.ID = @id";
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
                            if (idTeacher == reader.GetInt32(0))
                                flag = true;
                        }
                    }
                }
                connection.Close();
            }
            return flag;
        }

        public bool checkingAccessToTheCourseByThemeId(int idTheme, int idTeacher)
        {
            bool flag = false;
            string sqlExpression = "select Course.TeacherID from Theme join Course on Course.ID = Theme.CourseID where Theme.ID = @id";
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
                            if (idTeacher == reader.GetInt32(0))
                                flag = true;
                        }
                    }
                }
                connection.Close();
            }
            return flag;
        }

        public bool checkingAccessToTheCourseByCourseId(int idCourse, int idTeacher)
        {
            bool flag = false;
            string sqlExpression = "select Course.TeacherID from Course where Course.ID = @id";
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
                            if (idTeacher == reader.GetInt32(0))
                                flag = true;
                        }
                    }
                }
                connection.Close();
            }
            return flag;
        }



        public int getCourseByTaskID(int idTask)
        {
            int courseId = 0;
            string sqlExpression = "select Theme.CourseID from Task join Theme on Task.ThemeID = Theme.ID where Task.ID = @id";
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
                            courseId = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return courseId;
        }

        public int addCourse(string title, string descrip, bool isOpen, int teacherId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("Insert into Course(Title, TeacherID, Description, IsOpen) output INSERTED.ID Values (N'{0}', {1}, N'{2}', '{3}')", title, teacherId, descrip, isOpen);
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int modified = Convert.ToInt32(command.ExecuteScalar());
                sqlExpression = String.Format("Insert into GroupCourse(CourseID, Title, IsCommonGroup) output INSERTED.ID Values ({0}, N'{1}', {2})", modified, "Общая", 1);
                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return modified;
            }
        }

        public void deleteCourse(int id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                string sqlExpression = String.Format("Delete from Group where CourseID = {0}", id);
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                sqlExpression = String.Format("Delete from Course where id = {0}", id);
                connection.Open();
                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        

        public int getTaskNumber(int id)
        {
            int num = 0;
            string sqlExpression = "select Types.TaskNumber from Types join Theme on Theme.TypeID = Types.ID where Theme.ID = @id";
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

        public string getThemeTitle(int id)
        {
            string text = "";
            string sqlExpression = "select Title from Theme where Theme.ID = @id";
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
                            text = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            return text;

        }

        public List<ThemeForList> getAllThemeOnCourse(int id)
        {
            List<ThemeForList> themeList = new List<ThemeForList>();
            string sqlExpression = "select Theme.ID, Theme.Title, Theme.TypeID from Theme where Theme.CourseID = @id";
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
                            ThemeForList theme = new ThemeForList();
                            theme.Id = reader.GetInt32(0);
                            theme.Name = reader.GetString(1);
                            theme.Number = reader.GetInt32(2);
                            themeList.Add(theme);
                        }
                    }
                }
                connection.Close();
            }
            return themeList;
        }
        public List<Var> getAllVarsOnCourse(int id)
        {
            List<Var> varList = new List<Var>();
            string sqlExpression = "select Var.ID from Var where Var.CourseID = @id";
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
                            Var var = new Var();
                            var.Id = reader.GetInt32(0);
                            varList.Add(var);
                        }
                    }
                }
                connection.Close();
            }
            return varList;
        }

        public List<ThemeGenerate> getAllThemesForCourse(int id) {
            List<ThemeGenerate> themeList = new List<ThemeGenerate>();
            string sqlExpression = "select Theme.ID, Theme.Title, Types.TaskNumber from Theme join Types on Theme.TypeID = Types.ID where Theme.CourseID = @id";
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/")) {
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
                            ThemeGenerate theme = new ThemeGenerate();
                            theme.ThemeId = reader.GetInt32(0);
                            theme.Title = reader.GetString(1);
                            theme.Num = reader.GetInt32(2);
                            themeList.Add(theme);
                        }
                    }
                }
                connection.Close();
            }
            return themeList;
        }
    }
}
