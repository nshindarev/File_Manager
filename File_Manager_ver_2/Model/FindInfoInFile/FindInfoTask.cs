using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class FindInfoTask : FindInfo_Abstract
    {
        string FileForRegex;

        List<string>[] four_file_reg_archive = new List<string>[Environment.ProcessorCount]; // хранит список всех файлов директории
        public delegate void FindInfoDelegate(List<string> fileinfo);   // шаблон делегата


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
        public FindInfoTask(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { FileForRegex = "Regex_Task"; }
        public override void try_findinfo()
        {
            MyFile f4 = new MyFile(FileForRegex);



            Stopwatch watch = new Stopwatch();
            watch.Start();
            // сначала необходимо собрать всю информацию по файлам в папке


            Visitor v = new Visitor();
            this.Accept(v);
            IDelegates();


            //int Stoped = 0;
            Task[] r = new Task[file_reg_archive.Count];

            int n = 0;
            for (int i = 0; i < four_file_reg_archive.Length - 1; i++)
            {
                r[n] = Task.Factory.StartNew(() =>
                {
                    FindInfoInFile(four_file_reg_archive[i]);

                });

            }
            r[n].GetAwaiter().OnCompleted(() =>
            {
                // Stoped++;
            }); n++;

            FileWrite(FileForRegex);
            watch.Stop();
        }
    }
}
