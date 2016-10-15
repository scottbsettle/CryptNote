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
using System.Diagnostics;

namespace Journal
{
    public partial class Form1 : Form
    {
        #region Variables
        string Source = Properties.Settings.Default.SourceFile;
        int NumberPages = 1;
        int fontsize = 12;
        List<RichTextBox> Tabs = new List<RichTextBox>();
        List<bool> Encrypted = new List<bool>();
        Button temp = new Button();
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;
        _32bitEncryption _32bit = new _32bitEncryption();
        byte[] Encryptedbytes;
        string[] FileSources = new string[20];
        int WordCountint = 0;
        RichTextBox _Paste = new RichTextBox();
     //   TabControl tb;

        #endregion
        #region Initialize
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
        #endregion
        #region Custom_Methods
        public TabControl GetFiletabs()
        {
            return FileTabs;
        }
        public void SetTab(int _page, string _name)
        {
            if(_page <= NumberPages)
                FileTabs.TabPages[_page].Text = _name;
          
        }
        public void AddPage(string FileName, string _text)
        {
            TabPage tb = new TabPage(FileName);
            tb.BackColor = Color.White;
            RichTextBox text = new RichTextBox();
            text.BorderStyle = BorderStyle.None;
            string name = "New Page";
            text.ScrollBars = RichTextBoxScrollBars.Vertical;
            text.Multiline = true;
            text.Dock = DockStyle.Fill;
            tb.Controls.Add(text);
            text.ContextMenuStrip = contextMenuStrip1;
            name += NumberPages.ToString();
            text.Name = name;
            text.Text = _text;
            FontFamily family = new FontFamily("Microsoft Sans Serif");
            Font new_font = new Font(family, fontsize);
            text.Font = new_font;
            FileTabs.TabPages.Add(tb);
            Tabs.Add(text);
            Encrypted.Add(false);
            NumberPages++;
            FileSources[NumberPages - 1] = Source;
            FileTabs.SelectTab(NumberPages - 1);
            CalcWordCount();
            FontText.Text = fontsize.ToString();

            Debug.WriteLine("Added new page " + FileName);


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
            Debug.WriteLine("Encrpting Tab index: " + FileTabs.SelectedIndex.ToString() + " Name: " + Tabs[FileTabs.SelectedIndex].Name);
            return tmp;
        }
        public string Decrypt(string _tmp)
        {
            string tmp = "";
            byte[] Key;
            Key = _32bit.GetBytes(Properties.Settings.Default.Password);
            BinaryRW Read = new BinaryRW();
            byte[] bytetostring;
            byte[] IV = _32bit.GetBytes("12700779");
                bytetostring = Convert.FromBase64String(_tmp);
                tmp = _32bit.decrypt_function(bytetostring, Key, IV);
            Debug.WriteLine("Decrypting Tab index: " + FileTabs.SelectedIndex.ToString() + " Name: " + Tabs[FileTabs.SelectedIndex].Name);
            return tmp;
        }
       
