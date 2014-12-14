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

        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

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
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SocketConnection.connectDone.WaitOne();
                
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SocketConnection.connectDone.Set();
            }
        }

        private void ConnectCallback(IAsyncResult ar) 
        {
            try {

                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                
                byte[] comando = new byte[128];
                this.sock.BeginReceive(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(ReceiveResponseCallback), this.sock);
                SocketConnection.receiveDone.WaitOne();
                string aux = Encoding.ASCII.GetString(comando);                
                aux = aux.Substring(0, aux.IndexOf('\0') );

                MessageBox.Show("Comando Ricevuto: " + aux);

                if (aux == "+AUTH_USER") {
                    comando = Encoding.ASCII.GetBytes(this.utente.Username);
                    this.sock.BeginSend(comando,0,comando.Length, SocketFlags.None, new AsyncCallback(SendCommandCallback), this.sock);
                    SocketConnection.sendDone.WaitOne();
                    comando = new byte[128];
                    this.sock.BeginReceive(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(ReceiveResponseCallback), this.sock);
                    SocketConnection.receiveDone.WaitOne();
                    aux = Encoding.ASCII.GetString(comando);
                    aux = aux.Substring(0, aux.IndexOf('\0'));

                    MessageBox.Show("Comando Ricevuto: " + aux);

                    if (aux == "+AUTH_PWD") {
                        comando = Encoding.ASCII.GetBytes(this.utente.Password);
                        this.sock.BeginSend(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(SendCommandCallback), this.sock);
                        SocketConnection.sendDone.WaitOne();
                    }
                    else if (aux == "+REG_USER") {
                        DialogResult res = MessageBox.Show("Il Server richiede l'autorizzazione a registrare le tue credenziali. Procede?",
                            "AVVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (res == DialogResult.Yes) {
                            comando = Encoding.ASCII.GetBytes("+YES");
                            this.sock.BeginSend(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(SendCommandCallback), this.sock);
                            SocketConnection.sendDone.WaitOne();
                            comando = new byte[128];
                            this.sock.BeginReceive(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(ReceiveResponseCallback), this.sock);
                            SocketConnection.receiveDone.WaitOne();
                            aux = Encoding.ASCII.GetString(comando);

                            if (aux == "+REG_PWD") {
                                comando = Encoding.ASCII.GetBytes(this.utente.Password);
                                this.sock.BeginSend(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(SendCommandCallback), this.sock);
                                SocketConnection.sendDone.WaitOne();
                            }
                            else
                                ErrorProtocol();
                        }
                        else {
                            comando = Encoding.ASCII.GetBytes("+NO");
                            this.sock.BeginSend(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(SendCommandCallback), this.sock);
                            SocketConnection.sendDone.WaitOne();
                            SockDisconnect();
                            return;
                        }

                    }
                    else
                        ErrorProtocol();

                    comando = new byte[128];
                    this.sock.BeginReceive(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(ReceiveResponseCallback), this.sock);
                    SocketConnection.receiveDone.WaitOne();
                    aux = Encoding.ASCII.GetString(comando);
                    aux = aux.Substring(0, aux.IndexOf('\0'));

                    if (aux == "+OK")
                        MessageBox.Show("Connesso con" + this.sock.RemoteEndPoint.ToString() + "!!", 
                            "CONNECTION SUCCESS!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if( aux == "-ERR") {
                        MessageBox.Show("Connessione con il server fallita!!",
                            "CONNECTION FAILURE!!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }

                }
                else 
                    ErrorProtocol();

                SocketConnection.connectDone.Set();
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SocketConnection.connectDone.Set();
            }
        }

        private void ErrorProtocol() 
        {
            byte[] comando = Encoding.ASCII.GetBytes("-ERR");
            this.sock.BeginSend(comando, 0, comando.Length, SocketFlags.None, new AsyncCallback(SendCommandCallback), this.sock);
            SocketConnection.sendDone.WaitOne();
            MessageBox.Show("Errore nel protocollo di Autenticazione!! Connessione Fallita", 
                "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SockDisconnect();       
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

        private void SendCommandCallback(IAsyncResult ar)
        {
            try {
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);

                SocketConnection.sendDone.Set();
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message,"ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SocketConnection.sendDone.Set();
                SockDisconnect();
            }
        }

        private void ReceiveResponseCallback(IAsyncResult ar)
        {
            try {
                Socket client = (Socket)ar.AsyncState;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                SocketConnection.receiveDone.Set();
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SocketConnection.receiveDone.Set();
                SockDisconnect();
            }
        }

        public Socket Sock
        {
            get { return this.sock; }
            set { this.sock = value; }
        }

    }
}
