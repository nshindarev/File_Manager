using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class DotNETDirectory : IDirectory
    {
        private DirectoryInfo dirInfo;
        private string fullName;

        public DotNETDirectory(string fullName)
        {
            dirInfo = Directory.Exists(fullName) ? new DirectoryInfo(fullName) : null;
            this.fullName = fullName;
        }

        public string FullName
        {
            get { return dirInfo == null ? fullName : dirInfo.FullName; }
        }

        public string Name
        {
            get { return dirInfo == null ? MyFile.GetFileNameWithoutExtension(fullName) : dirInfo.Name; }
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

        public void Accept(IFileSystemVisitor visitor)
        {
            return;
        }

        // методы для поиска вложенных папок и файлов 
        public ICollection<IEntry> GetItems()
        {
            List<IEntry> list = new List<IEntry>();
            foreach (DirectoryInfo d in dirInfo.GetDirectories())
                list.Add(new DotNETDirectory(d.FullName));
            foreach (FileInfo d in dirInfo.GetFiles())
                list.Add(new DotNETFile(d.FullName));
            return list;
        }
        public ICollection<IFile> GetFiles()
        {
            return (List<IFile>)GetItems().OfType<IFile>().ToList();
        }
        public ICollection<IDirectory> GetDirectories()
        {
            ICollection<IEntry> test = GetItems();
            return GetItems().OfType<IDirectory>().ToList();
        }

        public void Create(byte[] content = null)
        {
            MyFileStream.CreateDirectory(FullName);
        }

    }
}
