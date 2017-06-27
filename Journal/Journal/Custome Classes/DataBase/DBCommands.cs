using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Journal
{


    class DBCommands
    {
        private MySqlCommand m_cmd;
        private MySqlDataReader m_reader;
       public  DBCommands()
        {
            
        }
       public void WrightQuery(MySqlConnection _connect, string m_query)
        {
            m_cmd = new MySqlCommand(m_query, _connect);
            m_reader = m_cmd.ExecuteReader();
        }
        //string Query = "insert into student.studentinfo(idStudentInfo,Name,Father_Name,Age,Semester)
        //values('" + this.IdTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FnameTextBox.Text +
        //"','" + this.AgeTextBox.Text + "','" + this.SemesterTextBox.Text + "');";
        public void WrightInsert(MySqlConnection _connect, string m_query)
        {
            m_cmd = new MySqlCommand(m_query, _connect);
            m_reader = m_cmd.ExecuteReader();
        }
        // string Query = "delete from student.studentinfo where idStudentInfo='" + this.IdTextBox.Text + "';";
        public void WrightDelete(MySqlConnection _connect, string m_query)
        {
            m_cmd = new MySqlCommand(m_query, _connect);
            m_reader = m_cmd.ExecuteReader();
        }
        //string Query = "update student.studentinfo set idStudentInfo='" + this.IdTextBox.Text +
        //    "',Name='" + this.NameTextBox.Text + "',Father_Name='" + this.FnameTextBox.Text + 
        //    "',Age='" + this.AgeTextBox.Text + "',Semester='" + this.SemesterTextBox.Text + "' where idStudentInfo='" + this.IdTextBox.Text + "';";
        public void WrightUpdate(MySqlConnection _connect, string m_query)
        {
            m_cmd = new MySqlCommand(m_query, _connect);
            m_reader = m_cmd.ExecuteReader();
        }
        // string Query = "select * from student.studentinfo;";
        public MySqlDataReader WriteSelectStatement(MySqlConnection _connect, string m_query)
        {
            m_cmd = new MySqlCommand(m_query, _connect);
            m_reader = m_cmd.ExecuteReader();
            return m_reader;
        }
    }
}
