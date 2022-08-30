using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StAllCoursesViewModel
    {
        public List<Course> courses { get; set; } = new List<Course>();
        // список курсов доступных для студента
    }
}
