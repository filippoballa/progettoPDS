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
        internal static string tempPath = Path.GetFullPath("..\\Temp\\");

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


            foreach (string data in Directory.EnumerateFiles(Program.tempPath))
                File.Delete(data);

            foreach( string data in Directory.EnumerateDirectories(Program.tempPath) )
                Directory.Delete(Program.tempPath, true);

            Clipboard.Clear();
        }
    }
}
