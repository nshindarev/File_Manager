using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class FileFactory
    {
        public static IEntry CreateEntry(string path)
        {
            if (path == String.Empty)
                throw new MyException("Пустая строка", new ArgumentException());
            if (path.EndsWith(".zip"))
                return new MyZip(path);
            if (MyFile.GetExtension(path) == String.Empty)
                return new MyDirectory(path);
            return new MyFile(path);
        }
    }
}
