using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ProgettoPDS_SERVER
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
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            this.user = aux;
            this.mng = new XmlManager('U');

            if (this.mng.Error) {
                this.menuStrip1.Enabled = false;
                this.LoginButton.Enabled = false;
                this.UsernameTextBox.Enabled = false;
                this.PasswordTextBox.Enabled = false;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (this.UsernameTextBox.Text == "" && this.PasswordTextBox.Text == "") {
                MessageBox.Show("Inserisci le tue credenziali complete prima di accedere!!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.UsernameTextBox.Focus();
                return;
            }
            else if (this.UsernameTextBox.Text == "" ) {
                MessageBox.Show("Inserisci il tuo username per accedere all'applicazione!!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.UsernameTextBox.Focus();
                return;
            }
            else if (this.PasswordTextBox.Text == "") {
                MessageBox.Show("Inserisci la tua password per accedere all'applicazione!!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PasswordTextBox.Focus();
                return;
            }
            else {

                if (VerificaCredenziali() == true)
                    this.Close();
                else
                {
                    MessageBox.Show("Login Fallito!!", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.UsernameTextBox.Clear();
                    this.PasswordTextBox.Clear();
                    this.UsernameTextBox.Focus();
                }
            }     
        }

        private bool VerificaCredenziali()
        {
            
            User aux = this.mng.SearchUser(this.UsernameTextBox.Text);
            SHA1 shaM = new SHA1Managed();
            string res = Encoding.ASCII.GetString(shaM.ComputeHash(Encoding.ASCII.GetBytes(this.PasswordTextBox.Text)));

            if( aux != null && (aux.Password == res ) ) {
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

        private void loginPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.InfoPanel.Visible = false;
            this.RemovePanel.Visible = false;
            this.RegistraPanel.Visible = false;
            this.MainPanel.Visible = true;
            this.ResumeLayout();
            this.UsernameTextBox.Focus();
            this.NotaLabel.Visible = true;
        }

        private void registraUtenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.InfoPanel.Visible = false;
            this.MainPanel.Visible = false;
            this.RemovePanel.Visible = false;
            this.RegistraPanel.Visible = true;
            this.ResumeLayout();
            this.NameRegTextBox.Focus();
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
            else if (this.PwdRegTextBox.Text == "")
            {
                this.PwdRegTextBox.Focus();
                MessageBox.Show("Inserire una Password nell'apposito campo del form!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                User aux = mng.SearchUser(this.UserRegTextBox.Text);

                if (aux == null)
                {
                    this.user.Name = this.NameRegTextBox.Text;
                    this.user.Surname = this.CognomeRegTextBox.Text;
                    this.user.Username = this.UserRegTextBox.Text;
                    this.user.Password = this.PwdRegTextBox.Text;
                    this.user.Login = true;

                    mng.AddNewUser(this.user);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("L'utente " + aux.Username + " è già registrato nell'applicazione!!", "ANOMALIA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.NameRegTextBox.Clear();
                    this.CognomeRegTextBox.Clear();
                    this.PwdRegTextBox.Clear();
                    this.UserRegTextBox.Clear();
                    this.NameRegTextBox.Focus();
                }

            }
            
        }

        private void indirizzoIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    MessageBox.Show("INDIRIZZO IP: " + ip.ToString(),
                        "INFORMAZIONI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            MessageBox.Show("Dispositivo non collegato ad Internet!!", "INFORMAZIONI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void informazioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.InfoPanel.Visible = true;
            this.MainPanel.Visible = false;
            this.RegistraPanel.Visible = false;
            //this.ChangePasswordPanel.Visible = false;
            this.ResumeLayout();
        }

        private void cambiaPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            //this.ChangePasswordPanel.Visible = true;
            this.MainPanel.Visible = false;
            this.RegistraPanel.Visible = false;
            this.InfoPanel.Visible = false;
            this.ResumeLayout();
            //this.ChangeUserTextBox.Focus();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (this.RemoveUserTextBox.Text == "" && this.RemovePwdTextBox.Text == "")
            {
                MessageBox.Show("Completa tutti i campi del form prima di cliccare\nsul bottone \"Remove!!\"",
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.RemoveUserTextBox.Focus();
            }
            else if (this.RemoveUserTextBox.Text == "")
            {
                MessageBox.Show("Specificare lo username dell'account che desideri\neliminare prima di procedere oltre...",
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.RemoveUserTextBox.Focus();
            }
            else if (this.RemovePwdTextBox.Text == "")
            {
                MessageBox.Show("Specificare la password dell'account che desideri\neliminare prima di procedere oltre...",
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.ChangeOldPwdTextBox.Focus();
            }
            else
            {

                User u = this.mng.SearchUser(this.RemoveUserTextBox.Text);
                SHA1 shaM = new SHA1Managed();

                if (u == null)
                {
                    MessageBox.Show("Lo user \"" + this.RemoveUserTextBox.Text + "\" non è mai stato registrato nell'applicazione!!",
                        "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.RemoveUserTextBox.Clear();
                    this.RemovePwdTextBox.Clear();
                    this.RemoveUserTextBox.Focus();
                }
                else
                {

                    byte[] pwd = Encoding.ASCII.GetBytes(this.RemovePwdTextBox.Text);

                    if (u.Password == Encoding.ASCII.GetString(shaM.ComputeHash(pwd)))
                    {
                        DialogResult res = MessageBox.Show("Desideri procedere alla rimozione di questo account??", "AVVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (res == DialogResult.Yes)
                        {
                            this.mng.RemoveUsers(u);
                            MessageBox.Show("Account rimosso correttamente!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                        MessageBox.Show("La password digitata non è quella associata all'account specificato!!",
                            "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.RemoveUserTextBox.Clear();
                    this.RemovePwdTextBox.Clear();
                    this.RemoveUserTextBox.Focus();

                }

            }
        }

        private void eliminaUtenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.RemovePanel.Visible = true;
            Thread.Sleep(5);
            this.NotaLabel.Visible = false;
            this.MainPanel.Visible = false;
            this.RegistraPanel.Visible = false;
            this.InfoPanel.Visible = false;
            this.ResumeLayout();
            this.RemoveUserTextBox.Focus();
        }

       
    }
    
}
