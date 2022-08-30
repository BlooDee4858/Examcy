using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StTasksController : Controller
    {
        public StTasksController()
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

                StTasksViewModel model = new StTasksViewModel();
                //if (idSet == 0) // 1 задание
                //{
                TaskRepository taskRepository = new TaskRepository();
                ThemeRepository themeRepository = new ThemeRepository();
                if (id < 0)
                {
                    id = id * (-1);
                    AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                    AssignedTasks assignedTasks = new AssignedTasks();

                    model.task = taskRepository.getFullTask(assignedTasksRepository.getTaskById(id).TaskId);
                    model.task.pattern = model.task.pattern.getTask(model.task.Number);
                    if (model.task.pattern != null)
                    {
                        model.content = true;
                        model.task.pattern.files = taskRepository.getFilesForTask(id);
                        if (model.task.pattern.files != null)
                        {
                            foreach (var f in model.task.pattern.files)
                            {
                                if (f.Type == "img" || f.Type == "file")
                                    f.Path = f.Path.Substring(7);
                            }
                        }
                        if (model.task.pattern._code)
                        {
                            foreach (var f in model.task.pattern.files)
                            {
                                if (f.Type == "code1")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[0] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code2")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[1] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code3")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[2] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code4")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[3] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code5")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[4] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                            }
                        }
                    }
                    model.isAssigned = id;
                }
                else
                {
                    model.task = taskRepository.getFullTask(id);
                    model.task.pattern = model.task.pattern.getTask(model.task.Number);
                    if (model.task.pattern != null)
                    {
                        model.content = true;
                        model.task.pattern.files = taskRepository.getFilesForTask(id);
                        if (model.task.pattern.files != null)
                        {
                            foreach (var f in model.task.pattern.files)
                            {
                                if (f.Type == "img" || f.Type == "file")
                                    f.Path = f.Path.Substring(7);
                            }
                        }
                        if (model.task.pattern._code)
                        {
                            foreach (var f in model.task.pattern.files)
                            {
                                if (f.Type == "code1")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[0] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code2")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[1] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code3")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[2] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code4")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[3] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                                if (f.Type == "code5")
                                {
                                    using (StreamReader reader = new StreamReader(f.Path))
                                    {
                                        model.task.pattern.codeText[4] = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                }
                            }
                        }
                    }
                    model.isAssigned = 0;
                }

                ViewBag.Title = "Задание №" + model.task.Id + " | Examcy";
                model.startTime = DateTime.Now;
                return View(model);
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult AssignedIndex(int id)
        {
            // id назначенной задачи

            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                AssignedTasks assignedTasks = new AssignedTasks();
                assignedTasks = assignedTasksRepository.getTaskById(id);
                if (idS == assignedTasks.StudentId)
                {
                    id = id * (-1);
                    return Redirect("~/StTasks/Index/" + id);
                }
                else
                {
                    return Redirect("~/StHome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult SolveTask(int TaskID, int AssignedTaskID, string Answer, DateTime StartTime)
        {
            DateTime date = DateTime.Now;

            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                int idS = u.GetStudentIdByUserId(idU); // id студента
                                                        // 
                var d = date.Subtract(StartTime);
                float time = d.Seconds + d.Minutes * 60 + d.Hours * 60 * 60;

                AnswersRepository answersRepository = new AnswersRepository();
                int ansId = answersRepository.addAnswer(idS, Answer, TaskID, date, time);

                answersRepository.addExp(TaskID, Answer, idS);

                if (AssignedTaskID != 0)
                {
                    AssignedTasksRepository assignedTasks = new AssignedTasksRepository();
                    assignedTasks.delassignedTaskById(AssignedTaskID);
                }

                return Redirect("~/StSolvedTasks/Index/" + ansId);
            }
            return Redirect("~/Home/Index");
        }

    }
}
