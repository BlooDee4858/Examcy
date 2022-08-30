using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TCreateTaskViewModel
    {
        public string theme { get; set; } = "";
        public int themeId { get; set; }
        public Pattern pattern { get; set; } = new Pattern();

    }
}
