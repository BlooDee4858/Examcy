using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StVarViewModel
    {
        public int Id { get; set; } = 0;
        public int varId { get; set; } = 0;
        public DateTime start { get; set; }
        public List<TaskInVar> var { get; set; } = new List<TaskInVar>(); // вариант+все задания варианта
    }
}
