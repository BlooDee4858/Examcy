using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Domain.Enum;
using Automarket.Domain.Extensions;
using Examcy.Domain.Response;
using Examcy.ViewModels.User;

namespace Examcy.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Login = x.Login,
                        Role = x.Role.GetDisplayName()
                    });

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}