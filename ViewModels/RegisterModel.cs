using System.ComponentModel.DataAnnotations;

namespace Examcy.ViewModels
{
    public class RegisterModel
    {
        [MaxLength(40, ErrorMessage = "Имя должно иметь длину меньше 40 символов")]
        [MinLength(8, ErrorMessage = "Имя должно иметь длину больше 8 символов")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;


        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; } = String.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}
