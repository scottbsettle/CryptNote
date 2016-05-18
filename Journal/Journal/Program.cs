using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.Password == "Password")
            {
                if (Properties.Settings.Default.Username == "Admin")
                {
                    UsernamePassword Editme = new UsernamePassword();
                    Editme.SetNewUser(true);
                    Application.Run(Editme);
                }
            }
            else
            Application.Run(new Login());
        }
    }
}
