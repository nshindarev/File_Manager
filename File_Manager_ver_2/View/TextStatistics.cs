using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager_ver_2
{
    public partial class TextStatistics : Form, ITXTStatistics
    {
        List<string> _TopTenList;
        string ITXTStatistics.totalWords
        {
            get
            {
                return this.totalWordsLabel.Text;
            }
            set
            {
                this.totalWordsLabel.Text = value;
            }
        }
        string ITXTStatistics.totalStrings
        {
            get
            {
                return totalStringsLabel.Text;
            }
            set
            {
                totalStringsLabel.Text = value;
            }
        }
        List<string> ITXTStatistics.TopTenList
        {
            get
            {
                return _TopTenList;
            }
            set
            {
                _TopTenList = value;
            }
        }

        public event EventHandler<MainFormEventArgs> StartStatistics;
        public event EventHandler<MainFormEventArgs> CloseClick;

        string Index;
        string Item;
        string CurrentDir;

        public TextStatistics(string Index, string Item, string CurrentDir)
        {
            this.Index = Index;
            this.Item = Item;
            this.CurrentDir = CurrentDir;

            InitializeComponent();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            if (StartStatistics != null)
                StartStatistics(this, new MainFormEventArgs(this.Index,
                                                            this.Item,
                                                            this.CurrentDir));
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (CloseClick != null)
                CloseClick(this, new MainFormEventArgs(this.Index,
                                                       this.Item,
                                                       this.CurrentDir));
        
        }

      
      
        public void ImproveChanges()
        {
            TopTen.Items.Clear();
            for (int i = 0; i < _TopTenList.Count; i++) TopTen.Items.Add(_TopTenList[i]);
            _TopTenList.Clear();
        }
        public void End()
        {
            this.Close();
        }
    }
}
