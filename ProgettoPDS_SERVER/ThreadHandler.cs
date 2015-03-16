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
        private const int PREC = 10000; //fattore per rendere preciso il rapporto tra X e Y 
        private const int T = 1;//tempo di attesa per lo spostamento del mouse
        public const int MAXTHREAD = 5;
        private static int threadcounter = 0;
        private static Mutex mut = new Mutex();
        public static ManualResetEvent mrMaxThreads = new ManualResetEvent(false);
        public static ManualResetEvent mrMouseThreads = new ManualResetEvent(false);
        public static Queue<string[]> MouseQueue = new Queue<string[]>();
        public static Queue<string[]> KeyBoardQueue = new Queue<string[]>();

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

            //elimino il pacchetto dalla coda
            MouseQueue.Dequeue();

            //se ci sono altri pacchetti in coda lancio il primo della lista
            if(MouseQueue.Count>0)
            {
                string[] d = MouseQueue.Peek();

                Thread t = new Thread(MouseThreadProc);

                while (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                    ThreadHandler.mrMaxThreads.WaitOne();

                t.Start(d);
                ThreadHandler.ThreadCounter++;

                if (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                    ThreadHandler.mrMaxThreads.Reset();
            }

            //decremento il contatore dei thread attivi e segnalo di aver finito la gestione di un Thread
            ThreadCounter--;
            mrMaxThreads.Set();
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
                        if (i % 2 != 0)
                            Thread.Sleep(T);
                    }

                }
                else//spostamento in alto
                {
                    for (int i = 0; i < ScrollY; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, old.Y + i, 0, UIntPtr.Zero);
                        if (i % 2 != 0)
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
                        if (i % 2 != 0)
                            Thread.Sleep(T);
                    }
                }
                else//spostamento a dx
                {
                    for (int i = 0; i < ScrollX; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, old.X + i, 0, 0, UIntPtr.Zero);
                        if (i % 2 != 0)
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
                int signx = 1;
                int signy = 1;

                #region Controllo Spostamenti
                if (ScrollX < 0 && ScrollY > 0)//solo X negativo
                {
                    signx = -1;
                }
                else if (ScrollX > 0 && ScrollY < 0)//solo Y negativo
                {
                    signy = -1;
                }
                else if (ScrollX < 0 && ScrollY < 0)//entrambi negativi
                {
                    signx = -1;
                    signy = -1;
                }
                #endregion
                if (ScrollX == ScrollY)//spostamenti uguali asse X e Y (M = 1)
                {
                    for (int i = 0; i < ScrollY; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, signx, 0, 0, UIntPtr.Zero);
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, signy, 0, UIntPtr.Zero);
                        if (i % 2 != 0)
                            Thread.Sleep(T);
                    }
                }
                else
                {
                    //Moltiplicazione per il fattore di precisione
                    long LScrollX = signx * ScrollX * PREC;
                    long LScrollY = signy * ScrollY * PREC;

                    if (LScrollX > LScrollY)//spostamento maggiore sull'asse x
                    {
                        //Calcolo rapporto
                        M = (int)(LScrollX / LScrollY);
                        for (long i = 0; i < LScrollX; i += PREC)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, signx, 0, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, signy, 0, UIntPtr.Zero);
                                Thread.Sleep(T);
                            }
                            counter++;
                            //Thread.Sleep(T);
                        }
                    }
                    else//spostamento maggiore sull'asse y
                    {
                        //Calcolo rapporto
                        M = (int)(LScrollY / LScrollX);

                        for (long i = 0; i < LScrollY; i += PREC)
                        {
                            mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, signy, 0, UIntPtr.Zero);
                            if (counter >= M)
                            {
                                counter = 0;
                                mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, signx, 0, 0, UIntPtr.Zero);
                                Thread.Sleep(T);
                            }
                            counter++;
                            //Thread.Sleep(T);
                        }
                    }
                }
            }
            #endregion

        }
        #endregion
        #region KEYBOARD
        public static void KeyBoardThreadProc(object data)
        {
            String[] KBData = data as String[];

            //caso tasto singolo/semplice
            if(KBData[1]==ApplicationConstants.KEYBOARDEVENT_SINGLEKEY)
            {
                PressKey(Byte.Parse(KBData[2],System.Globalization.NumberStyles.HexNumber));
            }
            else
            {
                ApplicationConstants.Modificatore m = (ApplicationConstants.Modificatore)Enum.Parse(typeof(ApplicationConstants.Modificatore),KBData[1]);

                byte mod1,mod2,mod3;
                switch(m)
                {
                    case ApplicationConstants.Modificatore.A :
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_MENU);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                    case ApplicationConstants.Modificatore.C:
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_CONTROL);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                    case ApplicationConstants.Modificatore.S:
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_SHIFT);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                    case ApplicationConstants.Modificatore.CA:
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_MENU);
                        mod2 = Convert.ToByte(ApplicationConstants.VK_CONTROL);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                    case ApplicationConstants.Modificatore.CS:
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_SHIFT);
                        mod2 = Convert.ToByte(ApplicationConstants.VK_CONTROL);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                    case ApplicationConstants.Modificatore.CAS:
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_SHIFT);
                        mod2 = Convert.ToByte(ApplicationConstants.VK_CONTROL);
                        mod3 = Convert.ToByte(ApplicationConstants.VK_MENU);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        keybd_event(mod3, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        keybd_event(mod3, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                    case ApplicationConstants.Modificatore.AS:
                        //attivazione combinazione
                        mod1 = Convert.ToByte(ApplicationConstants.VK_SHIFT);
                        mod2 = Convert.ToByte(ApplicationConstants.VK_MENU);

                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                        PressKey(Byte.Parse(KBData[2], System.Globalization.NumberStyles.HexNumber));
                        keybd_event(mod1, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        keybd_event(mod2, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
                        break;
                }
 
            }

            //elimino il pacchetto dalla coda
            KeyBoardQueue.Dequeue();

            //se ci sono altri pacchetti in coda lancio il primo della lista
            if (KeyBoardQueue.Count > 0)
            {
                string[] d = KeyBoardQueue.Peek();

                Thread t = new Thread(KeyBoardThreadProc);

                while (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                    ThreadHandler.mrMaxThreads.WaitOne();

                t.Start(d);
                ThreadHandler.ThreadCounter++;

                if (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                    ThreadHandler.mrMaxThreads.Reset();
            }

            //decremento il contatore dei thread attivi e segnalo di aver finito la gestione di un Thread
            ThreadCounter--;
            mrMaxThreads.Set();
        }

        private static void PressKey(byte keyCode)
        {
            keybd_event(keyCode, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
            keybd_event(keyCode, 0, ApplicationConstants.KEYEVENTF_EXTENDEDKEY | ApplicationConstants.KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        #endregion
        #region CLIPBOARD
        public static void ClipBoardThreadProc(object data)
        {
            ThreadCounter--;
            mrMaxThreads.Set();
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
        //Virtual Key Scan  char to short
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern short VkKeyScan(char ch);
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
