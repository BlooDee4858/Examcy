using Examcy.Data.Models;

namespace Examcy.Data.Interfaces
{
    public interface ISessions
    {
        IEnumerable<Sessions> AllSessions { get; /*set; */}
        int getUserId(string id); // возвращает id пользователя

    }
}
