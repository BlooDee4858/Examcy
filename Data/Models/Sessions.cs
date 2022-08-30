namespace Examcy.Data.Models
{
    public class Sessions
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; } = new User();
    }
}
