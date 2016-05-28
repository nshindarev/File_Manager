using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public interface IDirectory : IEntry
    {
        ICollection<IEntry> GetItems();
        ICollection<IFile> GetFiles();
        ICollection<IDirectory> GetDirectories();
    }
}
