using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TSolvedTasksViewModel
    {
        public ViewAnswer answer { get; set; } = new ViewAnswer();
        public bool isTrue = false;
        public bool content = false;
    }
}
