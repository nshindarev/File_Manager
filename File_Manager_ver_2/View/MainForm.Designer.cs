namespace File_Manager_ver_2
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListBox = new System.Windows.Forms.ListBox();
            this.CurrentDir = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPlinqStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.findInfoZipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multithreadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFindInfoParallel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFindInfoThread = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFindInfoAwait = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFindInfoDelegate = new System.Windows.Forms.ToolStripMenuItem();
            this.bynFindInfoInZip = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnParallelArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTaskArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.btnThreadArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelegateArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAwaitArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtboxArchivation = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListBox
            // 
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Location = new System.Drawing.Point(12, 96);
            this.ListBox.Name = "ListBox";
            this.ListBox.Size = new System.Drawing.Size(231, 316);
            this.ListBox.TabIndex = 1;
            this.ListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox_MouseDoubleClick);
            // 
            // CurrentDir
            // 
            this.CurrentDir.Location = new System.Drawing.Point(12, 70);
            this.CurrentDir.Name = "CurrentDir";
            this.CurrentDir.Size = new System.Drawing.Size(231, 20);
            this.CurrentDir.TabIndex = 7;
            this.CurrentDir.Text = "C:\\\\";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMenu,
            this.multithreadingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(255, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnMenu
            // 
            this.btnMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCopy,
            this.btnInsert,
            this.btnDelete,
            this.statisticToolStripMenuItem,
            this.findInfoZipToolStripMenuItem});
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(50, 20);
            this.btnMenu.Text = "Menu";
            // 
            // btnCopy
            // 
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(152, 22);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(152, 22);
            this.btnInsert.Text = "Insert";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // statisticToolStripMenuItem
            // 
            this.statisticToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlinqStatistics});
            this.statisticToolStripMenuItem.Name = "statisticToolStripMenuItem";
            this.statisticToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.statisticToolStripMenuItem.Text = "Statistic";
            // 
            // btnPlinqStatistics
            // 
            this.btnPlinqStatistics.Name = "btnPlinqStatistics";
            this.btnPlinqStatistics.Size = new System.Drawing.Size(101, 22);
            this.btnPlinqStatistics.Text = "Plinq";
            this.btnPlinqStatistics.Click += new System.EventHandler(this.btnPlinqStatistics_Click);
            // 
            // findInfoZipToolStripMenuItem
            // 
            this.findInfoZipToolStripMenuItem.Name = "findInfoZipToolStripMenuItem";
            this.findInfoZipToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.findInfoZipToolStripMenuItem.Text = "AllFindInfo";
            this.findInfoZipToolStripMenuItem.Click += new System.EventHandler(this.findInfoZipToolStripMenuItem_Click);
            // 
            // multithreadingToolStripMenuItem
            // 
            this.multithreadingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findInfoToolStripMenuItem,
            this.archiveToolStripMenuItem});
            this.multithreadingToolStripMenuItem.Name = "multithreadingToolStripMenuItem";
            this.multithreadingToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.multithreadingToolStripMenuItem.Text = "Multithreading ";
            // 
            // findInfoToolStripMenuItem
            // 
            this.findInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFindInfoParallel,
            this.btnFindInfoThread,
            this.btnFindInfoAwait,
            this.btnFindInfoDelegate,
            this.bynFindInfoInZip});
            this.findInfoToolStripMenuItem.Name = "findInfoToolStripMenuItem";
            this.findInfoToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.findInfoToolStripMenuItem.Text = "FindInfo";
            // 
            // btnFindInfoParallel
            // 
            this.btnFindInfoParallel.Name = "btnFindInfoParallel";
            this.btnFindInfoParallel.Size = new System.Drawing.Size(130, 22);
            this.btnFindInfoParallel.Text = "Parallel";
            this.btnFindInfoParallel.Click += new System.EventHandler(this.btnFindInfoParallel_Click);
            // 
            // btnFindInfoThread
            // 
            this.btnFindInfoThread.Name = "btnFindInfoThread";
            this.btnFindInfoThread.Size = new System.Drawing.Size(130, 22);
            this.btnFindInfoThread.Text = "Thread";
            this.btnFindInfoThread.Click += new System.EventHandler(this.btnFindInfoThread_Click);
            // 
            // btnFindInfoAwait
            // 
            this.btnFindInfoAwait.Name = "btnFindInfoAwait";
            this.btnFindInfoAwait.Size = new System.Drawing.Size(130, 22);
            this.btnFindInfoAwait.Text = "Await";
            this.btnFindInfoAwait.Click += new System.EventHandler(this.btnFindInfoAwait_Click);
            // 
            // btnFindInfoDelegate
            // 
            this.btnFindInfoDelegate.Name = "btnFindInfoDelegate";
            this.btnFindInfoDelegate.Size = new System.Drawing.Size(130, 22);
            this.btnFindInfoDelegate.Text = "Delegate";
            this.btnFindInfoDelegate.Click += new System.EventHandler(this.btnFindInfoDelegate_Click);
            // 
            // bynFindInfoInZip
            // 
            this.bynFindInfoInZip.Name = "bynFindInfoInZip";
            this.bynFindInfoInZip.Size = new System.Drawing.Size(130, 22);
            this.bynFindInfoInZip.Text = "Find In Zip";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnParallelArchive,
            this.btnTaskArchive,
            this.btnThreadArchive,
            this.btnDelegateArchive,
            this.btnAwaitArchive});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // btnParallelArchive
            // 
            this.btnParallelArchive.Name = "btnParallelArchive";
            this.btnParallelArchive.Size = new System.Drawing.Size(120, 22);
            this.btnParallelArchive.Text = "Parallel";
            this.btnParallelArchive.Click += new System.EventHandler(this.btnParallelArchive_Click);
            // 
            // btnTaskArchive
            // 
            this.btnTaskArchive.Name = "btnTaskArchive";
            this.btnTaskArchive.Size = new System.Drawing.Size(120, 22);
            this.btnTaskArchive.Text = "Task";
            this.btnTaskArchive.Click += new System.EventHandler(this.btnTaskArchive_Click);
            // 
            // btnThreadArchive
            // 
            this.btnThreadArchive.Name = "btnThreadArchive";
            this.btnThreadArchive.Size = new System.Drawing.Size(120, 22);
            this.btnThreadArchive.Text = "Thread";
            this.btnThreadArchive.Click += new System.EventHandler(this.btnThreadArchive_Click);
            // 
            // btnDelegateArchive
            // 
            this.btnDelegateArchive.Name = "btnDelegateArchive";
            this.btnDelegateArchive.Size = new System.Drawing.Size(120, 22);
            this.btnDelegateArchive.Text = "Delegate";
            this.btnDelegateArchive.Click += new System.EventHandler(this.btnDelegateArchive_Click);
            // 
            // btnAwaitArchive
            // 
            this.btnAwaitArchive.Name = "btnAwaitArchive";
            this.btnAwaitArchive.Size = new System.Drawing.Size(120, 22);
            this.btnAwaitArchive.Text = "Await";
            this.btnAwaitArchive.Click += new System.EventHandler(this.btnAwaitArchive_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "*продолжительность архивации";
            // 
            // txtboxArchivation
            // 
            this.txtboxArchivation.Location = new System.Drawing.Point(12, 44);
            this.txtboxArchivation.Name = "txtboxArchivation";
            this.txtboxArchivation.Size = new System.Drawing.Size(231, 20);
            this.txtboxArchivation.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(255, 424);
            this.Controls.Add(this.txtboxArchivation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrentDir);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "File Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox ListBox;
        public System.Windows.Forms.TextBox CurrentDir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnMenu;
        private System.Windows.Forms.ToolStripMenuItem btnCopy;
        private System.Windows.Forms.ToolStripMenuItem btnInsert;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        private System.Windows.Forms.ToolStripMenuItem multithreadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnFindInfoParallel;
        private System.Windows.Forms.ToolStripMenuItem btnFindInfoThread;
        private System.Windows.Forms.ToolStripMenuItem btnFindInfoAwait;
        private System.Windows.Forms.ToolStripMenuItem btnFindInfoDelegate;
        private System.Windows.Forms.ToolStripMenuItem bynFindInfoInZip;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnParallelArchive;
        private System.Windows.Forms.ToolStripMenuItem btnTaskArchive;
        private System.Windows.Forms.ToolStripMenuItem btnThreadArchive;
        private System.Windows.Forms.ToolStripMenuItem btnDelegateArchive;
        private System.Windows.Forms.ToolStripMenuItem btnAwaitArchive;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtboxArchivation;
        private System.Windows.Forms.ToolStripMenuItem statisticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnPlinqStatistics;
        private System.Windows.Forms.ToolStripMenuItem findInfoZipToolStripMenuItem;
    }
}

