using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class ModelPresenterStatistics
    {
        ITXTStatistics StatisticsForm;
        
        public ModelPresenterStatistics(ITXTStatistics StatisticsForm)
        {
            this.StatisticsForm = StatisticsForm;
            StatisticsForm.StartStatistics += new EventHandler<MainFormEventArgs>(OnStartClick);
            StatisticsForm.CloseClick += new EventHandler<MainFormEventArgs>(OnCloseClick);
            StatisticsForm.TopTenList = new List<string>();
        }
        public void OnStartClick(object sender, MainFormEventArgs e)
        {
            List<string> allWords = new List<string>(MyFile.ReadAllText(e.CurrentDir +@"\"+ e.Item).ToLower().Split(new[] { " ", "\r" }, StringSplitOptions.None));

            int words = allWords.AsParallel().Where(word => !string.IsNullOrWhiteSpace(word) && !string.IsNullOrEmpty(word)).Count();
            int strings = allWords.AsParallel().Where(word => word.Contains('\r') || word.Contains('\n')).Count();

            allWords = allWords.AsParallel().Select(word => word.Replace("\n", "").Replace("\r", ""))
                       .Where(word => !string.IsNullOrEmpty(word) && !string.IsNullOrWhiteSpace(word)).ToList();


         
            int n = 0;

            List<int> counts = new List<int>();
            //if (progressBar.BInvoke())
            List<string> ordered = allWords.AsParallel().AsOrdered().OrderByDescending(
                word =>
                {
                    int count = allWords.AsParallel().AsOrdered().Where(w => w == word).Count();
                    counts.Add(count);
                    n++;
                

                    //progressBar.PerformStep();
                    return count;
                }).Distinct().ToList();


            counts = counts.AsParallel().AsOrdered().OrderByDescending(q => q).Distinct().ToList();

            int i = 0;
            while (i<ordered.Count)
            {
                StatisticsForm.TopTenList.Add("    " + "\n" + ordered[i] + "  ");
                i++;
            }
           
            


            StatisticsForm.totalWords = "Total words" + words.ToString();
            StatisticsForm.totalStrings = "Total strings" + strings.ToString();

            StatisticsForm.ImproveChanges();
        }
        public void OnCloseClick(object sender, MainFormEventArgs e)
        {
            StatisticsForm.End();
        }
     }
}
