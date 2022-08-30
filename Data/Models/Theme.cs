namespace Examcy.Data.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public string OtherText { get; set; } = "";
        public int TypeId { get; set; }
        public int CourseId { get; set; }
        public string Path { get; set; } = "";
        public virtual Course Course { get; set; } = new Course();
        public virtual Types Types { get; set; } = new Types();
    }
}
