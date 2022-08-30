using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.Domain.Helpers;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StSetsController : Controller
    {
        public StSetsController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
               

                StSetsViewModel model = new StSetsViewModel();
                model.User = u.GetUserById(idU);

                ViewBag.UserName = model.User.Login;
                ViewBag.Title = "Личный кабинет обучаемого | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }


        [HttpPost]
        public IActionResult EditStudent(string MyId, string FirstName, string LastName, string Login, string Password)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                UserRepository userRepository = new UserRepository();

                User user = new User();
                user = userRepository.GetUserById(idU);
                user.Id = idU;
                user.Login = Login;
                bool flag = false;
                if (Password != null)
                {
                    if (Password.Length > 0)
                    {
                        user.Password = HashPasswordHelper.HashPassowrd(Password); ;
                        flag = true;
                    }
                }
                else
                    
                user.LastName = LastName;
                user.FirstName = FirstName;

                userRepository.UpdateUser(user, flag);
                return Redirect("~/StSets/Index");
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
