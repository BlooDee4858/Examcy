using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examcy.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Examcy.Data.Interfaces;
using Examcy.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Examcy.Data.Models;

namespace Examcy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public const string SessionKeyName = "_Name";
        public const string SessionKeyId = "MyId";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) // здесь регистрируем плагины и прочее
        {
            // подключаем базу
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<AppDBContent>(options => options.UseSqlServer(connection));


            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });
                
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationCon>(options =>
               options.UseSqlServer(connection));

            services.InitializeRepositories();
            services.InitializeServices();


            // связываем интерфейс и классы которые будут работать с бд
            //services.AddTransient<IStudent, StudentRepository>();
            //services.AddTransient<IStudentAchievements, StudentAchievementsRepository>();
            //services.AddTransient<IAchievementsList, AchievementsListRepository>();  
            //services.AddTransient<IAnswers, AnswersRepository>();
            //services.AddTransient<ICourse, CourseRepository>();
            //services.AddTransient<IGroup, GroupRepository>();  
            //services.AddTransient<IGroupCourse, GroupCourseRepository>();
            //services.AddTransient<IModelsOfEGE, ModelsOfEGERepository>();
            //services.AddTransient<ISessions, SessionsRepository>();  
            //services.AddTransient<IStudentGroup, StudentGroupRepository>();
            //services.AddTransient<ITask, TaskRepository>();
            //services.AddTransient<ITaskType, TaskTypeRepository>(); 
            //services.AddTransient<ITaskTypeModel, TaskTypeModelRepository>();
            //services.AddTransient<ITeacher, TeacherRepository>();
            //services.AddTransient<ITheme, ThemeRepository>();
            //services.AddTransient<IThemeTask, ThemeTaskRepository>();
            //services.AddTransient<ITheory, TheoryRepository>();
            //services.AddTransient<IUser, UserRepository>();
            //services.AddTransient<IUserRole, UserRoleRepository>();
            // добавить для вариантов

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMvc(); // добавляем mvc 
            services.AddMvc(option => option.EnableEndpointRouting = false); // без этого падает mvc
        }


        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
 
            

            app.UseDeveloperExceptionPage(); // отображает ошибки
            app.UseStatusCodePages(); // отображает код страниц
           
            app.UseMvcWithDefaultRoute(); // сможем отслеживать URL страниц
            

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}");
            });

            app.Run(async (context) =>
            {
               /* if (string.IsNullOrEmpty(context.Session.GetString("MyId")))
                {
                    context.Session.SetInt32("MyId", 1);
                }*/
            });
            
        }
    }
}
