using System;
using System.Collections.Generic;
using System.IO;
//using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class ZipFindInfo 
    {
         string FileForRegex;
         string CurrentDir;
         int SelectedIndex;
         string SelectedItem;
         public ZipFindInfo(string CurrentDir, string SelectedItem, int SelectedIndex)
        {
            this.CurrentDir = CurrentDir;
            this.SelectedIndex = SelectedIndex;
            this.SelectedItem = SelectedItem;
            FileForRegex = "Regex";
        }


        // здесь будем хранить список файлов внутри зипа
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
        
        public void try_findinfo()
        {
            IEntry to_find_info_in_zip;
            if (SelectedIndex == -1)
            {
                to_find_info_in_zip = FileFactory.CreateEntry(CurrentDir );

            }
            else
            {
                to_find_info_in_zip = FileFactory.CreateEntry(CurrentDir + @"\" + SelectedItem);

            }
            // to_find_info_in_zip is IEntry
            GoDirs(to_find_info_in_zip);
            
            FindInfoInFile(file_reg_archive);
            
         //    
           FileWrite(FileForRegex);
        }
       /* public void FindInformation()
        {
            IEntry to_find_info_in = FileSystemFactory.CreateEntry(CurrentDir + @"\" + SelectedItem);
            if (to_find_info_in is IDirectory)
            {
                GoDirs((IDirectory)to_find_info_in);
            }
            /*IDirectory cd;
            if (SelectedIndex == -1)
            {
                cd = new IDirectory(CurrentDir);
            }


            else
            {
                cd = new IDirectory(CurrentDir + SelectedItem.ToString());
            }
            */

        // Пробег по папкам и сбор имен всех файлов
            // Как итог имеем Лист полный названий всяких файлов

           // GoDirs(cd);
         // имя -> dirinfo + GoDirs();*/
        public void GoZIP(MyZip currentDir) 
        {
            file_reg_archive.Add(currentDir.FullName);
        }
     /*   public void GoDirs(MyZip currentDir)
        {   
         
            List<IEntry> files_and_dirs_in_zip = currentDir.GetAllItems().ToList();
            List<IFile> files_in_zip = new List<IFile>();
            foreach (IEntry x in files_and_dirs_in_zip)
                if (x is IFile) files_in_zip.Add((IFile)x);
           
            foreach (IFile file in files_in_zip)
                 file_reg_archive.Add(file.FullName);
                // CurrentDir.Text + ListBox.SelectedItem.ToString()+ @"\" + file.Name
            
        }// собирает в список все подпапки   */
        public void GoDirs(IEntry selectedEntry)
        {
            if (selectedEntry is IDirectory) 
            {
                if (selectedEntry is MyZip)
                {
                    List<IEntry> files_and_dirs_in_zip = ((MyZip)selectedEntry).GetAllItems().ToList();
                    List<IFile>  files_in_zip = new List<IFile>();
                    foreach (IEntry x in files_and_dirs_in_zip)
                        if (x is IFile) files_in_zip.Add((IFile)x);

                    foreach (IFile file in files_in_zip)
                        file_reg_archive.Add(file.FullName);
                }
                else if (selectedEntry is MyDirectory)
                {
                    List<IDirectory> files_and_dirs = ((MyDirectory)selectedEntry).GetDirectories().ToList();
                    List<IFile>      files_in_zip   = new List<IFile>();

                    foreach (IDirectory x in files_and_dirs)
                        GoDirs(x);

                    foreach (IFile file in files_in_zip)
                        file_reg_archive.Add(file.FullName);
                }
                
            }
            else
            {
                if (selectedEntry is MyFile)
                {
                    file_reg_archive.Add(selectedEntry.FullName);
                }
            }
           
            // CurrentDir.Text + ListBox.SelectedItem.ToString()+ @"\" + file.Name

        }// собирает в список все подпапки 
        
        public void FindInfoInFile(List<string> filesFullName)
        {
            foreach (string file in filesFullName) FindInfoInFile(file);
        }
        public void FindInfoInFile(string fileFullName)
        {
          
                IEntry file_to_open =  FileFactory.CreateEntry(fileFullName);
                IFile FILE = (IFile)file_to_open;

                byte[] b = FILE.Open();
               // string f = System.Text.Encoding.UTF8.GetString(b);
                string f = BytesToString(b, Encoding.Default);
                string curstring = f;


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
                       

        public  string BytesToString(byte[] b, Encoding encoding)
        {
            return encoding.GetString(b);
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
    }
}
