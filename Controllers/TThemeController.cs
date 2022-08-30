using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Student;
using Examcy.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class TThemeController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        public TThemeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public IActionResult Index(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                // проверяем доступ к курсу
                StThemeViewModel model = new StThemeViewModel();
                ThemeRepository themeRepository = new ThemeRepository();
                TaskRepository taskRepository = new TaskRepository();
                model.theme = themeRepository.getThemeById(id);
                foreach (var t in taskRepository.getAllTaskForTheme(model.theme.Id))
                {
                    model.tasks.Add(t);
                }
                model.games = null; // получение из бд списка игр для данной темы
                
                ViewBag.Title = model.theme.Title + " | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }


        [HttpGet]
        public IActionResult CreateTheme(int id)
        {
            // id курса
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                // проверяем доступ к курсу
                TCreateThemeViewModel model = new TCreateThemeViewModel();
                model.courseId = id;
                ThemeRepository themeRepository = new ThemeRepository();
                model.themes = themeRepository.getAllThemesForCreatingTheme();

                ViewBag.Title = "Конструктор тем | Examcy";
                return View(model);
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }
        [HttpGet]
        public IActionResult UpdateTheme(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                
                CourseRepository courseRepository = new CourseRepository();


                if (courseRepository.checkingAccessToTheCourseByThemeId(id, idU))
                {
                    TUpdateThemeViewModel model = new TUpdateThemeViewModel();
                    ThemeRepository themeRepository = new ThemeRepository();
                    model.theme = themeRepository.getThemeById(id);
                    if (model.theme == null)
                    {
                        return Redirect("~/TCourses/Index");
                    }
                    ViewBag.Title = "Редактировать тему №" + model.theme.Id + " | Examcy";
                    return View(model);
                }
                else
                {
                    return Redirect("~/TCourses/Index");
                }
            }
            return Redirect("~/Home/Index");
        }


        [HttpPost]
        public IActionResult CreateTheme(int themeId, int courseId, string title, string text, string otherText, IFormFile uploads)
        {
            // id курса
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ThemeRepository themeRepository = new ThemeRepository();
                ViewBag.UserName = User.Identity.Name;
                Theme theme = new Theme();
                theme.TypeId = themeId;
                theme.CourseId = courseId;
                if (text == null)
                {
                    text = "Теория к данной теме не добавлена.";
                }
                if (otherText == null)
                {
                    otherText = "Дополнительной информации к данной теме нет.";
                }
                if (title == null)
                {
                    title = themeRepository.getTypeTitle(themeId);
                }
                if (uploads == null)
                {
                    theme.Path = "Нет файлов";
                }
                else
                {
                    theme.Path = "wwwroot\\TeacherFiles\\" + User.Identity.Name;
                    DirectoryInfo dirInfo = new DirectoryInfo(theme.Path);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }

                    using (var fileStream = new FileStream(theme.Path + "\\" + uploads.FileName, FileMode.Create))
                    {
                        uploads.CopyTo(fileStream);
                    }
                    theme.Path += uploads.FileName;
                }
                theme.Title = title;
                theme.Text = text;
                theme.OtherText = otherText;
                
                themeRepository.addTheme(theme);
                // добавляем в базу
                return Redirect("~/TCourses/CourseInfo/"+courseId);
            }
            else
            {
                return Redirect("~/THome/Index");
            }
        }
        [HttpPost]
        public IActionResult UpdateTheme(int themeId, int courseId, int typeId, string title, string text, string otherText, IFormFile uploaded)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;


                CourseRepository courseRepository = new CourseRepository();

                if (courseRepository.checkingAccessToTheCourseByThemeId(themeId, idU))
                {
                    ThemeRepository themeRepository = new ThemeRepository();
                    Theme theme = new Theme();
                    theme.Id = themeId;
                    theme.Title = title;
                    theme.CourseId = courseId;
                    theme.TypeId = typeId;
                    theme.Text=text;
                    theme.OtherText = otherText;
                    if (uploaded == null)
                    {
                        theme.Path = "Дополнительные файлы не добавлены";
                    }
                    else
                    {
                        theme.Path = "wwwroot\\TeacherFiles\\" + User.Identity.Name;
                        DirectoryInfo dirInfo = new DirectoryInfo(theme.Path);
                        if (!dirInfo.Exists)
                        {
                            dirInfo.Create();
                        }

                        using (var fileStream = new FileStream(theme.Path + "\\" + uploaded.FileName, FileMode.Create))
                        {
                            uploaded.CopyTo(fileStream);
                        }
                        theme.Path += uploaded.FileName;
                    }

                    themeRepository.UpdateTheme(theme, themeId);

                    return Redirect("~/TCourses/CourseInfo/"+theme.CourseId);
                }
                else
                {
                    return Redirect("~/TCourses/Index");
                }
            }
            return Redirect("~/Home/Index");
        }

    }
}
