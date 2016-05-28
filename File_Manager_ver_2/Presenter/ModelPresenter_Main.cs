using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;


namespace File_Manager_ver_2
{  
    
    public class ModelPresenter_Main
    {
        public IEntry currentDirectory;
        private string Copied;
        private string CopyName;
        
        INavigation navigationView;
        IMainAction ActionView;

        private IEntry copySource;
        private string copiedEntryName;

        // конструктор
        public ModelPresenter_Main (INavigation NavigationView, IMainAction ActionView)
        {
            this.navigationView = NavigationView;
            this.ActionView = ActionView;
            this.currentDirectory = FileFactory.CreateEntry("C:\\");

            NavigationView.MouseDoubleClick += new EventHandler<MainFormEventArgs>(OnMouseDoubleClick);


            ActionView.DeleteFile += new EventHandler<MainFormEventArgs>(OnFileDelete);
            ActionView.RenameFile += new EventHandler<MainFormEventArgs>(OnFileRename);
            ActionView.InsertFile += new EventHandler<MainFormEventArgs>(OnFileInsert);
            ActionView.CopyFile +=  new EventHandler<MainFormEventArgs>(OnFileCopy);

            ActionView.FindInfoAwait += new EventHandler<MainFormEventArgs>(OnFindInfoAwait);
            ActionView.FindInfoDelegate += new EventHandler<MainFormEventArgs>(OnFindInfoDelegate);
            ActionView.FindInfoParallel += new EventHandler<MainFormEventArgs>(OnFindInfoParallel);
            ActionView.FindInfoThread += new EventHandler<MainFormEventArgs>(OnFindInfoThread);
            ActionView.allFindInfo += new EventHandler<MainFormEventArgs>(OnZipFindInfo);

            ActionView.ArchiveAwait += new EventHandler<MainFormEventArgs>(OnArchiveAwait);
            ActionView.ArchiveDelegate += new EventHandler<MainFormEventArgs>(OnArchiveDelegate);
            ActionView.ArchiveParallel += new EventHandler<MainFormEventArgs>(OnArchiveParallel);
            ActionView.ArchiveTask += new EventHandler<MainFormEventArgs>(OnArchiveTask);
            ActionView.ArchiveThread += new EventHandler<MainFormEventArgs>(OnArchiveThread);

            ActionView.PlinqStatistics += new EventHandler<MainFormEventArgs>(OnPlinqStatistics);
            OpenedDirectory(NavigationView.ICurrentDir);
        }

       
    
