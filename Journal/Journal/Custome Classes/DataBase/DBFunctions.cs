using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Journal.DataBase;

namespace Journal
{
    class DBFunctions
    {
        private DBCommands m_Commands;
        private MySqlDataReader m_SQLReader;
        private LocalDB m_localDB;
        private MySqlConnection Connect;
        private bool m_Online;
        private string m_query;
       
       public  DBFunctions()
        {
            m_Commands = new DBCommands();
            m_localDB = new LocalDB();
            m_Online = false;
            Connect = new MySqlConnection();
        }
        public void ConnectDB()
        {
            Connect.ConnectionString = "server=10.0.0.5; userid=tomalex; password=mkoli123; database=CryptNote_DB;";
        }
        public void ConnectDB(string connect)
        {
            Connect.ConnectionString = connect;

        }
        public void OpenConectionDB()
        {
            Connect.Open();
        }
        public void CloseConnection()
        {
            Connect.Close();
        }
        // Grab User info from database
        public string[] PullUserInfo(string _username, string _key)
        {
            string[] UsrInfo = {"", "", "", "", "", ""};
            m_query = "Select * from user_tb where Username='" + _username + "' and PassKey=password('" + _key + "');";
            m_SQLReader = m_Commands.WriteSelectStatement(Connect, m_query);
            if (m_SQLReader.Read())
            {
                for(int loop= 0; loop < 6; loop++)
                UsrInfo[loop] = m_SQLReader.GetString(loop);
            }
            return UsrInfo;
        }
        private void SaveToLocalDB()
        {

        }
        private void CheckDBDiffrence()
        {

        }
        private void UpdateLocalDB()
        {

        }
        private void UpdateServerDB()
        {

        }
        // Grab Spefic note from DB
       private string[] GetNotes(User _user)
        {
            string[] NInfo = { "", "", "", "", "", "" };
            m_query = "Select * from user_tb where UserID='" + _user.m_UserId  +"' and PassKey=password('" + _user.m_Key + "');";
            m_SQLReader = m_Commands.WriteSelectStatement(Connect, m_query);
            if (m_SQLReader.Read())
            {
                for (int loop = 0; loop < 6; loop++)
                    NInfo[loop] = m_SQLReader.GetString(loop);
            }
            return NInfo;
        }
        // Save new Note to database 
        public bool AddNote(Note _Note, User _User)
        {
            try
            {
                m_query = "insert into CryptNote_DB.user_tb(CryptText,PassKey,UserId,FileName,IsOpen)values('" +
                _Note.m_NoteText + "',password('" + _Note.m_PassKey + "'),'" + _User.m_UserId + "'," + _Note.m_Name + "','" + _Note.m_IsOpen + "');";
                m_Commands.WrightInsert(Connect, m_query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Delete Note from Database 
        public bool DeleteNote(User _user, Note _note)
        {
            try
            {
                m_query = "delete  from notes_tb where FileName='" + _note.m_Name  + "' and PassKey=password('" + _user.m_Key +"');";
                m_Commands.WrightDelete(Connect, m_query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Edit Note on Database 
        private bool EditNote(User _user, Note _note)
        {
            try
            {
                m_query = "update notes_tb set CryptText='" + _note.m_NoteText + "', PassKey=password('" + _note.m_PassKey + "'), UserId='"+
                    _user.m_UserId +"', FileName='" + _note.m_Name + "', IsOpen='" + _note.m_IsOpen + "' where FileName='" + _note.m_Name + "';";
                m_Commands.WrightDelete(Connect, m_query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Check to see if user has rights
        public bool CheckRights(string _username, string _key)
        {
            m_query = "Select * from user_tb where Username='" + _username + "' and PassKey=password('" + _key + "');";
            m_SQLReader = m_Commands.WriteSelectStatement(Connect, m_query);
            if (m_SQLReader.HasRows)
            {
                return true;
            }
            else
                return false;
        }
        // Create new user to database 
        public bool CreateNewUser(string m_Username, string m_key, string m_Email, string m_SQ, string m_Answer)
        {
            try
            {
                m_query = "insert into CryptNote_DB.user_tb(Username,PassKey,Email,SQ,SQA)values('" +
                m_Username + "',password('" + m_key + "'),'" + m_SQ + "','" + m_Email + "','" + m_Answer + "');";
                m_Commands.WrightInsert(Connect, m_query);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        // Remove a user from database
        public bool RemoveUser(string m_Username)
        {
            try
            {
                m_query = "delete  from user_tb where Username='" + m_Username + "';";
                m_Commands.WrightDelete(Connect, m_query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
