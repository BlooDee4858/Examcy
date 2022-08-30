namespace Examcy.Data.Models
{
    public class SolvedTaskInVar
    {
        public int Id { get; set; }
        public int AssignedVarId { get; set; }

        public int TaskId { get; set; }
        public string Answer { get; set; } = "";
        public DateTime Date { get; set; }
        public double Time { get; set; }

        public virtual FullTask Task { get; set; } = new FullTask();
        virtual public AssignedVars AssignedVar { get; set; }
        public int numberTaskInVar { get; set; }

    }
}
