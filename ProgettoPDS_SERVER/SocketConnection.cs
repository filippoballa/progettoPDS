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
        private const int backlog = 1;
        private Socket sock, passiv;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private int porta;
        private String myIP;

        enum Stato
        {
            CONNESSO = 1,
            DISCONNESSO = 0
        };

        private const String AUTH_USER = "+AUTH_USER";
        private const String AUTH_PWD = "+AUTH_PWD";
        private const String REG_USER = "+REG_USER";
        private const String YES = "+YES";
        private const String REG_PWD = "+REG_PWD";
        private const String OK = "+OK";
        private const String ERR = "-ERR";

        private Stato stato;
        private char[] SEPARATOR = {'-'};
        private const string MOUSECODE = "M";
        private MainForm main;

        public SocketConnection(int porta)
        {
            stato = Stato.DISCONNESSO;
            // Create a TCP/IP socket.
            this.porta = porta;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            sock.Bind(new IPEndPoint(IPAddress.Any, porta));
        }
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
                stato = Stato.CONNESSO;
                main.PopUpShow(null,null);
                //lancio il controllo del mouse
                PacketsHandler(main);
            }
            else
            {
                main.notifyIcon1.ShowBalloonTip(2, "ERRORE", "Autenticazione Client non riuscita. Chiusura connessione.", ToolTipIcon.Warning);
            }
        }

        private bool ClientAutentication()
        {
            string comando, user, pwd;
            byte[] data;

            //richiesta user e password;

            comando = AUTH_USER;
            data = Encoding.ASCII.GetBytes(comando);

            try {
                passiv.Send(data);//invio richiesta di autenticazione AUTH_USER

                data = new byte[128];
                passiv.Receive(data);//ricevo l'USER
                user = Encoding.ASCII.GetString(data);
                user = user.Substring(0, user.IndexOf('\0'));

                // controllo che l'user sia già registrato
                if (user != ERR)
                {
                    if (user != " ")
                    {
                        DialogResult res = MessageBox.Show(user + " ha richiesto di autenticarsi.","DURO DURO",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                        if (res == DialogResult.OK)
                        {
                            string passwordXML = ControlloUser(user);

                            if (passwordXML != null)//SE USER è PRESENTE
                            {
                                comando = AUTH_PWD;

                                data = Encoding.ASCII.GetBytes(comando);
                                passiv.Send(data);//se l'user è registrato gli chiedo la pswrd AUTH_PWD

                                data = new byte[128];
                                passiv.Receive(data);//ricevo la password
                                pwd = Encoding.ASCII.GetString(data);
                                pwd = pwd.Substring(0, pwd.IndexOf('\0'));

                                if (pwd != ERR)
                                {
                                    if (passwordXML == pwd) // Controllo Password!!
                                    {
                                        comando = OK;
                                        data = Encoding.ASCII.GetBytes(comando);
                                        passiv.Send(data);//invio che l'autenticazione è riuscita
                                    }
                                    else//se la password non è corretta si chiude la connessione
                                    {
                                        comando = ERR;
                                        data = Encoding.ASCII.GetBytes(comando);
                                        passiv.Send(data);//invio che c'è stato un errore

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
                                comando = REG_USER;
                                data = Encoding.ASCII.GetBytes(comando);

                                passiv.Send(data);//INVIO RICHIESTA

                                data = new byte[128];
                                passiv.Receive(data);//RICEVO LA RISPOSTA
                                comando = Encoding.ASCII.GetString(data);
                                comando = comando.Substring(0, comando.IndexOf('\0'));

                                if (comando == YES)//se vuole registrarsi chiedo la password
                                {
                                    comando = REG_PWD;
                                    data = Encoding.ASCII.GetBytes(comando);

                                    passiv.Send(data);//richiesta pswrd

                                    data = new byte[128];
                                    passiv.Receive(data);//password ricevuta
                                    pwd = Encoding.ASCII.GetString(data);
                                    pwd = pwd.Substring(0, pwd.IndexOf('\0'));

                                    if (!AddNewUser(user, pwd))//inserimento non riuscito
                                    {
                                        comando = ERR;
                                        data = Encoding.ASCII.GetBytes(comando);
                                        passiv.Send(data);//invio che c'è stato un errore

                                        SockDisconnect();//e chiudo
                                        return false;
                                    }
                                }
                                else// se non ricevo YES
                                {
                                    SockDisconnect();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //da gestire Cancel
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

        private bool AddNewUser(string user, string pwd)
        {
            XmlManager document = new XmlManager('C');

            if ( document.Error )
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
            stato = Stato.DISCONNESSO;
            
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
            stato = Stato.DISCONNESSO;
            

        }
        public void PacketsHandler(MainForm form)
        {
            byte[] data;
            String comando;
            while (true)//ricevo i pachetti del client 
            {
                try
                {
                    //String[] MouseData = null;
                    data = new byte[45];
                    passiv.Receive(data);//password ricevuta
                    comando = Encoding.ASCII.GetString(data);
                    comando = comando.Substring(0, comando.IndexOf('\0'));

                    String[] MouseData = comando.Split(SEPARATOR);
                   

                    if (MouseData[0] != null && MouseData[0] == MOUSECODE)
                    {
                        double X = 0.0, Y = 0.0;

                        if (MouseData[2] != null)
                            X = Convert.ToDouble(MouseData[2],NumberFormatInfo.CurrentInfo);
                        if (MouseData[3] != null)
                            Y = Convert.ToDouble(MouseData[3], NumberFormatInfo.CurrentInfo);

                        int PosX = (int)X * Screen.PrimaryScreen.WorkingArea.Width;
                        int PosY = (int)Y * Screen.PrimaryScreen.WorkingArea.Height;

                        Cursor.Position = new Point(PosX, PosY);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    //break;
                }
            }
        }
    }

}

