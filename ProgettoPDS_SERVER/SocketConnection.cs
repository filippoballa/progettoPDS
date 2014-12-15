using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;

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

        private const String AUTH_USER = "+AUTH_USER";
        private const String AUTH_PWD = "+AUTH_PWD";
        private const String REG_USER = "+REG_USER";
        private const String YES = "+YES";
        private const String REG_PWD = "+REG_PWD";
        private const String OK = "+OK";
        private const String ERR = "-ERR";
        private const String CONNESSO = "CONNESSO";
        private const String DISCONNESSO = "DISCONNESSO";

        private static String stato;
        private const char[] SEPARATOR = {'-'};
        private const string MOUSECODE = "M";

        public SocketConnection(int porta)
        {
            stato = DISCONNESSO;
            // Create a TCP/IP socket.
            this.porta = porta;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            sock.Bind(new IPEndPoint(IPAddress.Any, porta));
        }
        public bool StartListening()
        {
            try
            {
                sock.Listen(backlog);
                // Program is suspended while waiting for an incoming connection.
                sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);
                // Wait until a connection is made before continuing.
                if (stato == CONNESSO)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            passiv = (Socket)ar.AsyncState;
            passiv = passiv.EndAccept(ar);

            MessageBox.Show("Connessione avvenuta inizio autenticazione client("+passiv.RemoteEndPoint.ToString()+")");
            if(ClientAutentication())
            {
                MessageBox.Show("Autenticazione Client effettuata con successo! Connessione avviata.");
            }
            else
            {
                MessageBox.Show("Autenticazione Client non riuscita. Chiusura connessione.","errore",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

                // controllo che l'user sia già registrato
                if (user != ERR)
                {
                    MessageBox.Show(user + " ha richiesto di autenticarsi.");

                    if (ControlloUser(user))
                    {
                        comando = AUTH_PWD;

                        data = Encoding.ASCII.GetBytes(comando);
                        passiv.Send(data);//se l'user è registrato gli chiedo la pswrd AUTH_PWD

                        data = new byte[128];
                        passiv.Receive(data);//ricevo la password
                        pwd = Encoding.ASCII.GetString(data);

                        if (pwd != ERR)
                        {
                            if (ControlloPassword(user, pwd))
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

                        passiv.Send(data);//se l'user è registrato gli chiedo la pswrd

                        data = new byte[128];
                        passiv.Receive(data);
                        comando = Encoding.ASCII.GetString(data);

                        if (comando == YES)//se vuole registrarsi chiedo la password
                        {
                            comando = REG_PWD;
                            data = Encoding.ASCII.GetBytes(comando);

                            passiv.Send(data);//richiesta pswrd

                            data = new byte[128];
                            passiv.Receive(data);//password ricevuta
                            pwd = Encoding.ASCII.GetString(data);

                            if (!AddNewUser(user, pwd))//inserimento non riuscito
                            {
                                comando = ERR;
                                data = Encoding.ASCII.GetBytes(comando);
                                passiv.Send(data);//invio che c'è stato un errore

                                SockDisconnect();//e chiudo
                                return false;
                            }
                        }else// se non ricevo YES
                        {
                            SockDisconnect();
                            return false;
                        }

                    }
                }else//se ricevo un ERR dopo AUTH_USER
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
            //TODO
            return true;
        }

        private bool ControlloPassword(string user, string pwd)
        {
            //TODO
            return true;
        }

        private bool ControlloUser(string user)
        {
            //TODO
            return true;
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

        public void SockDisconnect() {
            sock.Shutdown(SocketShutdown.Both);
            sock.Close();
        }
        public void PacketsHandler(MainForm form)
        {
            byte[] data;
            String comando;
            while (true)//ricevo i pachetti del client 
            {
                //String[] MouseData = null;
                data = new byte[128];
                passiv.Receive(data);//password ricevuta
                comando = Encoding.ASCII.GetString(data);

                String[] MouseData = comando.Split(SEPARATOR);

                if (MouseData[0]!=null && MouseData[0] == MOUSECODE)
                {
                    double X,Y;

                    if(MouseData[1]!=null)
                        X = Convert.ToDouble(MouseData[1]);
                    if(MouseData[2]!=null)
                        Y = Convert.ToDouble(MouseData[2]);

                    int PosX = (int)X * Screen.PrimaryScreen.WorkingArea.Width;
                }
            }
        }

    }

}

