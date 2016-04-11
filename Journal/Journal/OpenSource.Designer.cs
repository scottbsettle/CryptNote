namespace Journal
{
    partial class OpenSource
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelSource = new System.Windows.Forms.Button();
            this.SaveSource = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SourceText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Unless in the project folder ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Tip: Include the full file source for it to open";
            // 
            // CancelSource
            // 
            this.CancelSource.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelSource.Location = new System.Drawing.Point(139, 216);
            this.CancelSource.Name = "CancelSource";
            this.CancelSource.Size = new System.Drawing.Size(59, 23);
            this.CancelSource.TabIndex = 28;
            this.CancelSource.Text = "Cancel";
            this.CancelSource.UseVisualStyleBackColor = true;
            this.CancelSource.Click += new System.EventHandler(this.CancelSource_Click);
            // 
            // SaveSource
            // 
            this.SaveSource.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveSource.Location = new System.Drawing.Point(204, 216);
            this.SaveSource.Name = "SaveSource";
            this.SaveSource.Size = new System.Drawing.Size(59, 23);
            this.SaveSource.TabIndex = 27;
            this.SaveSource.Text = "Save";
            this.SaveSource.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(38, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "Enter the file source that you want";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(259, 31);
            this.label6.TabIndex = 25;
            this.label6.Text = "ZeroUnderscoreZero";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "File Source";
            // 
            // SourceText
            // 
            this.SourceText.Location = new System.Drawing.Point(97, 107);
            this.SourceText.Name = "SourceText";
            this.SourceText.Size = new System.Drawing.Size(145, 20);
            this.SourceText.TabIndex = 23;
            // 
            // OpenSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 240);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CancelSource);
            this.Controls.Add(this.SaveSource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SourceText);
            this.Name = "OpenSource";
            this.Text = "OpenSource";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CancelSource;
        private System.Windows.Forms.Button SaveSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SourceText;
    }
}