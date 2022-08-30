using Examcy.Data.Repository;

namespace Examcy.Data.Models
{
    public class AssignedVars
    {
        public int Id { get; set; }
        public int VarId { get; set; }
        public int StudentId { get; set; }
        public int Result { get; set; }
        public DateTime DateTime { get; set; }
        public double Time { get; set; }
        virtual public Var Var { get; set; }
        public string StudentName { get; set; } = "";
        public string strTime = "";

        internal int generate(int idC)
        {
            CourseRepository courseRepository = new CourseRepository();

            List<GenerateTask> var = new List<GenerateTask>();

            for (int i = 1; i <= 27; i++)
            {
                List<int> tasks = new List<int>();
                tasks = courseRepository.getTaskForGenerating(idC, i);

                if (tasks!= null && tasks.Count > 0)
                {
                    if (tasks.Count > 1)
                    {
                        Random random = new Random();
                        GenerateTask g = new GenerateTask();
                        g.Id = tasks[random.Next(0, tasks.Count - 1)];
                        g.Num = i;
                        var.Add(g);
                    }
                    else
                    {
                        GenerateTask g = new GenerateTask();
                        g.Id = tasks[0];
                        g.Num = i;
                        var.Add(g);
                    }
                }
                else
                {
                    return 0;
                }
            }
            VarRepository varRepository = new VarRepository();
            int result = varRepository.addVar(idC);
            foreach (var ii in var)
            {
                varRepository.addTaskInVar(result, ii);
            }

            return result;
        }
    }
}
