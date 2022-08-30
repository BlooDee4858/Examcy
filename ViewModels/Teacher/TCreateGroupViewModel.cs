using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TCreateGroupViewModel
    {
        public List<StudentForGroup> myStudents { get; set; } = new List<StudentForGroup>();
        public List<StudentForGroup> allStudents { get; set; } = new List<StudentForGroup>();
        public int course { get; set; } 
    }
}
