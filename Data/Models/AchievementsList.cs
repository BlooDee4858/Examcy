namespace Examcy.Data.Models
{
    public class AchievementsList
    {
        public int Id { get; set; }                     //  КЛЮЧ
        public string Title { get; set; } = "";         // заголовок достижения
        public string Description { get; set; } = "";   // описание достижения
        public string Img { get; set; } = "";           // адрес картинки достижения
        public int MaxCount { get; set; }               // максимальное количество раз, которое можно получить это достижение для одного пользователя

    }
}
