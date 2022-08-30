using Examcy.Data.Models;

namespace Examcy.ViewModels.Teacher
{
    public class TCreateThemeViewModel
    {
        public List<ThemeForCreating> themes = new List<ThemeForCreating>();
        public int courseId { get; set; }
    }
}
