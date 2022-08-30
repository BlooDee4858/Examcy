using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StVarController : Controller
    {
        //AppDBContent db;
        //public StVarController(AppDBContent context)
        //{
        //    db = context;
        //}

        public IActionResult Index(int id)
        {
            // id назначенного варианта 

            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StVarViewModel model = new StVarViewModel();
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                model.varId = assignedVarsRepository.getVarIdById(id);
                model.var = assignedVarsRepository.getVarByIdWithTasks(model.varId);

                model.Id = id;

                TaskRepository taskRepository = new TaskRepository();
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

                }
                model.start = DateTime.Now;
                ViewBag.Title = "Вариант №" + model.varId + " | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult AssignedVar(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                int idS = u.GetStudentIdByUserId(idU); // id студента

                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                int id2 = assignedVarsRepository.setVar(id, idS, DateTime.Now);// назначаем вариант
                return Redirect("~/StVar/Index/" + id2);
            }
            return Redirect("~/Home/Index");
        }
        public IActionResult Rating(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                int idS = u.GetStudentIdByUserId(idU); // id студента

                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                if(assignedVarsRepository.isSolved(id))
                {
                    return Redirect("~/StSolvedVar/ViewVar/" + id);
                }
                else
                {
                    return Redirect("~/StVar/Index/" + id);
                }
            }
            return Redirect("~/Home/Index");
        }
    }
}
