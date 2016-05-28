using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class FindInfoParallel : FindInfo_Abstract
    {
        string FileForRegex;
        public FindInfoParallel(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { FileForRegex = "Regex_Parallel"; }
        public override void try_findinfo()
        {
            FileInfo f3 = new FileInfo(FileForRegex);
            f3.Delete();


            Stopwatch watch = new Stopwatch();
            watch.Start();
            // сначала необходимо собрать всю информацию по файлам в папке


            Visitor v = new Visitor();
            this.Accept(v);
            Parallel.ForEach(file_reg_archive, file =>
            {
                FindInfoInFile(file);
            });


            FileWrite(FileForRegex);
            watch.Stop();

        }
    }
}
