using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TGenerateTaskViewModel
    {
        public List<Data.Models.Task> patterns { get; set; } = new List<Data.Models.Task>();
        public string themeTitle { get; set; } = "";
        public int themeId { get; set; }
    }
}
