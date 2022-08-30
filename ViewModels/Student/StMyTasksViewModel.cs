using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StMyTasksViewModel
    {
        public List<AssignedTaskForStudentList> myTasks { get; set; } = new List<AssignedTaskForStudentList>(); // назначенные
        public Data.Models.Task otherTask1 { get; set; } = new Data.Models.Task(); // рекомендованные
        public Data.Models.Task otherTask2 { get; set; } = new Data.Models.Task(); // рекомендованные

    }
}
