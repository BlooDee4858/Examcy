using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StTasksViewModel
    {
        public FullTask task { get; set; } = new FullTask();
        public DateTime startTime { get; set; }
        public int isAssigned = 0;
        public bool content = false;
    }
}
