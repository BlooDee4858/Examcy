using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface IStudentGroup
    {
        IEnumerable<StudentGroup> AllStudentGroup { get; /*set; */}
        StudentGroup getStudentGroup(int id);
    }
}
