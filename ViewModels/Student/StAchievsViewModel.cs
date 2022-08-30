using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StAchievsViewModel
    {
        public List<AchievementsList> achievements { get; set; } = new List<AchievementsList>(); // достижения студента
        public List<AchievementsList> all { get; set; } = new List<AchievementsList>(); // достижения которые студент еще не получил
    }
}
