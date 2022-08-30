using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TUpdateVarViewModel
    {
        public int VarId { get; set; }
        public List<TaskInVar> tasks = new List<TaskInVar>();
    }
}
