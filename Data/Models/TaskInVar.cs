namespace Examcy.Data.Models
{
    public class TaskInVar
    {
        public int Id { get; set; }
        public int numberTaskInVar { get; set; }
        public int TaskId { get; set; }
        public int VarId { get; set; }

        virtual public FullTask Task { get; set; } = new FullTask();
    }
}
