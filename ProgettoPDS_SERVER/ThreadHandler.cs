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
        public static void MouseThreadProc(object data)
        {
            double X = 0.0, Y = 0.0;
            String[] MouseData = data as String[];

            if (MouseData[2] != null)
                X = Convert.ToDouble(MouseData[2], NumberFormatInfo.CurrentInfo);
            if (MouseData[3] != null)
                Y = Convert.ToDouble(MouseData[3], NumberFormatInfo.CurrentInfo);

            int PosX = (int)X * Screen.PrimaryScreen.WorkingArea.Width;
            int PosY = (int)Y * Screen.PrimaryScreen.WorkingArea.Height;

            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_MOVE)
            {
                SetCursorPos(PosX, PosY);
            }
            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, PosX, PosY, 0, UIntPtr.Zero);
            }
            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, PosX, PosY, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, PosX, PosY, 0, UIntPtr.Zero);
            }
            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, PosX, PosY, 0, UIntPtr.Zero);
            }
            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, PosX, PosY, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, PosX, PosY, 0, UIntPtr.Zero);
            }

        }
        public static void KeyBoardThreadProc(object data)
        { }
        public static void ClipBoardThreadProc(object data)
        { }

        [DllImport("user32.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);
    }
}
