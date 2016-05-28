using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public abstract class FindInfo_Abstract : IFindInfo
    {

        public void Accept(Visitor visitor)
        {
            visitor.VisitFindInfo(this);
        }
        //public string FileForRegex;

        public string CurrentDir;
        public string SelectedItem;
        public int SelectedIndex;


        public FindInfo_Abstract(string CurrentDir, string SelectedItem, int SelectedIndex)
        {
            this.CurrentDir = CurrentDir;
            this.SelectedIndex = SelectedIndex;
            this.SelectedItem = SelectedItem;
        }


        // здесь будем хранить список файлов
        public List<string> file_reg_archive = new List<string>();


        // создаем массив, в котором будем хранить найденную всеми потоками информацию 
        public List<string>[] data = new List<string>[]
           {
               new List<string> { }, // Мылохранилище
               new List<string> { }, // FTP
               new List<string> { }  // Vk
           };

        // Шаблоны поиска
        public Regex[] regexes = new Regex[]
          {
            new Regex(@"[-a-zA-Z0-9\._\+]+ \@ [-a-zA-Z0-9\._]+ \. [a-zA-Z]{2,6} \.?",  RegexOptions.IgnorePatternWhitespace),  // Mail
            new Regex(@"[0-9]{3} \- [0-9]{3} \- [0-9]{3} \s [0-9]{2} ",                RegexOptions.IgnorePatternWhitespace),  // INN
            new Regex(@"vk\.com\/ [a-zA-Z0-9] [a-zA-Z0-9_]+ [a-zA-Z0-9]",              RegexOptions.IgnorePatternWhitespace)   // Vk
          };


        public void FindInformation()
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
        } // имя -> dirinfo + GoDirs();
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
        }// собирает в список все подпапки   
        public void FindInfoInFile(List<string> filesFullName)
        {
            foreach (string file in filesFullName) FindInfoInFile(file);
        }
        public void FindInfoInFile(string fileFullName)
        {

            StreamReader f = null;
            try
            {

                // Читаем исходный файл
                f = File.OpenText(fileFullName);
                string curstring;

                do
                {
                    curstring = f.ReadLine();

                    // Сравниваем со всеми шаблонами по порядку
                    for (int i = 0; i < regexes.Length; i++)
                    {
                        // В случае с пустым файлом, чтобы избежать ошибок, смотрим Matches не с curstring, а с ""
                        // Если curstring == null

                        Match m = regexes[i].Match(curstring == null ? "" : curstring);
                        while (m.Value != "")
                        {
                            data[i].Add(m.Value);
                            m = m.NextMatch();
                        }
                    }

                }
                while (!f.EndOfStream);
            }

               


            catch (UnauthorizedAccessException) { }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.ToString()); }

        }
        public void FileWrite(string file_name)
        {
            string fn = file_name + ".txt";
            StreamWriter f = new StreamWriter(@"C:\ForFileManager\" + file_name + ".txt");
            file_name += Path.GetRandomFileName();
            f.WriteLine("Найденные адреса электронной почты:");
            foreach (string s in data[0])
                f.WriteLine(s);
            f.WriteLine("\r\nНайденные INN:");
            foreach (string s in data[1])
                f.WriteLine(s);
            f.WriteLine("\r\nНайденные адреса страниц ВКонтакте:");
            foreach (string s in data[2])
                f.WriteLine(s);
            f.Close();
            //MessageBox.Show("Поиск завершён!\nВся найденная информация сохранена в файле:\n"
            //    + fn);

        }

        public abstract void try_findinfo();
    }
}
