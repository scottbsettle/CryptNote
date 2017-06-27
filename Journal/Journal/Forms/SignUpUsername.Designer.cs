namespace Journal
{
    partial class SignUpUsername
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
            this.label6 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.UserPasMatch = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.VerifyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UsernameText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InfoText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 29F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(46, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 44);
            this.label6.TabIndex = 52;
            this.label6.Text = "CryptNote";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Location = new System.Drawing.Point(128, 275);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 51;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Location = new System.Drawing.Point(209, 275);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(71, 23);
            this.OkButton.TabIndex = 50;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // UserPasMatch
            // 
            this.UserPasMatch.AutoSize = true;
            this.UserPasMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserPasMatch.ForeColor = System.Drawing.Color.Red;
            this.UserPasMatch.Location = new System.Drawing.Point(51, 196);
            this.UserPasMatch.Name = "UserPasMatch";
            this.UserPasMatch.Size = new System.Drawing.Size(182, 18);
            this.UserPasMatch.TabIndex = 49;
            this.UserPasMatch.Text = "Username do NOT match!";
            this.UserPasMatch.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 31);
            this.label3.TabIndex = 48;
            this.label3.Text = "Create Username";
            // 
            // VerifyTextBox
            // 
            this.VerifyTextBox.Location = new System.Drawing.Point(14, 173);
            this.VerifyTextBox.MaxLength = 64;
            this.VerifyTextBox.Name = "VerifyTextBox";
            this.VerifyTextBox.Size = new System.Drawing.Size(243, 20);
            this.VerifyTextBox.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Retype Username";
            // 
            // UsernameText
            // 
            this.UsernameText.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.UsernameText.Location = new System.Drawing.Point(14, 128);
            this.UsernameText.MaxLength = 64;
            this.UsernameText.Name = "UsernameText";
            this.UsernameText.Size = new System.Drawing.Size(243, 20);
            this.UsernameText.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Username";
            // 
            // InfoText
            // 
            this.InfoText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoText.Enabled = false;
            this.InfoText.Location = new System.Drawing.Point(14, 217);
            this.InfoText.Multiline = true;
            this.InfoText.Name = "InfoText";
            this.InfoText.ReadOnly = true;
            this.InfoText.Size = new System.Drawing.Size(258, 49);
            this.InfoText.TabIndex = 43;
            this.InfoText.Text = "CrypteNote will like to remind you that its not the safiest program. We are only " +
    "using a 64-bit encrpytion. Best regards from ZeroUnderscoreZero\r\n";
            // 
            // SignUpUsername
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(282, 301);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.UserPasMatch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VerifyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsernameText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InfoText);
            this.Name = "SignUpUsername";
            this.ShowIcon = false;
            this.Text = "SignUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label UserPasMatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox VerifyTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UsernameText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InfoText;
    }
}