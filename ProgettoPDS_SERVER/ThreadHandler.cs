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
        private const int T = 3;//tempo di attesa per lo spostamento del mouse
        public const int MAXTHREAD = 5;
        private static int threadcounter = 0;
        private static Mutex mut = new Mutex();
        public static ManualResetEvent mr = new ManualResetEvent(false);

        #region MOUSE
        public static void MouseThreadProc(object data)
        {
            int X = 0, Y = 0;
            String[] MouseData = data as String[];

            //get actual cursor position
            ApplicationConstants.POINT p;
            GetCursorPos(out p);

            //get proportion from data
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

            //calculate Scroll size
            int ScrollX = X - p.X;
            int ScrollY = Y - p.Y ;

            //MOUSE MOVE
            if (MouseData[1] == ApplicationConstants.MOUSEEVENT_MOVE)
            {
                MouseMove(ScrollX,ScrollY, p);
            }
            //MOUSE LEFT CLICK
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }
            //MOUSE LEFT DOUBLE CLICK
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_LEFTDBCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
                Thread.Sleep(150);
                mouse_event(ApplicationConstants.MOUSEEVENTF_LEFTDOWN | ApplicationConstants.MOUSEEVENTF_LEFTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }
            //MOUSE RIGHT CLICK
            else if (MouseData[1] == ApplicationConstants.MOUSEEVENT_RIGHTCLICK)
            {
                mouse_event(ApplicationConstants.MOUSEEVENTF_RIGHTDOWN | ApplicationConstants.MOUSEEVENTF_RIGHTUP, ScrollX, ScrollY, 0, UIntPtr.Zero);
            }
            //MOUSE RIGHT DOUBLE CLICK
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
            #region spostamento solo asse Y
            if (ScrollX == 0)//spostamento solo asse Y
            {
                if (ScrollX < 0)//spostamento in basso
                {
                    for (int i = ScrollY; i > 0; i--)
                    { 
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, old.Y + i, 0, UIntPtr.Zero);
                        Thread.Sleep(T);
                    } 

                }
                else//spostamento in alto
                {
                    for (int i = 1; i <= ScrollY; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, old.Y + i, 0, UIntPtr.Zero);
                        Thread.Sleep(T);
                    } 
                }
            }
            #endregion
            #region spostamento solo asse X
            else if (ScrollY == 0)//spostamento solo asse X
            {
                if (ScrollY < 0)//spostamento a sx
                {
                    for (int i = ScrollX; i > 0; i--)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, old.X + i, 0, 0, UIntPtr.Zero);
                        Thread.Sleep(T);
                    } 
                }
                else//spostamento a dx
                {
                    for (int i = 1; i <= ScrollX; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, old.X + i, 0, 0, UIntPtr.Zero);
                        Thread.Sleep(T);
                    } 
                }
            }
            #endregion
            #region spostamento entrambi gli assi
            else
            {
                int M = 0;
                int counter = 0;
                #region solo X negativo
                if (ScrollX < 0 && ScrollY > 0)//solo X negativo
                {
                    ScrollX = -ScrollX;
                    M = ScrollY / ScrollX;

                    if (ScrollX == ScrollY)//spostamenti uguali asse x e Y
                    {
                        for (int i = 0; i < ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, -1, 0, 0, UIntPtr.Zero);
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                            Thread.Sleep(T);
                        }
                    }
                    if (ScrollX > ScrollY)//spostamento maggiore sull'asse x
                    {
                        M = ScrollX / ScrollY;

                        for (int i = 1; i <= ScrollX; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, -1, 0, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                    else//spostamento maggiore sull'asse y
                    {
                        M = ScrollY / ScrollX;

                        for (int i = 1; i <= ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, -1, 0, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                }
                #endregion
                #region solo Y negativo
                else if (ScrollX > 0 && ScrollY < 0)//solo Y negativo
                {
                    ScrollY = -ScrollY;
                    M = ScrollY / ScrollX;

                    if (ScrollX == ScrollY)//spostamenti uguali asse x e Y
                    {
                        for (int i = 0; i < ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 1, 0, 0, UIntPtr.Zero);
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                            Thread.Sleep(T);
                        }
                    }
                    if (ScrollX > ScrollY)//spostamento maggiore sull'asse x
                    {
                        M = ScrollX / ScrollY;

                        for (int i = 1; i <= ScrollX; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 1, 0, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                    else//spostamento maggiore sull'asse y
                    {
                        M = ScrollY / ScrollX;

                        for (int i = 1; i <= ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 1, 0, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                }
                #endregion
                #region X&Y entrambi negativi
                else if (ScrollX < 0 && ScrollY < 0)//entrambi negativi
                {
                    ScrollY = -ScrollY;
                    ScrollX = -ScrollX;
                    M = ScrollY / ScrollX;

                    if (ScrollX == ScrollY)//spostamenti uguali asse x e Y
                    {
                        for (int i = 0; i < ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, -1, 0, 0, UIntPtr.Zero);
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                            Thread.Sleep(T);
                        }
                    }
                    if (ScrollX > ScrollY)//spostamento maggiore sull'asse x
                    {
                        M = ScrollX / ScrollY;

                        for (int i = 1; i <= ScrollX; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, -1, 0, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                    else//spostamento maggiore sull'asse y
                    {
                        M = ScrollY / ScrollX;

                        for (int i = 1; i <= ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, -1, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, -1, 0, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                }
                #endregion
                #region X&Y entrambi positivi
                else//entrambi positivi
                {

                    if (ScrollX == ScrollY)//spostamenti uguali asse x e Y
                    {
                        for (int i = 0; i < ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 1, 0, 0, UIntPtr.Zero);
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                            Thread.Sleep(T);
                        }
                    }
                    if (ScrollX > ScrollY)//spostamento maggiore sull'asse x
                    {
                        M = ScrollX / ScrollY;

                        for (int i = 1; i <= ScrollX; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 1, 0, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                    else//spostamento maggiore sull'asse y
                    {
                        M = ScrollY / ScrollX;

                        for (int i = 1; i <= ScrollY; i++)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, 1, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 1, 0, 0, UIntPtr.Zero);
                            }
                            Thread.Sleep(T);
                        }
                    }
                }
                #endregion
            }
            #endregion
        }
        #endregion
        #region KEYBOARD
        public static void KeyBoardThreadProc(object data)
        {
            String[] KBData = data as String[];

            //case KBData[1]

            ThreadCounter--;
            mr.Set();
        }

        private void PressKey(byte keyCode)
        {

            keybd_event(keyCode, 0x45, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
            keybd_event(keyCode, 0x45, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
        #endregion
        #region CLIPBOARD
        public static void ClipBoardThreadProc(object data)
        {
            ThreadCounter--;
            mr.Set();
        }
        #endregion
        #region Dll imports
        //set cursor method
        [DllImport("user32.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern bool SetCursorPos(int X, int Y);

        //mouse event method
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);

        //get cursor method
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out ApplicationConstants.POINT lpPoint);

        //keyboard event method
        [DllImport("user32.dll")]
        static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags,
           UIntPtr dwExtraInfo);
        #endregion
        public static int ThreadCounter
        {
            get
            {
                mut.WaitOne();
                int td = threadcounter;
                mut.ReleaseMutex();
                return td;
            }
            set
            {
                mut.WaitOne();
                threadcounter = value;
                mut.ReleaseMutex();
            }
        }
    }
}
