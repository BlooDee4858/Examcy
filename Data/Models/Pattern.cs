namespace Examcy.Data.Models
{
    public class Pattern
    {
        public List<TaskContent> files { get; set; } = new List<TaskContent>();
        public string[] codeText = new string[5];
        public bool _img1 = false;
        public bool _img2 = false;
        public bool _file1 = false;
        public bool _file2 = false;
        public bool _code = false;

        public Pattern getTask(int numTask)
        {
            Pattern pattern = new Pattern();

            if (numTask < 1 || numTask > 27 || numTask == 12 || numTask == 4 || numTask == 5 || numTask == 7 || numTask == 8 || numTask == 11 || numTask == 14 || numTask == 15 || numTask == 19 || numTask == 20 || numTask == 21 || numTask == 23 || numTask == 25)
                return null;

            if (numTask == 1)
            {
                // img + img
                pattern._img2 = true;
                pattern._img1 = true;
            }
            if (numTask == 18)
            {
                // file + img
                pattern._file1 = true;
                pattern._img1 = true;
            }
            if (numTask == 6 || numTask == 16 || numTask == 22)
            {
                // code
                pattern._code = true;
            }
            if (numTask == 3 || numTask == 9 || numTask == 10 || numTask == 24 || numTask == 26 || numTask == 17)
            {
                // file
                pattern._file1 = true;
            }
            if (numTask == 13 || numTask == 2)
            {
                // img
                pattern._img1 = true;
            }
            if (numTask == 27)
            {
                // file+file
                pattern._file1 = true;
                pattern._file2 = true;
            }
            return pattern;
        }

    }
}
