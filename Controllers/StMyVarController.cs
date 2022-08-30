using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StMyVarController : Controller
    {
        private readonly IUserService _userService;

        public StMyVarController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StMyVarsViewModel model = new StMyVarsViewModel();
                // получаем список назначенных вариантов
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                model.vars = assignedVarsRepository.getUnsolvedVars(idS);

                ViewBag.Title = "Мои варианты | Examcy";
                return View(model);
            }
            else
                return Redirect("~/Home/Index");
        }
    }
}