        public void OpenedDirectory(string curdirfullname)
        {
            navigationView.ClearItems();
            
             
    
                                                                               
                IDirectory curdir;
                ICollection<IDirectory> dirs;
                ICollection<IFile> files;

                //NavigationView.IListBox.Clear();
                navigationView.IListBox = new List<string>();
                curdir = new MyDirectory(curdirfullname);
            
                dirs = curdir.GetDirectories();
                files = curdir.GetFiles();

                navigationView.ICurrentDir = curdir.FullName;

                if (curdir.FullName.Length > 3) navigationView.IListBox.Add("..");

                foreach (MyDirectory dir in dirs) navigationView.IListBox.Add(dir.Name);
                foreach (MyFile file in files)    navigationView.IListBox.Add(file.Name);

                navigationView.Improve_Changes();
          
        }
        public void OnMouseDoubleClick(object sender, MainFormEventArgs e)
        {
            if (e.Item.Contains(".sys")) { }
            if (e.Item != "..")
            {
                IEntry selectedEntry = FileFactory.CreateEntry(e.CurrentDir + @"\" + e.Item);
                if (selectedEntry is IDirectory)
                {
                    currentDirectory = (IDirectory)selectedEntry;
                    navigationView.RefreshCurrentDirectoryEntries();
                }
                else // if (selectedEntry is IFile)
                    Process.Start(selectedEntry.FullName);
            }
            else
            {
                IEntry selectedEntry = FileFactory.CreateEntry(e.CurrentDir);
                if (selectedEntry is IDirectory)
                {
                    currentDirectory = (IDirectory)selectedEntry;
                    navigationView.RefreshCurrentDirectoryEntries();
                }
                else // if (selectedEntry is IFile)
                    Process.Start(selectedEntry.FullName);
            }
            navigationView.OnDeleted = false;

          
        }
      
        

        public void OnFileDelete(object sender, MainFormEventArgs e)
        {
            View.RenameDeleteForm rd = new View.RenameDeleteForm();
            rd.lblAreUSure.Visible = true;
            rd.txtBoxNewName.Visible = false;
            rd.LblNewName.Visible = false;
            rd.lblAreUSure.Font = new Font("SanSerif", 12, FontStyle.Underline);

            if (rd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IEntry entry = FileFactory.CreateEntry(e.CurrentDir + @"\" + e.Item);
                    if (entry is IDirectory)
                        MyDirectory.Delete(entry.FullName);
                    else
                        MyFile.Delete(entry.FullName);

                    navigationView.OnDeleted = true;
                    navigationView.RefreshCurrentDirectoryEntries_1();
                }
                catch (Exception ex)
                {
                    throw new MyException("Отказано в доступе", ex);
                }
            }
        }
        public void OnFileRename(object sender, MainFormEventArgs e)
        {
            View.RenameDeleteForm rd = new View.RenameDeleteForm();
            if (rd.ShowDialog() == DialogResult.OK)
            {
                string newEntry = currentDirectory.FullName+@"\"+rd.txtBoxNewName.Text;
                IEntry oldEntry = FileFactory.CreateEntry(currentDirectory.FullName + @"\");
                if (oldEntry is IDirectory)
                {
                    InsertDirectory((IDirectory)oldEntry, newEntry);
                    MyDirectory.Delete(oldEntry.FullName);
                }
                else
                {
                    InsertFile((IFile)oldEntry, newEntry);
                    MyFile.Delete(oldEntry.FullName);
                }
                navigationView.RefreshCurrentDirectoryEntries();
            }
            
            
           
            
          
         /*       string target = e.CurrentDir + e.Item;
               if (MyDirectory.Exists(target))
                    Directory.Move(target, e.CurrentDir + rd.txtBoxNewName.Text);
                else
                    File.Move(target, e.CurrentDir + rd.txtBoxNewName.Text);

                rd.Close();
                rd = null;
                OpenedDirectory(e.CurrentDir);

           */ 
        }
        public void OnFileInsert(object sender, MainFormEventArgs e)
        {
            try
            {
                if (copySource is MyDirectory) InsertDirectory(new MyDirectory(copySource.FullName), currentDirectory.FullName + @"\" + e.Item + @"\" + copiedEntryName + @"\");
                else
                    InsertFile(new MyFile(copySource.FullName), currentDirectory.FullName + @"\" + e.Item + @"\" + copiedEntryName);
                navigationView.RefreshCurrentDirectoryEntries();
            }
            catch (Exception ex)
            {
                throw new MyException("Ошибка в Insert", new Exception());
            }
          
            
            /*
            if (MyDirectory.Exists(Copied))
                InsertFolder(new MyDirectory(Copied), e.CurrentDir + CopyName + @"\");
            else
                InsertFile(new MyFile(Copied), e.CurrentDir + CopyName);

            OpenedDirectory(e.CurrentDir);*/
        }
        public void OnFileCopy(object sender, MainFormEventArgs e) 
        {
            copySource = FileFactory.CreateEntry(e.CurrentDir+@"\"+e.Item);
            copiedEntryName = "CopyOf" + MyFile.GetFileNameWithoutExtension(FileFactory.CreateEntry(e.CurrentDir + @"\" + e.Item).Name) +
                MyFile.GetExtension(FileFactory.CreateEntry(e.CurrentDir + @"\" + e.Item).Name);
            /*Copied = e.CurrentDir + e.Item;
            CopyName = "CopyOf" + e.Item;
            OpenedDirectory(e.CurrentDir);*/
        }


        private void InsertFolder(MyDirectory folder, string newfolderfullname)
        {
            try
            {
                ICollection<IDirectory> dirs = folder.GetDirectories();
                ICollection<IFile> files = folder.GetFiles();

                MyDirectory.Create(newfolderfullname);

                foreach (MyDirectory dir in dirs)
                    InsertFolder(dir, newfolderfullname + dir.Name + @"\");
                foreach (MyFile file in files)
                    InsertFile(file, newfolderfullname + file.Name);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Отказано в доступе!");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!");
            }
        }
        private static void InsertFile(IFile file, string newFileFullName)
        {
            new MyFile(newFileFullName).Create(file.Open());
        }
        private static void InsertDirectory(IDirectory folder, string newDirectoryFullName)
        {
            MyDirectory[] dirs = folder.GetItems().OfType<MyDirectory>().ToArray();
            MyFile[] files = folder.GetItems().OfType<MyFile>().ToArray();

            new MyDirectory(newDirectoryFullName).Create();

            foreach (MyDirectory dir in dirs)
                InsertDirectory(dir, newDirectoryFullName + dir.Name + @"\");
            foreach (MyFile file in files)
                InsertFile(file, newDirectoryFullName + file.Name);
        }


        public void OnFindInfoAwait (object sender, MainFormEventArgs e) 
        {
            try
            {
                FindInfoAwait FIA = new FindInfoAwait(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                FIA.try_findinfo();
                MessageBox.Show("Поиск завершён!\nВся найденная информация сохранена в файле:\n");
                OpenedDirectory(e.CurrentDir);

                //  txtThreadTimer.Text = TH.watch.Elapsed.ToString();
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        }
        public void OnFindInfoDelegate (object sender, MainFormEventArgs e)
        {
            try
            {
                FindInfoDelegate FIDE = new FindInfoDelegate(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                FIDE.try_findinfo();
                MessageBox.Show("Поиск завершён!\nВся найденная информация сохранена в файле:\n");
                OpenedDirectory(e.CurrentDir);

                //  txtThreadTimer.Text = TH.watch.Elapsed.ToString();
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        
        }
        public void OnFindInfoParallel (object sender, MainFormEventArgs e)
        {
            try
            {
                FindInfoParallel FIPA = new FindInfoParallel(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                FIPA.try_findinfo();
                MessageBox.Show("Поиск завершён!\nВся найденная информация сохранена в файле:\n");
                OpenedDirectory(e.CurrentDir);

                //  txtThreadTimer.Text = TH.watch.Elapsed.ToString();
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }

        }
        public void OnFindInfoThread (object sender, MainFormEventArgs e)
        {
            try
            {
                FindInfoThread FITHR = new FindInfoThread(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                FITHR.try_findinfo();
                MessageBox.Show("Поиск завершён!\nВся найденная информация сохранена в файле:\n");
                OpenedDirectory(e.CurrentDir);

                //  txtThreadTimer.Text = TH.watch.Elapsed.ToString();
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        
        }
        public void OnZipFindInfo (object sender, MainFormEventArgs e) 
        {
            try
            {
                ZipFindInfo ZFI = new ZipFindInfo(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                ZFI.try_findinfo();
                MessageBox.Show("Поиск завершён!\nВся найденная информация сохранена в файле:\n");
                navigationView.RefreshCurrentDirectoryEntries_1();

                //  txtThreadTimer.Text = TH.watch.Elapsed.ToString();
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        }
       



        public void OnArchiveAwait (object sender, MainFormEventArgs e)
        {
            try
            {
                navigationView.IArchivationLength = "";
                Archive_Await AA = new Archive_Await(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                AA.try_archivation();

                navigationView.IArchivationLength = AA.watch.Elapsed.ToString();
                navigationView.RefreshCurrentDirectoryEntries_1();

            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
       
        }
        public void OnArchiveDelegate (object sender, MainFormEventArgs e)
        {
            try
            {
                navigationView.IArchivationLength = "";
                Archive_Delegate AD = new Archive_Delegate(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                AD.try_archivation();
                navigationView.IArchivationLength = AD.watch.Elapsed.ToString();
                navigationView.RefreshCurrentDirectoryEntries_1();

            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        
        }
        public void OnArchiveParallel (object sender, MainFormEventArgs e)
        {
            try
            {
                navigationView.IArchivationLength = "";
                Archive_Parallel AP = new Archive_Parallel(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                AP.try_archivation();
                navigationView.IArchivationLength = AP.watch.Elapsed.ToString();
                navigationView.RefreshCurrentDirectoryEntries_1();
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        
        }
        public void OnArchiveTask (object sender, MainFormEventArgs e)
        {
            try
            {
                navigationView.IArchivationLength = "";
                Archive_Task AT = new Archive_Task(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                AT.try_archivation();
                navigationView.IArchivationLength = AT.watch.Elapsed.ToString();
                navigationView.RefreshCurrentDirectoryEntries_1();

            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        
        }
        public void OnArchiveThread (object sender, MainFormEventArgs e)
        {
            try
            {
                navigationView.IArchivationLength = "";
                Archive_Thread TH = new Archive_Thread(e.CurrentDir, e.Item, Convert.ToInt16(e.Index));
                TH.try_archivation();
                navigationView.IArchivationLength = TH.watch.Elapsed.ToString();
                navigationView.RefreshCurrentDirectoryEntries_1();

            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка!\n" + ex.Message); }
        
        }
 
        
        public static void OnPlinqStatistics(object sender, MainFormEventArgs e)
        {
            TextStatistics TS = new TextStatistics(e.Index, e.Item, e.CurrentDir);
            ModelPresenterStatistics mp_statistics = new ModelPresenterStatistics (TS);
            
            TS.ShowDialog();
        }
        }
}



