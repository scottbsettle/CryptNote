namespace Journal
{
    partial class _64bitCreateAccount
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
            this.InfoText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VerifyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UserPasMatch = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InfoText
            // 
            this.InfoText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoText.Enabled = false;
            this.InfoText.Location = new System.Drawing.Point(12, 217);
            this.InfoText.Multiline = true;
            this.InfoText.Name = "InfoText";
            this.InfoText.ReadOnly = true;
            this.InfoText.Size = new System.Drawing.Size(258, 49);
            this.InfoText.TabIndex = 33;
            this.InfoText.Text = "CrypteNote will like to remind you that its not the safiest program. We are only " +
    "using a 64-bit encrpytion. Best regards from ZeroUnderscoreZero\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Key :";
            // 
            // PasswordText
            // 
            this.PasswordText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordText.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.PasswordText.Location = new System.Drawing.Point(12, 128);
            this.PasswordText.MaxLength = 64;
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(243, 16);
            this.PasswordText.TabIndex = 35;
            this.PasswordText.Enter += new System.EventHandler(this.PasswordText_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Retype :";
            // 
            // VerifyTextBox
            // 
            this.VerifyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VerifyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerifyTextBox.Location = new System.Drawing.Point(12, 173);
            this.VerifyTextBox.MaxLength = 64;
            this.VerifyTextBox.Name = "VerifyTextBox";
            this.VerifyTextBox.PasswordChar = '*';
            this.VerifyTextBox.Size = new System.Drawing.Size(243, 16);
            this.VerifyTextBox.TabIndex = 37;
            this.VerifyTextBox.Enter += new System.EventHandler(this.VerifyTextBox_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 31);
            this.label3.TabIndex = 38;
            this.label3.Text = "Create Key";
            // 
            // UserPasMatch
            // 
            this.UserPasMatch.AutoSize = true;
            this.UserPasMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserPasMatch.ForeColor = System.Drawing.Color.Red;
            this.UserPasMatch.Location = new System.Drawing.Point(60, 196);
            this.UserPasMatch.Name = "UserPasMatch";
            this.UserPasMatch.Size = new System.Drawing.Size(146, 18);
            this.UserPasMatch.TabIndex = 39;
            this.UserPasMatch.Text = "Keys do NOT match!";
            this.UserPasMatch.Visible = false;
            // 
            // OkButton
            // 
            this.OkButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Location = new System.Drawing.Point(207, 275);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(71, 23);
            this.OkButton.TabIndex = 40;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Location = new System.Drawing.Point(126, 275);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 41;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 29F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(44, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 44);
            this.label6.TabIndex = 42;
            this.label6.Text = "CryptNote";
            // 
            // _64bitCreateAccount
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 301);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.UserPasMatch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VerifyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InfoText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "_64bitCreateAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Key Creation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox InfoText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox VerifyTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label UserPasMatch;
        private System.Windows.Forms.Button OkButton;
        public System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label6;
    }
}