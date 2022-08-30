using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Examcy.Controllers
{
    public class StHomeController : Controller
    {
        private readonly IUserService _userService;
        public StHomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {    
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                var model = new StHomeViewModel();

                // получаем список курсов пользователя
                GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                List<int> idsMyCourses = groupCourseRepository.getIdsCoursesForStudent(idS);
                CourseRepository courseRepository = new CourseRepository();
                foreach (var c in idsMyCourses)
                {
                    Course course = new Course();
                    course = courseRepository.getCourseById(c);
                    course.Teacher = u.GetUserById(course.TeacherId);
                    model.courses.Add(course);
                }


                // получаем какое-нибудь неполученное достижение
                StudentAchievementsRepository studentAchievementsRepository = new StudentAchievementsRepository();
                List<AchievementsList> notMyAchives = new List<AchievementsList>();
                foreach (var a in studentAchievementsRepository.AllAchivs())
                {
                    bool flag = false;
                    foreach (var my in studentAchievementsRepository.getMyAchives(idS))
                    {
                        if (my.Id == a.Id)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                        notMyAchives.Add(a);
                }
                if (notMyAchives.Count() > 0)
                    model.achievements = notMyAchives.First();
                else
                    model.achievements = null;



                // получаем список назначенных заданий
                AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                model.tasks = assignedTasksRepository.getTasksForStudent(idS);

                // рейтинг

                ViewBag.Title = "Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }

        // метод для перехода к заданию
        // метод для перехода к курсу
    }
}
