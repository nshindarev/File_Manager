using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class FindInfoThread : FindInfo_Abstract
    {
        string FileForRegex;
        public FindInfoThread(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { FileForRegex = "Regex_Thread"; }
        public override void try_findinfo()
        {
            MyFile f2 = new MyFile(FileForRegex);
          


            Stopwatch watch = new Stopwatch();
            watch.Start();
            // сначала необходимо собрать всю информацию по файлам в папке


            Visitor v = new Visitor();
            this.Accept(v);
            /*
            * Самое время запустить все потоки и собрать информацию
           */

            // создаем массив из 4-х процессов. (Ровно столько же, сколько у нашего компьютера ядер)

            MultiRegex[] mr = new MultiRegex[Environment.ProcessorCount];
            for (int i = 0; i < mr.Length; i++)
            {
                mr[i] = new MultiRegex(file_reg_archive, i);
            }

            // запускаем потоки
            for (int i = 0; i < mr.Length; i++)
            {
                mr[i].Start();
            }



            for (int i = 0; i < mr.Length; i++)
            {
                mr[i].End();
            }

            for (int i = 0; i < mr.Length; i++) // из каждого потока берем его List data
            {
                for (int j = 0; j < data.Length; j++) // @, INN, VK
                {
                    foreach (string s in mr[i].data[j]) // string из List<string>
                    {
                        data[j].Add(s);
                    }
                }

            }
            FileWrite(FileForRegex);
        }
    }
}
