using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ProgettoPDS_SERVER
{   
    public partial class LoginForm : Form
    {
        private User user;
        private XmlManager Users;
        

        public LoginForm( User aux )
        {
            InitializeComponent();
            this.user = aux;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if ( VerificaCredenziali()) 
                this.Close();
            else
                MessageBox.Show("Login Fallito!!", "ERRORE", MessageBoxButtons.OK,MessageBoxIcon.Error);         
        }

        private bool VerificaCredenziali()
        {
            Users = new XmlManager('U');
            string pwd = null;

            //prova inserimento
            //Users.AddNewUser(this.UsernameTextBox.Text, this.PasswordTextBox.Text);//

            pwd = Users.SearchUser(this.UsernameTextBox.Text);

            if(pwd!= null && pwd == this.PasswordTextBox.Text)
            {
                user.Name = this.UsernameTextBox.Text;
                user.Login = true;
                return true;
            }
            else
                return false;
        }

       
    }
    
}
