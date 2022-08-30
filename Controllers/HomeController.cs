using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Examcy.Controllers
{
    public class HomeController : Controller
    {
        //AppDBContent db;
        //public HomeController(AppDBContent context)
        //{
        //    db = context;
        //}
        public IActionResult Index()
        {
            CourseRepository courseRepository = new CourseRepository();
            InitViewModel model = courseRepository.getInitInfo();

            ViewBag.Title = "Examcy";
            return View(model);
        }

        public IActionResult Login()
        {
            ViewBag.Title = "Регистрация";
            return View();
        }
    }
}
