using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class Archive_Thread : Archivation_Abstract
    {

        public Archive_Thread(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { }
        public override void try_archivation()
        {

            Visitor v = new Visitor();
            this.Accept(v);
            watch.Start();

            // Создаем потоки, которые будут архивировать все файлы в папке
            ArchiveThread[] at = new ArchiveThread[Environment.ProcessorCount];
            for (int i = 0; i < at.Length; i++)
            {
                at[i] = new ArchiveThread(file_reg_archive, i, CurrentDir);
            }

            // запускаем потоки
            for (int i = 0; i < at.Length; i++)
            {
                at[i].Start();
            }

            for (int i = 0; i < at.Length; i++)
            {
                at[i].End();
            }
            watch.Stop();
            file_reg_archive.Clear();
        }
    }
}
