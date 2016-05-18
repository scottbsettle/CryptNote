using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public partial class UsernamePassword : Form
    {
        bool IsNewUser = false;
        public UsernamePassword()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public void SetNewUser(bool _isNew)
        {
            IsNewUser = _isNew;
            if (_isNew)
            {
                VerifyLabel.Visible = true;
                VerifyTextBox.Visible = true;
                Info.Text = "Please Enter New Username and Password";
                UserPasMatch.Visible = false;
                PasswordText.Text = null;
                UsernameText.Text = null;
                CancelButton.Visible = false;
            }
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Info.Text == "Please Enter Current Username and Password")
            {
                if (UsernameText.Text == Properties.Settings.Default.Username)
                {
                    if (PasswordText.Text == Properties.Settings.Default.Password)
                    {
                        VerifyLabel.Visible = true;
                        VerifyTextBox.Visible = true;
                        Info.Text = "Please Enter New Username and Password";
                        UserPasMatch.Visible = false;
                        PasswordText.Text = null;
                        UsernameText.Text = null;
                    }
                    else
                    {
                        UserPasMatch.Visible = true;
                    }
                }
                else
                {
                    UserPasMatch.Visible = true;
                }
            }
            else if (Info.Text == "Please Enter New Username and Password")
            {
                if (PasswordText.Text == VerifyTextBox.Text)
                {
                    Properties.Settings.Default.Username = UsernameText.Text;
                    Properties.Settings.Default.Password = PasswordText.Text;
                    Properties.Settings.Default.Save();
                    if (IsNewUser)
                    {
                        Login _login = new Login();
                        Hide();
                        _login.ShowDialog();
                    }
                    Close();
                }
                else
                {
                    UserPasMatch.Visible = true;
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
