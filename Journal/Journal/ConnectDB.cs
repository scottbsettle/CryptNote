using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Journal
{
    class ConnectDB
    {
        MySqlConnection Connect = new MySqlConnection();
        MySqlDataReader read;
        MySqlCommand cmd;

       public ConnectDB(string connect)
        {
            Connect.ConnectionString = connect;
        }

        public void ConnectToDB()
        {
            Connect.Open();
        }

        public void WriteCommand(string Statement, string column)
        {

        }
    }
}
