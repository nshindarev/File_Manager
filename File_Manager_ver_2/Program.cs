using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager_ver_2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm MV = new MainForm();
           
            var presenter = new ModelPresenter_Main(MV, MV);
                presenter.OpenedDirectory(@"C:\\");
            
            Application.Run(MV);
        
        }
    }
}
