using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class THomeController : Controller
    {
        public THomeController()
        {
        }

        public IActionResult Index()
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                THomeViewModel model = new THomeViewModel();
                CourseRepository courseRepository = new CourseRepository();
                ThemeRepository themeRepository = new ThemeRepository();

                List<Course> idCourses = new List<Course>();
                idCourses = courseRepository.getAllCourseForTeacher(idU);
                foreach (var course in idCourses)
                {
                    int countTheme = themeRepository.getAllThemesForCourse(course.Id).Count;
                    int countStudent = courseRepository.getCountStudent(course.Id);
                    CourseForTeacher courseForTeacher = new CourseForTeacher();
                    courseForTeacher.Id = course.Id;
                    courseForTeacher.Title = course.Title;
                    courseForTeacher.countTheme = countTheme;
                    courseForTeacher.countStudent = countStudent;
                    model.courses.Add(courseForTeacher);
                }

                AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                model.tasks = assignedTasksRepository.getTasksForTeacher(idU);
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                model.vars = assignedVarsRepository.getVarsForTeacher(idU);

                ViewBag.Title = "Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}
