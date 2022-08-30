namespace Examcy.Data.Models
{
    public class AssignedTasks
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TaskId { get; set; }
        public DateTime Date { get; set; }

        virtual public Student Student { get; set; }
        virtual public Task Task { get; set; }
    }
}
