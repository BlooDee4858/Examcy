using Examcy.Data;
using Examcy.Data.Interfaces;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StSolvedVarController : Controller
    {
        private readonly IUserService _userService;
        public StSolvedVarController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index(List<string> answer, int idVar, DateTime start)
        {
            // idVar это назначенный
            DateTime end = DateTime.Now;
            //idVar это id назначенного варианта
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StSolvedVarViewModel model = new StSolvedVarViewModel();
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                model.var = assignedVarsRepository.solvedVar(idVar, idS, answer, start, end);
                AssignedVars v = new AssignedVars();
                v = assignedVarsRepository.getVarById(idVar);

                model.varId = v.VarId;
                model.result = v.Result;
                
                ViewBag.Title = "Вариант №" + model.varId + " | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult ViewVar(int id)
        {
            //idVar это id назначенного варианта

            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StSolvedVarViewModel model = new StSolvedVarViewModel();
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                AssignedVars v = new AssignedVars();
                

                TaskRepository taskRepository = new TaskRepository();
                v = assignedVarsRepository.getVarById(id);
                model.result = v.Result;
                model.varId = v.VarId;
                model.var = assignedVarsRepository.getVarWithAnswers(id, idS);
                foreach (var task in model.var)
                {
                    task.Task = taskRepository.getFullTask(task.TaskId);
                    task.Task.pattern = task.Task.pattern.getTask(task.Task.Number);
                    if (task.Task.pattern != null)
                    {
                        task.Task.pattern.files = taskRepository.getFilesForTask(task.TaskId);
                        if (task.Task.pattern.files != null)
                        {
                            foreach (var f in task.Task.pattern.files)
                            {
                                if (f.Type == "img" || f.Type == "file")
                                    f.Path = f.Path.Substring(7);
                            }
                        }
                        if (task.Task.pattern._code)
                        {
                            foreach (var f in task.Task.pattern.files)
                            {
                                if (f.Type == "code1")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[0] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code2")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[1] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code3")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[2] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code4")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[3] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code5")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        task.Task.pattern.codeText[4] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                            }
                        }
                    }

                    task.AssignedVar = assignedVarsRepository.getVarById(task.AssignedVarId);
                }

                ViewBag.Title = "Вариант №" + model.varId + " | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }

    }
}
