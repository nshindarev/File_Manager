using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
   public class MainFormEventArgs: EventArgs 
    {
        public string Index { get; set; }
        public string Item { get; set; }
        public string CurrentDir { get; set; }
        public MainFormEventArgs (string Index, string Item, string CurrentDir)
        {
            this.Index = Index;
            this.Item = Item;
            this.CurrentDir = CurrentDir;
        }
    }
}
