namespace Examcy.Data.Models
{
    public class SolvedVarForTeacherList
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";

        public int VarId { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public int mark { get; set; } = 0;
        public string time { get; set; } = "";
    }
}
