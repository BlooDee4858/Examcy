namespace Examcy.Data.Models
{
    public class GroupForList
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool isCommon { get; set; }
        public int countStudent { get; set; }
    }
}
