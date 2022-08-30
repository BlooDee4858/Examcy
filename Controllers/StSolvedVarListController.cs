using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StSolvedVarListController : Controller
    {
        private readonly IUserService _userService;
        public StSolvedVarListController(IUserService userService)
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
                StSolvedVarListViewModel model = new StSolvedVarListViewModel();
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                model.vars = assignedVarsRepository.getSolvedVarForStudent(idS);

                ViewBag.Title = "Решенные варианты | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}
