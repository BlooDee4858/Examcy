namespace Examcy.Data.Models
{
    public class AssignedVarsForStudent
    {
        public int Id { get; set; }
        public int VarId { get; set; }
        public DateTime Date { get; set; }
        public string CourseTitle { get; set; } = "";
        public string Time = "";
        public int result { get; set; }
    }
}
