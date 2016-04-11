namespace Journal
{
    partial class DialogWindow
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
            this.FileName = new System.Windows.Forms.TabPage();
            this.FileNameText = new System.Windows.Forms.TextBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.TipLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.EditDialog = new System.Windows.Forms.TabControl();
            this.FileName.SuspendLayout();
            this.EditDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileName
            // 
            this.FileName.Controls.Add(this.label1);
            this.FileName.Controls.Add(this.TipLabel);
            this.FileName.Controls.Add(this.CancelButton);
            this.FileName.Controls.Add(this.SaveButton);
            this.FileName.Controls.Add(this.Info);
            this.FileName.Controls.Add(this.label4);
            this.FileName.Controls.Add(this.FileNameLabel);
            this.FileName.Controls.Add(this.FileNameText);
            this.FileName.Location = new System.Drawing.Point(4, 22);
            this.FileName.Name = "FileName";
            this.FileName.Padding = new System.Windows.Forms.Padding(3);
            this.FileName.Size = new System.Drawing.Size(276, 235);
            this.FileName.TabIndex = 0;
            this.FileName.Text = "FileName";
            this.FileName.UseVisualStyleBackColor = true;
            // 
            // FileNameText
            // 
            this.FileNameText.Location = new System.Drawing.Point(92, 97);
            this.FileNameText.Name = "FileNameText";
            this.FileNameText.Size = new System.Drawing.Size(145, 20);
            this.FileNameText.TabIndex = 0;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(19, 97);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(54, 13);
            this.FileNameLabel.TabIndex = 1;
            this.FileNameLabel.Text = "File Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 31);
            this.label4.TabIndex = 9;
            this.label4.Text = "ZeroUnderscoreZero";
            // 
            // Info
            // 
            this.Info.AutoSize = true;
            this.Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Info.Location = new System.Drawing.Point(33, 63);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(215, 17);
            this.Info.TabIndex = 10;
            this.Info.Text = "Enter the file name that you want";
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Location = new System.Drawing.Point(199, 206);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(59, 23);
            this.SaveButton.TabIndex = 11;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(134, 206);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(59, 23);
            this.CancelButton.TabIndex = 12;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TipLabel
            // 
            this.TipLabel.AutoSize = true;
            this.TipLabel.Location = new System.Drawing.Point(24, 130);
            this.TipLabel.Name = "TipLabel";
            this.TipLabel.Size = new System.Drawing.Size(233, 13);
            this.TipLabel.TabIndex = 13;
            this.TipLabel.Text = "Tip: Don\'t include .txt at the end of the file name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "This changes the current tab selected";
            // 
            // EditDialog
            // 
            this.EditDialog.Controls.Add(this.FileName);
            this.EditDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditDialog.Location = new System.Drawing.Point(0, 0);
            this.EditDialog.Multiline = true;
            this.EditDialog.Name = "EditDialog";
            this.EditDialog.SelectedIndex = 0;
            this.EditDialog.Size = new System.Drawing.Size(284, 261);
            this.EditDialog.TabIndex = 0;
            // 
            // DialogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.EditDialog);
            this.Name = "DialogWindow";
            this.Text = "DialogWindows";
            this.FileName.ResumeLayout(false);
            this.FileName.PerformLayout();
            this.EditDialog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage FileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TipLabel;
        new private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.TextBox FileNameText;
        private System.Windows.Forms.TabControl EditDialog;
    }
}