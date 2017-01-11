using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace Journal.DataBase
{
    class LocalDB
    {
        // Connect to Database
        private static SQLiteConnection m_dbConnection;
        // Execute SQL command
        private SQLiteCommand m_Command;
        // Execute sql query
        private SQLiteDataReader m_Reader;
     
        public LocalDB()
        {

        }
        // Create Database
        public void CreateDB()
        {
            SQLiteConnection.CreateFile("CryptNote_db.sqlite");
        }
        // Connect to DB 
        public void ConnectDB()
        {
            m_dbConnection = new SQLiteConnection("Data Source=CryptNote_db.sqlite;Version=3;");
            m_dbConnection.Open();
        }
        // Execute SQL command 
        public void ExecuteQueryTable(string _query)
        {
            m_Command = new SQLiteCommand(_query, m_dbConnection);
            m_Command.ExecuteNonQuery();
        }
        // read query from table 
        public SQLiteDataReader ReadFromTable(string _query)
        {
            m_Command = new SQLiteCommand(_query, m_dbConnection);
            m_Reader = m_Command.ExecuteReader();
            return m_Reader;
        }
       
    }
}
