using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    public struct Note
    {
        public string m_Name;
        public string m_NoteText;
        public string m_LastUpdated;
        public string m_NoteId;
        public string m_UserId;
        public string m_PassKey;
        public bool m_IsOpen;
    }
    public struct User
    {
        public string m_Username;
        public string m_Key;
        public string m_Email;
        public string m_SQ;
        public string m_SQA;
        public string m_UserId;
    }
    class UserInfo
    {
        User CurUser;
        private string m_key;
        private bool m_Online;
        private DBFunctions m_DBFunctions;
        private List<Note> m_notes;

        public UserInfo()
        {
            m_DBFunctions = new DBFunctions();
        }
        public UserInfo(string _key, bool _Online, User _Info)
        {
            m_key = _key;
            m_Online = _Online;
            CurUser = _Info;
        }

        public void connectOffline()
        {
           
        }
        public void ConnectOnline()
        {
            m_DBFunctions.ConnectDB();
            m_DBFunctions.OpenConectionDB();
        }
        public void CloseConnection()
        {
            m_DBFunctions.CloseConnection();
        }
        //public string GetUsername()
        //{
        //    return m_UserInfo[(int)Info.USERNAME];
        //}
        //private string GetKey()
        //{
        //    return m_UserInfo[(int)Info.KEY];
        //}
        //public void SetKey(string _key)
        //{
        //    m_UserInfo[(int)Info.KEY] = _key;
        //}
        private bool CheckAccount()
        {
            return false;
        }
        private void GetUserInfo()
        {

        }
    }
}
