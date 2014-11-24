using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encoding;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ProgettoPDS_SERVER
{
    class SocketConnection
    {
        private const int backlog = 1;
        private Socket sock, passiv;
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public SocketConnection(int porta)
        {

            // Create a TCP/IP socket.
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            sock.Bind(new IPEndPoint(IPAddress.Any, porta));
        }
        public void StartListening()
        {
            try
            {
                sock.Listen(backlog);
                // Start listening for connections.
                /* Main Server Loop */
                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();
                    // Program is suspended while waiting for an incoming connection.
                    sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);
                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            ClientAutentication();
        }

        private bool ClientAutentication()
        {
            string comando, user, pwd;
            bool presente;
            byte[] data;

            //richiesta user e password;

            comando = "+AUTH_USER";
            data = Encoding.ASCII.GetBytes(comando);
            passiv.Send(data);

            passiv.Receive(data);
            user = Encoding.ASCII.GetString(data);

            presente = ControlloUser(user);// controllo che l'user sia già registrato
            if (presente)
            {
                comando = "+AUTH_PWD";
                data = Encoding.ASCII.GetBytes(comando);
                passiv.Send(data);

                passiv.Receive(data);
                pwd = Encoding.ASCII.GetString(data);
            }
            else if (user == "+REGISTRATION")
            {
                comando = "";
                data = Encoding.ASCII.GetBytes(comando);
            }
            //....
            return true;
        }

        private bool ControlloUser(string user)
        {
            return true;
        }

    }

}

