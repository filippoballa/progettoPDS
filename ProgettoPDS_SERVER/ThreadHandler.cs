using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProgettoPDS_SERVER
{
    class ThreadHandler
    {
        #region Attributes
        private const int PREC = 10000; //fattore per rendere preciso il rapporto tra X e Y 
        private const int T = 1;//tempo di attesa per lo spostamento del mouse
        private const int t = 5;
        public const int MAXTHREAD = 5;
        private const int SBuffSize = 2*Int16.MaxValue;
        private const int RBuffSize = 2*Int16.MaxValue;
        private static int threadcounter = 0;
        private static Mutex mut = new Mutex();
        public static ManualResetEvent mrMaxThreads = new ManualResetEvent(false);
        public static ManualResetEvent mrMouseThreads = new ManualResetEvent(false);
        public static Queue<string[]> MouseQueue = new Queue<string[]>();
        public static Queue<string[]> KeyBoardQueue = new Queue<string[]>();
        private static bool clipBoardWork = false;
        #endregion

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
                        if (i % t != 0)
                            Thread.Sleep(T);
                    }

                }
                else//spostamento in alto
                {
                    for (int i = 0; i < ScrollY; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, 0, old.Y + i, 0, UIntPtr.Zero);
                        if (i % t != 0)
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
                        if (i % t != 0)
                            Thread.Sleep(T);
                    }
                }
                else//spostamento a dx
                {
                    for (int i = 0; i < ScrollX; i++)
                    {
                        mouse_event(ApplicationConstants.MOUSEEVENTF_MOVE, old.X + i, 0, 0, UIntPtr.Zero);
                        if (i % t != 0)
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
                        if (i % t != 0)
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
                                if (i % t != 0) 
                                    Thread.Sleep(T);
                            }
                            counter++;
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
                                if (i % t != 0) 
                                    Thread.Sleep(T);
                            }
                            counter++;
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
        public static void ClipBoardThreadProc(object threaddata)
        {
            //getting information arguments
            String CBDataInfo = null;
            object[] objdata = threaddata as object[];

            CBDataInfo = objdata[1] as String;
            MainForm main = objdata[2] as MainForm;
            SocketConnection Sconnection = objdata[3] as SocketConnection;
            string statoCB = main.labelTipoCB.Text;

            //destroy the unused objects
            objdata = null;
            threaddata = null;

            try
            {
                #region GET CLIPBOARD
                if (CBDataInfo == ApplicationConstants.CLIPBOARDEVENTGET)
                {
                    //gestione invio clipboard

                    //creazione dati da inviare
                    byte[] dataToSend = CreateDataToSend(statoCB, main);

                    if (dataToSend != null)
                    {
                        //inizio comunicazione 

                        //invio tipo di dati e dimensione [TIPO]-[DIM]
                        byte[] datagram = new byte[64];
                        datagram = Encoding.ASCII.GetBytes(statoCB + "-" + dataToSend.Length);
                        Sconnection.PassivTransfer.Send(datagram);

                        //ricevo risp
                        datagram = new byte[64]; ;
                        Sconnection.PassivTransfer.Receive(datagram);

                        string datgramString = Encoding.ASCII.GetString(datagram);
                        datgramString = datgramString.Substring(0, datgramString.IndexOf("\0"));

                        if (datgramString == ApplicationConstants.OK)
                        {
                            //inizio trasferimento file

                            //set Buffer Trasmettitore
                            Sconnection.PassivTransfer.SendBufferSize = SBuffSize;

                            main.Invoke(main.startProgressBarDelegate, new Object[] { dataToSend.Length });

                            int dataSended = 0;
                            while (dataSended < dataToSend.Length && ClipBoardWork)
                            {
                                byte[] d = new byte[SBuffSize];

                                for (int i = dataSended; i < SBuffSize + dataSended && i < dataToSend.Length; i++)
                                {
                                    d[i - dataSended] = dataToSend[i];
                                    main.Invoke(main.doStepProgressBarDelegate);
                                }

                                Sconnection.PassivTransfer.Send(d);

                                dataSended += d.Length;
                            }

                            main.Invoke(main.closeProgressBarDelegate);
                        }
                        else
                        {
                            //non ho ricevuto l'ok
                        }
                    }

                }
                #endregion
                #region SET CLIPBOARD
                else if (CBDataInfo == ApplicationConstants.CLIPBOARDEVENTSET)
                {
                    //gestione ricezione clipboard
                    byte[] dataToReceive;
                    int dim;

                    //ricezione tipo di dati e dimensione [TIPO]-[DIM]
                    byte[] datagram = new byte[64];
                    Sconnection.PassivTransfer.Receive(datagram);
                    string datagramString = Encoding.ASCII.GetString(datagram);
                    string[] dati = datagramString.Split(ApplicationConstants.SEPARATOR);

                    //ricezione corretta
                    if (dati.Length == 2)
                    {
                        statoCB = dati[0];
                        dim = Convert.ToInt32(dati[1]);
                        dataToReceive = new byte[dim];

                        //invio risp +OK
                        datagram = Encoding.ASCII.GetBytes(ApplicationConstants.OK);
                        Sconnection.PassivTransfer.Send(datagram);

                        //ricezione dei/del file
                        Sconnection.PassivTransfer.ReceiveBufferSize = RBuffSize;

                        int dataReceived = 0;

                        main.Invoke(main.startProgressBarDelegate, new Object[] { dim });

                        while (dataReceived < dim && ClipBoardWork)
                        {
                            datagram = new byte[RBuffSize];
                            Sconnection.PassivTransfer.Receive(datagram);

                            for (int i = dataReceived; i < RBuffSize + dataReceived && i < dim; i++)
                            {
                                dataToReceive[i] = datagram[i - dataReceived];
                                main.Invoke(main.doStepProgressBarDelegate);
                            }

                            dataReceived += datagram.Length;
                        }

                        //creazione dei dati da mettere in clipboard se il thread deve lavorare
                        if(ClipBoardWork)
                        {
                            object data = CreateDataToSet(statoCB, dataToReceive);

                            if (data != null)
                            {
                                main.Invoke(main.SetCliboardDataDelegate, new Object[] { statoCB, data });
                            }
                        }
                        
                        main.Invoke(main.closeProgressBarDelegate);
                    }

                    else//ricezione comando sbagliato
                    {
                        datagram = Encoding.ASCII.GetBytes(ApplicationConstants.ERR);
                        Sconnection.PassivTransfer.Send(datagram);//invio -ERR
                    }
                }
                #endregion
            }
            catch(Exception e)
            {
                MessageBox.Show( e.Message,"Errore nella gestione della ClipBoard");
                if(main!=null)
                    main.Invoke(main.closeProgressBarDelegate);
            }
            finally
            {
                ThreadCounter--;
                mrMaxThreads.Set();
            }
            
        }

        private static object CreateDataToSet(string statoCB, byte[] dataToReceive)
        {
            object data = null;

            try
            {
                if (statoCB == ApplicationConstants.StatoClipBoard.AUDIO.ToString())
                {
                    MemoryStream ms = new MemoryStream(dataToReceive);
                    
                    data = ms;
                }
                else if (statoCB == ApplicationConstants.StatoClipBoard.IMMAGINE.ToString())
                {
                    ImageConverter ic = new ImageConverter();
                    Image i = (Image)ic.ConvertFrom(dataToReceive);

                    data = i;
                }
                else if (statoCB == ApplicationConstants.StatoClipBoard.TEXT.ToString())
                {
                    data = Encoding.ASCII.GetString(dataToReceive);
                }
                else if (statoCB == ApplicationConstants.StatoClipBoard.FILE_DROP.ToString())
                {
                    using (var ms = new MemoryStream(dataToReceive))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        object o = bf.Deserialize(ms);

                        object[] arr = new object[3];
                        arr = o as object[];

                        int Nfiles = (int)arr[0];
                        System.Collections.Specialized.StringCollection filenames = arr[1] as System.Collections.Specialized.StringCollection;
                        List<byte[]> files = arr[2] as List<byte[]>;

                        for (int i = 0; i < Nfiles; i++)
                        {
                            
                            filenames[i] = ApplicationConstants.TempPath + filenames[i];
                            File.WriteAllBytes(filenames[i], files[i]);
                            filenames[i] = Path.GetFullPath(filenames[i]);
                        }

                        //set clipboard file drop
                        data = filenames;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message,"Errore nella creazione dei dati");
                data = null;

                return data;
            }

            return data;
        }

        private static byte[] CreateDataToSend(string statoCB, MainForm main)
        {
            byte[] dataToSend = null;

            try
            {
                //creazione dei byte da inviare
                if (statoCB == ApplicationConstants.StatoClipBoard.AUDIO.ToString())
                {
                    Stream s = main.Invoke(main.GetCliboardDataDelegate) as Stream;
                    MemoryStream ms = new MemoryStream();
                    s.CopyTo(ms);
                    dataToSend = new byte[ms.ToArray().Length];
                    dataToSend = ms.ToArray();
                }
                else if (statoCB == ApplicationConstants.StatoClipBoard.IMMAGINE.ToString())
                {
                    Image i = main.Invoke(main.GetCliboardDataDelegate) as Image;

                    ImageConverter ic = new ImageConverter();
                    dataToSend = (byte[])ic.ConvertTo(i, typeof(byte[]));
                }
                else if (statoCB == ApplicationConstants.StatoClipBoard.TEXT.ToString())
                {
                    dataToSend = Encoding.ASCII.GetBytes(main.Invoke(main.GetCliboardDataDelegate) as String);
                }
                else if (statoCB == ApplicationConstants.StatoClipBoard.FILE_DROP.ToString())
                {
                    System.Collections.Specialized.StringCollection filenames = main.Invoke(main.GetCliboardDataDelegate) as System.Collections.Specialized.StringCollection;
                    object[] arr = new object[3];

                    List<byte[]> files = new List<byte[]>();

                    for (int i = 0; i < filenames.Count; i++)
                    {
                        byte[] f = File.ReadAllBytes(filenames[i]);
                        filenames[i] = Path.GetFileName(filenames[i]);

                        files.Add(f);
                    }
                    arr[0] = files.Count;
                    arr[1] = filenames;//eventualmente mandare solo il nome senza il percorso
                    arr[2] = files;


                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, arr);
                    dataToSend = ms.ToArray();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message,"Errore nella creazione dei dati da inviare");
                dataToSend = null;

                return dataToSend;
            }
            return dataToSend;
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
        public static bool ClipBoardWork
        {
            get
            {
                mut.WaitOne();
                bool cbw = clipBoardWork;
                mut.ReleaseMutex();
                return cbw;
            }
            set
            {
                mut.WaitOne();
                clipBoardWork = value;
                mut.ReleaseMutex();
            }
        }
    }
}
