using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
       public class MyFileStream
        {
            public static byte[] Open(string path)
            {
                try
                {
                   
                
                        byte[] content;
                        using (FileStream inStream = File.Open(path, FileMode.Open))
                        {
                            content = new byte[inStream.Length];
                            inStream.Read(content, 0, (int)inStream.Length);
                        }
                        return content;
                    
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка в чтении потока", e);
                }
            }
            public static void CreateFile(string path, byte[] content)
            {
                try
                {
                    IEntry zip = FileFactory.CreateEntry(path);
                    if (zip is MyZip)
                    {
                        string pathToZip;
                        string pathInsideZip;
                        ParseZipPath(path, out pathToZip, out pathInsideZip);

                        MyZip _zip = new MyZip(pathToZip);
                        _zip.AddToZip(pathInsideZip, content);
                    }
                    else
                        using (Stream outStream = File.Open(path, FileMode.Create))
                        {
                            outStream.Write(content, 0, content.Length);
                        }
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при создании файла", e);
                }
            }
            public static void CreateDirectory(string path)
            {
                try
                {
                    IEntry zIp = FileFactory.CreateEntry(path);
                    if (zIp is MyZip)
                    {
                        string pathToZip;
                        string pathInsideZip;

                        ParseZipPath(path, out pathToZip, out pathInsideZip);

                        MyZip zip = new MyZip(pathToZip);
                        zip.AddToZip(pathInsideZip, null);
                    }
                    else
                        Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при создании папки", e);
                }
            }

            public static void Delete(string fullName)
            {
                try
                {
                    IEntry entry = FileFactory.CreateEntry(fullName);
                    if (entry is MyZip)
                    {
                        string pathToZip;
                        string pathInsideZip;

                        ParseZipPath(fullName, out pathToZip, out pathInsideZip);

                        MyZip zip = new MyZip(pathToZip);
                        zip.RemoveFromZip(pathInsideZip);
                    }
                    else if (Directory.Exists(fullName))
                        Directory.Delete(fullName, true);
                    else
                        File.Delete(fullName);
                }
                catch (Exception e)
                {
                    throw new MyException("Ошибка при удалении", e);
                }
            }
          
            
            public static void ParseZipPath(string entirePath, out string pathToZip, out string pathInsideZip)
            {
                string[] pathBits = entirePath.Split('\\');
                pathToZip = "";
                int i = 0;
 
                
                while (!(pathToZip.Contains(".zip")))
                {
                    pathToZip += pathBits[i] + @"\";
                    i++;
                };

                pathToZip = pathToZip.Remove(pathToZip.Length - 1);

                pathInsideZip = "";
                while (i != pathBits.Length)
                {
                    pathInsideZip += pathBits[i] + @"\";
                    i++;
                };
                pathInsideZip = pathInsideZip.Remove(pathInsideZip.Length - 1);
            }
            
        }
    }


