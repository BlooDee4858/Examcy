using Microsoft.AspNetCore.Mvc;
using Examcy.Data.Repository;
using Examcy.ViewModels.Teacher;

namespace Examcy.Controllers
{
    public class TStatController : Controller
    {
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                TStatViewModel model = new TStatViewModel();
                CourseRepository courseRepository = new CourseRepository();
                model.courses = courseRepository.getAllCourseForListForTeacher(idU);
                if (model.courses != null && model.courses.Count > 0)
                {
                    ViewBag.Title = "Статистика | Examcy";
                    return View(model);
                }
                return View();
            }
            return Redirect("~/Home/Index");
        }
    }
}
