using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TCreateVarViewModel
    {
        public List<TaskForVarCreating> tasks = new List<TaskForVarCreating>();
        public int taskCount = 27;
        public int courseId { get; set; }
    }
}
