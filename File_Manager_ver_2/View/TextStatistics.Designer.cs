namespace File_Manager_ver_2
{
    partial class TextStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.totalWordsLabel = new System.Windows.Forms.Label();
            this.totalStringsLabel = new System.Windows.Forms.Label();
            this.TopTen = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.CancelButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // totalWordsLabel
            // 
            this.totalWordsLabel.AutoSize = true;
            this.totalWordsLabel.Location = new System.Drawing.Point(12, 19);
            this.totalWordsLabel.Name = "totalWordsLabel";
            this.totalWordsLabel.Size = new System.Drawing.Size(65, 13);
            this.totalWordsLabel.TabIndex = 1;
            this.totalWordsLabel.Text = "Total words:";
            // 
            // totalStringsLabel
            // 
            this.totalStringsLabel.AutoSize = true;
            this.totalStringsLabel.Location = new System.Drawing.Point(12, 45);
            this.totalStringsLabel.Name = "totalStringsLabel";
            this.totalStringsLabel.Size = new System.Drawing.Size(67, 13);
            this.totalStringsLabel.TabIndex = 5;
            this.totalStringsLabel.Text = "Total strings:";
            // 
            // TopTen
            // 
            this.TopTen.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TopTen.FormattingEnabled = true;
            this.TopTen.ItemHeight = 14;
            this.TopTen.Location = new System.Drawing.Point(12, 75);
            this.TopTen.Name = "TopTen";
            this.TopTen.Size = new System.Drawing.Size(269, 200);
            this.TopTen.TabIndex = 16;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(33, 319);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(229, 22);
            this.progressBar.TabIndex = 19;
            this.progressBar.Visible = false;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(151, 281);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(85, 32);
            this.CancelButton.TabIndex = 18;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(49, 281);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(80, 32);
            this.startButton.TabIndex = 17;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // TextStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(301, 355);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.TopTen);
            this.Controls.Add(this.totalStringsLabel);
            this.Controls.Add(this.totalWordsLabel);
            this.Name = "TextStatistics";
            this.Text = "TextStatistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalWordsLabel;
        private System.Windows.Forms.Label totalStringsLabel;
        private System.Windows.Forms.ListBox TopTen;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button CancelButton;
        public System.Windows.Forms.Button startButton;
    }
}