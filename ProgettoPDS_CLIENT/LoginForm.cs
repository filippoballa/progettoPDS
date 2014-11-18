using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgettoPDS_CLIENT
{   
    public partial class LoginForm : Form
    {
        private User user;

        public LoginForm( User aux )
        {
            InitializeComponent();
            this.user = aux;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if ( VerificaCredenziali() == true) 
                this.Close();
            else
                MessageBox.Show("Login Fallito!!", "ERRORE", MessageBoxButtons.OK,MessageBoxIcon.Error);         
        }

        private bool VerificaCredenziali()
        {
            if (this.UsernameTextBox.Text == "filippo" && this.PasswordTextBox.Text == "balla")
            {
                user.Name = this.UsernameTextBox.Text;
                user.Password = this.PasswordTextBox.Text;
                user.Login = true;
                return true;
            }
            else
                return false;
        }
    }
}
