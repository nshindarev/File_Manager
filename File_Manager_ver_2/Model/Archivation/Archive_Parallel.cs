using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class Archive_Parallel : Archivation_Abstract
    {

        public Archive_Parallel(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { }


        public override void try_archivation()
        {

            Visitor v = new Visitor();
            this.Accept(v);
            watch.Start();
            Parallel.ForEach(file_reg_archive, file =>
            {
                ArchiveFile(file);
            });
            watch.Stop();
            file_reg_archive.Clear();
        }
    }
}
