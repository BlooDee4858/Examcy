using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StMyCourseViewModel
    {
        public Course course { get; set; } = new Course();                 // курс
        public List<Theme> themes { get; set; } = new List<Theme>();             // список тем
        public List<Data.Models.Task> tasks { get; set; } = new List<Data.Models.Task>();      // список заданий
        public List<Var> vars { get; set; } = new List<Var>();              // список вариантов
        public Review review = new Review();
    }
}
