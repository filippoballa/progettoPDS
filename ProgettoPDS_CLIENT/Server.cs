using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_CLIENT
{
    class Server
    {
        private string host;
        private string indirizzoIP;
        private int porta;

        public Server()
        {
            this.host = "";
            this.indirizzoIP = "";
            this.porta = -1;
            
        }

        public Server(string host, string ip, int port) 
        {
            this.host = host;
            this.indirizzoIP = ip;
            this.porta = port;
        
        }

        public string HostName {   
            get { return this.host;}
            set { this.host = value; }
        }

        public string IPAddr {
            get { return this.indirizzoIP; }
            set { this.indirizzoIP = value; }
        }

        public int Porta
        {
            get { return this.porta; }
            set { this.porta = value; }
        }

        public override string ToString()
        {
            return this.host + " -  IP: " + this.indirizzoIP + " , PORTA: " + this.porta.ToString();
        }
    }  
}
