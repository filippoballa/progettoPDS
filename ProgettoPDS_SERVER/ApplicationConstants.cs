using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProgettoPDS_SERVER
{
    public static class ApplicationConstants
    {
        //enums
        public enum Stato
        {
            [Description("Connesso")]
            CONNESSO = 1,
            [Description("Disconnesso")]
            DISCONNESSO = 0,
            [Description("In Attesa ...")]
            IN_ATTESA = 2
        };
        public enum StatoClipBoard
        {
            [Description("Piena")]
            PIENA = 1,
            [Description("Vuota")]
            VUOTA = 0,
            [Description("File Audio")]
            AUDIO = 2,
            [Description("File Immagine")]
            IMMAGINE = 3,
            [Description("File Testo")]
            TEXT = 4,
            [Description("Link")]
            FILE_DROP = 5
        };
        public enum Modificatore
        {
            A = 0,//Alt
            C = 1,//Ctrl
            S = 2,//Shift
            CA = 3,//Ctrl+Alt
            CS = 4,//Ctrl+Shift
            AS = 5,//Alt+Shift
            CAS = 6//Ctrl+Alt+Shift
        };
        //paths
        private const string tempPath = @"..\Temp\";

        //structs
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }
        //protocol messages
        const string auth_user = "+AUTH_USER";
        const string auth_pwd = "+AUTH_PWD";
        const string reg_user = "+REG_USER";
        const string yes = "+YES";
        const string reg_pwd = "+REG_PWD";
        const string ok = "+OK";
        const string err = "-ERR";

        //protocol fields separator
        static char[] separator = { '-' };

        //type of packets
        const string mousecode = "M";
        const string keyboardcode = "K";
        const string clipboardcode = "C";
        const string quitcode = "QUIT";
        const string clipboardquitcode = "CLOSE";

        //mouse flags
        const uint mouseeventf_absolute = 0x8000;
        const uint mouseeventf_leftdown = 0x0002;
        const uint mouseeventf_leftup = 0x0004;
        const uint mouseeventf_middledown = 0x0020;
        const uint mouseeventf_middleup = 0x0040;
        const uint mouseeventf_move = 0x0001;
        const uint mouseeventf_rightdown = 0x0008;
        const uint mouseeventf_rightup = 0x0010;
        const uint mouseeventf_xdown = 0x0080;
        const uint mouseeventf_xup = 0x0100;
        const uint mouseeventf_wheel = 0x0800;
        const uint mouseeventf_hwheel = 0x01000;

        //mouse events
        const string mouseeventAbsolute = "ABSOLUTE";
        const string mouseeventLeftclick = "LCLK";
        const string mouseeventLeftdbclick = "LDBCLCK";
        const string mouseeventMiddleclick = "MCLK";
        const string mouseeventMiddledbclick = "MDBCLK";
        const string mouseeventMove = "MOVE";
        const string mouseeventRightclick = "RCLK";
        const string mouseeventRightdbclick = "RDBCLK";
        const string mouseeventXclick = "XCLK";
        const string mouseeventXdbclick = "XDBCLK";
        const string mouseeventWheel = "WHEEL";
        const string mouseeventHwheel = "HWHEEL";

        //keyboard flags
        const uint keyeventf_extendedkey = 0x0001;
        const uint keyeventf_keyup = 0x0002;

        //keyboard virtual keys
        const byte vk_menu = 0x12;
        const byte vk_control = 0x11;
        const byte vk_shift = 0x10;

        //keyboard events
        const string keyboardeventsinglekey = "SNGKEY";

        //clipboard events
        const string clipboardeventGet = "GET_CLIP";
        const string clipboardeventSet = "SET_CLIP";

        //others attributes 
        private static DialogResult res = DialogResult.No;

        //readonly methods
        public static string TempPath { get { return tempPath; } }

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
        public static string QUITCODE { get { return quitcode; } }
        public static string CLIPQUITCODE { get { return clipboardquitcode; } }

        public static uint MOUSEEVENTF_ABSOLUTE { get { return mouseeventf_absolute; } }
        public static uint MOUSEEVENTF_LEFTDOWN { get { return mouseeventf_leftdown; } }
        public static uint MOUSEEVENTF_LEFTUP { get { return mouseeventf_leftup; } }
        public static uint MOUSEEVENTF_MIDDLEDOWN { get { return mouseeventf_middledown; } }
        public static uint MOUSEEVENTF_MIDDLEUP { get { return mouseeventf_middleup; } }
        public static uint MOUSEEVENTF_MOVE { get { return mouseeventf_move; } }
        public static uint MOUSEEVENTF_RIGHTDOWN { get { return mouseeventf_rightdown; } }
        public static uint MOUSEEVENTF_RIGHTUP { get { return mouseeventf_rightup; } }
        public static uint MOUSEEVENTF_XDOWN { get { return mouseeventf_xdown; } }
        public static uint MOUSEEVENTF_XUP { get { return mouseeventf_xup; } }
        public static uint MOUSEEVENTF_WHEEL { get { return mouseeventf_wheel; } }
        public static uint MOUSEEVENTF_HWHEEL { get { return mouseeventf_hwheel; } }

        public static string MOUSEEVENT_ABSOLUTE { get { return mouseeventAbsolute; } }
        public static string MOUSEEVENT_LEFTCLICK { get { return mouseeventLeftclick; } }
        public static string MOUSEEVENT_LEFTDBCLICK { get { return mouseeventLeftdbclick; } }
        public static string MOUSEEVENT_MIDDLECLICK { get { return mouseeventMiddleclick; } }
        public static string MOUSEEVENT_MIDDLEDBCLICK { get { return mouseeventMiddledbclick; } }
        public static string MOUSEEVENT_MOVE { get { return mouseeventMove; } }
        public static string MOUSEEVENT_RIGHTCLICK { get { return mouseeventRightclick; } }
        public static string MOUSEEVENT_RIGHTDBCLICK { get { return mouseeventRightdbclick; } }
        public static string MOUSEEVENT_XCLICK { get { return mouseeventXclick; } }
        public static string MOUSEEVENT_XDBCLICK { get { return mouseeventXdbclick; } }
        public static string MOUSEEVENT_WHEEL { get { return mouseeventWheel; } }
        public static string MOUSEEVENT_HWHEEL { get { return mouseeventHwheel; } }

        public static uint KEYEVENTF_EXTENDEDKEY { get { return keyeventf_extendedkey; } }
        public static uint KEYEVENTF_KEYUP { get { return keyeventf_keyup; } }

        public static byte VK_MENU { get { return vk_menu; } }
        public static byte VK_CONTROL { get { return vk_control; } }
        public static byte VK_SHIFT { get { return vk_shift; } }

        public static string KEYBOARDEVENT_SINGLEKEY { get { return keyboardeventsinglekey; } }

        public static string CLIPBOARDEVENTGET { get { return clipboardeventGet; } }
        public static string CLIPBOARDEVENTSET { get { return clipboardeventSet; } }

        public static DialogResult RES { get { return res; } set { res = value; } }
    }
}
