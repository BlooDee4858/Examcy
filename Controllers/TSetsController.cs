using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;
using Examcy.Domain.Helpers;

namespace Examcy.Controllers
{
    public class TSetsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {

                TSetsViewModel model = new TSetsViewModel();
                model = u.GetInfoForSets(idU);

                ViewBag.UserName = model.Login;
                ViewBag.Title = "Личный кабинет преподавателя | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }



        [HttpPost]
        public IActionResult EditTeacher(string FirstName, string LastName, string Login, string Password)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {

                User user = new User();
                user = u.GetUserById(idU);
                user.Id = idU;
                user.Login = Login;
                bool flag = false;
                if (Password != null)
                {
                    if (Password.Length > 0)
                    {
                        user.Password = HashPasswordHelper.HashPassowrd(Password);
                        flag = true;
                    }
                }
                user.LastName = LastName;
                user.FirstName = FirstName;

                u.UpdateUser(user, flag);
                return Redirect("~/TSets/Index");
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult Exit()
        {
            // удаляем запись из таблицы Session

            return Redirect("~/Home/Index");
        }
    }
}
