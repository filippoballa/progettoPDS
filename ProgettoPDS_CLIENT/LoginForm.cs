﻿using System;
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
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            this.user = aux;
            this.mng = new XmlManager("XMLUsers.xml");
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
            this.SuspendLayout();           
            this.label3.Visible = false;
            this.label4.Visible = false;
            this.ChangeButton.Visible = false;
            this.TitleInfoLabel.Visible = false;
            this.UsernameLabel.Visible = false;
            this.PasswordLabel.Visible = false;
            this.UsernameTextBox.Visible = false;
            this.PasswordTextBox.Visible = false;
            this.NotaLabel.Visible = false;
            this.InfoPanel.Visible = false;
            this.MainPanel.Visible = false;
            this.ChangePasswordPanel.Visible = false;
            Thread.Sleep(10);
            this.groupBox1.Visible = true;
            this.RegistraButton.Visible = true;
            this.RegistraPanel.Visible = true;
            this.ResumeLayout();
            this.NameRegTextBox.Focus();
        }

        private void loginPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();           
            this.groupBox1.Visible = false;
            this.RegistraButton.Visible = false;
            this.label3.Visible = false;
            this.label4.Visible = false;
            this.TitleInfoLabel.Visible = false;
            this.ChangeButton.Visible = false;
            this.InfoPanel.Visible = false;
            this.ChangePasswordPanel.Visible = false;
            this.RegistraPanel.Visible = false;
            Thread.Sleep(10);
            this.UsernameLabel.Visible = true;
            this.PasswordLabel.Visible = true;
            this.NotaLabel.Visible = true;
            this.UsernameTextBox.Visible = true;
            this.PasswordTextBox.Visible = true;
            this.MainPanel.Visible = true;
            this.ResumeLayout();
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
                else {
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
            string ip = SocketConnection.MyIpInfo();

            if( ip != null )
                MessageBox.Show("INDIRIZZO IP: " + ip, "INFORMAZIONI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Dispositivo non collegato ad Internet!!", "INFORMAZIONI", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.UsernameLabel.Visible = false;
            this.PasswordLabel.Visible = false;
            this.UsernameTextBox.Visible = false;
            this.PasswordTextBox.Visible = false;
            this.groupBox1.Visible = false;
            this.RegistraButton.Visible = false;
            this.ChangeButton.Visible = false;
            this.label3.Visible = true;
            this.label4.Visible = true;
            this.TitleInfoLabel.Visible = true;
            Thread.Sleep(10);
            this.InfoPanel.Visible = true;
            this.NotaLabel.Visible = false;
            this.MainPanel.Visible = false;
            this.RegistraPanel.Visible = false;
            this.ChangePasswordPanel.Visible = false;
            this.ResumeLayout();

        }

        private void cambiaPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.label3.Visible = false;
            this.label4.Visible = false;
            this.TitleInfoLabel.Visible = false;
            this.UsernameLabel.Visible = false;
            this.PasswordLabel.Visible = false;
            this.UsernameTextBox.Visible = false;
            this.PasswordTextBox.Visible = false;
            this.groupBox1.Visible = false;
            this.RegistraButton.Visible = false;
            this.ChangeButton.Visible = true;
            Thread.Sleep(10);
            this.ChangePasswordPanel.Visible = true;
            this.NotaLabel.Visible = false;
            this.MainPanel.Visible = false;
            this.RegistraPanel.Visible = false;
            this.InfoPanel.Visible = false;
            this.ResumeLayout();
            this.ChangeUserTextBox.Focus();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (this.ChangePwdTextBox.Text == "" && this.ChangeUserTextBox.Text == "") {
                MessageBox.Show("Completa tutti i campi del form prima di cliccare\nsul bottone \"CHANGE!!\"", 
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ChangeUserTextBox.Focus();
            }
            else if (this.ChangeUserTextBox.Text == "") {
                MessageBox.Show("Username assente!! Specificalo prima di procedere oltre...",
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ChangeUserTextBox.Focus();
            }
            else if (this.ChangePwdTextBox.Text == "")
            {
                MessageBox.Show("Nuova Password Assente!! Specificala prima di procedere oltre...",
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ChangePwdTextBox.Focus();
            }
            else {

                if ( this.mng.SearchUser(this.ChangeUserTextBox.Text) == null) {
                    MessageBox.Show("Lo user \"" + this.ChangeUserTextBox.Text + "\" non è mai stato registrato nell'applicazione!!",
                    "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ChangeUserTextBox.Clear();
                    this.ChangePwdTextBox.Clear();
                    this.ChangeUserTextBox.Focus();
                }
                else {
                    DialogResult res = MessageBox.Show("Desideri procedere con la modifica della password??", "AVVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                        this.mng.ModifyPwdUser(this.ChangeUserTextBox.Text, this.ChangePwdTextBox.Text);
                    
                    this.ChangeUserTextBox.Clear();
                    this.ChangePwdTextBox.Clear();
                    this.ChangeUserTextBox.Focus();

                }
            }
            
        }
        
    }
}
