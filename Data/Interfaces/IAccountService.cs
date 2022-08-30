using System.Security.Claims;
using System.Threading.Tasks;
using Examcy.ViewModels;
using Examcy.Domain.Response;


namespace Examcy.Data.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginModel model);
    }
}
