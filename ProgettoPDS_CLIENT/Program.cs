using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgettoPDS_CLIENT
{
    static class Program
    {
        public static DialogResult res = DialogResult.No;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            User user = new User();

            do {

                res = DialogResult.No;

                Application.Run(new LoginForm(user));

                if (user.Login)
                    Application.Run(new MainForm(user));

                user.Login = false;

            } while (res != DialogResult.No );

        }
    }
}
