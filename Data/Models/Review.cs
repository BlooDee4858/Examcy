namespace Examcy.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int lost { get; set; }
        public string text { get; set; } = "";
        public bool flag = false;
        public int courseId { get; set; }
    }

}
