using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class ArchiveThread
    {
        Thread th;
        int Thread_ID;
        List<string> ls;
        string DirectiveName;

        public void Start()
        {
            th.Start();
        }
        public void End()
        {
            th.Join();

        }
        public ArchiveThread(List<string> ls, int Thread_ID, string DirectiveName)
        {
            this.DirectiveName = DirectiveName;
            this.ls = ls;
            this.Thread_ID = Thread_ID;
            th = new Thread(new ThreadStart(Archive));
        }
        public void Archive()
        {
            // цикл вызываем столько раз, сколько у нас файлов
            // внутри цикла ищем повторения
            // заодно равномерно распределили файлы по всем потокам

            if ((Thread_ID == 0) && (ls.Count == 1)) ArchiveFile(ls[0]);
            for (int i = Thread_ID; i < (ls.Count); i += Environment.ProcessorCount)
            {
                ArchiveFile(ls[i]);
            }
        }
        public void ArchiveFile(string sourceFile)
        {
            string destFile;
            bool compress;
            IEntry _sourceFile = FileFactory.CreateEntry(sourceFile);
            // Определяемся с операцией архивирования/ обратной операции
            if (_sourceFile is MyZip)
            {
                destFile = Path.GetFileNameWithoutExtension(sourceFile);
                string temp = Path.GetExtension(destFile);
                destFile = Path.GetFileNameWithoutExtension(destFile);
                destFile = DirectiveName + destFile + "Unzipped" + temp;

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
                System.Windows.Forms.MessageBox.Show("Отказано в доступе!");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка!");
            }
        }
    }
}
