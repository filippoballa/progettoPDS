using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_CLIENT
{
    public class User
    {
        private string name;
        private string password;
        private bool IsLog;

        public User()
        {
            this.name = "";
            this.password = "";
            this.IsLog = false;
        }

        public User(string name, string pwd) 
        {
            this.name = name;
            this.password = pwd;
            this.IsLog = false;
        
        }

        public string Name {   
            get { return this.name;}
            set { this.name = value; }
        }

        public string Password {
            get { return this.password; }
            set { this.password = value; }
        }

        public bool Login {
            get { return this.IsLog; }
            set { this.IsLog = value; }
        }

       
    }
}
