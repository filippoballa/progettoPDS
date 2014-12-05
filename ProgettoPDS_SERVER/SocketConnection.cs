﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Encoding;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ProgettoPDS_SERVER
{
    class SocketConnection
    {
        private const int backlog = 1;
        private Socket sock, passiv;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private int porta;
        private String myIP;

        public SocketConnection(int porta)
        {

            // Create a TCP/IP socket.
            this.porta = porta;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            sock.Bind(new IPEndPoint(IPAddress.Any, porta));
        }
        public void StartListening()
        {
            
            try
            {
                sock.Listen(backlog);

                    // Set the event to nonsignaled state.
                    //allDone.Reset();
                    // Program is suspended while waiting for an incoming connection.
                    sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);
                    // Wait until a connection is made before continuing.
                    //allDone.WaitOne();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            //allDone.Set();

            passiv = (Socket)ar.AsyncState;
            passiv = passiv.EndAccept(ar);

            MessageBox.Show("Connessione avvenuta inizio autenticazione client("+passiv.RemoteEndPoint.ToString()+")");
            if(ClientAutentication())
            {}
            else
            {}
        }

        private bool ClientAutentication()
        {
            string comando, user, pwd;
            byte[] data;

            //richiesta user e password;

            comando = "+AUTH_USER";
            data = Encoding.ASCII.GetBytes(comando);
            passiv.Send(data);//invio richiesta di autenticazione

            passiv.Receive(data);
            user = Encoding.ASCII.GetString(data);

            // controllo che l'user sia già registrato
            if (ControlloUser(user))
            {
                comando = "+AUTH_PWD";
                data = Encoding.ASCII.GetBytes(comando);
                passiv.Send(data);//se l'user è registrato gli chiedo la pswrd

                passiv.Receive(data);
                pwd = Encoding.ASCII.GetString(data);

                if (ControlloPassword(user))
                {
                    //...
                }
                else//la password non è corretta
                {
                    //...
                }
            }
            else if (user == "+REGISTRATION")//se il client ha richiesto di registrarsi
            {
                comando = "";
                data = Encoding.ASCII.GetBytes(comando);
                //..
            }
            else//se il client non vuole registrarsi si chiude la connessione
            { 
                CloseSocket(); 
            }
            return true;
        }

        private void CloseSocket()
        {
            throw new NotImplementedException();
        }

        private bool ControlloPassword(string user)
        {
            return true;
        }

        private bool ControlloUser(string user)
        {
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

    }

}

