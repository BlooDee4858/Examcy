using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TAssignTaskListViewModel
    {
        public List<AssignedTaskForTeacherList> tasks = new List<AssignedTaskForTeacherList>();
        public List<CourseForList> courses = new List<CourseForList>();
    }
}
