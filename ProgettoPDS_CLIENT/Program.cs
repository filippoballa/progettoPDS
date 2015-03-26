using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProgettoPDS_CLIENT
{
    static class Program
    {
        internal static DialogResult res = DialogResult.No;
        internal const string tempPath = "..\\Temp\\";

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            User user = new User();

            if (!Directory.Exists(Program.tempPath))
                Directory.CreateDirectory(Program.tempPath);

            do {

                res = DialogResult.No;

                Application.Run(new LoginForm(user));

                if (user.Login)
                    Application.Run(new MainForm(user));

                user.Login = false;

            } while (res != DialogResult.No );

            Directory.Delete(Program.tempPath, true);

        }
    }
}
