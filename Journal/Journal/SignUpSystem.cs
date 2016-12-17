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
    public partial class SignUpSystem : Form
    {
        private int Maxpanels = 2;
        private int CurrentPanel = 1;

        public SignUpSystem()
        {
            InitializeComponent();
        }

        public string GetUsername()
        {
            return UsernameText.Text;
        }
        public string GetPassword()
        {
            return PasswordText.Text;
        }
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            if (CurrentPanel == 1)
            {
                if (UsernameText.Text.Length >= 6 && PasswordText.Text == RetypePasswordText.Text && PasswordText.Text.Length >= 8)
                {
                    Usrn6Label.Visible = false;
                    passwrd8.Visible = false;
                    Nomatch.Visible = false;
                    DialogResult = DialogResult.OK;
                    Close();

                }
                else
                {
                    if (UsernameText.Text.Length < 6)
                        Usrn6Label.Visible = true;
                    else
                        Usrn6Label.Visible = false;

                    if (PasswordText.Text.Length < 8)
                        passwrd8.Visible = true;
                    else
                        passwrd8.Visible = false;
                    if (PasswordText.Text != RetypePasswordText.Text)
                        Nomatch.Visible = true;
                    else
                        Nomatch.Visible = false;
                }
            }
            else if (CurrentPanel == 2)
            {

            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            SecurityQuestionPanel.Visible = false;
            InfoPanel.Visible = !InfoPanel.Visible;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            SecurityQuestionPanel.Visible = !SecurityQuestionPanel.Visible;
            
        }

        private void SQ1_MouseHover(object sender, EventArgs e)
        {
           SQ1.BackColor = Color.LightGray;
        }

        private void SQ1_MouseLeave(object sender, EventArgs e)
        {
            SQ1.BackColor = Color.WhiteSmoke;
        }

        private void SQ2_MouseHover(object sender, EventArgs e)
        {
            SQ2.BackColor = Color.LightGray;
        }

        private void SQ2_MouseLeave(object sender, EventArgs e)
        {
            SQ2.BackColor = Color.WhiteSmoke;
        }

        private void SQ3_MouseHover(object sender, EventArgs e)
        {
            SQ3.BackColor = Color.LightGray;
        }

        private void SQ3_MouseLeave(object sender, EventArgs e)
        {
            SQ3.BackColor = Color.WhiteSmoke;
        }

        private void SQ4_MouseHover(object sender, EventArgs e)
        {
            SQ4.BackColor = Color.LightGray;
        }

        private void SQ4_MouseLeave(object sender, EventArgs e)
        {
            SQ4.BackColor = Color.WhiteSmoke;
        }

        private void SQ1_Click(object sender, EventArgs e)
        {
            textBox3.Text = SQ1.Text;
            SecurityQuestionPanel.Visible = false;
        }

        private void SQ2_Click(object sender, EventArgs e)
        {
            textBox3.Text = SQ2.Text;
            SecurityQuestionPanel.Visible = false;
        }

        private void SQ3_Click(object sender, EventArgs e)
        {
            textBox3.Text = SQ3.Text;
            SecurityQuestionPanel.Visible = false;
        }

        private void SQ4_Click(object sender, EventArgs e)
        {
            textBox3.Text = SQ4.Text;
            SecurityQuestionPanel.Visible = false;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            SecurityQuestionPanel.Visible = false;
            InfoPanel.Visible = !InfoPanel.Visible;
            
        }
    }
}
