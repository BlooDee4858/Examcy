namespace Examcy.Data.Models
{
    public class SetTasks // наборы заданий, содержит сами задачи
    {
        public int StudentId { get; set; }
        public int Id { get; set; }
        virtual public Task Task { get; set; } = new Task();
        virtual public Student Student { get; set; } = new Student();
    }
}
