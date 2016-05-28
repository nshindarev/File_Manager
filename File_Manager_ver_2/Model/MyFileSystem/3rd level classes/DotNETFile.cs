using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class DotNETFile : IFile
    {
        private FileInfo file;
        private string fullName;

        public DotNETFile(string fullName)
        {
            file = File.Exists(fullName) ? new FileInfo(fullName) : null;
            this.fullName = fullName;
        }

        public string FullName
        {
            get { return file == null ? fullName : file.FullName; }
        }

        public string Name
        {
            get { return file == null ? fullName.Split('\\').Last() : file.Name; }
        }

        public IEntry Parent
        {
            get
            {
                int index = FullName.LastIndexOf('\\');
                string parentFullName = FullName.Remove(index);
                return new DotNETDirectory(parentFullName);
            }
        }

        public void Accept(IFileSystemVisitor visitor) { return; }

        public long Length
        {
            get { return file == null ? -1 : file.Length; }
        }

        public string Extension
        {
            get { return file == null ? "" : file.Extension; }
        }

        public byte[] Open()
        {
            return MyFileStream.Open(FullName);
        }

        public void Create(byte[] content)
        {
            MyFileStream.CreateFile(FullName, content);
        }
    }
}
