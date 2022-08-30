namespace Examcy.Data.Models
{
    public class CourseForTeacher
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int countStudent { get; set; } = 0;
        public int countTheme { get; set; } = 0;
    }
}
