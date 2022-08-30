using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class TGroupController : Controller
    {
        public IActionResult Index()
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                TGroupViewModel model = new TGroupViewModel();

                CourseRepository courseRepository = new CourseRepository();
                model.courses = courseRepository.getAllCourseForListForTeacher(idU);
                if(model.courses != null && model.courses.Count>0)
                {

                    foreach (var course in model.courses)
                    {
                        List<GroupForList> groups = new List<GroupForList>();
                        groups = courseRepository.getAllGroupForCourse(course.Id);
                        model.groups.AddRange(groups);
                        course.allCount = groups.Count;
                    }

                    foreach (var group in model.groups)
                    {
                        List<StudentForGroup> students = new List<StudentForGroup>();
                        students = courseRepository.getAllStudentForGroup(group.Id);
                        model.students.AddRange(students);
                        group.countStudent = students.Count;
                    }

                    ViewBag.Title = "Группы | Examcy";
                    return View(model);
                }
                return View();
                
            }
            return Redirect("~/Home/Index");
        }


        [HttpGet]
        public IActionResult CreateGroup(int id)
        {
            // id course
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;

                TCreateGroupViewModel model = new TCreateGroupViewModel();
                model.course = id;
                
                CourseRepository courseRepository = new CourseRepository();
                model.myStudents = courseRepository.getAllStudentFromMyCourse(id);
                List<int> idStud = new List<int>();
                foreach (var stud in model.myStudents)
                {
                    idStud.Add(stud.StudentId);
                }
                model.allStudents = courseRepository.getAllStudentNotFromThisCourse(id, idStud);

                ViewBag.Title = "Создать группу | Examcy";
                return View(model);

            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult CreateGroup(string title, List<int> newStudents,int course,  List<int> oldStudents)
        {
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                CourseRepository courseRepository = new CourseRepository();
                int groupId = courseRepository.addGroup(title, course);

                if (oldStudents != null)
                {
                    foreach (var student in oldStudents)
                    {
                        int idStudent = courseRepository.deleteStudentFromGroup(student);
                        courseRepository.addStudentIntoGroup(idStudent, groupId);
                    }
                }
                if(newStudents != null)
                {
                    foreach(var student in newStudents)
                    {
                        courseRepository.addStudentIntoGroup(student, groupId);
                        courseRepository.addNewStudentToCourse(student, course);
                    }    
                }

                return Redirect("~/TGroup/Index");

            }
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult UpdateGroup(int id)
        {
            // id group
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                ViewBag.UserName = User.Identity.Name;


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
                return Redirect("~/TGroup/Index");

            }
            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public IActionResult UpdateGroup(int group, bool isCommon, int course, string title, List<int> newStudents, List<int> oldStudents, List<int> delStudents)
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
                        if(common == group)
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
                        try
                        {
                            StudentGroupRepository studentGroupRepository = new StudentGroupRepository(); 
                            studentGroupRepository.addNewStudentToCourse(student, course);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                return Redirect("~/TGroup/Index");


            }
            return Redirect("~/Home/Index");
        }


        public IActionResult DeleteStudentFromGroup(int id)
        {
            // id from StudentGroup
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.deleteStudentFromGroupAndCourse(id);
                return Redirect("~/TGroup/Index");
            }
            return Redirect("~/Home/Index");
        }


        public IActionResult GroupInfo(int id)
        {
            // id from StudentGroup
            UserRepository u = new UserRepository();
            int idU = u.GetUserIdByLogin(User.Identity.Name);
            if (idU != 0)
            {
                GroupCourseRepository groupCourseRepository = new GroupCourseRepository();
                int idCommonGroup = groupCourseRepository.getIdCommonGroupForCourse(id);
                if(idCommonGroup != 0)
                {
                    return Redirect("~/TGroup/UpdateGroup/" + idCommonGroup);
                }
                return Redirect("~/TGroup/Index");
            }
            return Redirect("~/Home/Index");
        }

        //[HttpGet]
        //public IActionResult MoveToGroup(int id)
        //{
        //    // id from StudentGroup
        //    UserRepository u = new UserRepository();
        //    int idU = u.GetUserIdByLogin(User.Identity.Name);
        //    if (idU != 0)
        //    {
        //        ViewBag.UserName = User.Identity.Name;

        //        TGroupViewModel model = new TGroupViewModel();


        //        ViewBag.Title = "Группы | Examcy";
        //        return View(model);

        //    }
        //    return Redirect("~/Home/Index");
        //}

        //[HttpPost]
        //public IActionResult MoveToGroup(int student, int group)
        //{
        //    UserRepository u = new UserRepository();
        //    int idU = u.GetUserIdByLogin(User.Identity.Name);
        //    if (idU != 0)
        //    {
        //        ViewBag.UserName = User.Identity.Name;

        //        TGroupViewModel model = new TGroupViewModel();


        //        ViewBag.Title = "Группы | Examcy";
        //        return View(model);

        //    }
        //    return Redirect("~/Home/Index");
        //}

    }
}
