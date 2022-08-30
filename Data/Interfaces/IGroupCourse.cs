using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface IGroupCourse
    {
        IEnumerable<GroupCourse> AllGroupCourse { get; /*set; */}
        GroupCourse getIdCourseForGroup(int id);
    }
}
