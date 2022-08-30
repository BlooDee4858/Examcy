using Examcy.Data.Models;
using Examcy.ViewModels.User;

namespace Examcy.ViewModels.Student
{
    public class StTaskViewModel
    {
        public UserViewModel User { get; set; } = new UserViewModel();
        public string TypeTask { get; set; } = string.Empty;

        public Data.Models.Task task { get; set; } = new Data.Models.Task();
        public bool isOneTask = true;
        public DateTime startTime { get; set; }
        public string Answer { get; set; } = string.Empty;
        public string task_detail { get; set; } = string.Empty;

        public string task_type { get; set; } = string.Empty;
        public string[] JavascriptToRun { get; set; } = { "", "" };
    }

}
