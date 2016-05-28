using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class FindInfoDelegate : FindInfo_Abstract
    {
        string FileForRegex;


        List<string>[] four_file_reg_archive = new List<string>[Environment.ProcessorCount]; // хранит список всех файлов директории
        public delegate void DelegateFindInfo(List<string> fileinfo);   // шаблон делегата
        // флаг используем для завершения цикла 
        private int EndedDelegatesCounter = 1;
        public void EndFunc(IAsyncResult result)
        {

            EndedDelegatesCounter++;
            if (EndedDelegatesCounter == Environment.ProcessorCount)
            {




                string fileName = FileForRegex + ".txt";
                //  FileForRegex += Path.GetRandomFileName();

                StreamWriter f = new StreamWriter(@"C:\ForFileManager\" + FileForRegex + ".txt");
                f.WriteLine("Найденные адреса электронной почты:");
                foreach (string s in data[0])
                    f.WriteLine(s);
                f.WriteLine("\r\nНайденные INN:");
                foreach (string s in data[1])
                    f.WriteLine(s);
                f.WriteLine("\r\nНайденные адреса страниц ВКонтакте:");
                foreach (string s in data[2])
                    f.WriteLine(s);
                f.Close();

            }
        }
        public void IDelegates()
        {
            for (int i = 0; i < four_file_reg_archive.Length; i++)
                four_file_reg_archive[i] = new List<string>();

            int j = 0;
            //foreach (string _file in file_reg_archive)
            for (int i = 0; i < file_reg_archive.Count; i++)
            {
                four_file_reg_archive[j].Add(file_reg_archive[i]);
                //four_file_reg_archive[j].Add(_file);
                j = (j == 3) ? (0) : (j + 1);
            }
        }
        public FindInfoDelegate(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { FileForRegex = "Regex_Delegate"; }
        public override void try_findinfo()
        {
            FileInfo f1 = new FileInfo(FileForRegex);
            f1.Delete();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            // сначала необходимо собрать всю информацию по файлам в папке



            Visitor v = new Visitor();
            this.Accept(v);
            IDelegates(); // делим файлы на всех


            // далее создаем 4 делегата
            DelegateFindInfo[] Delegates = new DelegateFindInfo[Environment.ProcessorCount];
            for (int i = 0; i < Delegates.Length; i++)
            {
                Delegates[i] = new DelegateFindInfo(FindInfoInFile);
            }

            IAsyncResult[] AsyncArchive = new IAsyncResult[Environment.ProcessorCount];
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                AsyncArchive[i] = Delegates[i].BeginInvoke(four_file_reg_archive[i], new AsyncCallback(EndFunc), null);
            }
            FileWrite(FileForRegex);
        }
    }
}
