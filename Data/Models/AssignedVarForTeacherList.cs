namespace Examcy.Data.Models
{
    public class AssignedVarForTeacherList
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";

        public int VarId { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = "";
    }
}
