using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restarunttest2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Loginform loginform = new Loginform();
            Application.Run(loginform);
            auth auth = new auth();
            //if login is accepted ie login application.Run(auth.cs program){}
            //auth class classnameauth = new class auth()
           

            ;
        }
    }
}
