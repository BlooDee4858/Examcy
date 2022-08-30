namespace Examcy.Data.Models
{
    public class Answers
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TaskId { get; set; }
        public string Answer { get; set; } = "";
        public virtual Student Student { get; set; } = new Student();
        public virtual Task Task { get; set; } = new Task();
        public DateTime Date { get; set; }
        public double Time { get; set; }

    }
}
