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
            int X = 0, Y = 0;
            String[] MouseData = data as String[];

            ApplicationConstants.POINT p;
            GetCursorPos(out p);

            if (MouseData[2] != null)
                X = Convert.ToInt32(MouseData[2])*Screen.PrimaryScreen.WorkingArea.Width / 1000;
            if (MouseData[3] != null)
                Y = Convert.ToInt32(MouseData[3]) * Screen.PrimaryScreen.WorkingArea.Width / 1000;

            //controllo di non uscire dallo schermo
            if (X < 0)
                X = 0;
            else if (X > Screen.PrimaryScreen.WorkingArea.Width)
                X = Screen.PrimaryScreen.WorkingArea.Width;
            if (Y < 0)
                Y = 0;
            else if (Y > Screen.PrimaryScreen.WorkingArea.Height)
                Y = Screen.PrimaryScreen.WorkingArea.Height;

            int ScrollX = X- p.X;

            int ScrollY = Y - p.Y ;


            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_MOVE)
            {
                int M = 0;
                if(ScrollX == ScrollY)
                {
                    for(int i = 0; i<ScrollY;i++)
                    {
                        MouseMove(1, 0, p);
                        MouseMove(0, 1, p);
                    }
                }
                if (ScrollX > ScrollY)
                {
                    M = ScrollX / ScrollY;
                    //da finire
                }
                else
                {
                    M = ScrollY / ScrollX;
                }

                MouseMove(ScrollX,ScrollY, p);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }

            ThreadCounter--;
            mr.Set();
        }

        private static void MouseMove(int ScrollX, int ScrollY, ApplicationConstants.POINT old)
        {
            if(ScrollX == 0)//solo asse Y
            {
                if (ScrollX < 0)//spostamento in basso
                {
                    for (int i = ScrollY; i > 0; i--)
                    { 
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, old.Y + i, 0, UIntPtr.Zero);
                        Thread.Sleep(10);
                    } 

                }
                else//spostamento in alto
                {
                    for (int i = 1; i <= ScrollY; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, old.Y + i, 0, UIntPtr.Zero);
                        Thread.Sleep(10);
                    } 
                }
            }
            else if (ScrollY == 0)//solo asse X
            {
                if (ScrollY < 0)//spostamento a sx
                {
                    for (int i = ScrollX; i > 0; i--)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, old.X + i, 0, 0, UIntPtr.Zero);
                        Thread.Sleep(10);
                    } 
                }
                else//spostamento a dx
                {
                    for (int i = 1; i <= ScrollX; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, old.X + i, 0, 0, UIntPtr.Zero);
                        Thread.Sleep(10);
                    } 
                }
            }
            else
            {

            }
            
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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out ApplicationConstants.POINT lpPoint);
    }
}
