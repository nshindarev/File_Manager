using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Ionic.Zip;

namespace File_Manager_ver_2
{
    public class MyZip : IFile, IDirectory
    {
        private string fullName;

        public MyZip(string name)
        {
            if (MyFile.GetExtension(name) != ".zip")
                throw new ArgumentException();
            else
                fullName = name;
        }

        public long Length
        {
            get { return (new MyFile(FullName)).Length; }
        }

        public string FullName
        {
            get { return fullName; }
        }

        public string Name
        {
            get { return fullName.Split('\\').Last(); }
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
            get { return ".zip"; }
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.Visit((IDirectory)this);
        }

        public byte[] GetContentsAsByteArray()
        {
            using (MemoryStream zippedStream = new MemoryStream(File.ReadAllBytes(FullName)))
            using (ZipInputStream decompressStream = new ZipInputStream(zippedStream, true))
            using (MemoryStream resultStream = new MemoryStream())
            {
                try
                {
                    byte[] buffer = new byte[16384];

                    ZipEntry z = decompressStream.GetNextEntry();
                    while (z != null)
                    {
                        int iCount = 1;
                        while (iCount > 0)
                        {
                            iCount = decompressStream.Read(buffer, 0, buffer.Length);
                            resultStream.Write(buffer, 0, iCount);
                        }
                        z = decompressStream.GetNextEntry();
                    }

                    return resultStream.ToArray();
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при чтении архива", e);
                }

            }
        }

        public ICollection<IEntry> GetItems()
        {
            using (ZipFile temp = new ZipFile(FullName))
            {
                List<ZipEntry> entries = temp.Entries.ToList();
                List<IEntry> directoriesInsideZip = new List<IEntry>();
                List<IEntry> filesAndZipsInsideZip = new List<IEntry>();

                foreach (var v in entries)
                {
                    if (!(v.IsDirectory))
                    {
                        if (v.FileName.Contains("/"))
                        {
                            var pathParts = v.FileName.Split('/');
                            directoriesInsideZip.Add(new MyDirectory(temp.Name + @"\" + pathParts[0]));
                        }
                        else if (MyFile.GetExtension(v.FileName) == ".zip")
                            filesAndZipsInsideZip.Add(new MyZip(temp.Name + @"\" + v.FileName));
                        else
                            filesAndZipsInsideZip.Add(new MyFile(temp.Name + @"\" + v.FileName));
                    }
                }
                return directoriesInsideZip
                    .Union(filesAndZipsInsideZip)
                    .Distinct(new EntriesComparer<IEntry>())
                    .ToList();
            }
        }

        public ICollection<IEntry> GetAllItems()
        {
            using (ZipFile temp = new ZipFile(FullName))
            {
                List<ZipEntry> entries = temp.Entries.ToList();
                List<IEntry> directoriesInsideZip = new List<IEntry>();
                List<IEntry> filesAndZipsInsideZip = new List<IEntry>();

                foreach (var v in entries)
                {
                    if (!(v.IsDirectory))
                    {
                        if (MyFile.GetExtension(v.FileName) == ".zip")
                            filesAndZipsInsideZip.Add(new MyZip(temp.Name + @"\" + v.FileName));
                        else
                            filesAndZipsInsideZip.Add(new MyFile(temp.Name + @"\" + v.FileName));
                    }
                }
                return directoriesInsideZip.Union(filesAndZipsInsideZip).ToList();
            }
        }

        public ICollection<IFile> GetFiles()
        {
            return GetItems().OfType<IFile>() as List<IFile>;
        }

        public ICollection<IDirectory> GetDirectories()
        {
            return GetItems().OfType<IDirectory>() as List<IDirectory>;
        }

        public void AddToZip(string pathInsideArchive, byte[] content)
        {
            using (ZipFile temp = new ZipFile(FullName))
            {
                try
                {
                    if (content == null)
                        temp.AddDirectoryByName(pathInsideArchive);
                    else
                        temp.AddEntry(pathInsideArchive, content);
                    temp.Save();
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при добавлении в архив", e);
                }
            }
        }

        public byte[] GetFromZip(string pathInsideArchive)
        {
            using (ZipFile temp = new ZipFile(FullName))
            using (MemoryStream output = new MemoryStream())
            {
                try
                {
                    ZipEntry entry = temp[pathInsideArchive];
                    entry.Extract(output);
                    return output.ToArray();
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при извлечении из архива", e);
                }
            }
        }

        public void RemoveFromZip(string pathInsideArchive)
        {
            using (ZipFile temp = new ZipFile(FullName))
            {
                try
                {
                    temp.RemoveEntry(temp[pathInsideArchive]);
                    temp.Save();
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при удалении из архива", e);
                }
            }
        }

        public byte[] Open()
        {
            return MyFileStream.Open(FullName);
        }

        public void Create(byte[] content)
        {
            try
            {
                MyFileStream.CreateFile(FullName, content);
            }
            catch (Exception e)
            {
                throw new MyException("Ошибка при создании файла", e);
            }
        }

    }
}