using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StAchievsController : Controller
    {
        public StAchievsController()
        {
        }

        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                StAchievsViewModel model = new StAchievsViewModel();

                try
                {
                    int idS = u.GetStudentIdByUserId(idU); // id студента


                    StudentAchievementsRepository studentAchievementsRepository = new StudentAchievementsRepository();
                    model.achievements = studentAchievementsRepository.getMyAchives(idS);
                    foreach (var a in studentAchievementsRepository.AllAchivs())
                    {
                        bool flag = false;
                        foreach (var my in model.achievements)
                        {
                            if (my.Id == a.Id)
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                            model.all.Add(a);
                    }
                }
                catch(Exception ex)
                {
                    model.achievements = null;
                }
                ViewBag.Title = "Достижения | Examcy";
                return View(model);
            }
            else
                return Redirect("~/Home/Index");
        }
    }
}
