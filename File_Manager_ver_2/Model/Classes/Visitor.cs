using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    public interface IVisitor
    {
        void VisitArchivation(Archivation_Abstract x);
        void VisitFindInfo(FindInfo_Abstract y);
    }

    public class Visitor : IVisitor
    {
        public Visitor() { }
        public void VisitArchivation(Archivation_Abstract x)
        {
            /*метод смотрит: файл  -> добавляем в лист
                             папка -> cd = dirinfo (куда ткнули)*/

            if (MyDirectory.Exists(x.CurrentDir +@"\"+ x.SelectedItem.ToString()))
            {
                IDirectory cd;
                if (x.SelectedIndex == -1)
                {
                    cd = new MyDirectory(x.CurrentDir);
                }


                else
                {
                    cd = new MyDirectory(x.CurrentDir + x.SelectedItem.ToString());
                };
                x.GoDirs(cd);
            }

                else 
            {
                MyFile cd = new MyFile(x.CurrentDir +@"\"+ x.SelectedItem.ToString());
                x.file_reg_archive.Add(cd.FullName);
            }

                // Пробег по папкам и сбор имен всех файлов
                // Как итог имеем Лист полный названий всяких файлов

                
            }
        
        public void VisitFindInfo(FindInfo_Abstract x)
        {
            
            if (MyDirectory.Exists(x.CurrentDir +@"\"+ x.SelectedItem.ToString()))
            {
                IDirectory cd;
                if (x.SelectedIndex == -1)
                {
                    cd = new MyDirectory(x.CurrentDir);
                }


                else 
                {
                    cd = new MyDirectory(x.CurrentDir + x.SelectedItem.ToString());
                };
                x.GoDirs(cd);
            }

            else if (MyFile.Exists(x.CurrentDir + @"\" + x.SelectedItem.ToString()))
            {
                MyFile cd = new MyFile(x.CurrentDir +@"\"+ x.SelectedItem.ToString());
                x.file_reg_archive.Add(cd.FullName);
            }
            else
            {
                
                throw new MyException("такого IEntry не существует",new Exception() );
            }
           
        } // имя -> dirinfo + GoDirs();
        
    }
    
}
