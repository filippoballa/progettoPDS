using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

namespace ProgettoPDS_CLIENT
{   
    public partial class LoginForm : Form
    {
        private User user;
        private XmlManager mng;

        public LoginForm( User aux )
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.user = aux;
            this.mng = new XmlManager("XMLUsers.xml");
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (this.UsernameTextBox.Text == "" || this.PasswordTextBox.Text == "") {
                this.UsernameTextBox.Focus();
                MessageBox.Show("Inserisci le tue credenziali complete prima di accedere!!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (VerificaCredenziali() == true)
                this.Close();
            else {
                MessageBox.Show("Login Fallito!!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.UsernameTextBox.Clear();
                this.PasswordTextBox.Clear();
                this.UsernameTextBox.Focus();
            }
        }

        private bool VerificaCredenziali()
        {
            User aux = this.mng.SearchUser(this.UsernameTextBox.Text);
            SHA1 shaM = new SHA1Managed();
            string res = Encoding.ASCII.GetString(shaM.ComputeHash(Encoding.ASCII.GetBytes(this.PasswordTextBox.Text)));

            if ( aux != null && (aux.Password == res ) ) {
                this.user.Name = aux.Name;
                this.user.Username = aux.Username;
                this.user.Password = aux.Password;
                this.user.Surname = aux.Surname;
                this.user.Login = true;
                return true;
            }
            else 
                return false;
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;          
            this.RegistraPanel.Visible = true;
            this.NameRegTextBox.Focus();  
        }

        private void loginPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RegistraPanel.Visible = false;
            this.MainPanel.Visible = true;
            this.UsernameTextBox.Focus();

        }

        private void RegistraButton_Click(object sender, EventArgs e)
        {

            if (this.UserRegTextBox.Text == "" && this.PwdRegTextBox.Text == "" && this.NameRegTextBox.Text == "" && this.CognomeRegTextBox.Text == "") {
                this.NameRegTextBox.Focus();
                MessageBox.Show("Completa tutti i campi del form prima di cliccare sul bottone \"Registra\"", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this.NameRegTextBox.Text == "") {
                this.NameRegTextBox.Focus();
                MessageBox.Show("Inserire un Nome nell'apposito campo del form!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this.CognomeRegTextBox.Text == "") {
                this.CognomeRegTextBox.Focus();
                MessageBox.Show("Inserire un Cognome nell'apposito campo del form!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this.UserRegTextBox.Text == "") {
                this.UserRegTextBox.Focus();
                MessageBox.Show("Inserire uno Username valido nell'apposito campo del form!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this.PwdRegTextBox.Text == "") {
                this.PwdRegTextBox.Focus();
                MessageBox.Show("Inserire una Password nell'apposito campo del form!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
               
                User aux = mng.SearchUser(this.UserRegTextBox.Text);

                if (aux == null) {
                    this.user.Name = this.NameRegTextBox.Text;
                    this.user.Surname = this.CognomeRegTextBox.Text;
                    this.user.Username = this.UserRegTextBox.Text;
                    this.user.Password = this.PwdRegTextBox.Text;
                    this.user.Login = true;

                    mng.AddNewUser(this.user);

                    this.Close();
                }
                else
                    MessageBox.Show("L'utente " + aux.Username + " è già registrato nell'applicazione!!", "ANOMALIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }

        }

        private void indirizzoIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("INDIRIZZO IP: " + SocketConnection.MyIpInfo(), "INFORMAZIONI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
