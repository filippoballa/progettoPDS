﻿using System;
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
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
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
            string[] data = null;

            //prova inserimento
            //Users.AddNewUser(this.UsernameTextBox.Text, this.PasswordTextBox.Text);//

            data = Users.SearchUser(this.UsernameTextBox.Text);//ritorna null se non trova lo user

            if(data!= null && data[1] == this.PasswordTextBox.Text)//se non ha ritornato null e la pswrd è uguale
            {
                user.Username = data[0];
                user.Password = data[1];
                user.Name = data[2];
                user.Surname = data[3];
                user.Login = true;
                return true;
            }
            else
                return false;
        }

        private void loginPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RegistraPanel.Visible = false;
            this.UsernameTextBox.Focus();
            this.MainPanel.Visible = true;
        }

        private void registraUtenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.NameRegTextBox.Focus();
            this.RegistraPanel.Visible = true;
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
    
}
