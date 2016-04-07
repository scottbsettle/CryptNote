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
    public partial class Form1 : Form
    {
        string Source = Properties.Settings.Default.SourceFile;
        int NumberPages = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public void SetTab(int _page, string _name)
        {
            if(_page <= NumberPages)
                FileTabs.TabPages[_page].Text = _name;
          
        }
        public void AddPage(string _name)
        {
            NumberPages++;
            TabPage tb = new TabPage(_name);
            TextBox text = new TextBox();
            string name = "JournalText";
            text.Multiline = true;
            text.Dock = DockStyle.Fill;
            tb.Controls.Add(text);
            name += NumberPages.ToString();
            text.Name = name;
            FileTabs.TabPages.Add(tb);
        }
       public string GetTabName()
        {
            return Source;
        }
        public string GetJouralText()
        {
            return JournalText.Text;
        }
        public void SetText(string _text)
        {
            JournalText.Text = _text;
        }
        public string Encrypt(string _tmp)
        {
            //   zyxwvutsrqponmlkjihgfedcba
            bool alpha = false;
            string tmp = "";
            string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ \'" , Encryption = "zyxwvutsrqponmlkjihgfedcbaZYXWVUTSRQPONMLKJIHGFEDCBA-*";
            for(int loop = 0; loop < _tmp.Length; loop++)
            {
                for(int i = 0; i < Alphabet.Length; i++)
                {
                    if(_tmp[loop] == Alphabet[i])
                    {
                        tmp += Encryption[i];
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
            return tmp;
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
            return _tmp;
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            string tmp = "";
            if(Source != "")
            {
                /*
                string name = "the_name_you_know";
                Control ctn = this.Controls[name];
                ctn.Text = "Example...";
                */
                try
                {
                    StreamWriter writer = new StreamWriter(Source);
                    tmp = Encrypt(JournalText.Text);
                    // writer.WriteLine("File created using StreamWriter class.");
                    for (int loop = 0; loop < JournalText.Text.Length; loop++)
                    {
                        writer.Write(tmp[loop]);
                         if(loop != 0 && loop != 1 && loop % 150 == 0)
                            writer.Write("/n");

                    }
                    writer.Close();
                }
                catch
                {
                    StreamWriter writer = new StreamWriter("Entery.txt");
                    //JournalText.Text = Encrypt(JournalText.Text);
                    tmp = Encrypt(JournalText.Text);
                    // writer.WriteLine("File created using StreamWriter class.");
                    for (int loop = 0; loop < JournalText.Text.Length; loop++)
                    {
                        writer.Write(tmp[loop]);
                        if (loop != 0 && loop != 1 && loop % 150 == 0)
                            writer.Write("/n");

                    }
                    writer.Close();
                }
               
            }
            
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            AddPage("NewPage");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                FileTabs.TabPages.Remove(FileTabs.SelectedTab);
            }
            catch
            {

            }
        }
    }
}
