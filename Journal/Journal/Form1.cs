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
        int NumberPages = 1;
        List<TextBox> Tabs = new List<TextBox>();
        //TextBox[] Tabs = new TextBox[20];
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            Tabs.Add(JournalText);
        }
        public TabControl GetFiletabs()
        {
            return FileTabs;
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
            Tabs.Add(text);
            

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

                string FileName = FileTabs.SelectedTab.Text;
                if (FileName == "NewPage")
                {
                    DialogWindow dlg = new DialogWindow();
                    if(DialogResult.OK == dlg.ShowDialog())
                    {
                        string change;
                        change = dlg.GetFileName() + ".txt";
                        FileTabs.SelectedTab.Text = change;
                        FileName = change;
                        dlg.Close();
                    }
                }
                    StreamWriter writer = new StreamWriter(FileName);
                    //JournalText.Text = Encrypt(JournalText.Text);
                    tmp = Encrypt(Tabs[FileTabs.SelectedIndex].Text);
                    // writer.WriteLine("File created using StreamWriter class.");
                    for (int loop = 0; loop < Tabs[FileTabs.SelectedIndex].Text.Length; loop++)
                    {
                        writer.Write(tmp[loop]);
                        if (loop != 0 && loop != 1 && loop % 150 == 0)
                            writer.Write("/n");

                    }
                    writer.Close();

            }
            
        }

        private void FileTabs_Selected(object sender, TabControlEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (NumberPages < 20)
            AddPage("NewPage");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                Tabs.Remove(Tabs[FileTabs.SelectedIndex]);
                FileTabs.TabPages.Remove(FileTabs.SelectedTab);
                NumberPages--;
                
            }
            catch
            {

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
       
    }
}
