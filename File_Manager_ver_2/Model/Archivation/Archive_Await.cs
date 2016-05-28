using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class Archive_Await : Archivation_Abstract
    {

        public Archive_Await(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { }

        public async override void try_archivation()  //нужен ли async?
        {

            Visitor v = new Visitor();
            this.Accept(v);

            watch.Start();
            foreach (string FILE in file_reg_archive)
            {

                await Task.Run(() =>
                {
                    ArchiveFile(FILE);
                });
            }

            watch.Stop();
            file_reg_archive.Clear();
        }
    }
}
