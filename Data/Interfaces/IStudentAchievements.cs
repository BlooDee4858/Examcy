using Examcy.Data.Models;

namespace Examcy.Data.Interfaces
{
    public interface IStudentAchievements
    {
        IEnumerable<StudentAchievements> allStudentAchievements { get; /*set; */}
        IEnumerable<StudentAchievements> getAchievsThisStudent(int id);
        // + функция возвр. любое достижение которое студент еще не получил
    }
}
