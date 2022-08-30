using System.ComponentModel.DataAnnotations;

namespace Examcy.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}