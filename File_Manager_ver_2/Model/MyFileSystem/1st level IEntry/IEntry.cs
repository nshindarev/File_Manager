using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
   
   public interface IEntry
   {
       string FullName { get; }
       string Name { get; }
       IEntry Parent { get; }
       void Accept(IFileSystemVisitor visitor);
       void Create(byte[] content);
    }
}
