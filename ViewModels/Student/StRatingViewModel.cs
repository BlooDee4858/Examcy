using Examcy.Data.Models;

namespace Examcy.ViewModels.Student
{
    public class StRatingViewModel
    {
        public List<Rating> allratings { get; set; } = new List<Rating>();
        public List<Rating> testratings { get; set; } = new List<Rating>();
        public List<Rating> visitors { get; set; } = new List<Rating>();
        public int first_allraiting { get; set; } = 0;
        public int second_allraiting { get; set; } = 0;
        public int first_testraiting { get; set; } = 0;
        public int second_testraiting { get; set; } = 0;
        public int first_visitors { get; set; } = 0;
        public int second_visitors { get; set; } = 0;
        public int n_allraiting { get; set; } = 0;
        public int n_testraiting { get; set; } = 0;
        public int n_visitors { get; set; } = 0;

        public int idVar = 0;

    }
}
