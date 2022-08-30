using Examcy.Data.Repository;
using Examcy.ViewModels;
using Examcy.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examcy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Examcy.Data.Interfaces;
using Examcy.Domain.Helpers;
using Examcy.ViewModels.Teacher;
using Examcy.ViewModels.Student;

namespace Examcy.Controllers
{
    public class AHomeController : Controller
    {
        // GET: AHomeController
        private readonly IAccountService _accountService;
        public AHomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Users()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                var model = new AHomeViewModel();

            model.users = u.AllUsers();
            

            return View(model);
            }
            return Redirect("~/Home/Index");
        }
        //[HttpGet]
        //public IActionResult ChangeGroup(int id)
        //{
        //    var model = new ChangeGroupViewModel();
        //    UserRepository u = new UserRepository();

        //    model.students = u.GetAllUserInGroup(id);

        //    return View(model);
        //}
        [HttpGet]
        public IActionResult ChangeUser(int id)
        {
                UserRepository u = new UserRepository();
                int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                if (idU != 0)
                {
                    Examcy.Data.Models.User model = new Examcy.Data.Models.User();


            model = u.GetUserById(id);

            return View(model);
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult DeleteUserInGroup(int id)
        {
                    UserRepository u = new UserRepository();
                    int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                    if (idU != 0)
                    {
                        UserRepository user = new UserRepository();
            user.DeleteUserFromGroup(id);

            return RedirectToAction("Groups", "AHome");
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult Courses()
        {
                        UserRepository u = new UserRepository();
                        int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                        if (idU != 0)
                        {
                            var model = new CourseViewModel();
            CourseRepository course = new CourseRepository();

            model.courses = course.GetAllCourseAdmin();

            return View(model);
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult Groups()
        {
                            UserRepository u = new UserRepository();
                            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                            if (idU != 0)
                            {
                                var model = new GroupViewModel();
            GroupCourseRepository groupCourse = new GroupCourseRepository();

            model.groups = groupCourse.AllGroupsAdmin();

            return View(model);
            }
            return Redirect("~/Home/Index");
        }
        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
                                UserRepository u = new UserRepository();
                                int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                                if (idU != 0)
                                {
                                    UserRepository user = new UserRepository();
            user.DeleteUser(id);
            
            return RedirectToAction("Users", "AHome");
            }
            return Redirect("~/Home/Index");
        }
        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
                                    UserRepository u = new UserRepository();
                                    int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                                    if (idU != 0)
                                    {
                                        CourseRepository c = new CourseRepository();
            c.deleteCourse(id);

            return RedirectToAction("Courses", "AHome");
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult DeleteGroup(int id)
        {
                                        UserRepository u = new UserRepository();
                                        int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                                        if (idU != 0)
                                        {
                                            GroupCourseRepository g = new GroupCourseRepository();
            g.DeleteGroup(id);

            return RedirectToAction("Groups", "AHome");
            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult EditUser(string MyId, string FirstName, string LastName, string Login, string Password)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
            if (idU != 0)
            {
                UserRepository userRepository = new UserRepository();

                Examcy.Data.Models.User user = new User();
                user = userRepository.GetUserById(Convert.ToInt32(MyId));
                user.Id = Convert.ToInt32(MyId);
                user.Login = Login;
                bool flag = false;
                if (Password != null)
                {
                    if (Password.Length > 0)
                    {
                        user.Password = HashPasswordHelper.HashPassowrd(Password); ;
                        flag = true;
                    }
                }
                else

                    user.LastName = LastName;
                user.FirstName = FirstName;

                userRepository.UpdateUser(user, flag);
                return Redirect("~/AHome/Users");
            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult ChangeGroup(int id)
        {
            // id group
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {


                TUpdateGroupViewModel model = new TUpdateGroupViewModel();
                model.group = id;
                GroupCourse gr = new GroupCourse();

                CourseRepository courseRepository = new CourseRepository();
                gr = courseRepository.getGroup(id);
                if (gr != null)
                {
                    model.name = gr.Title;
                    model.isCommon = gr.IsCommonGroup;
                    model.course = gr.CourseId;

                    model.students = courseRepository.getAllStuddentsForGroup(id);
                    model.myStudents = courseRepository.getAllStudentFromMyCourse(model.course);
                    List<int> idStud = new List<int>();
                    foreach (var stud in model.myStudents)
                    {
                        idStud.Add(stud.StudentId);
                    }
                    model.allStudents = courseRepository.getAllStudentNotFromThisCourse(model.course, idStud);

                    ViewBag.Title = "Редактировать группу | Examcy";
                    return View(model);
                }
                return Redirect("~/AHome/Groups");

            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult ChangeGroup(int group, bool isCommon, int course, string title, List<int> newStudents, List<int> oldStudents, List<int> delStudents)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.updateGroup(title, group);

                if (delStudents != null)
                {
                    GroupCourseRepository groupCourseRepository = new GroupCourseRepository();

                    int common = groupCourseRepository.getIdCommonGroupForCourse(course);

                    foreach (var student in delStudents)
                    {
                        if (common == group)
                        {
                            courseRepository.deleteStudentFromGroupAndCourse(student);
                        }
                        else
                        {
                            int idStudent = courseRepository.deleteStudentFromGroup(student);
                            courseRepository.addStudentIntoGroup(idStudent, common);
                        }

                    }
                }

                if (oldStudents != null)
                {
                    foreach (var student in oldStudents)
                    {
                        int idStudent = courseRepository.deleteStudentFromGroup(student);
                        courseRepository.addStudentIntoGroup(idStudent, group);
                    }
                }
                if (newStudents != null)
                {
                    foreach (var student in newStudents)
                    {
                        courseRepository.addStudentIntoGroup(student, group);
                        courseRepository.addNewStudentToCourse(student, course);
                    }
                }

                return Redirect("~/AHome/Groups");


            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult AddUser() => View();

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterModel model)
        {
                                            UserRepository u = new UserRepository();
                                            int idU = u.GetUserIdByLogin(User.Identity.Name);// id пользователя
                                            if (idU != 0)
                                            {
                                                int r;
            if (ModelState.IsValid)
            {
         
                var response = await _accountService.Register(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    r = u.GetRoleByLogin(response.Data.Name);
                    if(r == 2)
                    {
                        u.addStudent(u.GetUserIdByLogin(response.Data.Name));
                    }
                    return RedirectToAction("Users", "AHome");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
            }
            return Redirect("~/Home/Index");
        }


    }
}
