namespace Examcy.Data.Models
{
    public class CourseForList
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int allCount { get; set; } = 0; // количество вариантов или заданий, зависит от ситуации
        public int assignedCount { get; set; } = 0;
        public int solvedCount { get; set; } = 0; 
    } 
}
