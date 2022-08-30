namespace Examcy.Data.Models
{
    public class SetTasksAnswer // наборы заданий, но уже решенные с ответами
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        virtual public SetTasks SetTasks { get; set; }
        public string Answer { get; set; } = "";
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
