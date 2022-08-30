namespace Examcy.Data.Models
{
    public class StudentForGroup
    {
        public int Id { get; set; }
        public int StudentId { get; set;}
        public string StudentName { get; set; } = "";
        public int groupId { get; set; }
    }
}
