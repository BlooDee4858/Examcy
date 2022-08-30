using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TGenerateVarViewModel
    {
        public List<ThemeGenerate> themes = new List<ThemeGenerate>();
        public int taskCount = 27;
        public bool themeFlag = true;
        public bool taskFlag = true;
        public int courseId { get; set; }
        public string theme = "";
    }
}
