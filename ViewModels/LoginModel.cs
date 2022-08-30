using System.ComponentModel.DataAnnotations;

namespace Examcy.ViewModels
{
    public class LoginModel
    {
        [MaxLength(40, ErrorMessage = "Имя должно иметь длину меньше 40 символов")]
        [MinLength(8, ErrorMessage = "Имя должно иметь длину больше 8 символов")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
