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
        private string ConnectDB;
        public LoginDB()
        {

            InitializeComponent();
            try
            {
                Connect.ConnectionString = "server=localhost; userid=root; password=Mkoli123!; database=CryptNote_DB;";
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            SignUpUsername SignUp = new SignUpUsername();
            _64bitCreateAccount Key = new _64bitCreateAccount(); 
            if(SignUp.ShowDialog() == DialogResult.OK)
            {
                if(Key.ShowDialog() == DialogResult.OK)
                {
                    m_Username = SignUp.GetUsername();
                    m_key = Key.GetKey();
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

        }
    }
}
