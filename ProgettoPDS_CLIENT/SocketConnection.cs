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
        #region Variables

        private Socket sock;
        private IPEndPoint remoteEP, localEP;
        private User utente;
        public const int localPort = 5000;
        private int remotePort;
        public static bool isBindLocal = false;
        private bool isDisconect;
        private MainForm m;

        #endregion

        #region Constructor

        public SocketConnection(string addr, int port, User utente, MainForm m ) 
        {
            this.utente = utente;
            this.m = m;
            this.GetLocalIP();
            this.remoteEP = new IPEndPoint(IPAddress.Parse(addr), Convert.ToInt32(port) );
            this.remotePort = port;
            this.isDisconect = false;

            try {
                this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                if (!SocketConnection.isBindLocal)
                    this.BindingLocalEP();
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region PROTOCOLLO DI AUTENTICAZIONE

        // Esegue una connessione al Server
        public void StartClientConnection() 
        {
            try {
                this.sock.BeginConnect(this.remoteEP, new AsyncCallback(this.ConnectCallback), sock);                            
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectCallback(IAsyncResult ar) 
        {
            try {
          
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                byte[] comando = new byte[128];
                client.Receive(comando);
                string aux = Encoding.ASCII.GetString(comando);                
                aux = aux.Substring(0, aux.IndexOf('\0') );

                if (aux == "+AUTH_USER") {
                    comando = Encoding.ASCII.GetBytes(this.utente.Username);
                    client.Send(comando); // Invio username dell'utente. 
                    comando = new byte[128];
                    client.Receive(comando);
                    aux = Encoding.ASCII.GetString(comando);
                    aux = aux.Substring(0, aux.IndexOf('\0'));

                    if ( aux == "+AUTH_PWD" ) {
                        comando = Encoding.ASCII.GetBytes(this.utente.Password);
                        client.Send(comando); // Invio password dell'utente
                    }
                    else if (aux == "+REG_USER") {
                        DialogResult res = MessageBox.Show("Il Server richiede l'autorizzazione a registrare le tue credenziali. Procedere?",
                            "AVVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (res == DialogResult.Yes) {
                            comando = Encoding.ASCII.GetBytes("+YES");
                            client.Send(comando); // Il client accetta di registrarsi sul server!!
                            comando = new byte[128];
                            client.Receive(comando);
                            aux = Encoding.ASCII.GetString(comando);
                            aux = aux.Substring(0, aux.IndexOf('\0'));

                            if (aux == "+REG_PWD") {
                                comando = Encoding.ASCII.GetBytes(this.utente.Password);
                                client.Send(comando); // Invio password dell'utente per registrarla sul server.
                            }
                            else {
                                ErrorProtocol();
                                return;
                            }
                        }
                        else {
                            comando = Encoding.ASCII.GetBytes("+NO");
                            client.Send(comando); // Il client NON accetta di registrarsi sul server!!
                            SockClose();
                            return;
                        }

                    }
                    else {
                        ErrorProtocol();
                        return;
                    }

                    comando = new byte[128];
                    client.Receive(comando);
                    aux = Encoding.ASCII.GetString(comando);
                    aux = aux.Substring(0, aux.IndexOf('\0'));

                    if (aux == "+OK")
                        MessageBox.Show("Connesso con " + this.sock.RemoteEndPoint.ToString() + "!!", 
                            "CONNECTION SUCCESS!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if( aux == "-ERR") {
                        MessageBox.Show("Connessione con il server fallita!!",
                            "CONNECTION FAILURE!!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }

                }
                else 
                    ErrorProtocol();

                this.m.Invoke(this.m.myHandler);
                

            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ErrorProtocol() 
        {
            byte[] comando = Encoding.ASCII.GetBytes("-ERR");
            this.sock.Send(comando);
            MessageBox.Show("Errore nel protocollo di Autenticazione!! Connessione Fallita", 
                "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.SockClose();
        }

        #endregion

        #region Other Methods and Properties

        // Bind dell'endpoint locale
        private void BindingLocalEP()
        {
            if (this.sock != null)
            {
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
                    return AddrInformation;
                }
            }

            return AddrInformation;

        }

        // Ottiene configurazione Locale dell'Host
        private void GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localEP = new IPEndPoint(ip, localPort);
                    break;
                }
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
            this.sock.Disconnect(true);
        }

        public void SockClose()
        {
            this.sock.Shutdown(SocketShutdown.Both);
            this.sock.Close();
        }

        public Socket Sock
        {
            get { return this.sock; }
            set { this.sock = value; }
        }

        public bool IsDisconnect 
        {
            get { return this.isDisconect; }
            set { this.isDisconect = value; }
        }

        // La funzione controlla la correttezza di un indirizzo IP e restituisce true se è corretto.
        public static bool verificaIPAddres(string addr)
        {
            string[] nums = addr.Split('.');

            if (nums.Length != 4)
                return false;

            int aux, res;

            for (int i = 0; i < nums.Length; i++) {

                if (!Int32.TryParse(nums[i], out res))
                    return false;

                aux = Convert.ToInt32(nums[i]);

                if (aux < 0 || aux > 255)
                    return false;
            }

            return true;
        }

        // Test sulla connessione.
        bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);

            if (part1 && part2)
                return false;
            else
                return true;

        }

        #endregion

    }

}
