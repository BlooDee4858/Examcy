using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StTaskController : Controller
    {
        // type_graph = 0 --- undirected graph
        public string GenerateGraph(int type_graph)
        {
            var rand = new Random();
            string upper_diagonal = "";

            int number_vertex = rand.Next(4,7);
            upper_diagonal += number_vertex;

            if (type_graph == 0)
            {
                for (int i = 0; i < number_vertex; i++)
                {
                    for (int j = i; j < number_vertex; j++)
                    {
                        if (i != j)
                        {
                            upper_diagonal += rand.Next(2).ToString();
                            
                        }
                        else
                        {
                            upper_diagonal += 0;
                        }
                    }
                }
            }else if(type_graph == 1)
            {
                for (int i = 0; i < number_vertex; i++)
                {
                    for (int j = i; j < number_vertex; j++)
                    {
                        if (i != j)
                        {
                            upper_diagonal += rand.Next(20).ToString();
                        }
                        else
                        {
                            upper_diagonal += 0;
                        }
                    }
                }
            }

            return upper_diagonal;
        }
        public string GenerateStringNumbersAddMachine(int type)
        {
            var rand = new Random();
            string tmp = "";
            int tmp_number = 0;

            if (type == 1)
            {    
                for (int i = 0; i < 2; i++)
                {
                    int type_symbol = rand.Next(2);
                    int number = rand.Next(1, 10);
                    if (i == 0) { tmp_number = number; }
                    if (i == 1 && tmp_number % 2 == 0 && number % 2 == 0) { number++; }

                    switch (type_symbol)
                    {
                        case 0:
                            if (i == 0)
                            {
                                tmp += "+" + number.ToString() + " ";
                            }
                            else
                            {
                                tmp += "-" + number.ToString() + " ";
                            }
                            break;
                        case 1:
                            if (i == 1)
                            {
                                tmp += "-" + number.ToString() + " ";
                            }
                            else
                            {
                                tmp += "+" + number.ToString() + " ";
                            }
                            break;
                    }
                }
                string start_number = (rand.Next() % 100).ToString();
                string finish_number = (rand.Next() % 100).ToString();

                tmp += start_number + " " + finish_number;
            }
            else if (type == 0)
            {
                int number;
                int tmp_mod = 1;
                for (int i = 0; i < 2; i++)
                {
                    number = rand.Next(1, 5);
                    if (i == 0) { tmp_number = number; }
                    if (i == 1 && tmp_number % 2 == 0 && number % 2 == 0) { tmp_mod = 0; }
                    else if (i == 1 && tmp_number % 2 != 0 || number % 2 != 0) { tmp_mod = 1; }

                    if (i == 0)
                    {
                        tmp += "+" + number.ToString() + " ";
                    }
                    else if (i == 1)
                    {
                        tmp += "*" + number.ToString() + " ";
                    }
                }
                int _start_number = (rand.Next(1, 10));
                int _finish_number = (rand.Next(11, 100));

                if(_start_number%2 != 0 && _finish_number % 2 == 0 && tmp_mod == 0) { _start_number++; }
                else if (_start_number % 2 == 0 && _finish_number % 2 != 0 && tmp_mod == 0) { _finish_number++; }

                string start_number = _start_number.ToString();
                string finish_number = _finish_number.ToString();

                tmp += start_number + " " + finish_number;
            }
            return tmp;
        }

        
        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StTaskViewModel model = new StTaskViewModel();
                //if (idSet == 0) // 1 задание
                //{
                TaskRepository taskRepository = new TaskRepository();
                ThemeRepository themeRepository = new ThemeRepository();

                AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                AssignedTasks assignedTasks = new AssignedTasks();

                model.task = taskRepository.getTaskById(id);
                model.task.Theme = themeRepository.getThemeById(model.task.ThemeId);
                model.isOneTask = true;

                /*
                 "ГЕНЕРАЦИЯ"
                 */
                //var rand = new Random();
                if (/*rand.Next(2) == 0 */  model.task.Id == 35)
                {
                    model.task_detail = GenerateStringNumbersAddMachine(1);
                    //Временно
                    model.task.Title = "Арифмометр с движением в обе стороны.";
                    model.task.Text = "Поочередно нажимая на кнопки, подобрери такую последовательность команд, чтобы из начального числа получилось итоговое число.";
                    model.task_type = "DAddMachin";

                    string[] Notif = new string[] { "/img/FirstSteps.svg", "First steps!" };
                    model.JavascriptToRun = Notif;
                }
                if (/*rand.Next(2) == 0 */  model.task.Id == 34)
                {
                    model.task_detail = GenerateStringNumbersAddMachine(0);
                    //Временно
                    model.task.Title = "Арифмометры";
                    model.task.Text = "Поочередно нажимая на кнопки, подобрери такую последовательность команд, чтобы из начального числа получилось итоговое число.";
                    model.task_type = "AddMachin";
                }
                else if (model.task.Id == 32/*model.task.Theme.Title == "UGraph"*/)
                {
                    model.task_detail = GenerateGraph(0);
                    //Временно
                    model.task.Title = "Неоднозначное соотнесение таблицы и построение графа.";
                    model.task.Text = "В таблице содержатся сведения о дорогах между населенными пунктами, постройте граф.";
                    model.task_type = "UGraph";
                }
                else if (model.task.Id == 33/*model.task.Theme.Title == "UGraph"*/)
                {
                    model.task_detail = GenerateGraph(1);
                    //Временно
                    model.task.Title = "Oднозначное соотнесение таблицы и построение графа.";
                    model.task.Text = "В таблице содержатся сведения о дорогах между населенными пунктами, постройте граф.";
                    model.task_type = "Graph";
                }

                ViewBag.Title = "Задание №" + model.task.Id + " | Examcy";


                /*  ???Достижения??? */
                /*
                string[] Notif = new string[] { "/img/FirstSteps.svg", "First steps!" };
                model.JavascriptToRun = Notif;
                */

                return View(model);
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }

        public IActionResult AssignedIndex(int id)
        {
            // id назначенной задачи

            SessionsRepository s = new SessionsRepository();
            int idU = s.GetActiveUserId(); // id пользователя
            if (idU != 0)
            {
                return Redirect("~/StTask/Index/" + id);
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult Index(StTaskViewModel _res_model)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            int idS = u.GetStudentIdByUserId(idU); // id студента

            if (idU == 0 || idU == null)/*ПРОВЕРКА ЛОГИНА*/
            {
                return Redirect("~/Home/Index");
            }

            Answers model = new Answers();

            DateTime date = DateTime.Now;
            var d = date.Subtract(_res_model.startTime);
            float time = d.Seconds + d.Minutes * 60 + d.Hours * 60 * 60;


            /*  Проверка результата */
            TaskRepository taskRepository = new TaskRepository();
                model.Task = taskRepository.getTaskById(_res_model.task.Id);
                
            if(model.Task.Answer == _res_model.Answer)
            {
                    AnswersRepository answersRepository = new AnswersRepository();
                    //int ansId = answersRepository.addAnswer(idU, _res_model.Answer, _res_model.task.Id, date, time);
                    
                    

                    return Redirect("~/StMyTasks/Index");
            }


            
            return Redirect("~/StHome/Index");
        }


        public IActionResult Exit()
        {
            // удаляем запись из таблицы Session

            return Redirect("~/Home/Index");
        }
    }
}
