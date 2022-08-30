namespace Examcy.Data.Models
{
    public class StudentAchievements
    {
        public int Id { get; set; }         // КЛЮЧ
        public int IdStudent { get; set; }  // Id студента 
        public int IdAchive { get; set; }   // Id достижения из справочника
        public virtual Student Student { set; get; } = new Student();
        public virtual AchievementsList AchievementsList { set; get; } = new AchievementsList();    
    }
}
