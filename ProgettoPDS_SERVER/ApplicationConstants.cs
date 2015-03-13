using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.InteropServices;

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
        public enum Modificatore
        {
            C = 1,
            A = 0,
            S = 2,
            CA = 3,
            CS = 4,
            AS = 5,
            CAS = 6
        };
        //structs
        public struct Data
        {
            //socket
            public Socket sock;
            //data[]
            public byte[] data;
            //buffer size
            public int BufferSize;
        }

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

        //keyboard events
        const string keyboardeventsinglekey = "SNGKEY";
        const string keyboardeventsystemkey = "SYSKEY";
        //const string keyboardeventmodificator = "";

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

        public static string KEYBOARDEVENT_SINGLEKEY { get { return keyboardeventsinglekey; } }
        //public static string KEYBOARDEVENT_HOTKEY { get { return keyboardeventhotkey; } }
        public static string KEYBOARDEVENT_SYSTEMKEY { get { return keyboardeventsystemkey; } }

        //Virtual keys
        public const byte VK_LBUTTON = 0x1;//Left mouse button
        public const byte VK_RBUTTON = 0x2;//Right mouse button
        public const byte VK_CANCEL = 0x3;//Control-break processing
        public const byte VK_MBUTTON = 0x4;//Middle mouse button (three-button mouse)
        public const byte VK_XBUTTON1 = 0x5;//X1 mouse button
        public const byte VK_XBUTTON2 = 0x6;//X2 mouse button
        public const byte VK_BACK   = 0x8;//BACKSPACE key
        public const byte VK_TAB = 0x9;//TAB key
        public const byte VK_CLEAR  = 0xC;//CLEAR key
        public const byte VK_RETURN = 0xD;//ENTER key
        public const byte VK_SHIFT  = 0x10;//SHIFT key
        public const byte VK_CONTROL= 0x11;//CTRL key
        public const byte VK_MENU   = 0x12;//ALT key
        public const byte VK_PAUSE  = 0x13;//PAUSE key
        public const byte VK_CAPITAL= 0x14;//CAPS LOCK key
        public const byte VK_KANA   = 0x15;//IME Kana mode
        public const byte VK_JUNJA  = 0x17;//IME Junja mode
        public const byte VK_FINAL  = 0x18;//IME final mode
        public const byte VK_HANJA  = 0x19;//IME Hanja mode
        public const byte VK_ESCAPE = 0x1B;//ESC key
        public const byte VK_CONVERT= 0x1C;//IME convert
        public const byte VK_NONCONVERT  = 0x1D;//IME nonconvert
        public const byte VK_ACCEPT = 0x1E;//IME accept
        public const byte VK_MODECHANGE  = 0x1F;//IME mode change request
        public const byte VK_SPACE  = 0x20;//SPACEBAR
        public const byte VK_PRIOR  = 0x21;//PAGE UP key
        public const byte VK_NEXT   = 0x22;//PAGE DOWN key
        public const byte VK_END = 0x23;//END key
        public const byte VK_HOME   = 0x24;//HOME key
        public const byte VK_LEFT   = 0x25;//LEFT ARROW key
        public const byte VK_UP  = 0x26;//UP ARROW key
        public const byte VK_RIGHT  = 0x27;//RIGHT ARROW key
        public const byte VK_DOWN = 0x28;//DOWN ARROW key
        public const byte VK_SELECT = 0x29;//SELECT key
        public const byte VK_PRINT = 0x2A;//PRINT key
        public const byte VK_EXECUTE= 0x2B;//EXECUTE key
        public const byte VK_SNAPSHOT = 0x2C;//PRINT SCREEN key
        public const byte VK_INSERT = 0x2D;//INS key
        public const byte VK_DELETE = 0x2E;//DEL key
        public const byte VK_HELP   = 0x2F;//HELP key
        public const byte VK_LWIN   = 0x5B;//Left Windows key (Natural keyboard)
        public const byte VK_RWIN   = 0x5C;//Right Windows key (Natural keyboard)
        public const byte VK_APPS   = 0x5D;//Applications key (Natural keyboard)
        public const byte VK_SLEEP  = 0x5F;//Computer Sleep key
        public const byte VK_NUMPAD0 = 0x60;//Numeric keypad 0 key
        public const byte VK_NUMPAD1 = 0x61;//Numeric keypad 1 key
        public const byte VK_NUMPAD2 = 0x62;//Numeric keypad 2 key
        public const byte VK_NUMPAD3 = 0x63;//Numeric keypad 3 key
        public const byte VK_NUMPAD4 = 0x64;//Numeric keypad 4 key
        public const byte VK_NUMPAD5 = 0x65;//Numeric keypad 5 key
        public const byte VK_NUMPAD6 = 0x66;//Numeric keypad 6 key
        public const byte VK_NUMPAD7 = 0x67;//Numeric keypad 7 key
        public const byte VK_NUMPAD8= 0x68;//Numeric keypad 8 key
        public const byte VK_NUMPAD9= 0x69;//Numeric keypad 9 key
        public const byte VK_MULTIPLY = 0x6A;//Multiply key
        public const byte VK_ADD = 0x6B;//Add key
        public const byte VK_SEPARATOR   = 0x6C;//Separator key
        public const byte VK_SUBTRACT = 0x6D;//Subtract key
        public const byte VK_DECIMAL= 0x6E;//Decimal key
        public const byte VK_DIVIDE = 0x6F;//Divide key
        public const byte VK_F1  = 0x70;//F1 key
        public const byte VK_F2  = 0x71;//F2 key
        public const byte VK_F3  = 0x72;//F3 key
        public const byte VK_F4  = 0x73;//F4 key
        public const byte VK_F5  = 0x74;//F5 key
        public const byte VK_F6  = 0x75;//F6 key
        public const byte VK_F7  = 0x76;//F7 key
        public const byte VK_F8  = 0x77;//F8 key
        public const byte VK_F9  = 0x78;//F9 key
        public const byte VK_F10 = 0x79;//F10 key
        public const byte VK_F11 = 0x7A;//F11 key
        public const byte VK_F12 = 0x7B;//F12 key
        public const byte VK_F13 = 0x7C;//F13 key
        public const byte VK_F14 = 0x7D;//F14 key
        public const byte VK_F15 = 0x7E;//F15 key
        public const byte VK_F16 = 0x7F;//F16 key
        public const byte VK_F17 = 0x80;//F17 key
        public const byte VK_F18 = 0x81;//F18 key
        public const byte VK_F19 = 0x82;//F19 key
        public const byte VK_F20 = 0x83;//F20 key
        public const byte VK_F21 = 0x84;//F21 key
        public const byte VK_F22 = 0x85;//F22 key
        public const byte VK_F23 = 0x86;//F23 key
        public const byte VK_F24 = 0x87;//F24 key
        public const byte VK_NUMLOCK= 0x90;//NUM LOCK key
        public const byte VK_SCROLL = 0x91;//SCROLL LOCK key
        public const byte VK_LSHIFT = 0xA0;//Left SHIFT key
        public const byte VK_RSHIFT = 0xA1;//Right SHIFT key
        public const byte VK_LCONTROL = 0xA2;//Left CONTROL key
        public const byte VK_RCONTROL = 0xA3;//Right CONTROL key
        public const byte VK_LMENU  = 0xA4;//Left MENU key
        public const byte VK_RMENU  = 0xA5;//Right MENU key
        public const byte VK_BROWSER_BACK= 0xA6;//Browser Back key
        public const byte VK_BROWSER_FORWARD = 0xA7;//Browser Forward key
        public const byte VK_BROWSER_REFRESH = 0xA8;//Browser Refresh key
        public const byte VK_BROWSER_STOP= 0xA9;//Browser Stop key
        public const byte VK_BROWSER_SEARCH = 0xAA;//Browser Search key
        public const byte VK_BROWSER_FAVORITES = 0xAB;//Browser Favorites key
        public const byte VK_BROWSER_HOME= 0xAC;//Browser Start and Home key
        public const byte VK_VOLUME_MUTE = 0xAD;//Volume Mute key
        public const byte VK_VOLUME_DOWN = 0xAE;//Volume Down key
        public const byte VK_VOLUME_UP   = 0xAF;//Volume Up key
        public const byte VK_MEDIA_NEXT_TRACK = 0xB0;//Next Track key
        public const byte VK_MEDIA_PREV_TRACK = 0xB1;//Previous Track key
        public const byte VK_MEDIA_STOP  = 0xB2;//Stop Media key
        public const byte VK_MEDIA_PLAY_PAUSE = 0xB3;//Play/Pause Media key
        public const byte VK_LAUNCH_MAIL = 0xB4;//Start Mail key
        public const byte VK_LAUNCH_MEDIA_SELECT = 0xB5;//Select Media key
        public const byte VK_LAUNCH_APP1 = 0xB6;//Start Application 1 key
        public const byte VK_LAUNCH_APP2 = 0xB7;//Start Application 2 key
        public const byte VK_OEM_1  = 0xBA;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:;//key
        public const byte VK_OEM_PLUS = 0xBB;//For any country/region, the '+;//key
        public const byte VK_OEM_COMMA   = 0xBC;//For any country/region, the ',;//key
        public const byte VK_OEM_MINUS   = 0xBD;//For any country/region, the '-;//key
        public const byte VK_OEM_PERIOD  = 0xBE;//For any country/region, the '.;//key
        public const byte VK_OEM_2  = 0xBF;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?;//key
        public const byte VK_OEM_3  = 0xC0;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '~;//key
        public const byte VK_OEM_4  = 0xDB;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '[{;//key
        public const byte VK_OEM_5  = 0xDC;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\|;//key
        public const byte VK_OEM_6  = 0xDD;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ']};//key
        public const byte VK_OEM_7  = 0xDE;//Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the 'single-quote/double-quote;//key
        public const byte VK_OEM_8  = 0xDF;//Used for miscellaneous characters; it can vary by keyboard.
        public const byte VK_OEM_102= 0xE2;//Either the angle bracket key or the backslash key on the RT 102-key keyboard
        public const byte VK_PROCESSKEY  = 0xE5;//IME PROCESS key
        public const byte VK_PACKET = 0xE7;//Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
        public const byte VK_ATTN   = 0xF6;//Attn key
        public const byte VK_CRSEL  = 0xF7;//CrSel key
        public const byte VK_EXSEL  = 0xF8;//ExSel key
        public const byte VK_EREOF  = 0xF9;//Erase EOF key
        public const byte VK_PLAY   = 0xFA;//Play key
        public const byte VK_ZOOM   = 0xFB;//Zoom key
        public const byte VK_NONAME = 0xFC;//Reserved
        public const byte VK_PA1 = 0xFD;//PA1 key
        public const byte VK_OEM_CLEAR   = 0xFE;//Clear key
    }
}
