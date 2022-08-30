using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TTaskIndexViewModel
    {
        public FullTask task { get; set; } = new FullTask();
        public bool content = false;
    }
}
