namespace Examcy.Data.Models
{
    public class AssignedTaskForStudentList
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public string Theme { get; set; } = "";
        public string CourseTitle { get; set; } = "";
    }
}
