using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Journal.Form1;

namespace Journal
{
    class Save_Open__File
    {
        List<DataVariable> DataVar;
        private string Source;
        byte[] Encryptedbytes;
       // List<string> FileSources = new List<string>();
        _32bitEncryption _32bit = new _32bitEncryption();
        
        public Save_Open__File(List<DataVariable> _DataVar, string _Source)
        {
            DataVar = _DataVar;
            Source = _Source;
        }
        public void SetEncryptedBytes(byte[] _Encrypt)
        {
            Encryptedbytes = _Encrypt;
        }
        public string Encrypt(string _tmp)
        {
            string EncrptText = "";
            byte[] Key;
            Key = _32bit.GetBytes(Properties.Settings.Default.Password);
            byte[] bytetostring;
            byte[] IV = _32bit.GetBytes("12700779");
            bytetostring = _32bit.encrypt_function(_tmp, Key, IV);
            EncrptText = Convert.ToBase64String(bytetostring);
            Encryptedbytes = bytetostring;
            //Debug.WriteLine("Encrpting Tab index: " + FileTabs.SelectedIndex.ToString() + " Name: " + Tabs[FileTabs.SelectedIndex].Name);
            return EncrptText;
        }

        public string Decrypt(string _tmp)
        {
            string DecryptText = "";
            byte[] Key;
            Key = _32bit.GetBytes(Properties.Settings.Default.Password);
            BinaryRW Read = new BinaryRW();
            byte[] bytetostring;
            byte[] IV = _32bit.GetBytes("12700779");
            bytetostring = Convert.FromBase64String(_tmp);
            DecryptText = _32bit.decrypt_function(bytetostring, Key, IV);
            //Debug.WriteLine("Decrypting Tab index: " + FileTabs.SelectedIndex.ToString() + " Name: " + Tabs[FileTabs.SelectedIndex].Name);
            return DecryptText;
        }
        public void SaveFile(TabControl FileTabs, List<DataVariable> DataVar)
        {

            string RTBText = "";

            string FileName = FileTabs.SelectedTab.Text;
            if (FileName.Contains("NewPage"))
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
                        //DataVar[FileTabs.SelectedIndex].FileSources = change;
                        DataVariable DatavarTemp = new DataVariable();
                        DatavarTemp.Tabs = DataVar[FileTabs.SelectedIndex].Tabs;
                        DatavarTemp.Encrypted = DataVar[FileTabs.SelectedIndex].Encrypted;
                        DatavarTemp.FileSources = change;
                        DataVar[FileTabs.SelectedIndex] = DatavarTemp;
                    }
                }
            }
                if (!DataVar[FileTabs.SelectedIndex].Encrypted)
                    RTBText = Encrypt(DataVar[FileTabs.SelectedIndex].Tabs.Rtf);
                BinaryRW write = new BinaryRW();
                write.writeBytes(Encryptedbytes, DataVar[FileTabs.SelectedIndex].FileSources);
                // Debug.WriteLine("Save Passed " + FileName);

        }
        public void SaveAllFile(TabControl FileTabs, List<DataVariable> DataVar, int NumberPages)
        {
            for (int mainloop = 0; mainloop < NumberPages; mainloop++)
            {
                string SaveText = "";

                FileTabs.SelectTab(mainloop);
                string FileName = FileTabs.SelectedTab.Text;
                if (FileName.Contains("NewPage"))
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
                            DataVariable DatavarTemp = new DataVariable();
                            DatavarTemp.Tabs = DataVar[mainloop].Tabs;
                            DatavarTemp.Encrypted = DataVar[mainloop].Encrypted;
                            DatavarTemp.FileSources = change;
                            DataVar[mainloop] = DatavarTemp;
                        }
                    }
                }
                if (!DataVar[FileTabs.SelectedIndex].Encrypted)
                    SaveText = Encrypt(DataVar[mainloop].Tabs.Rtf);
                BinaryRW write = new BinaryRW();
                write.writeBytes(Encryptedbytes, DataVar[mainloop].FileSources);
            }
        }

        public void SaveAs(TabControl FileTabs, List<DataVariable> DataVar)
        {
            string SaveAsText = "";
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
                    // FileSources[FileTabs.SelectedIndex] = change;
                    DataVariable DatavarTemp = new DataVariable();
                    DatavarTemp.Tabs = DataVar[FileTabs.SelectedIndex].Tabs;
                    DatavarTemp.Encrypted = DataVar[FileTabs.SelectedIndex].Encrypted; ;
                    DatavarTemp.FileSources = change;
                    DataVar[FileTabs.SelectedIndex] = DatavarTemp;
                }
            }
            if (!DataVar[FileTabs.SelectedIndex].Encrypted)
                SaveAsText = Encrypt(DataVar[FileTabs.SelectedIndex].Tabs.Rtf);
            BinaryRW write = new BinaryRW();
            write.writeBytes(Encryptedbytes, FileName);
        }
        public string[] OpenFile()
        {
            string[] ArgVal = new string[3];
            OpenSource dlg = new OpenSource();
            string  FileName = "";
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
                                //FileSources.Add(Source);
                                string DecryotedText;
                                string GrabbedText;
                                byte[] bytes;
                                BinaryRW Read = new BinaryRW();
                                bytes = Read.ReadBytes(Source);
                                GrabbedText = Convert.ToBase64String(bytes);
                                DecryotedText = Decrypt(GrabbedText);
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
                                ArgVal[0] = FileName;
                                ArgVal[1] = DecryotedText;
                                ArgVal[2] = Source;
                                return ArgVal;
                               // AddPage(FileName, DecryotedText);
                                //Debug.WriteLine("Open File Passed " + FileName);
                            }
                            catch
                            {
                               // Debug.WriteLine("Open File Failed " + FileName);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return null;
        }
    }
}
