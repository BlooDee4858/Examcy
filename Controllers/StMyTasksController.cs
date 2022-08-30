using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StMyTasksController : Controller
    {
        private readonly IUserService _userService;
        public StMyTasksController(IUserService userService)
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

                StMyTasksViewModel model = new StMyTasksViewModel();
                // получаем назначенные задания 
                AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                model.myTasks = assignedTasksRepository.getTasksForStudent(idS);

                // выбираем рекомендованные задания
                // рекомендованные выбираются из всех заданий из пройденных тем (т.е. из тем в которых решено хотя бы одно задание)

                // получаем id тем которые студент решал
                TaskRepository taskRepository = new TaskRepository();
                try
                {
                    model.otherTask1 = taskRepository.getSolvedTask(idS);
                    model.otherTask2 = taskRepository.getUnSolvedTask(idS);
                }
                catch (Exception ex)
                {
                    model.otherTask1 = null;
                    model.otherTask2 = null;
                }
                ViewBag.Title = "Мои задания | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }
    }
}
