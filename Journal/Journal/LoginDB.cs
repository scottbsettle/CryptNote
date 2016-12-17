using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Journal
{
    public partial class LoginDB : Form
    {
        private string m_Username, m_key;
        private ConnectDB Connect = new ConnectDB();
        public LoginDB()
        {

            InitializeComponent();
            try
            {
                Connect.ConnectToDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            SignUpSystem SignUp = new SignUpSystem();
            if(SignUp.ShowDialog() == DialogResult.OK)
            {
                string userN, pswrd;
               userN = SignUp.GetUsername();
               pswrd = SignUp.GetPassword();
               
            }

        }

        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide(); 
            if(new Login().ShowDialog() == DialogResult.OK){
                Show();
            }
            else
            {
                Close();
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

        }
    }
}
