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
        private int localPort, remotePort;
        private static ManualResetEvent connectDone = new ManualResetEvent(false);

        // Costruttore Client
        public SocketConnection(string addr, int port ) 
        {            
            this.GetLocalIP();
            this.remoteEP = new IPEndPoint(IPAddress.Parse(addr), Convert.ToInt16(port) );
            this.remotePort = port;

            try {
                this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Ottiene configurazione Locale dell'Host
        private void GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry( Dns.GetHostName() );
            this.localPort = 5000;

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localEP = new IPEndPoint(ip, this.localPort);
                    break;
                }
            }

        }

        // Esegue una connessione al Server
        public void StartClientConnection() 
        {
            try {
                this.sock.BeginConnect(this.remoteEP, new AsyncCallback(this.ConnectCallback), sock);
                connectDone.WaitOne();


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
                connectDone.Set();
            }
            catch (Exception ecc) {
                MessageBox.Show(ecc.ToString(), "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectDone.Set();
            }
        }

    }
}
