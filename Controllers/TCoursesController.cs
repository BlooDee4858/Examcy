using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class TCoursesController : Controller
    {
        [HttpGet]
        public IActionResult CreateCourse()
        {
            // id курса
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            ТСreateCourseViewModel model = new ТСreateCourseViewModel();
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                TypesRepository typesRepository = new TypesRepository();
                //CourseRepository courseRepository = new CourseRepository();

                model.types = typesRepository.getAllTypes();

                foreach (var t in typesRepository.getAllTypes())
                {
                    model.types.Add(t);
                }

                ViewBag.Title = "Конструктор курса | Examcy";
                ViewBag.types = model.types;
                return View(model);
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }

        public IActionResult Index()
        {
            // id курса
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            TCourseListViewModel model = new TCourseListViewModel();
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                CourseRepository courseRepository = new CourseRepository();
                //model.courses = courseRepository.getAllCourseForTeacher(idU);
                foreach (var t in courseRepository.getAllCourseForTeacher(idU))
                {
                    model.courses.Add(t);
                    t.allCount++;
                }

                ViewBag.Title = "Курсы | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }

        public IActionResult CourseInfo(int id)
        {
            // id курса
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            TCourseInfoViewModel model = new TCourseInfoViewModel();
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                CourseRepository courseRepository = new CourseRepository();
                TaskRepository taskRepository = new TaskRepository();
                model.CourseTitle = courseRepository.getCourseById(id).Title;
                model.courseID = courseRepository.getCourseById(id).Id;
                model.Title = courseRepository.getCourseById(id).Title;
                model.themes = courseRepository.getAllThemeOnCourse(id);
                model.tasks = taskRepository.getAllTaskForCourse(id);
                model.vars = courseRepository.getAllVarsOnCourse(id);

                ViewBag.Title = "Курсы | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }

        public IActionResult DeleteCourse(int id)
        {
            CourseRepository c = new CourseRepository();
            c.deleteCourse(id);

            return Redirect("~/TCourses/Index");
        }

        [HttpPost]
        public IActionResult CreateCourse(/*int courseId, */string title, Types type, bool IsOpen, string descrip)
        {
            // id курса
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                CourseRepository courseRepository = new CourseRepository();
                //UserRepository userRepository = new UserRepository();
                int k = courseRepository.addCourse(title, descrip, IsOpen, idU);
                
                // добавляем в базу
                return Redirect("~/TTheme/CreateTheme/" + k); // ссыль заменить
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }


    }
}
