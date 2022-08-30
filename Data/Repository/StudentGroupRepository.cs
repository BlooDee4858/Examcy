using Examcy.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class StudentGroupRepository
    {
        public StudentGroupRepository()
        {
        }
        /////////Ошибка, ничего не передалось
        public void addNewStudentToGroup(int StudentID, int GroupID)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                string sqlExpression = String.Format("Insert into StudentGroup(StudentID,GroupID) output INSERTED.ID Values (@student,@group)");

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                studentParam.Value = StudentID;
                command.Parameters.Add(studentParam);

                SqlParameter idParam = new SqlParameter("@group", System.Data.SqlDbType.Int);
                idParam.Value = GroupID;
                command.Parameters.Add(idParam);

                command.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
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

        public void addNewStudentToVar(int StudentID, int CourseID)
        {
            int var = 0;
            DateTime date = DateTime.Now;
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                string sqlExpression = String.Format("select VarId, Date  from AssignedVars join Var on Var.ID = AssignedVars.VarId where isTestVar = 0 and CourseID = @id");

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                idParam.Value = CourseID;
                command.Parameters.Add(idParam);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            var = reader.GetInt32(0);
                            date = reader.GetDateTime(1);
                            break;
                        }
                    }
                }

                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

            }

            if (var != 0)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
                {
                    connection.Open();

                    string sqlExpression = String.Format("insert into AssignedVars (VarId, StudentID, Result, Date, Time, isTestVar) values (@var, @student, 0, @date, 0, 0)");

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
                    studentParam.Value = StudentID;
                    command.Parameters.Add(studentParam);

                    SqlParameter idParam = new SqlParameter("@var", System.Data.SqlDbType.Int);
                    idParam.Value = var;
                    command.Parameters.Add(idParam);

                    SqlParameter dateParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                    dateParam.Value = date;
                    command.Parameters.Add(dateParam);

                    command.ExecuteNonQuery();

                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                }
            }
        }


        //// список id групп к которым относится отдельный студент
        //public List<int> getAllGroupsIdForStudent(int id) // id студента
        //{
        //    List<int> gr = new List<int>();
        //    foreach (var g in appDBContent.studentGroups)
        //    {
        //        if (g.StudentId == id)
        //        {
        //            gr.Add(g.GroupId);
        //        }
        //    }
        //    return gr; // список id групп к которым относится студент
        //}


    }
}
