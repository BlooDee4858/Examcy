using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StSolvedTaskListController : Controller
    {
        public StSolvedTaskListController()
        {
        }

        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StSolvedTaskListViewModel model = new StSolvedTaskListViewModel();
                // список решенных заданий
                AnswersRepository answersRepository = new AnswersRepository();
                model.answers = answersRepository.getAllAnswersForStudent(idS);


                ViewBag.Title = "Список решенных заданий | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}
