namespace Examcy.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        
        public string Text { get; set; } = "";
        public string Answer { get; set; } = "";
        public string Solution { get; set; } = string.Empty;
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; } = new Theme();

        public Task generate(int task)
        {
            Task taskTask = new Task();
            Random random = new Random();
            switch (task)
            {
                case 0:
                    {
                        int p = 0;
                        do
                        {
                            int n, k;
                            if (random.Next(1000) % 2 == 0)
                            {
                                n = 2; // +
                                k = 3; // *
                            }
                            else
                            {
                                n = 1; // +
                                k = 2; // *
                            }

                            int rez = random.Next(1, 150);
                            List<int> steps = new List<int>();

                            int num = rez;
                            for (int i = 0; i < 5; i++)
                            {
                                if (num % k == 0)
                                {
                                    steps.Add(2);
                                    num = num / k;
                                }
                                else
                                {
                                    steps.Add(1);
                                    num = num - n;
                                }
                            }

                            steps.Reverse();
                            taskTask.Answer = "";
                            foreach (int i in steps)
                            {
                                taskTask.Answer += i.ToString();
                            }

                            taskTask.Text = "У исполнителя две команды, которым присвоены номера: " +
                                "\n1. прибавь " + n.ToString() + ", " +
                                "\n2. умножь на " + k.ToString() + ". " +
                                "\nПервая из них увеличивает число на экране на " + n.ToString() + ", вторая увеличивает в " + k.ToString() + " раз. " +
                                "\nЗапишите порядок команд в программе преобразования числа " + num.ToString() + " в число " + rez.ToString() + ", содержащей не более 5 команд, указывая лишь номера команд.";

                            if (rez < 1 || num < 1)
                            {
                                p++;
                            }
                            else
                            {
                                break;
                            }
                        } while (p < 10);
                        return taskTask;
                    }

                case 1:
                    {
                        int p = 0;
                        do
                        {
                            int n, k;
                            if (random.Next(1000) % 2 == 0)
                            {
                                n = 2; // -
                                k = 5; // *
                            }
                            else
                            {
                                n = 2; // -
                                k = 3; // *
                            }

                            int rez = random.Next(1, 150);
                            List<int> steps = new List<int>();

                            int num = rez;
                            for (int i = 0; i < 5; i++)
                            {
                                if (num % k == 0)
                                {
                                    steps.Add(2);
                                    num = num / k;
                                }
                                else
                                {
                                    steps.Add(1);
                                    num = num + n;
                                }
                            }

                            steps.Reverse();
                            taskTask.Answer = "";
                            foreach (int i in steps)
                            {
                                taskTask.Answer += i.ToString();
                            }

                            taskTask.Text = "У исполнителя две команды, которым присвоены номера: " +
                                "\n1. вычти " + n.ToString() + ", " +
                                "\n2. умножь на " + k.ToString() + ". " +
                                "\nПервая отнимает от числа на экране " + n.ToString() + ", вторая умножает число на " + k.ToString() + " раз. " +
                                "\nЗапишите порядок команд в программе, которая содержит не более 5 команд и переводит число " + num.ToString() + " в число " + rez.ToString() + ". В ответе указывайте лишь номера команд, пробелы между цифрами не ставьте.";

                            if (rez < 1 || num < 1)
                            {
                                p++;
                            }
                            else
                            {
                                break;
                            }
                        } while (p < 10);
                        return taskTask;
                    }

                case 2:
                    {
                        int p = 0;
                        do
                        {
                            bool flag = false;
                            int rez;
                            do
                            {
                                rez = random.Next(22, 1818);
                                if (rez < 100)
                                    flag = true;
                                if (rez > 100 && rez < 1000)
                                {
                                    if (rez / 10 < 19 && rez % 100 < 19 && rez % 100 != 0 && rez % 10 != 0)
                                        flag = true;
                                }
                                if (rez > 1000)
                                {
                                    if (rez / 100 < 19 && rez % 100 < 19 && rez % 100 != 0)
                                        flag = true;
                                }
                            } while (!flag);

                            int first = 0, second = 0, third = 0;
                            int a = 0, b = 0;
                            if (rez < 100)
                            {
                                a = rez / 10;
                                b = rez % 10;
                            }

                            if (rez > 100 && rez < 1000)
                            {

                                a = rez / 100;
                                b = rez % 100;
                                if (a > 18 || b > 18)
                                {
                                    a = rez / 10;
                                    b = rez % 10;
                                }
                            }

                            if (rez > 1000)
                            {
                                a = rez / 100;
                                b = rez % 100;
                            }

                            if (a > b) // b = 1+2
                            {
                                for (int i = 1; i < 10; i++)
                                {
                                    if (b - i < a && b - i < 10)
                                    {
                                        first = i;
                                        break;
                                    }
                                }
                                second = b - first;
                                third = a - second;
                            }
                            else // a = 1+2
                            {
                                for (int i = 1; i < 10; i++)
                                {
                                    if (a - i < b && a - i < 10)
                                    {
                                        first = i;
                                        break;
                                    }
                                }

                                second = a - first;
                                third = b - second;
                            }


                            string h = (first + second).ToString() + (second + third).ToString();
                            if (h == rez.ToString())
                            {
                                taskTask.Text = "Автомат получает на вход трёхзначное число. По этому числу строится новое число по следующим правилам." +
                                "\n1.Складываются первая и вторая, а также вторая и третья цифры исходного числа." +
                                "\n2.Полученные два числа записываются друг за другом в порядке убывания(без разделителей)." +
                                "\nПример.Исходное число: 348.Суммы: 3 + 4 = 7; 4 + 8 = 12.Результат: 127." +
                                "\nУкажите наименьшее число, в результате обработки которого автомат выдаст число " + rez.ToString() + ".";

                                taskTask.Answer = first.ToString() + second.ToString() + third.ToString();
                                return taskTask;
                            }
                            else
                                p++;
                        } while (p < 10);
                        break;
                    }
                case 3:
                    {
                        int k, n, i, j;
                        double rez;

                        if (random.Next(1000) % 2 == 0)
                            n = 24;
                        else
                            n = 16;

                        i = random.Next(1, 50) * 100;
                        j = random.Next(1, 35) * 100;
                        k = random.Next(1, 100) * 1000;

                        double z;
                        if (n == 24)
                            z = i * j * 5.0 / k;
                        else
                            z = i * j * 4.0 / k;

                        rez = Math.Round(z, 0, MidpointRounding.AwayFromZero);


                        taskTask.Answer = rez.ToString();
                        taskTask.Text = "Сколько секунд потребуется модему, передающему информацию со скоростью " + k.ToString() + " бит/с, чтобы передать " + n.ToString() + "─цветное растровое изображение размером " + i.ToString() + " на " + j.ToString() + " пикселей, при условии что цвет кодируется минимально возможным количеством бит.";
                        return taskTask;
                    }
                case 4:
                    {
                        int k, n, t;
                        double rez, d;

                        if (random.Next(1000) % 2 == 0)
                            n = 16;
                        else
                            n = 8;

                        d = Math.Pow(2, random.Next(1, 6)) * 3 * 1000;
                        t = random.Next(1, 10) * 10;
                        k = random.Next(1, 100) * 1000;

                        double z = 2 * t * d * n / k;
                        rez = Math.Round(z, 0, MidpointRounding.AwayFromZero);

                        taskTask.Answer = rez.ToString();

                        taskTask.Text = "Стереоаудиофайл передается со скоростью " + k.ToString() + " бит/ с.Файл был записан при среднем качестве звука: глубина кодирования – " + n.ToString() + " бит, частота дискретизации – " + d.ToString() + " измерений в секунду, время записи ─ " + t.ToString() + " сек.Сколько времени будет передаваться файл? Время укажите в секундах.";
                        return taskTask;
                    }
                case 5:
                    {

                        int t, n;
                        double rez;
                        n = random.Next(5, 16);
                        t = random.Next(1, 100);
                        double k = Math.Pow(2, n);
                        rez = Math.Pow(2, n - 4) * t;
                        taskTask.Answer = rez.ToString();
                        taskTask.Text = "Скорость передачи данных через модемное соединение равна " + k.ToString() + " бит/с. Передача текстового файла через это соединение заняла " + t.ToString() + " с. Определите, сколько символов содержал переданный текст, если известно, что он был представлен в 16-битной кодировке Unicode.";

                        return taskTask;
                    }
                case 6:
                    {
                        int k, n, m, t;
                        m = random.Next(1, 5) * 2; // страницы
                        n = random.Next(1, 3000); // символов на 1 стр
                        t = random.Next(1, 6) * 2; // время
                        k = m * n * 8 / t;
                        taskTask.Answer = t.ToString();
                        taskTask.Text = "Средняя скорость передачи данных с помощью модема равна " + k.ToString() + " бит/с. Сколько секунд понадобится модему, чтобы передать " + m.ToString() + " страниц текста в 8-битной кодировке КОИ8, если считать, что на каждой странице в среднем " + n.ToString() + " символов?";

                        return taskTask;
                    }
                case 7:
                    {
                        double k, m, t;
                        m = random.Next(1, 50) * 1.0 / 100;
                        k = random.Next(1, 10) * 1.0 / 2;
                        t = m * 1024 / k;
                        double rez = Math.Round(t, 0, MidpointRounding.AwayFromZero);

                        taskTask.Answer = rez.ToString();
                        taskTask.Text = "Ученик скачивал файл объемом " + m.ToString() + " Мбайт, содержащий контрольную работу. Информация по каналу связи передается со скоростью " + k.ToString() + " Кбайт/с. Какое время понадобится для скачивания файла? Укажите время в секундах, округлив до целых.";
                        return taskTask;
                    }
                case 8:
                    {

                        int k, m;
                        k = random.Next(3, 18);
                        m = random.Next(2, 5);
                        double rez = Math.Pow(k, m);
                        taskTask.Answer = rez.ToString();
                        taskTask.Text = "Некоторый алфавит содержит " + k.ToString() + " различных символов. Сколько " + m.ToString() + "-буквенных слов можно составить из символов этого алфавита, если символы в слове могут повторяться?";

                        return taskTask;
                    }
                case 9:
                    {
                        int min, max;
                        double rez;
                        min = random.Next(1, 3);
                        max = random.Next(min + 1, 5);
                        rez = random.Next(Convert.ToInt32(Math.Pow(3, max - 1)) + 1, Convert.ToInt32(Math.Pow(3, max)) - 1);

                        taskTask.Answer = max.ToString();
                        taskTask.Text = "Световое табло состоит из лампочек. Каждая лампочка может находиться в одном из трех состояний («включено», «выключено» или «мигает»). Какое наименьшее количество лампочек должно находиться на табло, чтобы с его помощью можно было передать " + rez.ToString() + " различных сигналов?";
                        return taskTask;
                    }
            }
            return null;
        }


        public List<Task> getPatterns(int themeId)
        {
            List<Task> patterns = new List<Task>();

            if (themeId == 5)
            {
                Task pattern = new Task();
                pattern.Id = 0;
                pattern.Text = "У исполнителя две команды, которым присвоены номера: " +
                                "\n1. прибавь N, " +
                                "\n2. умножь на K. " +
                                "\nПервая из них увеличивает число на экране на N, вторая увеличивает в K раз. " +
                                "\nЗапишите порядок команд в программе преобразования числа A в число B, содержащей не более 5 команд, указывая лишь номера команд.";
                patterns.Add(pattern);
                pattern = new Task();
                pattern.Id = 1;
                pattern.Text = "У исполнителя две команды, которым присвоены номера:" +
                                "\n1.вычти N" +
                                "\n2.умножь на K" +
                                "\nПервая отнимает от числа на экране N, вторая умножает число на K. Запишите порядок команд в программе, которая содержит не более 5 команд и переводит число A в число B. В ответе указывайте лишь номера команд, пробелы между цифрами не ставьте.";
                patterns.Add(pattern);

                pattern = new Task();
                pattern.Id = 2;
                pattern.Text = "Автомат получает на вход трёхзначное число. По этому числу строится новое число по следующим правилам." +
                    "\n1.Складываются первая и вторая, а также вторая и третья цифры исходного числа." +
                    "\n2.Полученные два числа записываются друг за другом в порядке убывания(без разделителей)." +
                    "\nПример.Исходное число: 348.Суммы: 3 + 4 = 7; 4 + 8 = 12.Результат: 127." +
                    "\nУкажите наименьшее число, в результате обработки которого автомат выдаст число K.";
                patterns.Add(pattern);

            }

            if (themeId == 7)
            {
                Task pattern = new Task();
                pattern.Id = 3;
                pattern.Text = "Сколько секунд потребуется модему, передающему информацию со скоростью K бит/с, чтобы передать N─цветное растровое изображение размером I на J пикселей, при условии что цвет кодируется минимально возможным количеством бит.";
                patterns.Add(pattern);

                pattern = new Task();
                pattern.Id = 4;
                pattern.Text = "Стереоаудиофайл передается со скоростью K бит/ с.Файл был записан при среднем качестве звука: глубина кодирования – N бит, частота дискретизации – D измерений в секунду, время записи ─ T сек.Сколько времени будет передаваться файл? Время укажите в секундах.";
                patterns.Add(pattern);

                pattern = new Task();
                pattern.Id = 5;
                pattern.Text = "Скорость передачи данных через модемное соединение равна K бит/с. Передача текстового файла через это соединение заняла T с. Определите, сколько символов содержал переданный текст, если известно, что он был представлен в 16-битной кодировке Unicode.";
                patterns.Add(pattern);

                pattern = new Task();
                pattern.Id = 6;
                pattern.Text = "Средняя скорость передачи данных с помощью модема равна K бит/с. Сколько секунд понадобится модему, чтобы передать M страниц текста в 8-битной кодировке КОИ8, если считать, что на каждой странице в среднем D символов?";
                patterns.Add(pattern);

                pattern = new Task();
                pattern.Id = 7;
                pattern.Text = "Ученик скачивал файл объемом M Мбайт, содержащий контрольную работу. Информация по каналу связи передается со скоростью K Кбайт/с. Какое время понадобится для скачивания файла? Укажите время в секундах, округлив до целых.";
                patterns.Add(pattern);
            }

            if (themeId == 8)
            {
                Task pattern = new Task();
                pattern.Id = 8;
                pattern.Text = "Некоторый алфавит содержит K различных символов. Сколько M-буквенных слов можно составить из символов этого алфавита, если символы в слове могут повторяться?";
                patterns.Add(pattern);

                pattern = new Task();
                pattern.Id = 9;
                pattern.Text = "Световое табло состоит из лампочек. Каждая лампочка может находиться в одном из трех состояний («включено», «выключено» или «мигает»). Какое наименьшее количество лампочек должно находиться на табло, чтобы с его помощью можно было передать K различных сигналов?";
                patterns.Add(pattern);
            }
            return patterns;
        }
    }
}
