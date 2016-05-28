using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager_ver_2
{
    public interface INavigation
    {
       bool OnDeleted {get; set;}
      List<string> IListBox {get; set;}
      string ICurrentDir {get; set;}
      string IArchivationLength { get; set; }

      void RefreshCurrentDirectoryEntries();
      void RefreshCurrentDirectoryEntries_1();
      void Improve_Changes();
      void ClearItems();
      //IDirectory GetCurrentDiectory {get; set;};
   //   void RefreshCurrentDirectoryEntries(ModelPresenter_Main e, string newdir);


        
    //  event EventHandler<MainFormEventArgs> SelectedIndexChanged;
      event EventHandler<MainFormEventArgs> MouseDoubleClick;
    }
}
