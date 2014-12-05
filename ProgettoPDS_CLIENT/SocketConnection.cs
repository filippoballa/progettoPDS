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
        private const int localPort = 5000;
        private int remotePort;
        //private static ManualResetEvent connectDone = new ManualResetEvent(false);

        // Costruttore Client usato per Connessione con il server
        public SocketConnection(string addr, int port ) 
        {            
            this.GetLocalIP();
            this.remoteEP = new IPEndPoint(IPAddress.Parse(addr), Convert.ToInt32(port) );
            this.remotePort = port;

            try {
                this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.sock.Bind(this.localEP);
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

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
                //connectDone.WaitOne();
                

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
                MessageBox.Show("Connesso con" + this.sock.RemoteEndPoint.ToString() + "!!");
                //connectDone.Set();
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //connectDone.Set();
            }
        }

    }
}
