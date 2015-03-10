using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProgettoPDS_SERVER
{
    class ThreadHandler
    {
        public const int MAXTHREAD = 5;
        private static int threadcounter = 0;
        private static Mutex mut = new Mutex();
        public static ManualResetEvent mr = new ManualResetEvent(false);
        
        public static void MouseThreadProc(object data)
        {
            int X = 0, Y = 0, Xold = Cursor.Position.X, Yold = Cursor.Position.Y;
            String[] MouseData = data as String[];

            if (MouseData[2] != null)
                X = Convert.ToInt32(MouseData[2]);
            if (MouseData[3] != null)
                Y = Convert.ToInt32(MouseData[3]);

            int PosX = ((X * Screen.PrimaryScreen.WorkingArea.Width) / 1000) - Xold;
            int PosY = ((Y * Screen.PrimaryScreen.WorkingArea.Height) / 1000) - Yold;

            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_MOVE)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_ABSOLUTE | ApplicationConstants.MOUSEEVENTF_MOVE, PosX, PosY, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
            }

            ThreadCounter--;
            mr.Set();
        }
        public static void KeyBoardThreadProc(object data)
        {
            ThreadCounter--;
            mr.Set();
        }
        public static void ClipBoardThreadProc(object data)
        {
            ThreadCounter--;
            mr.Set();
        }

        public static int ThreadCounter 
        {
            get 
            {
                mut.WaitOne(); 
                int td =threadcounter;
                mut.ReleaseMutex();
                return td;
            } 
            set 
            { 
                mut.WaitOne();
                threadcounter=value;
                mut.ReleaseMutex();
            } 
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);
    }
}
