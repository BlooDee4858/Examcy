using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TGroupViewModel
    {
        public List<GroupForList> groups = new List<GroupForList>();
        public List<StudentForGroup> students = new List<StudentForGroup>();
        public List<CourseForList> courses = new List<CourseForList>();
    }
}
