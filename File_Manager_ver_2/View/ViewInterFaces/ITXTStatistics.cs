using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public interface ITXTStatistics
    {
        // для доступа к элементам формы
        string totalWords {get; set;}
        string totalStrings {get; set;}
        List<string> TopTenList {get; set;}


        // события
        event EventHandler<MainFormEventArgs> StartStatistics;
        event EventHandler<MainFormEventArgs> CloseClick;


        //вспомогательные методы
        void ImproveChanges();
        void End();
    }
}
