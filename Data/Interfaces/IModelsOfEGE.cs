using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface IModelsOfEGE
    {
        IEnumerable<ModelsOfEGE> AllModelOfEGE { get; /*set; */}
        ModelsOfEGE getModelOfEGE(int id);
    }
}
