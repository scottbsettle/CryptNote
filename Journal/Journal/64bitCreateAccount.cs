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
    public partial class _64bitCreateAccount : Form
    {
        public _64bitCreateAccount()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if(PasswordText.Text.Length < 20 && PasswordText.Text.Length > 0)
            {
                const string message =
        "Are you sure your password is Under 20 characters";
                const string caption = "Set Password";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    // cancel the closure of the form.
                    return;
                }



            }
            if(PasswordText.Text.Length == 0)
            {
                const string message =
       "Please enter a password!";
                const string caption = "No Password";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                
                // If the no button was pressed ...
               
                    // cancel the closure of the form.
                    return;
                

            }
            if (PasswordText.Text == VerifyTextBox.Text)
            {
                Properties.Settings.Default.Password = PasswordText.Text;
                Properties.Settings.Default.Save();
                Close();               
            }
            else
            {
                UserPasMatch.Visible = true;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PasswordText_Enter(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }

        private void VerifyTextBox_Enter(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}
