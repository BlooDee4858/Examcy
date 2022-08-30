using Examcy.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Repository
{
    public class SolvedTaskInVarRepository
    {
        public void addSolvedTaskInVar(int AssignedVarID, int TaskID, string Answer)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=tcp:examcyf.database.windows.net,1433;Initial Catalog=Examcy;User Id=examcya@examcyf;Password=zcxvzcxv48/"))
            {
                connection.Open();

                string sqlExpression = String.Format("Insert into SolvedTaskInVar(AssignedVarID, TaskID, Answer) Values (@varId, @taskId, @answer)");


                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter varParam = new SqlParameter("@varId", System.Data.SqlDbType.Int);
                varParam.Value = AssignedVarID;
                command.Parameters.Add(varParam);

                SqlParameter taskParam = new SqlParameter("@taskId", System.Data.SqlDbType.Int);
                taskParam.Value = TaskID;
                command.Parameters.Add(taskParam);

                SqlParameter answerParam = new SqlParameter("@answer", System.Data.SqlDbType.NVarChar);
                answerParam.Size = 3000;
                answerParam.Value = Answer;
                command.Parameters.Add(answerParam);

                int number = command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
