using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TCourseInfoViewModel
    {
        public int courseID { get; set; }
        public string CourseTitle = "";
        public int ID { get; set; }
        public string Title { get; set; } = "";
        public List<ThemeForList> themes = new List<ThemeForList>();
        public List<TaskForList> tasks = new List<TaskForList>();
        public List<Var> vars = new List<Var>();


    }
}
