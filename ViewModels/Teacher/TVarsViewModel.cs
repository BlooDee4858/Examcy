using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TVarListViewModel
    {
        public List<CourseForList> courses = new List<CourseForList>();
        
        public List<Var> allVars = new List<Var>();
        public List<AssignedVarForTeacherList> assignedVars = new List<AssignedVarForTeacherList>();
        public List<SolvedVarForTeacherList> solvedVars = new List<SolvedVarForTeacherList>();
    }
}
