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
        #region Variables
        string Source = Properties.Settings.Default.SourceFile;
        int NumberPages = 1;
        int fontsize = 9;
        List<RichTextBox> Tabs = new List<RichTextBox>();
        List<bool> Encrypted = new List<bool>();
        Button temp = new Button();
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;
        _32bitEncryption _32bit = new _32bitEncryption();
        byte[] Encryptedbytes;
        string[] FileSources = new string[20];
        #endregion
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            Tabs.Add(JournalText);
            int tabLength = FileTabs.ItemSize.Width;
            Encrypted.Add(false);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // get the inital length
            int tabLength = FileTabs.ItemSize.Width;

            // measure the text in each tab and make adjustment to the size
            for (int i = 0; i < this.FileTabs.TabPages.Count; i++)
            {
                TabPage currentPage = FileTabs.TabPages[i];
                FileTabs.Padding = new System.Drawing.Point(5, 3);
                int currentTabLength = TextRenderer.MeasureText(currentPage.Text, FileTabs.Font).Width;
                // adjust the length for what text is written
                currentTabLength += LEADING_SPACE + CLOSE_SPACE + CLOSE_AREA;

                if (currentTabLength > tabLength)
                {
                    tabLength = currentTabLength;
                }
            }
            Size newTabSize = new Size(tabLength, FileTabs.ItemSize.Height);
            FileTabs.ItemSize = newTabSize;
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
            RichTextBox text = new RichTextBox();
            text.BorderStyle = BorderStyle.None;
            text.MaxLength = 0;
            text.ScrollBars = RichTextBoxScrollBars.Vertical;
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
            string tmp = "";
            byte[] Key;
            Key = _32bit.GetBytes(Properties.Settings.Default.Password);
            byte[] bytetostring;
            byte[] IV = _32bit.GetBytes("12700779");
            bytetostring = _32bit.encrypt_function(_tmp, Key, IV);
            tmp = Convert.ToBase64String(bytetostring);
            Encryptedbytes = bytetostring;
            return tmp;
        }
        public string Decrypt(string _tmp)
        {
            string tmp = "";
            byte[] Key;
            Key = _32bit.GetBytes(Properties.Settings.Default.Password);
            BinaryRW Read = new BinaryRW();
            
            byte[] bytetostring = Read.ReadBytes(_tmp);
            byte[] IV = _32bit.GetBytes("12700779");
            if(bytetostring != null)
            tmp = _32bit.decrypt_function(bytetostring, Key, IV);
            else
            {
                bytetostring = Convert.FromBase64String(_tmp);
                tmp = _32bit.decrypt_function(bytetostring, Key, IV);
            }
            return tmp;
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (NumberPages > 0)
            {
                string tmp = "";
                if (Source != "")
                {
                    string FileName = FileTabs.SelectedTab.Text;
                    if (FileName == "NewPage")
                    {
                        FileName = "";
                        Stream myStream;
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        int FileNameL = 0;
                        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 2;
                        saveFileDialog1.RestoreDirectory = true;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if ((myStream = saveFileDialog1.OpenFile()) != null)
                            {
                                // Code to write the stream goes here.
                                string change;
                                change = saveFileDialog1.FileName;
                                for (int loop = 0; loop < change.Length; loop++)
                                {
                                    if (change[loop] == '\\')
                                    {
                                        FileNameL = 0;
                                    }
                                    else
                                        FileNameL++;
                                }
                                for (int loop = change.Length - FileNameL; loop < change.Length; loop++)
                                {
                                    FileName += change[loop];
                                }
                                FileTabs.SelectedTab.Text = FileName;
                                FileName = change;
                                myStream.Close();
                                FileSources[FileTabs.SelectedIndex] = change;
                            }
                        }
                    }
                    else if (FileTabs.SelectedIndex > 0 && FileName == "NewPage" + FileTabs.SelectedIndex.ToString())
                    {

                        FileName = "";
                        Stream myStream;
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        int FileNameL = 0;
                        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 2;
                        saveFileDialog1.RestoreDirectory = true;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if ((myStream = saveFileDialog1.OpenFile()) != null)
                            {
                                // Code to write the stream goes here.
                                string change;
                                change = saveFileDialog1.FileName;
                                for (int loop = 0; loop < change.Length; loop++)
                                {
                                    if (change[loop] == '\\')
                                    {
                                        FileNameL = 0;
                                    }
                                    else
                                        FileNameL++;
                                }
                                for (int loop = change.Length - FileNameL; loop < change.Length; loop++)
                                {
                                    FileName += change[loop];
                                }
                                FileTabs.SelectedTab.Text = FileName;
                                FileName = change;
                                myStream.Close();
                                FileSources[FileTabs.SelectedIndex] = change;
                            }
                        }
                    }
                    if (!Encrypted[FileTabs.SelectedIndex])
                        tmp = Encrypt(Tabs[FileTabs.SelectedIndex].Text);
                    BinaryRW write = new BinaryRW();
                    write.writeBytes(Encryptedbytes, FileSources[FileTabs.SelectedIndex]);
                }
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
                    FileTabs.SelectTab(NumberPages - 1);
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tabs.Count == 1)
                {
                    Close();
                }
                if(FileTabs.SelectedIndex != NumberPages - 1 && FileTabs.SelectedIndex != 0)
                for (int loop = FileTabs.SelectedIndex; loop < NumberPages; loop++)
                {
                        FileSources[loop] = FileSources[loop + 1]; 
                }
                int _neweselected = FileTabs.SelectedIndex - 1;
                Tabs.Remove(Tabs[FileTabs.SelectedIndex]);
                FileTabs.TabPages.Remove(FileTabs.SelectedTab);
                Encrypted.Remove(false);
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
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            try
                            {
                                Source = openFileDialog1.FileName;
                                string tmp2;
                                BinaryRW Read = new BinaryRW();
                                Read.ReadBytes(Source);
                                tmp2 = Decrypt(Source);
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
                                RichTextBox text = new RichTextBox();
                                text.BorderStyle = BorderStyle.None;
                                string name = "JournalText";
                                text.ScrollBars = RichTextBoxScrollBars.Vertical;
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
                                FileSources[NumberPages - 1] = Source;
                                FileTabs.SelectTab(NumberPages - 1);
                            }
                            catch
                            {

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            #region Shit open file
            //if (DialogResult.OK == dlg.ShowDialog())
            //{
            //    try
            //    {
            //        Source = dlg.GetSource();
            //        //StreamReader reader = new StreamReader(Source);
            //        string tmp = "", tmp2;
            //        char temp;
            //        //while (!reader.EndOfStream)
            //        //{
            //        //    temp = Convert.ToChar(reader.Read());
            //        //    tmp += temp;
            //        //}
            //        //tmp2 = Decrypt(tmp);
            //        //reader.Close();

            //        BinaryRW Read = new BinaryRW();
            //        Read.ReadBytes(Source);
            //        tmp2 = Decrypt(Source);
            //        for (int loop = 0; loop < Source.Length; loop++)
            //        {
            //            if (Source[loop] == '\\')
            //            {
            //                FileNameL = 0;
            //            }
            //            else
            //                FileNameL++;
            //        }
            //        for (int loop = Source.Length - FileNameL; loop < Source.Length; loop++)
            //        {
            //            FileName += Source[loop];
            //        }
            //        TabPage tb = new TabPage(FileName);
            //        TextBox text = new TextBox();
            //        string name = "JournalText";
            //        text.Multiline = true;
            //        text.Dock = DockStyle.Fill;
            //        tb.Controls.Add(text);
            //        name += NumberPages.ToString();
            //        text.Name = name;
            //        text.Text = tmp2;
            //        FontFamily family = new FontFamily("Microsoft Sans Serif");
            //        Font new_font = new Font(family, fontsize);
            //        text.Font = new_font;
            //        FileTabs.TabPages.Add(tb);
            //        Tabs.Add(text);
            //        Encrypted.Add(false);
            //        NumberPages++;
            //    }
            //    catch
            //    {

            //    }

            //}
            #endregion
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openToolStripButton_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
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
            if (Tabs.Count > 0)
                if (fontsize > 5)
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
            if (Tabs.Count > 0)
                if (fontsize < 20)
            {
                fontsize++;
                toolStripTextBox1.Text = fontsize.ToString();
                if (Tabs.Count > 0)
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
            if (Tabs.Count > 0)
                if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
             Tabs[FileTabs.SelectedIndex].Copy(); 
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                    Tabs[FileTabs.SelectedIndex].SelectedText = "";

                Tabs[FileTabs.SelectedIndex].Paste();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                    Tabs[FileTabs.SelectedIndex].Cut();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                Tabs[FileTabs.SelectedIndex].Undo();
                Tabs[FileTabs.SelectedIndex].ClearUndo();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                Tabs[FileTabs.SelectedIndex].SelectAll();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save undo in buffer then redo it in this snip of code 
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                cutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                copyToolStripMenuItem_Click(sender,e);
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                pasteToolStripMenuItem_Click(sender,e);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                undoToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                string tmp = "";

                string FileName = "";
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                int FileNameL = 0;
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        // Code to write the stream goes here.
                        string change;
                        change = saveFileDialog1.FileName;
                        for (int loop = 0; loop < change.Length; loop++)
                        {
                            if (change[loop] == '\\')
                            {
                                FileNameL = 0;
                            }
                            else
                                FileNameL++;
                        }
                        for (int loop = change.Length - FileNameL; loop < change.Length; loop++)
                        {
                            FileName += change[loop];
                        }
                        FileTabs.SelectedTab.Text = FileName;
                        FileName = change;
                        myStream.Close();
                        FileSources[FileTabs.SelectedIndex] = change;
                    }
                }
                if (!Encrypted[FileTabs.SelectedIndex])
                    tmp = Encrypt(Tabs[FileTabs.SelectedIndex].Text);
                BinaryRW write = new BinaryRW();
                write.writeBytes(Encryptedbytes, FileName);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (!Encrypted[FileTabs.SelectedIndex])
                {
                    if (Tabs[FileTabs.SelectedIndex].Text != "")
                    {
                        string _string =
                        Encrypt(Tabs[FileTabs.SelectedIndex].Text);
                        Tabs[FileTabs.SelectedIndex].Text = _string;
                        Tabs[FileTabs.SelectedIndex].ReadOnly = true;
                        Encrypted[FileTabs.SelectedIndex] = true;
                    }
                }
                else
                {
                    if (Tabs[FileTabs.SelectedIndex].Text != "")
                    {
                        string _string = Decrypt(Tabs[FileTabs.SelectedIndex].Text);
                        Tabs[FileTabs.SelectedIndex].Text = _string;
                        Tabs[FileTabs.SelectedIndex].ReadOnly = false;
                        Encrypted[FileTabs.SelectedIndex] = false;
                    }
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (Tabs[FileTabs.SelectedIndex].BulletIndent == 0)
                {
                    Tabs[FileTabs.SelectedIndex].BulletIndent = 10;
                    Tabs[FileTabs.SelectedIndex].SelectionBullet = true;
                    Tabs[FileTabs.SelectedIndex].SelectionIndent = 10;

                }
                else
                {
                    Tabs[FileTabs.SelectedIndex].BulletIndent = 0;
                    Tabs[FileTabs.SelectedIndex].SelectionBullet = false;
                    Tabs[FileTabs.SelectedIndex].SelectionIndent = 0;


                }
            }
        }

    }
}
