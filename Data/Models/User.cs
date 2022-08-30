using Examcy.Domain.Enum;


namespace Examcy.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        //public int RoleId { get; set; }
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime LastDate { get; set; }
      //  public string Img { get; set; }

        public Role Role { get; set; }

        //virtual public UserRole Role { get; set; } = new UserRole();
    }
}
