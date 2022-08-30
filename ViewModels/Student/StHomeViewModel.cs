using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StHomeViewModel
    {
        public List<AssignedTaskForStudentList> tasks { get; set; } = new List<AssignedTaskForStudentList>(); // список назначенных заданий 
        public List<Course> courses { get; set; } = new List<Course>(); // список курсов студента
        public AchievementsList achievements { get; set; } = new AchievementsList(); // любое достижение которое студент еще не получил
        
        // что-то для рейтинга
    }
}
