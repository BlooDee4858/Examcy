using Examcy.Data;
using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels;
using Examcy.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class StAllCoursesController : Controller
    {
        public StAllCoursesController()
        {
        }
        
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                ViewBag.UserName = u.getLogin(idU);
                int idS = u.GetStudentIdByUserId(idU); // id студента

                StAllCoursesViewModel model = new StAllCoursesViewModel();
                // узнаем к каким группам относится студент и к каким курсам у него есть доступ
                GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                CourseRepository courseRepository = new CourseRepository();
                // добавляем в модель только те курсы, на которые студент еще не добавлен

                foreach (var c in courseRepository.AllCourses())
                {
                    bool flag = false;
                    foreach (var ind in groupCourseRepository.getIdsCoursesForStudent(idS))
                    {
                        if (ind == c.Id)
                            flag = true;
                    }
                    if (!flag)
                    {
                        c.Teacher = u.GetUserById(c.TeacherId);
                        model.courses.Add(c);
                    }
                }

                ViewBag.Title = "Курсы | Examcy";
                return View(model);
            }
            else
                return Redirect("~/Home/Index");
        }

        public IActionResult SignUp(int id)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {               
                ViewBag.UserName = User.Identity.Name;
                int idS = u.GetStudentIdByUserId(idU); // id студента


                // получаем id общей группы для этого курса
                GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                int idCommonGroup = groupCourseRepository.getIdCommonGroupForCourse(id);
                if (idCommonGroup == 0)
                {
                    // выводим сообщение что запись на курс временно недоступна
                    return Redirect("~/StAllCourse/Index");
                }
                else
                {
                    // добавляем в нее студента
                    StudentGroupRepository studentGroupRepository = new StudentGroupRepository();
                    studentGroupRepository.addNewStudentToGroup(idS, idCommonGroup);
                    try
                    {
                        studentGroupRepository.addNewStudentToCourse(idS, id);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return Redirect("~/StHome/Index");
            }
            else
                return Redirect("~/Home/Index");

        }
    }
}
