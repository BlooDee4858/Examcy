using Examcy.Data.Models;

namespace Examcy.Data.Interfaces
{
    public interface IAchievementsList
    {
        IEnumerable<AchievementsList> AllAchievs { get; /*set; */}
        AchievementsList getAchievs(int id);
    }
}
