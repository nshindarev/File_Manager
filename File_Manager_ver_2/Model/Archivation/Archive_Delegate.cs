using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class Archive_Delegate : Archivation_Abstract
    {

        int EndedDelegatesCounter = 0;
        // показывает, какой тип делегата мы будем использовать
        public delegate void ArchiveDelegate(List<string> fileinfo);
        // хранит 4 списка всех файлов директории
        List<string>[] four_file_reg_archive = new List<string>[Environment.ProcessorCount];

        public Archive_Delegate(string CurrentDir, string SelectedItem, int SelectedIndex) : base(CurrentDir, SelectedItem, SelectedIndex) { }


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


        public void EndFunc(IAsyncResult result)
        {

            EndedDelegatesCounter++;
            if (EndedDelegatesCounter == Environment.ProcessorCount)
            {

                file_reg_archive.Clear();
            }
        }
        public override void try_archivation()
        {

            Visitor v = new Visitor();
            this.Accept(v);

            watch.Start();


            IDelegates();

            // имеем на данный момент files_for_regex полный файлов, которые нужно архивировать
            ArchiveDelegate[] Delegates = new ArchiveDelegate[Environment.ProcessorCount];
            for (int i = 0; i < Delegates.Length; i++)
            {
                Delegates[i] = new ArchiveDelegate(ArchiveFile);
            }


            // имеем массив делегатов, где каждый ссылается на метод ArchiveFile
            // создадим массив из объектов, реализующих класс IAsyncResult для использования асинхронных делегатов
            IAsyncResult[] AsyncArchive = new IAsyncResult[Environment.ProcessorCount];
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                AsyncArchive[i] = Delegates[i].BeginInvoke(four_file_reg_archive[i], new AsyncCallback(EndFunc), null);
            }


            watch.Stop();
            file_reg_archive.Clear();
        }
    }
}
