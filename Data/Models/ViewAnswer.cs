namespace Examcy.Data.Models
{
    public class ViewAnswer
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Answer { get; set; } = "";
        public string StudentName { get; set; } = "";
        public virtual FullTask Task { get; set; } = new FullTask();
        public DateTime Date { get; set; }
        public string Time { get; set; } = "";
        public int TaskId { get; internal set; }
    }
}
