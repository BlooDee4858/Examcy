using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StMyCourseController : Controller
    {
        private readonly IUserService _userService;
        public StMyCourseController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                StMyCourseViewModel model = new StMyCourseViewModel();
                // получаем курс
                CourseRepository courseRepository = new CourseRepository();
                model.course = courseRepository.getCourseById(id);

                // получаем темы курса
                ThemeRepository themeRepository = new ThemeRepository();
                TypesRepository typesRepository = new TypesRepository();
                model.themes = themeRepository.getAllThemesForCourse(id);
                TaskRepository taskRepository = new TaskRepository();
                foreach (var theme in model.themes)
                {
                    //theme.Types = typesRepository.getTypeById(theme.TypeId);
                    foreach (var t in taskRepository.getAllTaskForTheme(theme.Id))
                    {
                        model.tasks.Add(t);
                    }
                }

                try
                {
                    model.review = courseRepository.getReview(idS, id);
                    if (model.review.Id != 0)
                    { model.review.flag = true;
                        model.review.courseId = id;
                    }

                    if (model.review.lost < 20 && model.review.Id == 0)
                    {
                        model.review.flag = true;
                        model.review.Id = courseRepository.addReview(idS, id);
                        model.review.courseId = id;
                    }
                }
                catch (Exception ex)
                {
                    model.review = null;

                }
                // получаем список вариантов
                VarRepository varRepository = new VarRepository();
                model.vars = varRepository.getAllVarsForCourse(id);

                ViewBag.Title = model.course.Title + " | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/Home/Index");

            }
        }

        [HttpGet]
        public IActionResult Review(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StReviewViewModel model = new StReviewViewModel();
                CourseRepository courseRepository = new CourseRepository();
                model.review = courseRepository.getReviewById(id);


                ViewBag.Title = "Отзыв на курс | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/Home/Index");

            }
        }

        [HttpPost]
        public IActionResult Review(int id, string text, int courseId)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {                
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.updateReview(id, text);

                return Redirect("~/StMyCourse/Review/" + courseId);
            }
            else
            {
                return Redirect("~/Home/Index");

            }
        }

    }
}
