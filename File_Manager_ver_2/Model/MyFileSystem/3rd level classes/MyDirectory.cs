using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public class MyDirectory : IDirectory
    {
        private DotNETDirectory dotNETDir; /* decorator pattern */

        // конструктор
        public MyDirectory(string fullName)
        {
            dotNETDir = new DotNETDirectory(fullName);
        }
        
        
        //подпапки
        public ICollection<IEntry> GetItems()
        {
            List<IEntry> subItems = new List<IEntry>();

            IEntry fullname = FileFactory.CreateEntry(FullName);
            if (fullname is MyZip)
            {
               
                string outsideZip, insideZip;
                MyFileStream.ParseZipPath(FullName, out outsideZip, out insideZip);
              
                MyZip zip = new MyZip(outsideZip);
                foreach (IEntry entry in zip.GetAllItems())
                {
                    if (entry.FullName.Contains(Name + "/"))
                        subItems.Add(FileFactory.CreateEntry(entry.FullName.Replace('/', '\\')));
                }

            }
            else
            {
                foreach (IDirectory dir in dotNETDir.GetDirectories()) subItems.Add(FileFactory.CreateEntry(dir.FullName));
                foreach (IFile f in dotNETDir.GetFiles())              subItems.Add(FileFactory.CreateEntry(f.FullName));
            }
            return subItems;
        }

        public ICollection<IFile> GetFiles()
        {
            return (ICollection<IFile>) GetItems().OfType<IFile>().ToList();
        }

        public ICollection<IDirectory> GetDirectories()
        {
            return (ICollection<IDirectory>)GetItems().OfType<IDirectory>().ToList();
        }


        //________________________________________________
        //________________________________________________
        //________________________________________________

        public string FullName
        {
            get { return dotNETDir.FullName; }
        }

        public string Name
        {
            get { return dotNETDir.Name; }
        }

        public IEntry Parent
        {
            get
            {
                int index = FullName.LastIndexOf('\\');
                string parentFullName = FullName.Remove(index);
                if (parentFullName.Last() == ':') parentFullName += "\\";
                return FileFactory.CreateEntry(parentFullName);
            }
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.Visit(this);
        }

        # region static service methods
        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public void Create(byte[] content = null)
        {
            MyFileStream.CreateDirectory(FullName);
        }
        public static void Create (string create)
        {
            Directory.CreateDirectory(create);
        }

        public static void Delete(string path)
        {
            MyFileStream.Delete(path);
        }
        #endregion


    }
}
