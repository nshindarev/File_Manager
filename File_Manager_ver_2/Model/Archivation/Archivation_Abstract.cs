using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public abstract class Archivation_Abstract
    {


        public void Accept(Visitor visitor)
        {
            visitor.VisitArchivation(this);
        }
        /*data inf from main form*/
        /*copied from form archive*/
        public string SelectedItem;
        public string CurrentDir;
        public int SelectedIndex;

        public Archivation_Abstract(string CurrentDir, string SelectedItem, int SelectedIndex)
        {
            this.CurrentDir = CurrentDir;
            this.SelectedIndex = SelectedIndex;
            this.SelectedItem = SelectedItem;
        }

        // saves files in selected Directory
        public List<string> file_reg_archive = new List<string>();
        // stopwatch for time count
        public Stopwatch watch = new Stopwatch();

        public void FindInformation()
        {
            /*метод смотрит: файл  -> добавляем в лист
                             папка -> cd = dirinfo (куда ткнули)*/

            if (MyDirectory.Exists(CurrentDir + SelectedItem.ToString()))
            {
                IDirectory cd;
                if (SelectedIndex == -1)
                {
                    cd = new MyDirectory(CurrentDir);
                }


                else
                {
                    cd = new MyDirectory(CurrentDir + SelectedItem.ToString());
                }



                // Пробег по папкам и сбор имен всех файлов
                // Как итог имеем Лист полный названий всяких файлов

                GoDirs(cd);

            }
            else
            {
                MyFile cd;
                cd = new MyFile(CurrentDir + SelectedItem.ToString());
                file_reg_archive.Add(CurrentDir + SelectedItem.ToString());
            }
        }
        // вспомогательный для findinfo()
        public void GoDirs(IDirectory currentDir)
        {
            List<IDirectory> dirs = currentDir.GetDirectories().ToList();
            List<IFile> files = currentDir.GetFiles().ToList();
            try
            {
                if (dirs.Count != 0)
                    foreach (IDirectory dir in dirs)
                        GoDirs(dir);


                foreach (IFile file in files)
                    file_reg_archive.Add(file.FullName);
                // CurrentDir.Text + ListBox.SelectedItem.ToString()+ @"\" + file.Name
            }
            catch (UnauthorizedAccessException) { }
        }

        /*метод архивации одного/группы файлов*/
        public void ArchiveFile(string sourceFile)
        {
            string destFile;
            bool compress;
            IEntry source = FileFactory.CreateEntry(sourceFile);
            // Определяемся с операцией архивирования/ обратной операции
            if (source is MyZip)
            {
                destFile = Path.GetFileNameWithoutExtension(sourceFile);
                string temp = Path.GetExtension(destFile);
                destFile = Path.GetFileNameWithoutExtension(destFile);
                destFile = CurrentDir + destFile + "Unzipped" + temp;

                compress = false;
            }

            else
            {
                destFile = sourceFile + ".zip";
                compress = true;
            }

            const int BufferSize = 16384;
            byte[] buffer = new byte[BufferSize];

            try
            {


                using (Stream inFileStream = File.Open(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outFileStream = File.Open(destFile, FileMode.Create, FileAccess.Write, FileShare.None))
                using (GZipStream gzipStream = new GZipStream(
                    compress ? outFileStream : inFileStream,
                    compress ? CompressionMode.Compress : CompressionMode.Decompress))
                {
                    Stream inStream = compress ? inFileStream : gzipStream;
                    //compress true работаем со вторым файлом
                    //если false то с infile
                    Stream outStream = compress ? gzipStream : outFileStream;
                    int bytesRead = 0;
                    do
                    {
                        bytesRead = inStream.Read(buffer, 0, BufferSize);
                        outStream.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead > 0);
                }
            }

            catch (UnauthorizedAccessException)
            {
                // System.Windows.Forms.MessageBox.Show("Отказано в доступе!");
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show("Ошибка!");
            }
        }
        public void ArchiveFile(List<string> sourceFiles)
        {
            foreach (string sourceFile in sourceFiles)
            {
                ArchiveFile(sourceFile);
            }
        }

        public abstract void try_archivation();
    }
}
