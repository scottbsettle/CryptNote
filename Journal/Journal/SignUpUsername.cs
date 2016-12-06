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
    public partial class SignUpUsername : Form
    {
        public SignUpUsername()
        {
            InitializeComponent();
        }

        public string GetUsername()
        {
            return UsernameText.Text;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (UsernameText.Text.Length == 0)
            {
                const string message =
       "Please enter a Username!";
                const string caption = "No Username";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...

                // cancel the closure of the form.
                return;


            }
            if (UsernameText.Text == VerifyTextBox.Text)
            {
                Properties.Settings.Default.Password = UsernameText.Text;
                Properties.Settings.Default.Save();
                Close();
            }
            else
            {
                UserPasMatch.Visible = true;
            }
        }
    }
}
