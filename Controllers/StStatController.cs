using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StStatController : Controller
    {
        private readonly IUserService _userService;
        public StStatController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                StStatViewModel model = new StStatViewModel();
                GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                List<int> idsMyCourses = groupCourseRepository.getIdsCoursesForStudent(idS);
                model.count = idsMyCourses.Sum();
                if (model.count != 0)
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