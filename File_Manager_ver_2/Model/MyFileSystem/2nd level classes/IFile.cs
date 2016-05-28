using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public interface IFile : IEntry
    {
        long Length { get; }
        string Extension { get; }
        byte[] Open();
    }
}
