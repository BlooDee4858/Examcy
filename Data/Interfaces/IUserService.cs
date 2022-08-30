using Examcy.Domain.Response;
using Examcy.Data.Models;
using Examcy.ViewModels.User;

namespace Examcy.Data.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
    }
}
