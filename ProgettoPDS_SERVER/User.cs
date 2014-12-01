using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPDS_SERVER
{
    public class User
    {
        private string name;
        private string user;
        private string surname;
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

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string Username
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }

        public bool Login
        {
            get { return this.IsLog; }
            set { this.IsLog = value; }
        }


    }
}