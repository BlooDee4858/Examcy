namespace Examcy.Data.Models
{
    public class StudentGroup
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public virtual Student Student { set; get; } = new Student();
        public virtual GroupCourse Group { set; get; } = new GroupCourse();
    }
}
