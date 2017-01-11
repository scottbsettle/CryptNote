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
using System.Runtime.InteropServices;

namespace Journal
{
    public partial class Login : Form
    {
        _32bitEncryption Encryption = new _32bitEncryption();
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        public Login()
        {
            InitializeComponent();
            CenterToScreen();
#if DEBUG
          //  AllocConsole();
#endif
        }
        public string Decrypt(string _tmp)
        {
            #region ScumO-crypt
            //bool alpha = false;
            //string tmp = "";
            //string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ \'", Encryption = "zyxwvutsrqponmlkjihgfedcbaZYXWVUTSRQPONMLKJIHGFEDCBA-*";
            //for (int loop = 0; loop < _tmp.Length; loop++)
            //{
            //    for (int i = 0; i < Encryption.Length; i++)
            //    {
            //        if (_tmp[loop] == Encryption[i])
            //        {
            //            tmp += Alphabet[i];
            //            alpha = true;
            //            break;
            //        }
            //    }
            //    if (alpha == false)
            //    {
            //        tmp += _tmp[loop];
            //    }
            //    alpha = false;
            //}
            //_tmp = tmp;
            //return _tmp;
            #endregion
            byte[] _bytes = Encryption.GetEncrptBytes();
            byte[] _Key = Encryption.GetBytes(Properties.Settings.Default.Password);
            Encryption.decrypt_function(_bytes, _Key, Encryption.GetCrypto().IV);
            return Encryption.GetText();
        }
        private void button1_Click(object sender, EventArgs e)
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

        private void userNamePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            const string message =
      "Are you sure you want to reset your key. You will lose any encrypted files.";
            const string caption = "Reset Key";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.
                return;
            }
            else
            {
                _64bitCreateAccount _key = new _64bitCreateAccount();
                _key.ShowDialog();
            }

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

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Password == "")
            {
                //Hide();
                _64bitCreateAccount create_key = new _64bitCreateAccount();
                create_key.CancelButton.Visible = false;
                create_key.ShowDialog();
                if (Properties.Settings.Default.Password == "")
                {
                    Close();
                }
            }
           // Hide();
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
