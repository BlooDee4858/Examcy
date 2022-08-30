using Examcy.Data.Models;
namespace Examcy.ViewModels.Student
{
    public class StSolvedTaskListViewModel
    {
        public List<AnswersForList> answers { get; set; } = new List<AnswersForList>(); // список всех заданий, которые решал студент
    }
}
