using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface ICourse
    {
        IEnumerable<Course> AllCourse { get; /*set; */}
        Course getCourse(int id);
        // + функция возвращающая количество курсов
    }
}
