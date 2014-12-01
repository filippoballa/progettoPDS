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

namespace ProgettoPDS_CLIENT
{   
    public partial class LoginForm : Form
    {
        private User user;

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

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void registraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.NameRegTextBox.Focus();            
            this.RegistraPanel.Visible = true;
        }

        private void loginPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RegistraPanel.Visible = false;
            this.UsernameTextBox.Focus();
            this.MainPanel.Visible = true;

        }

        
    }
}
