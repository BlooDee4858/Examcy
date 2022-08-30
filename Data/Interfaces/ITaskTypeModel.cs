using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface ITaskTypeModel
    {
        IEnumerable<TypesModel> AllTaskTypeModel { get; /*set; */}
        TypesModel getTaskTypeModel(int id);
    }
}
