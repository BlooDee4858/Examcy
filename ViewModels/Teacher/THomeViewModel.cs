using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class THomeViewModel
    {
        public List<CourseForTeacher> courses { get; set; } = new List<CourseForTeacher>();
        public List<AssignedTaskForTeacherList> tasks { get; set; } = new List<AssignedTaskForTeacherList>();
        public List<AssignedVarsForTeacherList> vars { get; set; } = new List<AssignedVarsForTeacherList>();

    }
}
