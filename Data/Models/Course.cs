namespace Examcy.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int TeacherId { get; set; }
        public string Description { get; set; } = "";
        public bool IsOpen { get; set; }
        public virtual User Teacher { get; set; } = new User();
        public int allCount { get; set; } = 0;
    }
}