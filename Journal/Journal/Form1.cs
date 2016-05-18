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
        int fontsize = 9;
        //string Copy_Past;
        List<TextBox> Tabs = new List<TextBox>();
        List<bool> Encrypted = new List<bool>();
        Button temp = new Button();
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;

        //TextBox[] Tabs = new TextBox[20];
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            Tabs.Add(JournalText);
            int tabLength = FileTabs.ItemSize.Width;
            Encrypted.Add(false);
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
            text.MaxLength = 0;
            text.ScrollBars = ScrollBars.Vertical;
            string name = "JournalText";
            text.Multiline = true;
            text.Dock = DockStyle.Fill;
            tb.Controls.Add(text);
            name += NumberPages.ToString();
            text.Name = name;
            FontFamily family = new FontFamily("Microsoft Sans Serif");
            Font new_font = new Font(family, fontsize);
            text.Font = new_font;
            FileTabs.TabPages.Add(tb);
            Tabs.Add(text);
            Encrypted.Add(false);

        }
       public string GetTabName()
        {
            return Source;
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //This code will render a "x" mark at the end of the Tab caption. 
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - CLOSE_AREA, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.FileTabs.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + LEADING_SPACE, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
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
            return tmp;
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
                else if (FileTabs.SelectedIndex > 0 && FileName == "NewPage" + FileTabs.SelectedIndex.ToString())
                {
                    DialogWindow dlg = new DialogWindow();
                    if (DialogResult.OK == dlg.ShowDialog())
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
                if (!Encrypted[FileTabs.SelectedIndex])
                    tmp = Encrypt(Tabs[FileTabs.SelectedIndex].Text);
                else
                    tmp = Tabs[FileTabs.SelectedIndex].Text;
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
            {
                if (NumberPages != 0)
                    AddPage("NewPage" + NumberPages);
                else if (NumberPages == 0)
                    AddPage("NewPage");
                if (NumberPages > 0)
                {
                    FileTabs.SelectedIndex = NumberPages - 1;
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (NumberPages == 0)
                {
                    Close();
                }
                int _neweselected = FileTabs.SelectedIndex - 1;
                Tabs.Remove(Tabs[FileTabs.SelectedIndex]);
                FileTabs.TabPages.Remove(FileTabs.SelectedTab);
                Encrypted.Remove(Encrypted[FileTabs.SelectedIndex]);
                if(_neweselected > 0)
                {
                    FileTabs.SelectedIndex = _neweselected;
                }
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

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenSource dlg = new OpenSource();
            string Source, FileName = "";
            int FileNameL = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                try
                {
                    Source = dlg.GetSource();
                    StreamReader reader = new StreamReader(Source);
                    string tmp = "", tmp2;
                    char temp;
                    while (!reader.EndOfStream)
                    {
                        temp = Convert.ToChar(reader.Read());
                        tmp += temp;
                    }
                    tmp2 = Decrypt(tmp);
                    reader.Close();
                    for (int loop = 0; loop < Source.Length; loop++)
                    {
                        if (Source[loop] == '\\')
                        {
                            FileNameL = 0;
                        }
                        else
                            FileNameL++;
                    }
                    for (int loop = Source.Length - FileNameL; loop < Source.Length; loop++)
                    {
                        FileName += Source[loop];
                    }
                    TabPage tb = new TabPage(FileName);
                    TextBox text = new TextBox();
                    string name = "JournalText";
                    text.Multiline = true;
                    text.Dock = DockStyle.Fill;
                    tb.Controls.Add(text);
                    name += NumberPages.ToString();
                    text.Name = name;
                    text.Text = tmp2;
                    FontFamily family = new FontFamily("Microsoft Sans Serif");
                    Font new_font = new Font(family, fontsize);
                    text.Font = new_font;
                    FileTabs.TabPages.Add(tb);
                    Tabs.Add(text);
                    Encrypted.Add(false);
                    NumberPages++;
                }
                catch
                {

                }
              
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openToolStripButton_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripButton_Click(sender,e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripButton_Click(sender, e);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info infoscreen = new Info();
            infoscreen.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(fontsize > 5)
            {
                fontsize--;
                toolStripTextBox1.Text = fontsize.ToString();
                for(int loop = 0; loop < NumberPages; loop++)
                {
                    FontFamily family = new FontFamily("Microsoft Sans Serif");
                    Font new_font = new Font(family,fontsize);
                    Tabs[loop].Font = new_font;
                }
               // Refresh();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (fontsize < 20)
            {
                fontsize++;
                toolStripTextBox1.Text = fontsize.ToString();
                if (NumberPages > 0)
                {
                    for (int loop = 0; loop < NumberPages; loop++)
                    {
                        FontFamily family = new FontFamily("Microsoft Sans Serif");
                        Font new_font = new Font(family, fontsize);
                        Tabs[loop].Font = new_font;
                    }
                }
               
                // Refresh();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
             Tabs[FileTabs.SelectedIndex].Copy(); 
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                Tabs[FileTabs.SelectedIndex].SelectedText = "";

            Tabs[FileTabs.SelectedIndex].Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                Tabs[FileTabs.SelectedIndex].Cut();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tabs[FileTabs.SelectedIndex].Undo();
            Tabs[FileTabs.SelectedIndex].ClearUndo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tabs[FileTabs.SelectedIndex].SelectAll();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save undo in buffer then redo it in this snip of code 
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender,e);
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender,e);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmp = "";
            if (Source != "")
            {
                string FileName = FileTabs.SelectedTab.Text;
               
                    DialogWindow dlg = new DialogWindow();
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        string change;
                        change = dlg.GetFileName() + ".txt";
                        FileTabs.SelectedTab.Text = change;
                        FileName = change;
                        dlg.Close();
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!Encrypted[FileTabs.SelectedIndex])
            {
                if (Tabs[FileTabs.SelectedIndex].Text != "")
                {
                    string _string = 
                    Encrypt(Tabs[FileTabs.SelectedIndex].Text);
                    Tabs[FileTabs.SelectedIndex].Text = _string;
                    Encrypted[FileTabs.SelectedIndex] = true;
                }
            }
            else
            {
                if (Tabs[FileTabs.SelectedIndex].Text != "")
                {
                    string _string =
                   Decrypt(Tabs[FileTabs.SelectedIndex].Text);
                    Tabs[FileTabs.SelectedIndex].Text = _string;
                    Encrypted[FileTabs.SelectedIndex] = false;
                }
            }
        }
    }
}
