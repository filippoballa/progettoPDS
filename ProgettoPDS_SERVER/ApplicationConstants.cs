using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_SERVER
{
    public static class ApplicationConstants
    {
        //enums
        public enum Stato
        {
            CONNESSO = 1,
            DISCONNESSO = 0
        };
        //protocol messages
        private static string auth_user = "+AUTH_USER";
        private static string auth_pwd = "+AUTH_PWD";
        private static string reg_user = "+REG_USER";
        private static string yes = "+YES";
        private static string reg_pwd = "+REG_PWD";
        private static string ok = "+OK";
        private static string err = "-ERR";

        //protocol fields separator
        private static char[] separator= { '-' };

        //type of packets
        private static string mousecode = "M";
        private static string keyboardcode = "K";
        private static string clipboardcode = "C";

        //events

        //readonly methods
        public static string AUTH_USER { get { return auth_user; } }
        public static string AUTH_PWD { get { return auth_pwd; } }
        public static string REG_USER { get { return reg_user; } }
        public static string REG_PWD { get { return reg_pwd; } }
        public static string YES { get { return yes; } }
        public static string OK { get { return ok; } }
        public static string ERR { get { return err; } }

        public static char[] SEPARATOR { get { return separator; } }

        public static string MOUSECODE { get { return mousecode; } }
        public static string KEYBOARDCODE { get { return keyboardcode; } }
        public static string CLIPBOARDCODE { get { return clipboardcode; } }
    }
}
