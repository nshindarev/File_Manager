using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    interface IFindInfo
    {
        void FindInformation(); // имя -> dirinfo + GoDirs();
        void GoDirs(IDirectory currentDir); // собирает в список все подпапки   
        void FindInfoInFile(List<string> filesFullName);
        void FindInfoInFile(string fileFullName);
        void FileWrite(string file_name);
        void try_findinfo();
    }
}
