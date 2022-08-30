using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StThemeViewModel
    {
        public Theme theme { get; set; } = new Theme();
        public List<Data.Models.Task> tasks { get; set; } = new List<Data.Models.Task>(); // тема+ все задания этой темы
        public List<GameList> games { get; set; } = new List<GameList>();
        public bool flag = true;
    }
}
