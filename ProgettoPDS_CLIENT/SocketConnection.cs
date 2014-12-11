using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ProgettoPDS_CLIENT
{
    class SocketConnection
    { 
        private Socket sock;
        private IPEndPoint remoteEP, localEP;
        private User utente;
        public const int localPort = 5000;
        private int remotePort;
        private static bool isBindLocal = false; 

        // Costruttore Client usato per Connessione con il server
        public SocketConnection(string addr, int port, User utente ) 
        {
            this.utente = utente;
            this.GetLocalIP();
            this.remoteEP = new IPEndPoint(IPAddress.Parse(addr), Convert.ToInt32(port) );
            this.remotePort = port;

            try {
                this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                if (!SocketConnection.isBindLocal)
                    this.BindingLocalEP();

                this.sock.ReceiveTimeout = 10000;
                this.sock.SendTimeout = 10000;
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Bind dell'endpoint locale
        private void BindingLocalEP() 
        {
            if (this.sock != null) {
                this.sock.Bind(this.localEP);
                SocketConnection.isBindLocal = true;
            }
        }

        // La funzione ritorna l'indirizzo IP della macchina
        public static string MyIpInfo() 
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string AddrInformation = null;

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    AddrInformation = ip.ToString();
                }
            }

            return AddrInformation;           
            
        }

        // Ottiene configurazione Locale dell'Host
        private void GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry( Dns.GetHostName() );

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localEP = new IPEndPoint(ip, localPort);
                    break;
                }
            }

        }

        // Esegue una connessione al Server
        public void StartClientConnection() 
        {
            try {
                this.sock.BeginConnect(this.remoteEP, new AsyncCallback(this.ConnectCallback), sock);
                
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectCallback(IAsyncResult ar) 
        {
            try {

                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
               
                byte[] comando = new byte[1024];

                this.sock.Receive(comando);

                string aux = Encoding.ASCII.GetString(comando);

                if (aux == "+AUTH_USER") {
                    comando = Encoding.ASCII.GetBytes(this.utente.Username);
                    this.sock.Send(comando);
                    this.sock.Receive(comando);
                    aux = Encoding.ASCII.GetString(comando);

                    if (aux == "+AUTH_PWD") {
                        comando = Encoding.ASCII.GetBytes(this.utente.Username);
                        this.sock.Send(comando);
                    }
                    else if( aux == "+REG_USER") {
                        comando = Encoding.ASCII.GetBytes("YES");
                        this.sock.Send(comando);
                        this.sock.Receive(comando);
                        aux = Encoding.ASCII.GetString(comando);

                        if (aux == "+REG_PWD") {
                            comando = Encoding.ASCII.GetBytes( this.utente.Username );
                            this.sock.Send(comando);
                        }
                    }

                    this.sock.Receive(comando);
                    aux = Encoding.ASCII.GetString(comando);
 
                    if( aux == "+OK" )
                        MessageBox.Show("Connesso con" + this.sock.RemoteEndPoint.ToString() + "!!");

                }
                else { 
                    // errore!!
                }

            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // La funzione ritorna "true" se il socket è connesso
        public bool IsConnected() 
        {
            if (this.sock.Connected)
                return true;
            else
                return false;
        }

        // La funzione rilascia le risorse associate al socket
        public void SockDisconnect() 
        {
            this.sock.Shutdown(SocketShutdown.Both);
            this.sock.Close();
        }

    }
}
