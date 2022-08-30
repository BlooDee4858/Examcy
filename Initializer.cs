using Examcy.Data.Repository;
using Examcy.Data.Interfaces;
using Examcy.Implementations;
using Examcy.Data.Models;
using Examcy.Domain.Response;
using Microsoft.Extensions.DependencyInjection;

namespace Examcy
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
         
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
