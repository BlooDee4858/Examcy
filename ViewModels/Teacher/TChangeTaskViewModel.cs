using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TChangeTaskViewModel
    {
        public List<FullTask> tasks = new List<FullTask>();
        public int idVar { get; set; }
        public int oldTaskId { get; set; }

    }
}
