using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using File_Manager_ver_2;
using System.Windows.Forms;

namespace UnitTestFileManager
{
    [TestClass]
    public class UnitTest_FM
    { 
     
        [TestMethod]
        public void TestLoadManager()
        {
            MainForm MV = new MainForm();
            var presenter = new ModelPresenter_Main(MV, MV);
            presenter.OpenedDirectory(@"C:\\");
        }
        [TestMethod]
     
        public void ZipPathTest()
        {
            string outside, inside;
            MyFileStream.ParseZipPath(@"C:\ForFileManager\ToCheckThreadsWork.zip\ToCheckThreadsWork", out outside, out inside);
            Assert.AreEqual(@"C:\ForFileManager\ToCheckThreadsWork.zip", outside);
            Assert.AreEqual(@"ToCheckThreadsWork", inside);
        }
        /*
        [TestMethod]
        public void CountTXTStatistics()
        {
            //string Index;
            //string Item;
            //string CurrentDir;

          //  MainFormEventArgs ev = new MainFormEventArgs(Index,Item,CurrentDir);
           // ModelPresenter_Main.OnPlinqStatistics(new object obj, ev);
        }*/
    
        [TestMethod]
        public void ArchiveParallelTest()
        {
            string CurrentDir = @"C:\UnitTest";
            string SelectedItem = @"ForRegexTXT@.txt";
            int SelectedIndex = 1;
            Archive_Parallel AP = new Archive_Parallel(CurrentDir, SelectedItem, SelectedIndex);
            AP.try_archivation();
        }
        [TestMethod]
        public void ArchiveThreadTest()
        {
            string CurrentDir = @"C:\UnitTest";
            string SelectedItem = @"ForRegexTXTINN.txt";
            int SelectedIndex = 3;
            Archive_Thread AP = new Archive_Thread(CurrentDir, SelectedItem, SelectedIndex);
            AP.try_archivation();
        }
        [TestMethod]
        public void FindInfoThreadTest()
        {
            string CurrentDir = @"C:\ForFileManager";
            string SelectedItem = @"ForRegexTXTINN.txt";
            int SelectedIndex = 6;
            FindInfoThread Ft = new FindInfoThread(CurrentDir, SelectedItem, SelectedIndex);
            Ft.try_findinfo();
        }
        [TestMethod]
        public void FindInfoTask()
        {
            string CurrentDir = @"C:\ForFileManager";
            string SelectedItem = @"ForRegexTXT@.txt";
            int SelectedIndex = 6;
            FindInfoThread Ft = new FindInfoThread(CurrentDir, SelectedItem, SelectedIndex);
            Ft.try_findinfo();
        }
        [TestMethod]
        public void FileDeleteTest()
        {
            IEntry entry = FileFactory.CreateEntry(@"C:\ForFileManager\ToDeleteByFile1.txt");
            MyFile.Delete(entry.FullName);
            Assert.IsFalse(MyFile.Exists(@"C:\ForFileManager\ToDeleteByFile1.txt"));
        }
        [TestMethod]
        public void DirectoryDeleteTest()
        {
            IEntry entry = FileFactory.CreateEntry(@"C:\ForFileManager\ToDeleteByFile1.txt");
            MyDirectory.Delete(entry.FullName);
            Assert.IsFalse(MyDirectory.Exists(@"C:\ForFileManager\ToDeleteByFile1.txt"));
        }
    }
}
