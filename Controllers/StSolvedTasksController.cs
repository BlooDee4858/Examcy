using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StSolvedTasksController : Controller
    {
        public StSolvedTasksController()
        {
        }

        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента
                StSolvedTasksViewModel model = new StSolvedTasksViewModel();
                AnswersRepository answersRepository = new AnswersRepository();

                model.answer = answersRepository.getAnswerById(id); ;
                TaskRepository taskRepository = new TaskRepository();
                model.answer.Task = taskRepository.getFullTask(model.answer.TaskId);
                model.answer.Task.pattern = model.answer.Task.pattern.getTask(model.answer.Task.Number);
                if (model.answer.Task.pattern != null)
                {
                    model.content = true;
                    model.answer.Task.pattern.files = taskRepository.getFilesForTask(id);
                    if (model.answer.Task.pattern.files != null)
                    {
                        foreach (var f in model.answer.Task.pattern.files)
                        {
                            if (f.Type == "img" || f.Type == "file")
                                f.Path = f.Path.Substring(7);
                        }
                    }
                    if (model.answer.Task.pattern._code)
                    {
                        foreach (var f in model.answer.Task.pattern.files)
                        {
                            if (f.Type == "code1")
                            {
                                using (StreamReader reader = new StreamReader(f.Path))
                                {
                                    model.answer.Task.pattern.codeText[0] = reader.ReadToEnd();
                                    reader.Close();
                                }
                            }
                            if (f.Type == "code2")
                            {
                                using (StreamReader reader = new StreamReader(f.Path))
                                {
                                    model.answer.Task.pattern.codeText[1] = reader.ReadToEnd();
                                    reader.Close();
                                }
                            }
                            if (f.Type == "code3")
                            {
                                using (StreamReader reader = new StreamReader(f.Path))
                                {
                                    model.answer.Task.pattern.codeText[2] = reader.ReadToEnd();
                                    reader.Close();
                                }
                            }
                            if (f.Type == "code4")
                            {
                                using (StreamReader reader = new StreamReader(f.Path))
                                {
                                    model.answer.Task.pattern.codeText[3] = reader.ReadToEnd();
                                    reader.Close();
                                }
                            }
                            if (f.Type == "code5")
                            {
                                using (StreamReader reader = new StreamReader(f.Path))
                                {
                                    model.answer.Task.pattern.codeText[4] = reader.ReadToEnd();
                                    reader.Close();
                                }
                            }
                        }
                    }
                }
                model.answer.Answer = model.answer.Answer.Trim();
                if (model.answer.Answer == model.answer.Task.Answer)
                    model.isTrue = true;
                else
                    model.isTrue = false;


                ViewBag.Title = "Результат | Examcy";
                return View(model);
            }
            return Redirect("~/Home/Index");
        }
    }
}
