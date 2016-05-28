using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class Archive_Task : Archivation_Abstract
    {

        public Archive_Task(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { }

        public override void try_archivation()
        {

            Visitor v = new Visitor();
            this.Accept(v);
            watch.Start();

            Task[] r = new Task[file_reg_archive.Count];
            int n = 0;

            foreach (string t in file_reg_archive)
            {

                r[n] = Task.Run(() =>
                {
                    ArchiveFile(t);
                });
                n++;
            }

            Task.WaitAll(r);

            watch.Stop();
            file_reg_archive.Clear();
        }
    }
}
