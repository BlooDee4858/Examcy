namespace Examcy.Data.Models
{
    public class Student
    {
        public int Id { get; set; }             // КЛЮЧ 
        public int UserId { get; set; }         // Id в списке пользователей 
        public double Exp { get; set; }
        public double ExpWeek { get; set; }
        public virtual User User { get; set; } = new User();

    }
}
