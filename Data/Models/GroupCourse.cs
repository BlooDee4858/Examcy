namespace Examcy.Data.Models
{
    public class GroupCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = "";
        public bool IsCommonGroup { get; set; }

        public virtual Course Course { set; get; } = new Course();
    }
}
