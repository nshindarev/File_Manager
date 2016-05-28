using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public interface IFileSystemVisitor
    {
        void Visit(IFile file);
        void Visit(IDirectory dir);
        bool IsDone { get; }
    }
}
