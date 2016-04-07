namespace Journal
{
    partial class UsernamePassword
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
            this.Info = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UserPasMatch = new System.Windows.Forms.Label();
            this.VerifyLabel = new System.Windows.Forms.Label();
            this.VerifyTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.PasswordMatch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Info
            // 
            this.Info.AutoSize = true;
            this.Info.Location = new System.Drawing.Point(27, 61);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(225, 13);
            this.Info.TabIndex = 0;
            this.Info.Text = "Please Enter Current Username and Password";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(30, 88);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(55, 13);
            this.Username.TabIndex = 1;
            this.Username.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(92, 132);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '-';
            this.textBox2.Size = new System.Drawing.Size(160, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "ZeroUnderscoreZero";
            // 
            // UserPasMatch
            // 
            this.UserPasMatch.AutoSize = true;
            this.UserPasMatch.ForeColor = System.Drawing.Color.Red;
            this.UserPasMatch.Location = new System.Drawing.Point(82, 111);
            this.UserPasMatch.Name = "UserPasMatch";
            this.UserPasMatch.Size = new System.Drawing.Size(170, 13);
            this.UserPasMatch.TabIndex = 9;
            this.UserPasMatch.Text = "Username or Password is incorrect";
            // 
            // VerifyLabel
            // 
            this.VerifyLabel.AutoSize = true;
            this.VerifyLabel.Location = new System.Drawing.Point(27, 165);
            this.VerifyLabel.Name = "VerifyLabel";
            this.VerifyLabel.Size = new System.Drawing.Size(33, 13);
            this.VerifyLabel.TabIndex = 10;
            this.VerifyLabel.Text = "Verify";
            // 
            // VerifyTextBox
            // 
            this.VerifyTextBox.Location = new System.Drawing.Point(92, 158);
            this.VerifyTextBox.Name = "VerifyTextBox";
            this.VerifyTextBox.PasswordChar = '-';
            this.VerifyTextBox.Size = new System.Drawing.Size(160, 20);
            this.VerifyTextBox.TabIndex = 11;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(221, 226);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(51, 23);
            this.OkButton.TabIndex = 12;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(167, 226);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(48, 23);
            this.CancelButton.TabIndex = 13;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // PasswordMatch
            // 
            this.PasswordMatch.AutoSize = true;
            this.PasswordMatch.ForeColor = System.Drawing.Color.Red;
            this.PasswordMatch.Location = new System.Drawing.Point(92, 185);
            this.PasswordMatch.Name = "PasswordMatch";
            this.PasswordMatch.Size = new System.Drawing.Size(126, 13);
            this.PasswordMatch.TabIndex = 14;
            this.PasswordMatch.Text = "Passwords do not match!";
            // 
            // UsernamePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.PasswordMatch);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.VerifyTextBox);
            this.Controls.Add(this.VerifyLabel);
            this.Controls.Add(this.UserPasMatch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Info);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "UsernamePassword";
            this.Text = "Username/Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label UserPasMatch;
        private System.Windows.Forms.Label VerifyLabel;
        private System.Windows.Forms.TextBox VerifyTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label PasswordMatch;
    }
}