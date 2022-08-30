using Examcy.Data.Models;

namespace Examcy.Data.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> AllUser { get; /*set; */}
        User GetUserById(int id);
        // + функция возвр. id студента, если это студент
        // + функция возвр. id препода, если это препод

    }
}
