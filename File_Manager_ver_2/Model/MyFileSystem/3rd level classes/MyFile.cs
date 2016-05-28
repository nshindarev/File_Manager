using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace File_Manager_ver_2
{
    public class MyFile : IFile
    {
        private DotNETFile dotNETFile; /* decorator pattern */

        public MyFile(string fullName)
        {
            dotNETFile = new DotNETFile(fullName);
        }

        public long Length
        {
            get { return dotNETFile.Length; }
        }

        public string FullName
        {
            get { return dotNETFile.FullName; }
        }

        public string Name
        {
            get { return dotNETFile.Name; }
        }

        public IEntry Parent
        {
            get
            {
                int index = FullName.LastIndexOf('\\');
                string parentFullName = FullName;
                if (FullName[index - 1] != ':') parentFullName = FullName.Remove(index);
                return FileFactory.CreateEntry(parentFullName);
            }
        }

        public string Extension
        {
            get { return dotNETFile.Extension; }
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.Visit(this);
        }

        public byte[] Open()
        {
            return MyFileStream.Open(FullName);
        }

        public void Create(byte[] content)
        {
            MyFileStream.CreateFile(FullName, content);
        }

        #region static service methods

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public static string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public static string GetRandomFileName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }

        public static string Combine(params string[] path)
        {
            return Path.Combine(path);
        }

        public static string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }

        public static void Delete(string fullName)
        {
            MyFileStream.Delete(fullName);
        }

        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        #endregion

    }
}
