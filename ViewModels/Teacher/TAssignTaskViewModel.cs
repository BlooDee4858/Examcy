using Examcy.Data.Models;
namespace Examcy.ViewModels.Teacher
{
    public class TAssignTaskViewModel
    {
        public List<GroupCourse> groups = new List<GroupCourse>();
        public List<StudentForAssign> students = new List<StudentForAssign>();
        public int task { get; set; }
    }
}
