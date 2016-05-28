using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public static class FileInfoExtension
    {

        public static string GetMD5(this FileInfo f)
        {
            try
            {

                FileStream file = new FileStream(f.FullName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (UnauthorizedAccessException)
            {
                return "Ошибка доступа";
            }

        }

        public static string AccesFile(this FileInfo f)
        {
            if (Path.GetExtension(f.FullName) != ".sys")
            {
                try
                {

                    FileInfo d = new FileInfo(f.FullName);
                    StringBuilder result = new StringBuilder();
                    FileSecurity ds = d.GetAccessControl();
                    foreach (FileSystemAccessRule permissions in ds.GetAccessRules(true, true, typeof(NTAccount)))
                    {
                        result.AppendLine(String.Format("Права: {0}", permissions.FileSystemRights.ToString()));
                        result.AppendLine();
                    }
                    return result.ToString();
                }
                catch (UnauthorizedAccessException) { return "Отказано в доступе"; }
            }
            else return "Системный файл";
        }

        public static string Encodingfile(this FileInfo f)
        {
            var bom = new byte[4];
            using (var file = new FileStream(f.FullName, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7.ToString();
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8.ToString();
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode.ToString(); //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode.ToString(); //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32.ToString();

            return Encoding.ASCII.ToString();
        }
    }
}
