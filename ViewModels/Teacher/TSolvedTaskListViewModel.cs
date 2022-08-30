using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TSolvedTaskListViewModel
    {
        public List<AnswersForList> answers { get; set; } = new List<AnswersForList>();
        public List<CourseForList> courses { get; set; } = new List<CourseForList>();
    }
}
