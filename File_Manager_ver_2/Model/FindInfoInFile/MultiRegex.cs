using System;
using System.Collections.Generic;
using System.IO;
//using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class MultiRegex
    {

        int Thread_ID;
        // шаблоны поиска совпадений
        Regex[] regexes = new Regex[3]
       {
          new Regex(@"[-a-zA-Z0-9\._\+]+ \@ [-a-zA-Z0-9\._]+ \. [a-zA-Z]{2,6} \.?", RegexOptions.IgnorePatternWhitespace),  // Mail
          new Regex(@"[0-9]{3} \- [0-9]{3} \- [0-9]{3} \s [0-9]{2} ",               RegexOptions.IgnorePatternWhitespace),  // INN
          new Regex(@"vk\.com\/ [a-zA-Z0-9] [a-zA-Z0-9_]+ [a-zA-Z0-9]",             RegexOptions.IgnorePatternWhitespace)  // Vk
       };


        // сам поток
        Thread th;

        // создаем массив, в котором будем хранить найденную информацию
        public List<string>[] data = new List<string>[]
        {
          new List<string> { }, // @
          new List<string> { }, // INN
          new List<string> { }  // Vk
        };


        public List<string> ls;        // хранятся все названия файлов, собранные рекурсивно по данной директории


        // конструктор, параметры - список файлов, по которым нужно провести поиск
        // и параметр - номер данного потока, чтобы рассматривать каждый 4-й файл из ls
        // используем ThreadId, чтобы создавались потоки с 1 по 4 в цикле, а не их хеши 
        public MultiRegex(List<string> ls, int Thread_ID)
        {
            this.ls = ls;
            this.Thread_ID = Thread_ID;
            th = new Thread(new ThreadStart(Find));
        }

        public void Start()
        {
            th.Start();
        }
        public void End()
        {
            th.Join();
        }


        private void Find()
        {


            // цикл вызываем столько раз, сколько у нас файлов
            // внутри цикла ищем повторения
            // заодно равномерно распределили файлы по всем потокам


            for (int i = Thread_ID; i < (ls.Count - Environment.ProcessorCount); i += Environment.ProcessorCount)
            {
                FindInfoInFile(ls[i]);
            }
        }
        private void FindInfoInFile(string fileFullName)
        {

            StreamReader f = null;
            try
            {
                // Читаем исходный файл
                f = File.OpenText(fileFullName);
                string curstring;

                do
                {
                    curstring = f.ReadLine();

                    // Сравниваем со всеми шаблонами по порядку
                    for (int i = 0; i < regexes.Length; i++)
                    {
                        // В случае с пустым файлом, чтобы избежать ошибок, смотрим Matches не с curstring, а с ""
                        // Если curstring == null

                        Match m = regexes[i].Match(curstring == null ? "" : curstring);
                        while (m.Value != "")
                        {
                            data[i].Add(m.Value);
                            m = m.NextMatch();
                        }
                    }

                }
                while (!f.EndOfStream);


            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.ToString()); }
            // finally { f.Close();}
        }


    }
}
