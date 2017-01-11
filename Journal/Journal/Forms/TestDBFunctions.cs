using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public partial class TestDBFunctions : Form
    {
        UserInfo m_userinfo = new UserInfo();
        DBFunctions m_dbfn = new DBFunctions();
        Note m_Note; User m_user;
        public TestDBFunctions()
        {
            InitializeComponent();
            m_Note.m_IsOpen = false;
            m_Note.m_Name = "Test";
            m_Note.m_NoteId = "18";
            m_Note.m_NoteText = "TESTETSTETSTETSTETSTETSTETSTETSTETSTETSTETSTETSTET";
            m_Note.m_PassKey = "Test001";
            m_Note.m_UserId = "42";

            m_user.m_Email = "TALEX0_001@test.com";
            m_user.m_Key = "Test001";
            m_user.m_SQ = "Test001";
            m_user.m_SQA = "001Test";
            m_user.m_UserId = "42";
            m_user.m_Username = "TALEX0_001";
        }
        // Connect
        private void button6_Click(object sender, EventArgs e)
        {
            m_dbfn.ConnectDB();
            m_dbfn.OpenConectionDB();
            
            richTextBox1.Text += "Connect PASS\n";
        }
        // Disconnect
        private void button3_Click(object sender, EventArgs e)
        {
            m_userinfo.CloseConnection();
            richTextBox1.Text += "DisConnect PASS\n";
        }
        // Create User
        private void button1_Click(object sender, EventArgs e)
        {
            m_dbfn.ConnectDB();
            m_dbfn.OpenConectionDB();
           if( m_dbfn.CreateNewUser(m_user.m_Username, m_user.m_Key, m_user.m_Email, m_user.m_SQ, m_user.m_SQA))
                richTextBox1.Text += "Create User PASS\n";
           else
                richTextBox1.Text += "Create User FAIL\n";
            m_dbfn.CloseConnection();
            
        }
        // Check UserInfo
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                m_dbfn.ConnectDB();
                m_dbfn.OpenConectionDB();
                if(m_dbfn.CheckRights(m_user.m_Username, m_user.m_Key))
                    richTextBox1.Text += "Check User login PASS\n";
                else
                    richTextBox1.Text += "check User login FAIL\n";
                m_dbfn.CloseConnection();
            }
            catch(Exception ex)
            {
                richTextBox1.Text += "check User login FAIL\n";
            }
        }
        // AddNote
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                m_dbfn.ConnectDB();
                m_dbfn.OpenConectionDB();
                //m_dbfn.AddNote();
                m_dbfn.CloseConnection();
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "Add Note FAIL\n";
            }
        }
        // RemoveNote
        private void button7_Click(object sender, EventArgs e)
        {

        }
        // GetNote
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            m_dbfn.ConnectDB();
            m_dbfn.OpenConectionDB();
            if (m_dbfn.RemoveUser("TALEX0_001"))
                richTextBox1.Text += "Remove Pass";
            else
                richTextBox1.Text += "Remove Fail";
            m_dbfn.CloseConnection();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            m_dbfn.ConnectDB();
            m_dbfn.OpenConectionDB();
            string[] m_user;
            m_user = m_dbfn.PullUserInfo("TALEX0_001", "Test001");
            for (int loop = 0; loop < 6; loop++)
                richTextBox1.Text += m_user[loop] + " ";
            m_dbfn.CloseConnection();
        }
    }
}
