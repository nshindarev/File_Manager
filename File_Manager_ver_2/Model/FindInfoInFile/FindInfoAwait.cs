using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class FindInfoAwait : FindInfo_Abstract
    {
        string FileForRegex;
        public FindInfoAwait(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { FileForRegex = "Regex_Await"; }
        public override async void try_findinfo()
        {
            FileInfo f5 = new FileInfo(FileForRegex);
            f5.Delete();


            Stopwatch watch = new Stopwatch();
            watch.Start();
            // сначала необходимо собрать всю информацию по файлам в папке



            Visitor v = new Visitor();
            this.Accept(v);



            foreach (string FILE in file_reg_archive)
            {

                await Task.Run(() =>
                {
                    FindInfoInFile(FILE);
                });

            }


            FileWrite(FileForRegex);
            watch.Stop();

        }
    }
}
