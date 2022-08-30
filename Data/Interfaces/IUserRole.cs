using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface IUserRole
    {
        IEnumerable<UserRole> AllRole { get; /*set; */}
        UserRole getRole(int id);
    }
}
