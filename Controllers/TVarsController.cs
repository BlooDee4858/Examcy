using Microsoft.AspNetCore.Mvc;
using Examcy.ViewModels.Teacher;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using System.Text;

namespace Examcy.Controllers
{
    public class TVarsController : Controller
    {
        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByVarId(id, idU))
                {
                    TVarIndexViewModel model = new TVarIndexViewModel();
                    model.varId = id;
                    TaskRepository taskRepository = new TaskRepository();
                    model.tasks = taskRepository.getAllTaskInVar(id);

                    foreach (var task in model.tasks)
                    {
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

                    ViewBag.Title = "Вариант №" + model.varId + " | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult VarList() // list of var for all corse
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                TVarListViewModel model = new TVarListViewModel();

                CourseRepository courseRepository = new CourseRepository();
                model.courses = courseRepository.getAllCourseForListForTeacher(idU);

                VarRepository varRepository = new VarRepository();
                if (model.courses != null && model.courses.Count > 0)
                {
                    foreach (var item in model.courses)
                    {
                        List<Var> vars = new List<Var>();
                        vars = varRepository.getAllVarsForCourse(item.Id);
                        item.allCount = vars.Count;
                        model.allVars.AddRange(vars);

                        List<SolvedVarForTeacherList> solvedVars = new List<SolvedVarForTeacherList>();
                        solvedVars = varRepository.getAllSolvedVarForCourse(item.Id);
                        item.solvedCount = solvedVars.Count;
                        model.solvedVars.AddRange(solvedVars);

                        List<AssignedVarForTeacherList> assignedVars = new List<AssignedVarForTeacherList>();
                        assignedVars = varRepository.getAllAssignedVarForCourse(item.Id);
                        item.assignedCount = assignedVars.Count;
                        model.assignedVars.AddRange(assignedVars);
                    }

                    ViewBag.Title = "Варианты | Examcy";
                    return View(model);
                }
                return View();
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult ChangeTask(int id) // list of all task for this num in EGE
        {
            // id - TaskInVar id
            // получаем список всех заданий для этого номера в егэ
            // выводим
            // переходим к ChangedTask

            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                // проверка доступа к курсу 
                CourseRepository courseRepository = new CourseRepository();

                int courseId = courseRepository.getCourseByTaskInVarID(id);
                if (courseRepository.checkingAccessToTheCourseByCourseId(courseId, idU))
                {
                    TChangeTaskViewModel model = new TChangeTaskViewModel();
                    TaskRepository taskRepository = new TaskRepository();
                    model.idVar = taskRepository.getIdVarByTaskInVarId(id);
                    int num = taskRepository.getNumByTaskInVarId(id);

                    List<int> idS = new List<int>();
                    idS = taskRepository.getAllTaskIdSForNumAndCourse(num, courseId);

                    model.oldTaskId = id;
                    if (idS != null)
                    {
                        foreach (int taskId in idS)
                        {
                            FullTask task = new FullTask();
                            task = taskRepository.getFullTask(taskId);
                            task.pattern = task.pattern.getTask(num);
                            if (task.pattern != null)
                            {
                                task.pattern.files = taskRepository.getFilesForTask(taskId);
                                if (task.pattern.files != null)
                                {
                                    foreach (var f in task.pattern.files)
                                    {
                                        if (f.Type == "img" || f.Type == "file")
                                            f.Path = f.Path.Substring(7);
                                    }
                                }
                                if (task.pattern._code)
                                {
                                    foreach (var f in task.pattern.files)
                                    {
                                        if (f.Type == "code1")
                                        {
                                            using (StreamReader reader = new StreamReader(f.Path))
                                            {
                                                task.pattern.codeText[0] = reader.ReadToEnd();
                                                reader.Close();
                                            }
                                        }
                                        if (f.Type == "code2")
                                        {
                                            using (StreamReader reader = new StreamReader(f.Path))
                                            {
                                                task.pattern.codeText[1] = reader.ReadToEnd();
                                                reader.Close();
                                            }
                                        }
                                        if (f.Type == "code3")
                                        {
                                            using (StreamReader reader = new StreamReader(f.Path))
                                            {
                                                task.pattern.codeText[2] = reader.ReadToEnd();
                                                reader.Close();
                                            }
                                        }
                                        if (f.Type == "code4")
                                        {
                                            using (StreamReader reader = new StreamReader(f.Path))
                                            {
                                                task.pattern.codeText[3] = reader.ReadToEnd();
                                                reader.Close();
                                            }
                                        }
                                        if (f.Type == "code5")
                                        {
                                            using (StreamReader reader = new StreamReader(f.Path))
                                            {
                                                task.pattern.codeText[4] = reader.ReadToEnd();
                                                reader.Close();
                                            }
                                        }
                                    }
                                }
                            }
                            model.tasks.Add(task);
                        }
                    }

                    ViewBag.Title = "Список заданий | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }
       
        [HttpPost]
        public IActionResult ChangedTask(int taskId, int varId, int oldTaskID) // сохранение измененного задания
        {
            // oldTaskID это id из TaskInVar
            // меняем задание в варианте
            // выводим UpdateVar
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                CourseRepository courseRepository = new CourseRepository();
                int courseId = courseRepository.getCourseByTaskInVarID(oldTaskID);
                if (courseRepository.checkingAccessToTheCourseByCourseId(courseId, idU))
                {
                    TaskRepository taskRepository = new TaskRepository();
                    taskRepository.updateTaskFromVar(oldTaskID, taskId);


                    return Redirect("~/TVars/UpdateVar/" + varId);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult UpdateVar(int id) // изменить вариант
        {
            // получаем все задания варианта
            // выводим  
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                CourseRepository courseRepository = new CourseRepository();
                if(courseRepository.checkingAccessToTheCourseByVarId(id, idU))
                {
                    TUpdateVarViewModel model = new TUpdateVarViewModel();
                    model.VarId = id;
                    TaskRepository taskRepository = new TaskRepository();
                    model.tasks = taskRepository.getAllTaskInVar(id);

                    foreach(var task in model.tasks)
                    {
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

                    ViewBag.Title = "Редактирование варианта | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }


        [HttpPost]
        public IActionResult AssignedVar(int varId, List<int> groupId, List<int> studentId, DateTime date) // назначить вариант
        {
            // получаем список групп курса
            // получаем список студентов для курса
            // выводим AssignedVar
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByVarId(varId, idU))
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
                        AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                        foreach (var i in idS)
                        {
                            assignedVarsRepository.setVar(varId, i, date);
                        }
                    }
                    return Redirect("~/TVars/VarList");
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult UnassignVar(int id)
        {
            // сначала проверка, вошел ли пользователь, если нет то
            //return Redirect("~/Home/Index");
            // иначе получаем id пользователя
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                int varId = assignedVarsRepository.getVarIdById(id);

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByVarId(varId, idU))
                {
                    assignedVarsRepository.delassignedVarById(id);
                    return Redirect("~/TVars/VarList");
                }
                else
                {
                    return Redirect("~/THome/Index");
                }
            }
            return Redirect("~/Home/Index");
        }



        public IActionResult AssignVar(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByVarId(id, idU))
                {
                    TAssignVarViewModel model = new TAssignVarViewModel();
                    model.varId = id;
                    VarRepository varRepository = new VarRepository();
                    int courseId = varRepository.getVarByTd(id).CourseId;
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
                        ViewBag.Title = "Назначить вариант | Examcy";
                        return View(model);
                    }
                    else
                        return Redirect("~/THome/Index");
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult SolvedVar(int id)
        {
            // проверяем относитя ли вариант к курсу данного препода
            // удаляем вариант + все ссылки на решение
            // выводим страницу с вариантами
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                AssignedVarsRepository assignedVarsRepository = new AssignedVarsRepository();
                int idVar = assignedVarsRepository.getVarIdById(id);

                CourseRepository courseRepository = new CourseRepository();
                if(courseRepository.checkingAccessToTheCourseByVarId(idVar, idU))
                {
                    TSolvedVarViewModel model = new TSolvedVarViewModel();
                    model.VarID = idVar;
                    TaskRepository taskRepository = new TaskRepository();

                    model.tasks = assignedVarsRepository.getVarWithAnswers(id, assignedVarsRepository.getStudentId(id));
                    foreach (var task in model.tasks)
                    {
                        task.Task = taskRepository.getFullTask(task.TaskId);
                        task.numberTaskInVar = task.Task.Number;
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

                    ViewBag.Title = "Решенный вариант №" + model.VarID + " | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult DeleteVar(int id)
        {
            // проверяем относитя ли вариант к курсу данного препода
            // удаляем вариант + все ссылки на решение
            // выводим страницу с вариантами
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;
                CourseRepository courseRepository = new CourseRepository();
                if(courseRepository.checkingAccessToTheCourseByVarId(id,idU))
                {
                    VarRepository varRepository = new VarRepository();
                    varRepository.deleteVar(id);

                    return Redirect("~/TVars/VarList");
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult GenerateVar(int id)
        {
            // проверяем относитя ли вариант к курсу данного препода
            // удаляем вариант + все ссылки на решение
            // выводим страницу с вариантами
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;


                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByCourseId(id, idU))
                {
                    TGenerateVarViewModel model = new TGenerateVarViewModel();

                    model.themes = courseRepository.getAllThemesForCourse(id);

                    for(int i = 1; i<= model.taskCount; i++)
                    {
                        if(!courseRepository.checkingExistingTask(i, id))
                        {
                            model.taskFlag = false;
                            model.theme += i.ToString() + ", ";
                        }
                        
                        if (model.themes.Find(p => p.Num == i) == null)
                        {
                            model.themeFlag = false;
                            ThemeGenerate theme = new ThemeGenerate();
                            theme.ThemeId = 0;
                            theme.Num = i;
                            theme.Title = "Нет тем для этого задания";
                            model.themes.Add(theme);
                        }
                    }
                    if (model.theme.Length > 2)
                    {
                        model.theme = model.theme.Substring(0, model.theme.Length - 2);
                    }
                    model.courseId = id;
                    
                    ViewBag.Title = "Конструктор вариантов | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult GeneratedVar(List<int> idTheme, int count)
        {
            // проверяем относитя ли вариант к курсу данного препода
            // удаляем вариант + все ссылки на решение
            // выводим страницу с вариантами
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0 && count != 0 && idTheme != null)
            {
                ViewBag.UserName = User.Identity.Name;
                TGeneratedVarViewModel model = new TGeneratedVarViewModel();
                ThemeRepository themeRepository = new ThemeRepository();

                if (idTheme.Count > 0)
                {
                    VarRepository varRepository = new VarRepository();
                    TaskRepository taskRepository = new TaskRepository();

                    List<GenerateTask> tasks = new List<GenerateTask>();
                    foreach (int i in idTheme)
                    {
                        tasks.AddRange(taskRepository.getAllTaskForGenerate(i));
                    }

                    int courseId = themeRepository.getCourseByThemeId(idTheme[0]);
                    Random random = new Random();
                    for (int i = 0; i < count; i++)
                    {
                        Var v = new Var();
                        v.Id = varRepository.addVar(courseId);
                        v.CourseId = courseId;
                        model.vars.Add(v);

                        for (int j = 1; j < 28; j++)
                        {
                            List<GenerateTask> generateTasks = new List<GenerateTask>();
                            generateTasks.AddRange(tasks.FindAll(p => p.Num == j));
                            if (generateTasks.Count != 0)
                            {
                                TaskInVar task = new TaskInVar();
                                int some = 0;
                                if (generateTasks.Count > 1)
                                    some = random.Next(0, generateTasks.Count - 1);

                                task.Id = varRepository.addTaskInVar(v.Id, generateTasks[some]);


                                task.Task = taskRepository.getFullTask(task.TaskId);
                                task.VarId = v.Id;
                                task.numberTaskInVar = j;
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

                                model.tasks.Add(task);
                            }
                        }
                    }

                    //ViewBag.Title = "Сгенерированные варианты | Examcy";
                    //return View(model);
                    return Redirect("~/TVars/VarList");
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }



        [HttpGet]
        public IActionResult CreateVar(int id)
        {
            // id course
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByCourseId(id, idU))
                {
                    TCreateVarViewModel model = new TCreateVarViewModel();
                    model.courseId = id;

                    for (int i = 1; i <= model.taskCount; i++)
                    {
                        TaskForVarCreating task = new TaskForVarCreating();
                        task.num = i;
                        task.pattern = task.pattern.getTask(i);
                        model.tasks.Add(task);
                    }

                    ViewBag.Title = "Добавление варианта | Examcy";
                    return View(model);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }


        [HttpPost]
        public IActionResult CreateVar(int courseId, List<string> themeTask, List<string> textTask, List<string> solutionTask, List<string> answerTask, List<string> code_6, List<string> code_16, List<string> code_22, List<IFormFile> uploadedImg,List<IFormFile> uploadedFile)
        {
            // id course
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                CourseRepository courseRepository = new CourseRepository();
                if (courseRepository.checkingAccessToTheCourseByCourseId(courseId, idU))
                {
                    ThemeRepository themeRepository = new ThemeRepository();
                    TaskRepository taskRepository = new TaskRepository();
                    VarRepository varRepository = new VarRepository();
                    int i = 1;

                    int varId = varRepository.addVar(courseId);
                    List<int> idSTask = new List<int>();

                    foreach(var theme in themeTask)
                    {
                        int themeId = themeRepository.addTheme(courseId, theme, i, themeRepository.getTypeByNum(i));

                        FullTask task = new FullTask();
                        task.Text = textTask[i - 1];
                        if (solutionTask[i - 1] == null)
                            task.Solution = "Решение для этого задания пока не добавлено.";
                        else
                            task.Solution = solutionTask[i - 1];
                        task.Answer = answerTask[i - 1];
                        task.ThemeId = themeId;

                        int taskId = taskRepository.addTask(task);
                        varRepository.addTaskInVar(varId, taskId, i);
                        idSTask.Add(taskId);
                        i++;
                    }

                    DateTime dateTime = DateTime.Now;
                    UserRepository u2 = new UserRepository();
                    string date = dateTime.Hour + "_" + dateTime.Minute + "_" + dateTime.Second + "_" + dateTime.Day + "_" + dateTime.Month + "_" + dateTime.Year;
                    string path = "wwwroot\\TeacherFiles\\" + u2.getLogin(idU) + "\\Tasks\\";


                    if (uploadedFile != null && uploadedFile.Count == 8)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        int j = 3;
                        foreach (var f in uploadedFile)
                        {
                            // 3  9 10 17 24 26 27 27
                            string myPath = path + date + "_" + f.FileName;
                            using (var fileStream = new FileStream(myPath, FileMode.Create))
                            {
                                f.CopyTo(fileStream);
                            }
                            taskRepository.addTaskContent(idSTask[j - 1], myPath, "file");

                            if (j == 3)
                                j = 9;
                            if (j == 9)
                                j = 10;
                            if (j == 10)
                                j = 17;
                            if (j == 17)
                                j = 24;
                            if (j == 24)
                                j = 26;
                            if (j == 26)
                                j = 27;
                        }
                    }

                    if (uploadedImg != null && uploadedImg.Count == 8)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        int j = 3;

                        // 1 1 2 2 3 12 13 17
                        bool flag = false;
                        foreach (var f in uploadedImg)
                        {
                            string myPath = path + date + "_" + f.FileName;
                            using (var fileStream = new FileStream(myPath, FileMode.Create))
                            {
                                f.CopyTo(fileStream);
                            }

                            taskRepository.addTaskContent(idSTask[j - 1], myPath, "img");

                            if (j == 1 && !flag)
                            {
                                flag = true;
                            }
                            if (j == 1 && flag)
                            {
                                j = 2;
                                flag = false;
                            }

                            if (j == 2 && !flag)
                            {
                                flag = true;
                            }
                            if (j == 2 && flag)
                            {
                                j = 3;
                            }


                            if (j == 3)
                                j = 12;
                            if (j == 12)
                                j = 13;
                            if (j == 13)
                                j = 17;
                        }
                    }
                    if (code_6 != null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        int k = 1;
                        foreach (var c in code_6)
                        {
                            string myPath = path + date + "_code" + k.ToString() + ".txt";

                            using (FileStream fstream = new FileStream(myPath, FileMode.OpenOrCreate))
                            {
                                // преобразуем строку в байты
                                byte[] buffer = Encoding.Default.GetBytes(c);
                                // запись массива байтов в файл
                                fstream.Write(buffer, 0, buffer.Length);
                                fstream.Close();
                            }
                            taskRepository.addTaskContent(idSTask[5], myPath, "code" + k);
                            k++;
                        }
                    }

                    if (code_16 != null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        int k = 1;
                        foreach (var c in code_16)
                        {
                            string myPath = path + date + "_code" + k.ToString() + ".txt";

                            using (FileStream fstream = new FileStream(myPath, FileMode.OpenOrCreate))
                            {
                                // преобразуем строку в байты
                                byte[] buffer = Encoding.Default.GetBytes(c);
                                // запись массива байтов в файл
                                fstream.Write(buffer, 0, buffer.Length);
                                fstream.Close();
                            }
                            taskRepository.addTaskContent(idSTask[5], myPath, "code" + k);
                            k++;
                        }
                    }

                    if (code_22 != null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        int k = 1;
                        foreach (var c in code_22)
                        {
                            string myPath = path + date + "_code" + k.ToString() + ".txt";

                            using (FileStream fstream = new FileStream(myPath, FileMode.OpenOrCreate))
                            {
                                // преобразуем строку в байты
                                byte[] buffer = Encoding.Default.GetBytes(c);
                                // запись массива байтов в файл
                                fstream.Write(buffer, 0, buffer.Length);
                                fstream.Close();
                            }
                            taskRepository.addTaskContent(idSTask[5], myPath, "code" + k);
                            k++;
                        }
                    }
                    return Redirect("~/TVars/Index/" + varId);
                }
                return Redirect("~/THome/Index");
            }
            return Redirect("~/Home/Index");
        }

    }


}
