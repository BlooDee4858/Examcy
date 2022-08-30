using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StRatingCourseController : Controller
    {
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                //int idC = 1; // id курса

                //idS = 1;

                RatingCourseRepository rt = new RatingCourseRepository();


                RaitingCourseViewModel model = new RaitingCourseViewModel();

                var ratings = rt.AllRatingCourse(idS);

                model.courselist.AddRange(ratings);

                ViewBag.Title = "Рейтинг | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}