using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Examcy.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Web;

namespace Examcy.Controllers
{
    public class TTasksController : Controller
    {
        public IActionResult Index(int id)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName =  User.Identity.Name;

                // проверяем доступ к курсу
                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByTaskId(id, idU))
                {
                    // получаем задание
                    TTaskIndexViewModel model = new TTaskIndexViewModel();
                    TaskRepository taskRepository = new TaskRepository();
                    model.task = taskRepository.getFullTask(id);
                    model.task.pattern = model.task.pattern.getTask(model.task.Number);
                    if (model.task.pattern != null)
                    {
                        model.content = true;
                        model.task.pattern.files = taskRepository.getFilesForTask(id);
                        if(model.task.pattern.files != null)
                        {
                            foreach (var f in model.task.pattern.files)
                            {
                                if (f.Type == "img" || f.Type=="file")
                                    f.Path = f.Path.Substring(7);
                            }
                        }
                        if (model.task.pattern._code)
                        {
                            foreach(var f in model.task.pattern.files)
                            {
                                if(f.Type == "code1")
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
                    ViewBag.Title = "Задание №" + model.task.Id + " | Examcy";
                    return View(model);
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }


        //+ вставить в UpdateTask доп материалы
        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByTaskId(id, idU))
                {
                    TUpdateTaskViewModel model = new TUpdateTaskViewModel();
                    TaskRepository taskRepository = new TaskRepository();
                    model.task = taskRepository.getFullTask(id);
                    model.task.pattern = model.task.pattern.getTask(model.task.Number);
                    if (model.task.pattern != null)
                    {
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

                    ViewBag.Title = "Редактировать задание №" + model.task.Id + " | Examcy";
                    return View(model);
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }


        [HttpPost]
        public IActionResult UpdateTask(int themeId, int taskId, string textTask, string solutionTask, string answerTask, List<IFormFile> uploadedFile, List<IFormFile> uploadedImg, List<string> code)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();

                if (courseRepository.checkingAccessToTheCourseByThemeId(themeId, idU) && textTask != null && answerTask != null)
                {
                    TaskRepository taskRepository = new TaskRepository();
                    FullTask task = new FullTask();

                    DateTime dateTime = DateTime.Now;
                    string date = dateTime.Hour + "_" + dateTime.Minute + "_" + dateTime.Second + "_" + dateTime.Day + "_" + dateTime.Month + "_" + dateTime.Year;
                    task.Id = taskId;
                    task.ThemeId = themeId;
                    task.Answer = answerTask;
                    task.countAnswers(answerTask);
                    task.Text = textTask;
                    if (solutionTask == null)
                    {
                        task.Solution = "Решение для этого задания пока не добавлено.";
                    }
                    else
                        task.Solution = solutionTask;

                    taskRepository.updateTask(task);
                    string path = "wwwroot\\TeacherFiles\\" + u2.getLogin(idU) + "\\Tasks\\";

                    if (uploadedFile != null && uploadedFile.Count != 0)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        
                        taskRepository.delTaskContent(task.Id, "file"); // удаляем старые файлы
                        foreach (var f in uploadedFile)
                        {
                            string myPath = path + date + "_" + f.FileName;
                            using (var fileStream = new FileStream(myPath, FileMode.Create))
                            {
                                f.CopyTo(fileStream);
                            }
                            taskRepository.addTaskContent(task.Id, myPath, "file");
                        }
                    }

                    if (uploadedImg != null && uploadedImg.Count != 0)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        taskRepository.delTaskContent(task.Id, "img"); // удаляем старые файлы
                        foreach (var f in uploadedImg)
                        {
                            string myPath = path + date + "_" + f.FileName;
                            using (var fileStream = new FileStream(myPath, FileMode.Create))
                            {
                                f.CopyTo(fileStream);
                            }
                            taskRepository.addTaskContent(task.Id, myPath, "img");
                        }
                    }
                    if (code != null && code.Count != 0)
                    {
                        List<string> codePath = new List<string>();
                        codePath = taskRepository.getCodePath(task.Id);

                        if (codePath != null && codePath.Count == 5)
                        {
                            int i = 0;
                            foreach (var c in code)
                            {
                                string myPath = codePath[i];

                                using (FileStream fstream = new FileStream(myPath, FileMode.OpenOrCreate))
                                {
                                    // преобразуем строку в байты
                                    byte[] buffer = Encoding.Default.GetBytes(c);
                                    // запись массива байтов в файл
                                    fstream.Write(buffer, 0, buffer.Length);
                                    fstream.Close();
                                }
                                i++;
                            }
                        }
                    }
                    return Redirect("~/TTasks/Index/" + task.Id);
                }
                return Redirect("~/THome/Index");


            }
            return Redirect("~/Home/Index");
        }

        public IActionResult DeleteTask(int id)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByTaskId(id, idU))
                {
                    TaskRepository taskRepository = new TaskRepository();
                    if (!taskRepository.IsTaskInVar(id))
                    {
                        // проверяем принадлежит ли задание к его курсу
                        // проверяем назначено ли оно
                        // проверяем входит ли оно в вариант
                        if (taskRepository.deleteTask(id))
                            return Redirect("~/TTasks/TaskList");
                    }
                    return Redirect("~/THome/Index");
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult AssignTask(int id)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByTaskId(id, idU))
                {
                    TAssignTaskViewModel model = new TAssignTaskViewModel();
                    TaskRepository taskRepository = new TaskRepository();
                    model.task = id;
                    int courseId = courseRepository.getCourseByTaskID(id);
                    if (courseId != 0)
                    {
                        GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                        model.groups = groupCourseRepository.GetAllGroupForCourse(courseId);
                        foreach (var group in model.groups)
                        {
                            model.students.AddRange(groupCourseRepository.GetAllStudentForGroup(group.Id));
                        }

                        if (model.students == null)
                        {
                            StudentForAssign student = new StudentForAssign();
                            student.Id = 0;
                            student.FirstName = ".";
                            student.LastName = "На курс не записан ни один студент";
                            student.GroupId = 0;
                            model.students.Add(student);
                        }
                        ViewBag.Title = "Назначить задание | Examcy";
                        return View(model);
                    }
                    else
                        return Redirect("~/TTasks/TaskList");
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult AssignTask(int taskId, List<int> studentId, List<int> groupId, DateTime date)
        {
            // date дата окончания 

            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByTaskId(taskId, idU))
                {
                    List<int> idS = new List<int>();
                    if (groupId != null)
                    {
                        GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                        foreach (var group in groupId)
                        {
                            idS.AddRange(groupCourseRepository.GetAllStudentIdsForGroup(group));
                        }
                    }
                    if (studentId != null)
                    {
                        foreach (var student in studentId)
                        {
                            if (student != 0)
                            {
                                bool flag = false;
                                foreach (var i in idS)
                                {
                                    if (i == student)
                                        flag = true;
                                }
                                if (!flag)
                                    idS.Add(student);
                            }
                        }
                    }

                    if (date.Year < 2022)
                    {
                        date = DateTime.Now;
                    }

                    if (idS != null)
                    {
                        AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                        foreach (var i in idS)
                        {
                            assignedTasksRepository.assignTaskForStudent(i, taskId, date);
                        }
                    }
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
                return Redirect("~/TTasks/TaskList");
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult UnassignTask(int id)
        {
            // проверки входа

            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                AssignedTasksRepository assignedTasksRepository = new AssignedTasksRepository();
                AssignedTasks assignedTasks = assignedTasksRepository.getTaskById(id);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByTaskId(assignedTasks.TaskId, idU))
                {
                    assignedTasksRepository.delassignedTaskById(id);
                    return Redirect("~/TTasks/TaskList");
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult GenerateTask(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();
                if(courseRepository.checkingAccessToTheCourseByThemeId(id, idU))
                {
                    TGenerateTaskViewModel model = new TGenerateTaskViewModel();
                    Data.Models.Task task = new Data.Models.Task();
                    model.themeId = id;
                    ThemeRepository themeRepository = new ThemeRepository();
                    model.patterns = task.getPatterns(themeRepository.getNumByTheme(id));

                    ViewBag.Title = "Генерация заданий | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult GeneratedTask(int taskId, int count, int theme)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByThemeId(theme, idU))
                {
                    TaskRepository taskRepository = new TaskRepository();
                    string idS = "";
                    for (int i = 0; i < count; i++)
                    {
                        Data.Models.Task newTask = new Data.Models.Task();
                        newTask = newTask.generate(taskId);
                        newTask.ThemeId = theme;
                        newTask.Solution = "Решение для этого задания пока не добавлено";
                        idS += taskRepository.addTask(newTask).ToString() + " ";
                    }
                    return Redirect("~/TTasks/TaskList");
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult GeneratedTasks(string st)
        {
            if (st != null)
            {
                string[] str = st.Split(" ");
                List<int> idS = new List<int>();
                foreach (string str2 in str)
                {
                    if (str2 != " " && str2 != "")
                        idS.Add(Convert.ToInt32(str2));
                }


                SessionsRepository s = new SessionsRepository();
                int idU = s.GetActiveUserId(); // id пользователя
                if (idU != 0)
                {
                    UserRepository u2 = new UserRepository();
                    ViewBag.UserName = u2.getLogin(idU);
                    TGeneratedTaskViewModel model = new TGeneratedTaskViewModel();
                    TaskRepository taskRepository = new TaskRepository();
                    foreach (var id in idS)
                    {
                        model.tasks.Add(taskRepository.getTaskById(id));
                    }

                    CourseRepository courseRepository = new CourseRepository();
                    if (model.tasks != null)
                    {
                        if (model.tasks.Count > 1)
                        {
                            model.courseId = courseRepository.getCourseByTaskID(model.tasks[0].Id);
                            model.themeTitle = courseRepository.getThemeTitle(model.tasks[0].ThemeId);
                        }
                    }


                    ViewBag.Title = "Сгенерированные задания | Examcy";
                    return View(model);
                }
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult SolvedTask(int id)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                AnswersRepository answersRepository = new AnswersRepository();
                CourseRepository courseRepository = new CourseRepository();
                TSolvedTasksViewModel model = new TSolvedTasksViewModel();
                model.answer = answersRepository.getAnswerById(id); ;
                if (courseRepository.checkingAccessToTheCourseByTaskId(model.answer.TaskId, idU))
                {
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

                    if (model.answer.Answer == model.answer.Task.Answer)
                        model.isTrue = true;
                    else
                        model.isTrue = false;

                    ViewBag.Title = "Решенное задание | Examcy";
                    return View(model);
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult TaskList()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                TTaskListViewModel model = new TTaskListViewModel();

                CourseRepository courseRepository = new CourseRepository();
                model.courses = courseRepository.getAllCourseForListForTeacher(idU);

                TaskRepository taskRepository = new TaskRepository();
                if (model.courses != null && model.courses.Count > 0)
                {
                    foreach (var item in model.courses)
                    {
                        List<TaskForList> tasks = new List<TaskForList>();
                        tasks = taskRepository.getAllTaskForCourse(item.Id);
                        item.allCount = tasks.Count;
                        model.allTasks.AddRange(tasks);

                        List<AnswersForList> solvedTasks = new List<AnswersForList>();
                        solvedTasks = taskRepository.getAllSolvedTaskForCourse(item.Id);
                        item.solvedCount = solvedTasks.Count;
                        model.solvedTasks.AddRange(solvedTasks);

                        List<AssignedTaskForTeacherList> assignedVars = new List<AssignedTaskForTeacherList>();
                        assignedVars = taskRepository.getAssignedTasksForCourse(item.Id);
                        item.assignedCount = assignedVars.Count;
                        model.assignedTasks.AddRange(assignedVars);
                    }

                    ViewBag.Title = "Задания | Examcy";
                    return View(model);
                }
                return View();
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult CreateTask(int id)
        {
            // id темы
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByThemeId(id, idU))
                {
                    UserRepository userRepository = new UserRepository();
                    ViewBag.UserName = userRepository.getLogin(idU);

                    int number = courseRepository.getTaskNumber(id);
                    
                    TCreateTaskViewModel model = new TCreateTaskViewModel();
                    model.pattern = model.pattern.getTask(number);
                    model.themeId = id;
                    model.theme = courseRepository.getThemeTitle(id);

                    ViewBag.Title = "Добавление задания | Examcy";
                    return View(model);
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult CreateTask(int themeId, string textTask, string solutionTask, string answerTask, List<IFormFile> uploadedFile, List<IFormFile> uploadedImg, List<string> code)
        {
            // + проверка что препод имеет право добавлять задания на этот курс 
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                UserRepository u2 = new UserRepository();
                ViewBag.UserName = u2.getLogin(idU);
                CourseRepository courseRepository = new CourseRepository();

                if (courseRepository.checkingAccessToTheCourseByThemeId(themeId, idU) && textTask != null && answerTask != null)
                {
                    TaskRepository taskRepository = new TaskRepository();
                    FullTask task = new FullTask();

                    DateTime dateTime = DateTime.Now;
                    string date = dateTime.Hour + "_" + dateTime.Minute + "_" + dateTime.Second + "_" + dateTime.Day + "_" + dateTime.Month + "_" + dateTime.Year;
                    task.ThemeId = themeId;
                    task.Answer = answerTask;
                    task.countAnswers(answerTask);
                    task.Text = textTask;
                    if (solutionTask == null)
                    {
                        task.Solution = "Решение для этого задания пока не добавлено.";
                    }
                    else
                        task.Solution = solutionTask;

                    int taskId = taskRepository.addTask(task);
                    string path = "wwwroot\\TeacherFiles\\" + u2.getLogin(idU) + "\\Tasks\\";

                    if (uploadedFile != null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        foreach(var f in uploadedFile)
                        {
                            string myPath = path  + date + "_" + f.FileName;
                            using (var fileStream = new FileStream(myPath, FileMode.Create))
                            {
                                f.CopyTo(fileStream);
                            }
                            taskRepository.addTaskContent(taskId, myPath, "file");
                        }
                    }

                    if (uploadedImg != null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        foreach (var f in uploadedImg)
                        {
                            string myPath = path + date + "_" + f.FileName;
                            using (var fileStream = new FileStream(myPath, FileMode.Create))
                            {
                                f.CopyTo(fileStream);
                            }
                            taskRepository.addTaskContent(taskId, myPath, "img");
                        }
                    }
                    if(code!=null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        int i = 1;
                        foreach(var c in code)
                        {
                            string myPath = path + date + "_code" +i.ToString() +".txt";

                            using (FileStream fstream = new FileStream(myPath, FileMode.OpenOrCreate))
                            {
                                // преобразуем строку в байты
                                byte[] buffer = Encoding.Default.GetBytes(c);
                                // запись массива байтов в файл
                                fstream.Write(buffer, 0, buffer.Length);
                                fstream.Close();
                            }
                            taskRepository.addTaskContent(taskId, myPath, "code"+i);
                            i++;
                        }
                    }
                    return Redirect("~/TTasks/Index/" + taskId);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }
    }
}
