using Examcy.Data.Models;
namespace Examcy.Data.Interfaces
{
    public interface IAnswers
    {
        IEnumerable<Answers> AllAnswers { get; /*set; */}
        Answers getAnswers(int id);
        // + функция возвращающая сколько раз студент решал конкретное задание
        // + функция возвращающая сколько по какой теме решено заданий 
        // + функции типа тех что выше, уточнить у Марины что ей надо для статистики

        // + функция возвр. количество разных тем с определенного курса
    }
}
