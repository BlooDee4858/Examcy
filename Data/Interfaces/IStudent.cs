using Examcy.Data.Models;

namespace Examcy.Data.Interfaces
{
    public interface IStudent
    {
        IEnumerable<Student> AllStudents { get; /*set; */}
        Student getStudent(int id);
        // +функция возвр. количество студентов

        void changeInfoAboutStudent(Student student);
    }
}
