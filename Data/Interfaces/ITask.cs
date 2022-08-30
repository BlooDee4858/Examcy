using Task = Examcy.Data.Models.Task;

namespace Examcy.Data.Interfaces
{
    public interface ITask
    {
        IEnumerable<Task> AllTask { get; /*set; */}
        Task getTask(int id);
    }
}
