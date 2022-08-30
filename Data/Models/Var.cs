namespace Examcy.Data.Models
{
    public class Var
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        virtual public Course Course { get; set; }
    }
}
