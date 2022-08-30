namespace Examcy.Data.Models
{
    public class AnswersForList
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string StudentName { get; set; } = "";
        public string Theme { get; set; } = "";
        public string Correct { get; set; } = "";
        public DateTime Date { get; set; }
        public string Time { get; set; } = "";
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = "";
    }
}
