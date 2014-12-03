using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_CLIENT
{
    public class User
    {
        private string username;
        private string password;
        private string name;
        private string surname;
        private bool IsLog;

        public User()
        {
            this.name = "";
            this.password = "";
            this.surname = "";
            this.username = "";
            this.IsLog = false;
        }

        public User(string user, string pwd, string name, string sur) 
        {
            this.username = user;
            this.password = pwd;
            this.name = name;
            this.surname = sur;
            this.IsLog = false;
        
        }

        public string Username {   
            get { return this.username;}
            set { this.username = value; }
        }

        public string Password {
            get { return this.password; }
            set { this.password = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }

        public bool Login {
            get { return this.IsLog; }
            set { this.IsLog = value; }
        }

       
    }
}
