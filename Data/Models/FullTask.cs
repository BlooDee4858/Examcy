namespace Examcy.Data.Models
{
    public class FullTask
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; } = "";
        public string Text { get; set; } = string.Empty;
        public Pattern pattern { get; set; } = new Pattern();
        public string Answer { get; set; } = "";

        public int countAnswer = 1;
        public string Solution { get; set; } = string.Empty;
        public int ThemeId { get; set; }
        public int CourseId { get; set; }
        public string ThemeTitle { get; set; } = "";


        public void countAnswers(string answer)
        {
            int count = 0;
            string[] str = answer.Split(';');
            foreach(string str2 in str)
            {
                if (str2 != " ")
                    count++;
            }
            countAnswer = count;
        }
        public bool checkTheAnswer(string answer, string correct)
        {
            if (answer == null)
                return false;
            answer = answer.Trim();
            if (answer == correct)
                return true;
            return false;
        }
        public bool checkAnswers(List<string> answer, string correct)
        {
            string ans = "";
            foreach(var a in answer)
            {
                ans += a.Trim() + ";";
            }
            if (ans == correct)
                return true;
            return false;
        }
    }
}
