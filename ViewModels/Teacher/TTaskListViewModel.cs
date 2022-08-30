using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TTaskListViewModel
    {        
        public List<CourseForList> courses = new List<CourseForList>();

        public List<TaskForList> allTasks = new List<TaskForList>();
        public List<AssignedTaskForTeacherList> assignedTasks = new List<AssignedTaskForTeacherList>();
        public List<AnswersForList> solvedTasks { get; set; } = new List<AnswersForList>();

    }
}
