namespace Examcy.Data.Models
{
    public class AssignedTaskForTeacherList
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";

        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public string Theme { get; set; } = "";
        public int ThemeId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = "";
    }
}
