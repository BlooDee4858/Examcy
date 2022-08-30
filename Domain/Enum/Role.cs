using System.ComponentModel.DataAnnotations;

namespace Examcy.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Преподаватель")]
        Teacher = 1,
        [Display(Name = "Обучающийся")]
        Student = 2,
        [Display(Name = "Админ")]
        Admin = 3,
    }
}
