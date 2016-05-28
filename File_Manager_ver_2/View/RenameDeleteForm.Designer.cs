namespace File_Manager_ver_2.View
{
    partial class RenameDeleteForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtBoxNewName = new System.Windows.Forms.TextBox();
            this.LblNewName = new System.Windows.Forms.Label();
            this.lblAreUSure = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(22, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(97, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txtBoxNewName
            // 
            this.txtBoxNewName.Location = new System.Drawing.Point(22, 41);
            this.txtBoxNewName.Name = "txtBoxNewName";
            this.txtBoxNewName.Size = new System.Drawing.Size(130, 20);
            this.txtBoxNewName.TabIndex = 2;
            // 
            // LblNewName
            // 
            this.LblNewName.AutoSize = true;
            this.LblNewName.Location = new System.Drawing.Point(41, 9);
            this.LblNewName.Name = "LblNewName";
            this.LblNewName.Size = new System.Drawing.Size(91, 13);
            this.LblNewName.TabIndex = 3;
            this.LblNewName.Text = "Enter New Name:";
            // 
            // lblAreUSure
            // 
            this.lblAreUSure.AutoSize = true;
            this.lblAreUSure.Location = new System.Drawing.Point(28, 41);
            this.lblAreUSure.Name = "lblAreUSure";
            this.lblAreUSure.Size = new System.Drawing.Size(76, 13);
            this.lblAreUSure.TabIndex = 4;
            this.lblAreUSure.Text = "Are You Sure?";
            this.lblAreUSure.Visible = false;
            // 
            // RenameDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 116);
            this.Controls.Add(this.lblAreUSure);
            this.Controls.Add(this.LblNewName);
            this.Controls.Add(this.txtBoxNewName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "RenameDeleteForm";
            this.Text = "RenameDeleteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox txtBoxNewName;
        public System.Windows.Forms.Label lblAreUSure;
        public System.Windows.Forms.Label LblNewName;
    }
}