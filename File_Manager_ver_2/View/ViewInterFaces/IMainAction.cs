using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
   public interface IMainAction
    {
        List<string> IListBox { get; set; }
        string ICurrentDir { get; set; }

        void Improve_Changes();
        void ClearItems();

        event EventHandler<MainFormEventArgs> DeleteFile;
        event EventHandler<MainFormEventArgs> RenameFile;
        event EventHandler<MainFormEventArgs> CopyFile;
        event EventHandler<MainFormEventArgs> InsertFile;

        event  EventHandler<MainFormEventArgs> FindInfoThread;
        event  EventHandler<MainFormEventArgs> FindInfoParallel;
        event  EventHandler<MainFormEventArgs> FindInfoAwait;
        event  EventHandler<MainFormEventArgs> FindInfoDelegate;
        event  EventHandler<MainFormEventArgs> allFindInfo;

        event  EventHandler<MainFormEventArgs> ArchiveThread ;
        event  EventHandler<MainFormEventArgs> ArchiveDelegate;
        event  EventHandler<MainFormEventArgs> ArchiveParallel;
        event  EventHandler<MainFormEventArgs> ArchiveTask;
        event  EventHandler<MainFormEventArgs> ArchiveAwait;

        event EventHandler<MainFormEventArgs> PlinqStatistics;
    }
}
