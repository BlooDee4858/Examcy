using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface ITheme
    {
        IEnumerable<Theme> AllTheme { get; /*set; */}
        Theme getTheme(int id);
    }
}
