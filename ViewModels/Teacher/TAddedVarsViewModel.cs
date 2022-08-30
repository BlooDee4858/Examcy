using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TAddedVarsViewModel
    {
        public List<Var> vars { get; set; } = new List<Var>();
        public List<TaskInVar> tasks { get; set; } = new List<TaskInVar>();
        public int courseId { get; set; }
    }
}
