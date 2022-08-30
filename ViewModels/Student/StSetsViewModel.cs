using Examcy.Data.Models;
using Examcy.ViewModels.User;

namespace Examcy.ViewModels.Student
{
    public class StSetsViewModel
    {
        public Data.Models.User User { get; set; } = new Data.Models.User();
    }

    public class StSetSaveViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
