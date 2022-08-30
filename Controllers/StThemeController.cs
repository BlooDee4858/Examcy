using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StThemeController : Controller
    {
        public StThemeController()
        {
        }


        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                StThemeViewModel model = new StThemeViewModel();

                ThemeRepository themeRepository = new ThemeRepository();
                TaskRepository taskRepository = new TaskRepository();
                model.theme = themeRepository.getThemeById(id);
                if (model.theme != null)
                {
                    if (!model.theme.Path.Equals("Нет файлов") && !model.theme.Path.Equals("Дополнительные файлы не добавлены"))
                    {
                        model.theme.Path = model.theme.Path.Substring(7);
                    }
                    else
                    {
                        model.flag = false;
                    }
                }

                foreach (var t in taskRepository.getAllTaskForTheme(model.theme.Id))
                {
                    model.tasks.Add(t);
                }

                ViewBag.Title = model.theme.Title + " | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}