        #endregion
        #region ToolStripButtons
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
                        saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
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
                    Debug.WriteLine("Save Passed " + FileName);
                }
            }
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (NumberPages < 20)
            {
                WordCountint = 0;
                WordCountText.Text = WordCountint.ToString();
                if (NumberPages != 0)
                    AddPage("NewPage" + NumberPages, "");
                else if (NumberPages == 0)
                    AddPage("NewPage", "");
                if (NumberPages > 0)
                {
                    FileTabs.SelectTab(NumberPages - 1);
                }
                Debug.WriteLine("Added new page ");
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
                Debug.WriteLine("Clossed Passed ");
            }
            catch
            {
                Debug.WriteLine("Close Failed" );
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
                            try
                            {
                                Source = openFileDialog1.FileName;
                                string tmp2;
                                string tmp3;
                                byte[] bytes;
                                BinaryRW Read = new BinaryRW();
                                bytes = Read.ReadBytes(Source);
                                tmp3 = Convert.ToBase64String(bytes);
                                tmp2 = Decrypt(tmp3);
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
                                AddPage(FileName, tmp2);
                                Debug.WriteLine("Open File Passed " + FileName);
                            }
                            catch
                            {
                                Debug.WriteLine("Open File Failed " + FileName);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
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
            Debug.WriteLine("Info Screen Called ");
            infoscreen.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                if (fontsize > 8)
            {
                fontsize--;
                FontText.Text = fontsize.ToString();
                    if(Tabs.Count > 0)
                    Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont.Name, fontsize,
                Tabs[FileTabs.SelectedIndex].SelectionFont.Style, Tabs[FileTabs.SelectedIndex].SelectionFont.Unit);
                    Debug.WriteLine("Font Size -- ");
                }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
                if (fontsize < 72)
            {
                fontsize++;
                FontText.Text = fontsize.ToString();
                if (Tabs.Count > 0)
                        Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont.Name, fontsize,
               Tabs[FileTabs.SelectedIndex].SelectionFont.Style, Tabs[FileTabs.SelectedIndex].SelectionFont.Unit);
                    Debug.WriteLine("Font Size ++");
                }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string buffertext = "";
            _Paste.Text = "";
            if (Tabs.Count > 0)
                if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                {
                        _Paste.Text += Tabs[FileTabs.SelectedIndex].SelectedText;
                    //  _Paste.Text = Tabs[FileTabs.SelectedIndex].SelectedText;
                    Tabs[FileTabs.SelectedIndex].Copy();
                }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                {
                    WordCountint -= CalcWordCount(Tabs[FileTabs.SelectedIndex].SelectedText);
                    Tabs[FileTabs.SelectedIndex].SelectedText = "";
                }
                if (_Paste.Text.Length > 0)
                {
                    WordCountint += CalcWordCount(_Paste.Text);
                    WordCountText.Text = WordCountint.ToString();
                        Tabs[FileTabs.SelectedIndex].SelectionFont = _Paste.SelectionFont;
                    Tabs[FileTabs.SelectedIndex].Paste();

                   
                }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (Tabs[FileTabs.SelectedIndex].SelectionLength > 0)
                {
                    WordCountint -= CalcWordCount(Tabs[FileTabs.SelectedIndex].SelectedText);
                    WordCountText.Text = WordCountint.ToString();
                    _Paste.Text = Tabs[FileTabs.SelectedIndex].SelectedText;
                    Tabs[FileTabs.SelectedIndex].SelectedText = "";
                }
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
            CalcWordCount();
        }
        //Save as 
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
                                FileNameL = 0;
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

                Debug.WriteLine("Save As Called ");
            }
        }
        //Encrypt / Decrypt
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Tabs.Count > 0)
            {
                if (!Encrypted[FileTabs.SelectedIndex])
                {
                    if (Tabs[FileTabs.SelectedIndex].Text != "")
                    {
                       
                        string _string =
                        Encrypt(Tabs[FileTabs.SelectedIndex].Rtf);
                            Tabs[FileTabs.SelectedIndex].Rtf = _string;
                        Tabs[FileTabs.SelectedIndex].ReadOnly = true;
                        Encrypted[FileTabs.SelectedIndex] = true;
                        Tabs[FileTabs.SelectedIndex].Enabled = false;
                    }
                }
                else
                {
                    if (Tabs[FileTabs.SelectedIndex].Text != "")
                    {
                        string _string = Decrypt(Tabs[FileTabs.SelectedIndex].Rtf);
                        Tabs[FileTabs.SelectedIndex].Rtf = _string;
                        Tabs[FileTabs.SelectedIndex].ReadOnly = false;
                        Encrypted[FileTabs.SelectedIndex] = false;
                        Tabs[FileTabs.SelectedIndex].SelectionStart = Tabs[FileTabs.SelectedIndex].Text.Length;
                        Tabs[FileTabs.SelectedIndex].Enabled = true;
                    }
                }
            }
        }
        //Bullet points
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

       
        //Save multiple files at once 
        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            if (NumberPages > 0)
            {
                for (int mainloop = 0; mainloop < NumberPages; mainloop++)
                {
                    string tmp = "";
                    if (Source != "")
                    {
                        FileTabs.SelectTab(mainloop);
                        string FileName = FileTabs.SelectedTab.Text ;
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
                                    FileSources[mainloop] = change;
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
                                    FileSources[mainloop] = change;
                                }
                            }
                        }
                        if (!Encrypted[mainloop])
                            tmp = Encrypt(Tabs[mainloop].Text);
                        BinaryRW write = new BinaryRW();
                        write.writeBytes(Encryptedbytes, FileSources[mainloop]);
                    }
                }
            }
        }
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog FontDia = new FontDialog();

            if (FontDia.ShowDialog() == DialogResult.OK)
            {
                Tabs[FileTabs.SelectedIndex].SelectionFont = FontDia.Font;
                fontsize = (int)Tabs[FileTabs.SelectedIndex].SelectionFont.Size + 1;
                FontText.Text = fontsize.ToString();

            }
        }
        //Change File Tab Background color
        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog Colordlg = new ColorDialog();
            if (Colordlg.ShowDialog() == DialogResult.OK)
            {
                Tabs[FileTabs.SelectedIndex].BackColor = Colordlg.Color;
                FileTabs.SelectedTab.ForeColor = Tabs[FileTabs.SelectedIndex].BackColor;
            }
        }

        #endregion
        #region Clac_WordCount
        //Calculate Word Count 
        private void CalcWordCount()
        {
            if (Tabs[FileTabs.SelectedIndex].Text.Length > 0)
            {
                WordCountint = 0;
                string[] numbers;
                numbers = Tabs[FileTabs.SelectedIndex].Text.Split(' ');
                WordCountText.Text = numbers.Length.ToString();
            }
            else
                WordCountText.Text = "0";
        }
        //Calculate the word count overload 1
        private int CalcWordCount(string _Word)
        {
            if (_Word.Length > 0)
            {
                string[] numbers;
                numbers = _Word.Split(' ');
                return numbers.Length;
            }
            else
            return 0;
        }
        #endregion
        #region VS_Methods
        private void FileTabs_Selected(object sender, TabControlEventArgs e)
        {
            throw new NotImplementedException();
        }
        //Do word count, update when user does lower case i to I 
        private void FileTabs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (Tabs[FileTabs.SelectedIndex].SelectedText.Length > 0)
                {
                    WordCountint -= CalcWordCount(Tabs[FileTabs.SelectedIndex].SelectedText);
                    WordCountText.Text = WordCountint.ToString();
                    CalcWordCount();
                }
                else if (Tabs[FileTabs.SelectedIndex].Text.Length > 1)
                    if (Tabs[FileTabs.SelectedIndex].Text[Tabs[FileTabs.SelectedIndex].Text.Length - 1] == ' ' && Tabs[FileTabs.SelectedIndex].Text[Tabs[FileTabs.SelectedIndex].Text.Length - 2] != ' ')
                    {
                        if (WordCountint > 0)
                            WordCountint--;
                        WordCountText.Text = WordCountint.ToString();
                    }
                fontsize = (int)Tabs[FileTabs.SelectedIndex].SelectionFont.Size + 1;
                FontText.Text = fontsize.ToString();
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (Tabs[FileTabs.SelectedIndex].SelectedText.Length > 0)
                {
                    WordCountint -= CalcWordCount(Tabs[FileTabs.SelectedIndex].SelectedText);
                    WordCountText.Text = WordCountint.ToString();
                }
                else if (Tabs[FileTabs.SelectedIndex].Text.Length > 0)
                    if (Tabs[FileTabs.SelectedIndex].Text[Tabs[FileTabs.SelectedIndex].Text.Length - 1] != ' ')
                    {
                        WordCountint++;
                        WordCountText.Text = WordCountint.ToString();
                    }

                //if (Tabs[FileTabs.SelectedIndex].Text.Length > 0)
                //    if (Tabs[FileTabs.SelectedIndex].Text[Tabs[FileTabs.SelectedIndex].Text.Length - 1] == 'i')
                //    {

                //        Tabs[FileTabs.SelectedIndex].Text = Tabs[FileTabs.SelectedIndex].Text.Replace(" i", " I");
                //        Tabs[FileTabs.SelectedIndex].SelectionStart = Tabs[FileTabs.SelectedIndex].Text.Length;
                //    }
            }
            CalcWordCount();
        }
        //Change word count to new tab word count
        private void FileTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            WordCountint = 0;
            for (int loop = 0; loop < Tabs[FileTabs.SelectedIndex].Text.Length; loop++)
            {
                if (loop > 1)
                    if (Tabs[FileTabs.SelectedIndex].Text.Length > 0)
                        if (Tabs[FileTabs.SelectedIndex].Text[loop] == ' ')
                            if (Tabs[FileTabs.SelectedIndex].Text[loop - 1] != ' ')
                            {
                                WordCountint++;
                                WordCountText.Text = WordCountint.ToString();
                            }
            }
            WordCountText.Text = WordCountint.ToString();
            fontsize = (int)Tabs[FileTabs.SelectedIndex].SelectionFont.Size;
            FontText.Text = fontsize.ToString();
        }
        //Change Font
        private void JournalText_Resize(object sender, EventArgs e)
        {
            Padding pad = new Padding(Size.Width - 400, WordCount.Margin.Top, WordCount.Margin.Right, WordCount.Margin.Bottom);
            WordCount.Margin = pad;
        }
        //Make sure no one can type letters or special characters in Font size
        private void FontText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        //When user hits enter on the font size 
        //TODO: need to make it go back to the files tab
        private void FontText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FontText.Text.Length > 0)
                {
                    int convert_string_int;
                    if (Int32.TryParse(FontText.Text, out convert_string_int))
                    {
                        fontsize = convert_string_int;
                        if (Tabs.Count > 0)
                            Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont.Name, fontsize,
                        Tabs[FileTabs.SelectedIndex].SelectionFont.Style, Tabs[FileTabs.SelectedIndex].SelectionFont.Unit);
                       
                    }
                }
            }
        }
        #endregion
        #region Bold_Underline_Italic
        //Make Text Italic
        private void ItalicButton_Click(object sender, EventArgs e)
        {
            if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Regular);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Italic | FontStyle.Bold);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold | FontStyle.Underline);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline | FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline | FontStyle.Bold | FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold | FontStyle.Italic);
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //Font temp = new Font(Tabs[FileTabs.SelectedIndex].Font, Tabs[FileTabs.SelectedIndex].Font.Style);

            if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Regular);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold | FontStyle.Underline);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold | FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline | FontStyle.Italic);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Regular);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline | FontStyle.Bold);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == false)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline | FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == false && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Underline | FontStyle.Bold | FontStyle.Italic);
            else if (Tabs[FileTabs.SelectedIndex].SelectionFont.Underline == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Bold == true && Tabs[FileTabs.SelectedIndex].SelectionFont.Italic == true)
                Tabs[FileTabs.SelectedIndex].SelectionFont = new Font(Tabs[FileTabs.SelectedIndex].SelectionFont, FontStyle.Bold | FontStyle.Italic);

        }
        #endregion
        #region Contextmenue_RightClick_Tab_Methods
        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tabs[FileTabs.SelectedIndex].SelectionBackColor != Color.Yellow)
                Tabs[FileTabs.SelectedIndex].SelectionBackColor = Color.Yellow;
            else
                Tabs[FileTabs.SelectedIndex].SelectionBackColor = Tabs[FileTabs.SelectedIndex].BackColor;
        }
       
        private void encrpytDecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllButton_Click(sender, e);
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton9_Click(sender, e);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton8_Click(sender, e);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItalicButton_Click(sender, e);
        }
        #endregion

        private void FileTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle rec = new Rectangle();
            rec.Size = e.Bounds.Size;
            rec.X = e.Bounds.X;
            rec.Y = e.Bounds.Y;
            rec.Location = e.Bounds.Location;
           // e.Graphics.DrawRectangle(Pens.Black, rec);
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 12, e.Bounds.Top );
            e.Graphics.DrawString(FileTabs.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 2, e.Bounds.Top + 6);
            
            e.DrawFocusRectangle();
        }

        private void FileTabs_DoubleClick(object sender, EventArgs e)
        {
            closeToolStripMenuItem_Click(sender, e);
        }
    }
}


