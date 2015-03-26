using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace ProgettoPDS_SERVER
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            User user = new User();

            if (!Directory.Exists(ApplicationConstants.TempPath))
                Directory.CreateDirectory(ApplicationConstants.TempPath);

            Application.Run(new LoginForm(user));

            if (user.Login)
                Application.Run(new MainForm(user));

            Directory.Delete(ApplicationConstants.TempPath, true);
        }
    }
}
