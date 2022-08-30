using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TSolvedVarViewModel
    {
        public int VarID { get; set; }
        public List<SolvedTaskInVar> tasks = new List<SolvedTaskInVar>();
    }
}
