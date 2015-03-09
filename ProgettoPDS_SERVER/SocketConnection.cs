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
        private Socket sock, passiv;
        private int porta;
        private String myIP;
        private ApplicationConstants.Stato stato;
        private MainForm main;

        public Socket Passiv
        {
            get { return this.passiv; }
        }

        //costruttore

        public SocketConnection(int porta)
        {
            stato = ApplicationConstants.Stato.DISCONNESSO;
            // Create a TCP/IP socket.
            this.porta = porta;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint
            sock.Bind(new IPEndPoint(IPAddress.Any, porta));
        }

        #region Socket Methods
        public void StartListening(MainForm main)
        {
            this.main = main;

            if(sock == null)
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
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
            passiv = (Socket)ar.AsyncState;
            passiv = passiv.EndAccept(ar);

            main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Inizio procedura autenticazione client . . .(" + passiv.RemoteEndPoint.ToString() + ")", ToolTipIcon.Info);
            if(ClientAutentication())
            {
                main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Autenticazione Client effettuata con successo! Connessione avviata.",ToolTipIcon.Info);
                stato = ApplicationConstants.Stato.CONNESSO;
                //disegno qualcosa per mostrare il controllo
                Thread t = new Thread(new ThreadStart(DrawBorders));
                //lancio il backGround worker per il controllo dei pacchetti
                main.PacketsHandlerbackgroundWorker.RunWorkerAsync();
            }
            else
            {
                main.notifyIcon1.ShowBalloonTip(2, "ERRORE", "Autenticazione Client non riuscita. Chiusura connessione.", ToolTipIcon.Warning);
            }
        }

        public void SockDisconnect(Socket sock)
        {
            if (this.sock != null)
            {
                if (sock.Connected)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                    main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Connessione chiusa correttamente.", ToolTipIcon.Info);
                }
                else
                {
                    main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Eri già disconnesso.", ToolTipIcon.Warning);
                }
            }
            else
            {
                main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Deve ancora essere creata una connessione.", ToolTipIcon.Warning);
            }
            stato = ApplicationConstants.Stato.DISCONNESSO;

        }

        public void SockDisconnect()
        {
            if (this.passiv != null)
            {
                if (this.passiv.Connected)
                {
                    this.passiv.Shutdown(SocketShutdown.Both);
                    this.passiv.Close();
                    main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Connessione chiusa correttamente.", ToolTipIcon.Info);
                }
                else
                {
                    main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Eri già disconnesso.", ToolTipIcon.Warning);
                }
            }
            else
            {
                main.notifyIcon1.ShowBalloonTip(2, "INFO STATO", "Deve ancora essere creata una connessione.", ToolTipIcon.Warning);
            }
            stato = ApplicationConstants.Stato.DISCONNESSO;

        }
        #endregion

        #region Client Authentication
        //protocollo di autenticazione/regisrazione
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
                int border = 3;
                //top
                g.FillRectangle(Brushes.Red, 0, 0, Screen.PrimaryScreen.Bounds.Width, border);
                //right
                g.FillRectangle(Brushes.Green, Screen.PrimaryScreen.Bounds.Width - border, border, border, Screen.PrimaryScreen.Bounds.Height - (2 * border));
                //bottom
                g.FillRectangle(Brushes.Red, 0, Screen.PrimaryScreen.Bounds.Height - border, Screen.PrimaryScreen.Bounds.Width, border);
                //left
                g.FillRectangle(Brushes.Yellow, 0, border, border, Screen.PrimaryScreen.Bounds.Height - (2 * border));
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
    }

}

