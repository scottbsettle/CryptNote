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
        public struct DataVariable
        {
            public RichTextBox Tabs;
            public bool Encrypted;
            public string FileSources;
        }
        #region Variables
        static string Source = Properties.Settings.Default.SourceFile;
        int NumberPages = 1;
        int fontsize = 12;
        DataVariable Datavar = new DataVariable();
        static List<DataVariable> DataVarList = new List<DataVariable>();
        //List<RichTextBox> Tabs = new List<RichTextBox>();
        //List<bool> Encrypted = new List<bool>();
        Undo_Redo UnRedo = new Undo_Redo();
        Button temp = new Button();
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;
        _32bitEncryption _32bit = new _32bitEncryption();
        byte[] Encryptedbytes;
        //static List<string> FileSources = new List<string>();
        int WordCountint = 0;
        RichTextBox _Paste = new RichTextBox();
        Save_Open__File Save_Open = new Save_Open__File(DataVarList, Source);
        ColorDialog Colordlg = new ColorDialog();
        //   TabControl tb;

        #endregion
        #region Initialize
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            Datavar.Tabs = JournalText;
            int tabLength = FileTabs.ItemSize.Width;
            Datavar.Encrypted = false;
            Datavar.FileSources = "Entery.txt";
            DataVarList.Add(Datavar);
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
            if (_page <= NumberPages)
                FileTabs.TabPages[_page].Text = _name;

        }
        public void AddPage(string FileName, string _text)
        {
            TabPage tb = new TabPage(FileName);
            tb.BackColor = Color.White;
            RichTextBox RTB = new RichTextBox();
            RTB.BorderStyle = BorderStyle.None;
            string name = "New Page";
            RTB.ScrollBars = RichTextBoxScrollBars.Vertical;
            RTB.Multiline = true;
            RTB.Dock = DockStyle.Fill;
            RTB.WordWrap = false;
            tb.Controls.Add(RTB);
            RTB.ContextMenuStrip = contextMenuStrip1;
            name += NumberPages.ToString();
            RTB.Name = name;
            FontFamily family = new FontFamily("Times New Roman");
            Font new_font = new Font(family, fontsize);
            RTB.Font = new_font;
            RTB.Rtf = _text;
            FileTabs.TabPages.Add(tb);
            Datavar = new DataVariable();
            Datavar.Tabs = RTB;
            Datavar.Encrypted = false;
            NumberPages++;
            Datavar.FileSources = Source;
            DataVarList.Add(Datavar);
            FileTabs.SelectTab(NumberPages - 1);
            CalcWordCount();
            FontText.Text = fontsize.ToString();
            UnRedo.AddPage();
            Debug.WriteLine("Added new page " + FileName);


        }
        #endregion
        #region ToolStripButtons
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (NumberPages > 0)
            {
                Save_Open.SetEncryptedBytes(Encryptedbytes);
                Save_Open.SaveFile(FileTabs, DataVarList);
            }
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (NumberPages < 20)
            {
                WordCountint = 0;
                WordCountText.Text = WordCountint.ToString();
                Source = "Entery.txt";
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


                if (DataVarList.Count == 1)
                {
                    Close();
                }
                //FileSources.RemoveAt(FileTabs.SelectedIndex);
                int _neweselected = FileTabs.SelectedIndex - 1;
                //Tabs.Remove(DataVarList[FileTabs.SelectedIndex].Tabs);
                DataVarList.RemoveAt(FileTabs.SelectedIndex);
                FileTabs.TabPages.Remove(FileTabs.SelectedTab);
                UnRedo.RemovePage(FileTabs.SelectedIndex);
                //Encrypted.Remove(false);
                if (_neweselected > 0)
                {
                    FileTabs.SelectedIndex = _neweselected;
                }
                NumberPages--;
                Debug.WriteLine("Clossed Passed ");
            }
            catch
            {
                Debug.WriteLine("Close Failed");
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
            string[] Arg = Save_Open.OpenFile();
            if (Arg != null)
            {
                Source = Arg[2];
                AddPage(Arg[0], Arg[1]);
            }
            CalcWordCount();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openToolStripButton_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
                saveToolStripButton_Click(sender, e);
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
            if (DataVarList.Count > 0)
                if (fontsize > 8)
                {
                    fontsize--;
                    FontText.Text = fontsize.ToString();
                    if (DataVarList.Count > 0)
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = new Font(DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Name, fontsize,
                    DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Style, DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Unit);
                    Debug.WriteLine("Font Size -- ");
                }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
                if (fontsize < 72)
                {
                    fontsize++;
                    FontText.Text = fontsize.ToString();
                    if (DataVarList.Count > 0)
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = new Font(DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Name, fontsize,
               DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Style, DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Unit);
                  
                    Debug.WriteLine("Font Size ++");
                }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string buffertext = "";
            _Paste.Text = "";
            if (DataVarList.Count > 0)
                if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionLength > 0)
                {
                    _Paste.Text += (DataVarList[FileTabs.SelectedIndex].Tabs.SelectedText);
                    //  _Paste.Text = DataVarList[FileTabs.SelectedIndex].Tabs.SelectedText;
                    DataVarList[FileTabs.SelectedIndex].Tabs.Copy();
                }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.Paste();
                CalcWordCount();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
            {
                if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionLength > 0)
                {
                    WordCountint -= CalcWordCount(DataVarList[FileTabs.SelectedIndex].Tabs.SelectedText);
                    WordCountText.Text = WordCountint.ToString();
                    _Paste.Text = DataVarList[FileTabs.SelectedIndex].Tabs.SelectedText;
                    DataVarList[FileTabs.SelectedIndex].Tabs.SelectedText = "";
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoButton_Click(sender, e);
            CalcWordCount();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
                DataVarList[FileTabs.SelectedIndex].Tabs.SelectAll();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save undo in buffer then redo it in this snip of code 
            if (UnRedo.CheckRedu(FileTabs.SelectedIndex))
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.Rtf = UnRedo.GetRedo(FileTabs.SelectedIndex);
                DataVarList[FileTabs.SelectedIndex].Tabs.Select(DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length, DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length);
            }
            CalcWordCount();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
                pasteToolStripMenuItem_Click(sender, e);
            CalcWordCount();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (UnRedo.CheckUndo(FileTabs.SelectedIndex))
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.Rtf = UnRedo.GetUndo(FileTabs.SelectedIndex);
                DataVarList[FileTabs.SelectedIndex].Tabs.Select(DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length, DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length);
            }
            CalcWordCount();
        }
        //Save as 
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
            {
                Save_Open.SetEncryptedBytes(Encryptedbytes);
                Save_Open.SaveAs(FileTabs, DataVarList);
                Debug.WriteLine("Save As Called ");
            }
        }
        //Encrypt / Decrypt
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
            {
                if (!DataVarList[FileTabs.SelectedIndex].Encrypted)
                {
                    if (DataVarList[FileTabs.SelectedIndex].Tabs.Text != "")
                    {

                        string _string =
                       Save_Open.Encrypt(DataVarList[FileTabs.SelectedIndex].Tabs.Rtf);

                        DataVarList[FileTabs.SelectedIndex].Tabs.WordWrap = true;
                        DataVarList[FileTabs.SelectedIndex].Tabs.Text = _string;
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectAll();
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectionColor = Color.Black;
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBackColor = Color.WhiteSmoke;
                        DataVarList[FileTabs.SelectedIndex].Tabs.ReadOnly = true;
                        //DataVarList[FileTabs.SelectedIndex].Encrypted = true;
                        DataVarList[FileTabs.SelectedIndex].Tabs.Enabled = false;
                        Datavar = new DataVariable();
                        Datavar.Tabs = DataVarList[FileTabs.SelectedIndex].Tabs;
                        Datavar.Encrypted = true;
                        Datavar.FileSources = DataVarList[FileTabs.SelectedIndex].FileSources;
                        DataVarList[FileTabs.SelectedIndex] = Datavar;
                        Encrpyt_Decrypt_Button.BackColor = Color.LightGray;
                        FileTabs.SelectedTab.BackColor = Color.WhiteSmoke;
                    }
                }
                else
                {
                    if (DataVarList[FileTabs.SelectedIndex].Tabs.Text != "")
                    {
                        DataVarList[FileTabs.SelectedIndex].Tabs.WordWrap = false;
                        string _string = Save_Open.Decrypt(DataVarList[FileTabs.SelectedIndex].Tabs.Text);
                        DataVarList[FileTabs.SelectedIndex].Tabs.Rtf = _string;
                        DataVarList[FileTabs.SelectedIndex].Tabs.ReadOnly = false;
                        //DataVarList[FileTabs.SelectedIndex].Encrypted = false;
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectionStart = DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length;
                        DataVarList[FileTabs.SelectedIndex].Tabs.Enabled = true;
                        Datavar = new DataVariable();
                        Datavar.Tabs = DataVarList[FileTabs.SelectedIndex].Tabs;
                        Datavar.Encrypted = false;
                        Datavar.FileSources = DataVarList[FileTabs.SelectedIndex].FileSources;
                        DataVarList[FileTabs.SelectedIndex] = Datavar;
                        Encrpyt_Decrypt_Button.BackColor = Color.White;
                        FileTabs.SelectedTab.BackColor = DataVarList[FileTabs.SelectedIndex].Tabs.BackColor;
                    }
                }
            }
        }
        //Bullet points
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (DataVarList.Count > 0)
            {
                if (DataVarList[FileTabs.SelectedIndex].Tabs.BulletIndent == 0)
                {
                    DataVarList[FileTabs.SelectedIndex].Tabs.BulletIndent = 10;
                    DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBullet = true;
                    DataVarList[FileTabs.SelectedIndex].Tabs.SelectionIndent = 10;
                    BulletPointButton.BackColor = Color.LightGray;

                }
                else
                {
                    DataVarList[FileTabs.SelectedIndex].Tabs.BulletIndent = 0;
                    DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBullet = false;
                    DataVarList[FileTabs.SelectedIndex].Tabs.SelectionIndent = 0;
                    BulletPointButton.BackColor = Color.White;


                }
            }
        }


        //Save multiple files at once 
        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            if (NumberPages > 0)
            {
                Save_Open.SetEncryptedBytes(Encryptedbytes);
                Save_Open.SaveAllFile(FileTabs, DataVarList, NumberPages);
            }
        }
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog FontDia = new FontDialog();

            if (FontDia.ShowDialog() == DialogResult.OK)
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = FontDia.Font;
                fontsize = (int)DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Size + 1;
                FontText.Text = fontsize.ToString();

            }
        }
        //Change File Tab Background color
        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ColorDialog Colordlg = new ColorDialog();
            if (Colordlg.ShowDialog() == DialogResult.OK)
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.BackColor = Colordlg.Color;
                FileTabs.SelectedTab.BackColor = DataVarList[FileTabs.SelectedIndex].Tabs.BackColor;
                for (int loop = 0; loop < DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length; loop++)
                {
                    DataVarList[FileTabs.SelectedIndex].Tabs.Select(loop, 1);
                    if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBackColor != Color.Yellow)
                    {
                        DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBackColor = Colordlg.Color;
                    }
                }
                DataVarList[FileTabs.SelectedIndex].Tabs.Select(DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length, 0);
            }
        }

        #endregion
        #region Clac_WordCount
        //Calculate Word Count 
        private void CalcWordCount()
        {
            if (DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length > 0)
            {
                WordCountint = 0;
                string[] numbers;
                numbers = DataVarList[FileTabs.SelectedIndex].Tabs.Text.Split(' ');
                for (int loop = 0; loop < numbers.Length; loop++)
                {
                    if (numbers[loop] != "")
                        WordCountint++;
                }
                WordCountText.Text = WordCountint.ToString();
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
            string _tb = DataVarList[FileTabs.SelectedIndex].Tabs.Rtf;
            UnRedo.AddToUndo(FileTabs.SelectedIndex, _tb);
            CalcWordCount();
        }
        //Change word count to new tab word count
        private void FileTabs_SelectedIndexChanged(object sender, EventArgs e)
        {

            WordCountText.Text = WordCountint.ToString();
            fontsize = (int)DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Size;
            FontText.Text = fontsize.ToString();
            CalcWordCount();
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
                        if (DataVarList.Count > 0)
                            DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = new Font(DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Name, fontsize,
                         DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Style, DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Unit);

                    }
                }
            }
        }
        #endregion
        #region Bold_Underline_Italic
        //Make Text Italic
        private void ItalicButton_Click(object sender, EventArgs e)
        {
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Italic)
                ItalicButton.BackColor = Color.White;
            else
                ItalicButton.BackColor = Color.LightGray;
            Font NFont = new Font(DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont, FontStyle.Regular);

            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Bold == true)
                NFont = new Font(NFont, FontStyle.Bold | NFont.Style);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Italic == false)
                NFont = new Font(NFont, FontStyle.Italic | NFont.Style);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Underline == true)
                NFont = new Font(NFont, FontStyle.Underline | NFont.Style);
            DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = NFont;
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Bold == true)
                BoldButton.BackColor = Color.White;
            else
                BoldButton.BackColor = Color.LightGray;
            Font NFont = new Font(DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont, FontStyle.Regular);

            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Bold == false)
                NFont = new Font(NFont, FontStyle.Bold | NFont.Style);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Italic == true)
                NFont = new Font(NFont, FontStyle.Italic | NFont.Style);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Underline == true)
                NFont = new Font(NFont, FontStyle.Underline | NFont.Style);
            DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = NFont;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Underline == true)
                UnderlineButton.BackColor = Color.White;
            else
                UnderlineButton.BackColor = Color.LightGray;

            Font NFont = new Font(DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont, FontStyle.Regular);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Bold == true)
                NFont = new Font(NFont, FontStyle.Bold | NFont.Style);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Italic == true)
                NFont = new Font(NFont, FontStyle.Italic | NFont.Style);
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont.Underline == false)
                NFont = new Font(NFont, FontStyle.Underline | NFont.Style);
            DataVarList[FileTabs.SelectedIndex].Tabs.SelectionFont = NFont;





        }
        #endregion
        #region Contextmenue_RightClick_Tab_Methods
        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBackColor != Color.Yellow)
                DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBackColor = Color.Yellow;
            else
                DataVarList[FileTabs.SelectedIndex].Tabs.SelectionBackColor = DataVarList[FileTabs.SelectedIndex].Tabs.BackColor;
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
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 12, e.Bounds.Top);
            e.Graphics.DrawString(FileTabs.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 2, e.Bounds.Top + 6);

            e.DrawFocusRectangle();
        }

        private void FileTabs_DoubleClick(object sender, EventArgs e)
        {
            closeToolStripMenuItem_Click(sender, e);
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (UnRedo.CheckRedu(FileTabs.SelectedIndex))
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.Rtf = UnRedo.GetRedo(FileTabs.SelectedIndex);
                DataVarList[FileTabs.SelectedIndex].Tabs.Select(DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length, DataVarList[FileTabs.SelectedIndex].Tabs.Text.Length);
            }
            CalcWordCount();
        }

        private void FontColorButton_Click(object sender, EventArgs e)
        {
            //ColorDialog Colordlg = new ColorDialog();
            if (Colordlg.ShowDialog() == DialogResult.OK)
            {
                DataVarList[FileTabs.SelectedIndex].Tabs.SelectionColor = Colordlg.Color;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataVarList.Clear();
        }
    }
}