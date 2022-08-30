using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TUpdateGroupViewModel
    {
        public List<StudentForGroup> students { get; set; } = new List<StudentForGroup>();
        public List<StudentForGroup> myStudents { get; set; } = new List<StudentForGroup>();
        public List<StudentForGroup> allStudents { get; set; } = new List<StudentForGroup>();
        public int course { get; set; }
        public int group { get; set; }
        public string name { get; set; } = "";
        public bool isCommon = false;
    }
}
