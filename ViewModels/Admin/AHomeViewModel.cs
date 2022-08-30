using Examcy.Data.Models;
using Examcy.Data.Repository;
using Examcy.ViewModels.User;

namespace Examcy.ViewModels.Admin
{
    public class AHomeViewModel
    {
        public List<Data.Models.User> users = new List<Data.Models.User>();
        public List<Course> courses = new List<Course>();  
        public List<GroupCourse> groups = new List<GroupCourse>();
        public int GroupID;
    }
}
