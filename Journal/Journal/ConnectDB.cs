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
        private MySqlConnection Connect = new MySqlConnection();
        private MySqlDataReader read;
        private MySqlCommand cmd;

        public ConnectDB()
        {
            Connect.ConnectionString = "server=localhost; userid=tomalex; password=mkoli123; database=CryptNote_DB;";
        }
       public ConnectDB(string connect)
        {
            Connect.ConnectionString = connect;
        }
       
        public void ConnectToDB()
        {
            Connect.Open();
        }

        public void DisconectFromDB()
        {
            Connect.Close();
        }
        public MySqlDataReader WriteCommand(string Statement, string column)
        {
            cmd.CommandText = Statement;
            read = cmd.ExecuteReader();
            read.Read();
            return read;
        }
        public void ReadClose()
        {
            read.Close();
        }
        public void AddUser(string _username, string _key)
        {

        }
        public void DeleteUser(string _username)
        {

        }
        

    }
}
