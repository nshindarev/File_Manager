using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager_ver_2
{
    public partial class MainForm : Form, INavigation, IMainAction
    {
        // поля для IListBox и ICurrentDir
        public List<string> _IListBox;
        public string  _ICurrentDir;
        public bool OnDeleted { get { return ondeleted; } set { ondeleted = value; } }
        public bool ondeleted = false;
        // реализация интерфейса INavigation, IMainAction
        public List<string> IListBox
        {
            get
            {
                return _IListBox;
            }
            set
            {
                _IListBox = value;
            }
        }
        public string  ICurrentDir
        {
            get
            {
                return CurrentDir.Text;
            }
            set
            {
                this.CurrentDir.Text = value;
                _ICurrentDir = value;
            }
        }
        public string IArchivationLength
        {
            get
            {
                return this.txtboxArchivation.Text;
            }
            set
            {
                this.txtboxArchivation.Text = value;
            }
        }

      //  public event  EventHandler<MainFormEventArgs> SelectedIndexChanged;
        public event  EventHandler<MainFormEventArgs> DeleteFile;
        public event  EventHandler<MainFormEventArgs> RenameFile;
        public event  EventHandler<MainFormEventArgs> CopyFile;
        public event  EventHandler<MainFormEventArgs> InsertFile;


        public event EventHandler<MainFormEventArgs> FindInfoThread;
        public event EventHandler<MainFormEventArgs> FindInfoParallel;
        public event EventHandler<MainFormEventArgs> FindInfoAwait;
        public event EventHandler<MainFormEventArgs> FindInfoDelegate;
        public event EventHandler<MainFormEventArgs> allFindInfo;

        public event EventHandler<MainFormEventArgs> ArchiveThread;
        public event EventHandler<MainFormEventArgs> ArchiveDelegate;
        public event EventHandler<MainFormEventArgs> ArchiveParallel;
        public event EventHandler<MainFormEventArgs> ArchiveTask;
        public event EventHandler<MainFormEventArgs> ArchiveAwait;

        public event EventHandler<MainFormEventArgs> PlinqStatistics;
        public event EventHandler<MainFormEventArgs> ThreadArchive;
        public new event EventHandler<MainFormEventArgs> MouseDoubleClick;
        
        // конструктор
        public MainForm()
        {
            InitializeComponent();
            ICurrentDir = @"C:\\";
        }
        public IDirectory Parent
        {
            get
            {
                int index = CurrentDir.Text.LastIndexOf('\\');
                string parentFullName = CurrentDir.Text;
                if (CurrentDir.Text[index - 1] != ':') parentFullName = CurrentDir.Text.Remove(index);
                else parentFullName = "C:\\";
                return (IDirectory)FileFactory.CreateEntry(parentFullName);

                // return (IDirectory)FileSystemFactory.CreateEntry(this.CurrentDir.Text);
            }
        }
        public IDirectory GetCurrentDirectory
        {
            get 
            {
                return (IDirectory)FileFactory.CreateEntry(this.CurrentDir.Text);
            }
        }
        public void RefreshCurrentDirectoryEntries()
        {
            if (ListBox.SelectedItem.ToString() != "..")
            {
                if (CurrentDir.Text == "C:\\") this.CurrentDir.Text += ListBox.SelectedItem.ToString();
                else this.CurrentDir.Text += @"\"+ListBox.SelectedItem.ToString() ;
                this.ListBox.Items.Clear();

                IEntry[] entries;
                if (OnDeleted) entries = this.Parent.GetItems().ToArray();
                else           entries = this.GetCurrentDirectory.GetItems().ToArray();
                ListBox.Items.Add(".."); // Даём возможность перейти на более высокий уровень
                foreach (IEntry entry in entries) ListBox.Items.Add(entry.Name);
        
            }
            else
            {
                
                this.ListBox.Items.Clear();
                IEntry[] entries = this.Parent.GetItems().ToArray();
                ListBox.Items.Add(".."); // Даём возможность перейти на более высокий уровень
                this.CurrentDir.Text = Parent.FullName;
                foreach (IEntry entry in entries) ListBox.Items.Add(entry.Name);
            }
            
        }

        public void RefreshCurrentDirectoryEntries_1()
        {
            if (ListBox.SelectedItem.ToString() != "..")
            {
                
                this.ListBox.Items.Clear();

                IEntry[] entries;
              //  if (OnDeleted)
               //     entries = this.Parent.GetItems().ToArray();
               // else 
                    entries = this.GetCurrentDirectory.GetItems().ToArray();
                ListBox.Items.Add(".."); // Даём возможность перейти на более высокий уровень
                foreach (IEntry entry in entries) ListBox.Items.Add(entry.Name);

            }
        
        }
        private void ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListBox.SelectedItem.ToString() != "..")
            {
                if (MouseDoubleClick != null)
                    MouseDoubleClick(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                                   this.ListBox.SelectedItem.ToString(),
                                                                   this.CurrentDir.Text));
            }
            else
            {
                if (MouseDoubleClick != null)
                    MouseDoubleClick(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                                   this.ListBox.SelectedItem.ToString(),
                                                                   this.Parent.FullName));
            }
     
            // далее нужно приравнять поля ModelPresenter и элементы формы

        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (CopyFile != null)
                CopyFile(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                                this.ListBox.SelectedItem.ToString(),
                                                                this.CurrentDir.Text));
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (InsertFile != null)
                InsertFile(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                          this.ListBox.SelectedItem.ToString(),
                                                          this.CurrentDir.Text));
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            
            if (RenameFile != null)
                RenameFile(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                          this.ListBox.SelectedItem.ToString(),
                                                          this.CurrentDir.Text));
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteFile != null)
                DeleteFile(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                                this.ListBox.SelectedItem.ToString(),
                                                                this.CurrentDir.Text));
        }


        // вспомогательные методы для работы с элементами формы
        // внутри Presenter и Model
        public void Improve_Changes()
        {
            foreach (string item in IListBox) this.ListBox.Items.Add(item);
            // this.CurrentDir = 
        }
        public void ClearItems()
        {
            this.ListBox.Items.Clear();
        }
        // методы по архивации и поиску информации внутри файлов
        private void btnFindInfoParallel_Click(object sender, EventArgs e)
        {
            if (FindInfoParallel != null)
                FindInfoParallel(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                             this.ListBox.SelectedItem.ToString(),
                                                             this.CurrentDir.Text));
        }
        private void btnFindInfoThread_Click(object sender, EventArgs e)
        {
            if (FindInfoThread != null)
                FindInfoThread(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                           this.ListBox.SelectedItem.ToString(),
                                                           this.CurrentDir.Text));
        }
        private void btnFindInfoAwait_Click(object sender, EventArgs e)
        {
            if (FindInfoAwait != null)
                FindInfoAwait(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                          this.ListBox.SelectedItem.ToString(),
                                                          this.CurrentDir.Text));
        }
        private void btnFindInfoDelegate_Click(object sender, EventArgs e)
        {
            if (FindInfoDelegate != null)
                FindInfoDelegate(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                             this.ListBox.SelectedItem.ToString(),
                                                             this.CurrentDir.Text));
        }
      
        private void btnParallelArchive_Click(object sender, EventArgs e)
        {
            if (ArchiveParallel != null)
                ArchiveParallel(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                            this.ListBox.SelectedItem.ToString(),
                                                            this.CurrentDir.Text));
        }
        private void btnTaskArchive_Click(object sender, EventArgs e)
        {
            if (ArchiveTask != null)
                ArchiveTask(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                        this.ListBox.SelectedItem.ToString(),
                                                        this.CurrentDir.Text));
        }
        private void btnThreadArchive_Click(object sender, EventArgs e)
        {
            if (ArchiveThread != null)
                ArchiveThread(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                          this.ListBox.SelectedItem.ToString(),
                                                          this.CurrentDir.Text));
        }
        private void btnDelegateArchive_Click(object sender, EventArgs e)
        {
            if (ArchiveDelegate != null)
                ArchiveDelegate(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                            this.ListBox.SelectedItem.ToString(),
                                                            this.CurrentDir.Text));
        }
        private void btnAwaitArchive_Click(object sender, EventArgs e)
        {
            if (ArchiveAwait != null)
                ArchiveAwait(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                          this.ListBox.SelectedItem.ToString(),
                                                          this.CurrentDir.Text));
        }
        private void btnPlinqStatistics_Click(object sender, EventArgs e)
        {
            if (PlinqStatistics != null)
                PlinqStatistics(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                            this.ListBox.SelectedItem.ToString(),
                                                            this.CurrentDir.Text));
        }

        private void findInfoZipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (allFindInfo != null)
                allFindInfo(this, new MainFormEventArgs(this.ListBox.SelectedIndex.ToString(),
                                                            this.ListBox.SelectedItem.ToString(),
                                                            this.CurrentDir.Text));
        }
     }
}

