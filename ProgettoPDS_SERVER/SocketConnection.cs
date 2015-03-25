using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ProgettoPDS_SERVER
{
    /*PROTOCOLLO AUTENTICAZIONE/REGISTRAZIONE
     * 
     * +AUTH_USER(SERVER)
     * +"USERNAME"(CLIENT)
     * 
     * USER PRESENTE:
     * * +AUTH_PWD(SERVER)
     * * +"PASSWORD"(CLIENT)
     * 
     * USER NON PRESENTE:
     * * +REG_USER(SERVER)
     * *
     * * SE IL CLIENT VUOLE REGISTRARSI:
     * * * +YES(CLIENT)
     * * * +REG_PWD(SERVER)
     * * * "PASSWORD"(CLIENT)
     * 
     * * SE IL CLIENT VUOLE REGISTRARSI:
     * * * +NO(CLIENT)
     * * * chiudo la connessione
     * 
     * +OK(SERVER)
     * CONNESSIONE CREATA CON SUCCESSO
     * -ERR(SERVER)
     * ERRORE CONNESSIONE CHIUSA AUTENTICAZIONE NON RIUSCITA, ANCHE SE RICEVO UN ERR DAL CLIENT
     * 
     */
    class SocketConnection
    {
        //attributi

        private const int backlog = 1;
        private Socket sock = null, passiv = null, sockTransfer = null, passivTransfer = null;
        private int porta;
        private String myIP;
        private MainForm main;
        private string client = null;
        public const int trasfport = 4000;

        public Socket Passiv
        {
            get { return passiv; }
        }
        public Socket PassivTransfer
        {
            get { return passivTransfer; }
        }

        private ApplicationConstants.Stato stato = ApplicationConstants.Stato.DISCONNESSO;
        public ApplicationConstants.Stato Stato { get { return stato; } set { stato = value; StatoChanged(stato); } }

        //costruttore

        public SocketConnection(int porta)
        {
            Stato = ApplicationConstants.Stato.DISCONNESSO;
            this.porta = porta;
        }

        #region Socket Methods
        public void StartListening(MainForm main)
        {
            this.main = main;
            Stato = ApplicationConstants.Stato.IN_ATTESA;
            
            if(sock == null)
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            try
            {
                // Bind the socket to the local endpoint
                if(!sock.IsBound)
                    sock.Bind(new IPEndPoint(IPAddress.Any, porta));

                sock.Listen(backlog);
                // Program is suspended while waiting for an incoming connection.
                sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            sock = (Socket)ar.AsyncState;
            passiv = sock.EndAccept(ar);

            main.notifyIcon1.ShowBalloonTip(main.ToolTipTimeOut, "INFO STATO", "Inizio procedura autenticazione client . . ." + passiv.RemoteEndPoint.ToString(), ToolTipIcon.Info);
            
            //sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            try
            {
                if (ClientAutentication())
                {
                    Stato = ApplicationConstants.Stato.CONNESSO;
                    //disegno qualcosa per mostrare il controllo
                    DrawBorders();
                    //lancio il backGround worker per il controllo dei pacchetti
                    main.PacketsHandlerbackgroundWorker.RunWorkerAsync();

                    if (sockTransfer == null)
                        sockTransfer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Bind the socket to the local endpoint
                    if (!sockTransfer.IsBound)
                        sockTransfer.Bind(new IPEndPoint(IPAddress.Any, trasfport));

                    sockTransfer.Listen(backlog);
                    // Program is suspended while waiting for an incoming connection.
                    passivTransfer = sockTransfer.Accept();

                    IPEndPoint passivIP = passiv.RemoteEndPoint as IPEndPoint;
                    IPEndPoint passivIPTS = passivTransfer.RemoteEndPoint as IPEndPoint;

                    if (passivIP.Address.ToString() != passivIPTS.Address.ToString())
                    {
                        SockDisconnectTS();
                        SockDisconnect();
                    }

                }
                else
                {
                    Stato = ApplicationConstants.Stato.DISCONNESSO;
                    main.notifyIcon1.ShowBalloonTip(main.ToolTipTimeOut, "ERRORE", "Autenticazione Client non riuscita. Chiusura connessione.", ToolTipIcon.Warning);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }

        public void SockDisconnect(Socket s)
        {
            try
            {
                if (s != null)
                {
                    if (s.Connected)
                    {
                        s.Shutdown(SocketShutdown.Both);
                        s.Close();
                        s = null;
                        main.notifyIcon1.ShowBalloonTip(main.ToolTipTimeOut, "INFO STATO", "Connessione chiusa correttamente.", ToolTipIcon.Info);
                    }
                }
                else
                {
                    main.notifyIcon1.ShowBalloonTip(main.ToolTipTimeOut, "INFO STATO", "Deve ancora essere creata una connessione.", ToolTipIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ECCEZIONE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SockDisconnect()
        {
            SockDisconnect(passiv);
            SockDisconnect(sock);

            Stato = ApplicationConstants.Stato.DISCONNESSO;
        }
        #endregion

        #region Transfer Socket Methods
        public void StartListeningTS()
        {
            if (sockTransfer == null)
                sockTransfer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            try
            {
                // Bind the socket to the local endpoint
                if (!sockTransfer.IsBound)
                    sockTransfer.Bind(new IPEndPoint(IPAddress.Any, trasfport));

                sockTransfer.Listen(backlog);
                // Program is suspended while waiting for an incoming connection.
                sockTransfer.BeginAccept(new AsyncCallback(AcceptCallbackTS), sock);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void AcceptCallbackTS(IAsyncResult ar)
        {
            sockTransfer = (Socket)ar.AsyncState;
            passivTransfer = sockTransfer.EndAccept(ar);

            IPEndPoint passivIP = passiv.RemoteEndPoint as IPEndPoint;
            IPEndPoint passivIPTS = passivTransfer.RemoteEndPoint as IPEndPoint;

            if(passivIP.Address != passivIPTS.Address)
            {
                SockDisconnectTS();
            }


        }
        public void SockDisconnectTS()
        {
            SockDisconnect(passivTransfer);
            SockDisconnect(sockTransfer);
        }
        #endregion

        #region Client Authentication
        //protocollo di autenticazione/registrazione
        private bool ClientAutentication()
        {
            string comando, user, pwd;
            byte[] data;

            //richiesta user e password;

            comando = ApplicationConstants.AUTH_USER;
            data = Encoding.ASCII.GetBytes(comando);

            try {
                passiv.Send(data);//invio richiesta di autenticazione AUTH_USER

                data = new byte[128];
                passiv.Receive(data);//ricevo l'USER
                user = Encoding.ASCII.GetString(data);
                user = user.Substring(0, user.IndexOf('\0'));
                //
                client = user;

                // controllo che l'user sia già registrato
                if (user != ApplicationConstants.ERR)
                {
                    if (user != " ")
                    {
                        DialogResult res = MessageBox.Show(user + " ha richiesto di autenticarsi.","RICHIESTA AUTENTICAZIONE",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                        if (res == DialogResult.OK)
                        {
                            string passwordXML = ControlloUser(user);

                            if (passwordXML != null)//SE USER è PRESENTE
                            {
                                comando = ApplicationConstants.AUTH_PWD;

                                data = Encoding.ASCII.GetBytes(comando);
                                passiv.Send(data);//se l'user è registrato gli chiedo la pswrd AUTH_PWD

                                data = new byte[128];
                                passiv.Receive(data);//ricevo la password
                                pwd = Encoding.ASCII.GetString(data);
                                pwd = pwd.Substring(0, pwd.IndexOf('\0'));

                                if (pwd != ApplicationConstants.ERR)
                                {
                                    if (passwordXML == pwd) // Controllo Password!!
                                    {
                                        comando = ApplicationConstants.OK;
                                        data = Encoding.ASCII.GetBytes(comando);
                                        passiv.Send(data);//invio che l'autenticazione è riuscita
                                    }
                                    else//se la password non è corretta si chiude la connessione
                                    {
                                        comando = ApplicationConstants.ERR;
                                        data = Encoding.ASCII.GetBytes(comando);
                                        passiv.Send(data);//invio messaggio di errore

                                        SockDisconnect();//e chiudo
                                        return false;
                                    }
                                }
                                else//è arrivato un ERR dopo AUTH_PWD
                                {
                                    SockDisconnect();//e chiudo
                                    return false;
                                }
                            }
                            else//se il client non è presente chiedo di registrarsi
                            {
                                comando = ApplicationConstants.REG_USER;
                                data = Encoding.ASCII.GetBytes(comando);

                                passiv.Send(data);//INVIO RICHIESTA

                                data = new byte[128];
                                passiv.Receive(data);//RICEVO LA RISPOSTA
                                comando = Encoding.ASCII.GetString(data);
                                comando = comando.Substring(0, comando.IndexOf('\0'));

                                if (comando == ApplicationConstants.YES)//se vuole registrarsi chiedo la password
                                {
                                    comando = ApplicationConstants.REG_PWD;
                                    data = Encoding.ASCII.GetBytes(comando);

                                    passiv.Send(data);//richiesta pswrd

                                    data = new byte[128];
                                    passiv.Receive(data);//password ricevuta
                                    pwd = Encoding.ASCII.GetString(data);
                                    pwd = pwd.Substring(0, pwd.IndexOf('\0'));

                                    if (!AddNewUser(user, pwd))//inserimento nuovo user non riuscito
                                    {
                                        comando = ApplicationConstants.ERR;
                                        data = Encoding.ASCII.GetBytes(comando);
                                        passiv.Send(data);//invio che c'è stato un errore

                                        SockDisconnect();//e chiudo
                                        return false;
                                    }
                                }
                                else// se non ricevo YES, ovvero l'user non vuole reistrarsi
                                {
                                    SockDisconnect();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //da gestire Cancel, ovvero non voglio che un certo user si connetta
                        }
                    }
                    else
                    {
                        //da gestire user vuoto
                    }
                }   
                else//se ricevo un ERR dopo AUTH_USER
                {
                    SockDisconnect();
                    return false;
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                SockDisconnect();//e chiudo
                return false;
            }
            return true;
        }
        #endregion

        #region XML methods
        private bool AddNewUser(string user, string pwd)
        {
            XmlManager document = new XmlManager('C');

            if (document.Error)
                return false;

            document.AddNewClient(user, pwd);

            return true;
        }

        private string ControlloUser(string user)
        {
            XmlManager document = new XmlManager('C');

            if (document.Error)
                return null;

            string pwd = document.SearchClient(user);

            return pwd;
        }
        #endregion
        
        #region Graphics
        public void DrawBorders()
        {
            IntPtr desktop = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(desktop))
            {
                int border = 5;
                //top
                g.FillRectangle(Brushes.Red, 0, 0, Screen.PrimaryScreen.Bounds.Width, border);
                //right
                g.FillRectangle(Brushes.Red, Screen.PrimaryScreen.Bounds.Width - border, border, border, Screen.PrimaryScreen.Bounds.Height - (2 * border));
                //bottom
                g.FillRectangle(Brushes.Red, 0, Screen.PrimaryScreen.Bounds.Height - border, Screen.PrimaryScreen.Bounds.Width, border);
                //left
                g.FillRectangle(Brushes.Red, 0, border, border, Screen.PrimaryScreen.Bounds.Height - (2 * border));
            }
            ReleaseDC(IntPtr.Zero, desktop);
        }

        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll", EntryPoint = "ReleaseDC", SetLastError = true)]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        #endregion

        public String GetMyIp()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIP = ip.ToString();
                    break;
                }
            }
            return myIP;
        }

        private void StatoChanged(ApplicationConstants.Stato value)
        {
            //
            if (main != null)
            {
                main.notifyIcon1.ShowBalloonTip(main.ToolTipTimeOut, "INFO STATO", "Lo stato dell' applicazione é : " + value.ToString(), ToolTipIcon.Info);
                main.Invoke(main.LabelStatoChangedDelegate,new Object[] {stato,client});
            }
        }
    }
}

