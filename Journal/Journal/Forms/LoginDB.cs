using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Journal
{
    public partial class LoginDB : Form
    {
        private User m_User;
        public LoginDB()
        {

            InitializeComponent();
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            SignUpSystem SignUp = new SignUpSystem();
            if(SignUp.ShowDialog() == DialogResult.OK)
            {
                string userN, pswrd;
               userN = SignUp.GetUsername();
               pswrd = SignUp.GetPassword();
               
            }

        }

        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (offlineToolStripMenuItem.Text == "Go Offline")
            {
                offlineToolStripMenuItem.Text = "Go Online";
                AcceptButton = OfflineAcceptButton;
            }
            else
            {
                offlineToolStripMenuItem.Text = "Go Offline";
                AcceptButton = OKButton;
            }
            OfflinePanel.Visible = !OfflinePanel.Visible;
        }

        private void OfflineAcceptButton_Click(object sender, EventArgs e)
        {
            if (PasswordText.Text == Properties.Settings.Default.Password)
            {

                LoginWarning.Visible = false;
                Form1 Journal = new Form1();
                Hide();

                Journal.SetTab(0, "NewPage");
                Journal.ShowDialog();
                Show();
                //  }

                PasswordText.Text = null;
                // Close();


            }
            else if (PasswordText.Text != Properties.Settings.Default.Password)
            {
                PasswordText.Text = "";
                LoginWarning.Visible = true;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

        }
    }
}
