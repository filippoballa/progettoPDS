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

            do
            {

                ApplicationConstants.RES = DialogResult.No;

                Application.Run(new LoginForm(user));

                if (user.Login)
                    Application.Run(new MainForm(user));

                user.Login = false;

            } while (ApplicationConstants.RES != DialogResult.No);

            foreach( string filename in Directory.EnumerateFiles(ApplicationConstants.TempPath))
            {
                File.Delete(filename);
            }
            foreach (string dir in Directory.EnumerateDirectories(ApplicationConstants.TempPath))
            {
                Directory.Delete(dir);
            }
        }
    }
}
