using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Journal
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            CenterToScreen();
          try
            {
                StreamReader reader = new StreamReader(Properties.Settings.Default.SourceFile);
            }
            catch
            {
                System.IO.FileStream fs = System.IO.File.Create(Properties.Settings.Default.SourceFile);
            }
        }
        public string Decrypt(string _tmp)
        {
            bool alpha = false;
            string tmp = "";
            string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ \'", Encryption = "zyxwvutsrqponmlkjihgfedcbaZYXWVUTSRQPONMLKJIHGFEDCBA-*";
            for (int loop = 0; loop < _tmp.Length; loop++)
            {
                for (int i = 0; i < Encryption.Length; i++)
                {
                    if (_tmp[loop] == Encryption[i])
                    {
                        tmp += Alphabet[i];
                        alpha = true;
                        break;
                    }
                }
                if (alpha == false)
                {
                    tmp += _tmp[loop];
                }
                alpha = false;
            }
            _tmp = tmp;
            return _tmp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (PasswordText.Text == Properties.Settings.Default.Password)
            {
                if(UsernameText.Text == Properties.Settings.Default.Username)
                {
                    LoginWarning.Visible = false;
                    Form1 Journal = new Form1();
                    Hide();
                    try
                    {
                        StreamReader reader = new StreamReader(Properties.Settings.Default.SourceFile);
                        string tmp = "", tmp2;
                        char temp;
                        while (!reader.EndOfStream)
                        {
                            temp = Convert.ToChar(reader.Read());
                            tmp += temp;
                        }
                        tmp2 = Decrypt(tmp);
                        reader.Close();
                        Journal.SetTab(0, Properties.Settings.Default.SourceFile);
                        Journal.SetText(tmp2);
                        Journal.ShowDialog();
                        Show();
                    }
                    catch
                    {
                       
                      //  System.Threading.Thread.Sleep(5000);
                        Journal.SetTab(0, "NewPage");
                        Journal.ShowDialog();
                        Show();
                    }
                    
                    UsernameText.Text = null;
                    PasswordText.Text = null;
                   // Close();
                }
                else if (UsernameText.Text != Properties.Settings.Default.Username)
                {
                    LoginWarning.Visible = true;
                    PasswordText.Text = "";
                }
            }
           else if (PasswordText.Text != Properties.Settings.Default.Password)
            {
                PasswordText.Text = "";
                LoginWarning.Visible = true;
            }
        }

        private void userNamePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsernamePassword Editme = new UsernamePassword();
            Editme.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info infoscreen = new Info();
            infoscreen.ShowDialog();
        }
    }
}
