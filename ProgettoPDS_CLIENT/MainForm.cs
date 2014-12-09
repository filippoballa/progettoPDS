﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ProgettoPDS_CLIENT
{
    public partial class MainForm : Form
    {
        private User user;
        private List<SocketConnection> connessioni;
        private int AltezzaForm, BaseForm, currServ;

        public MainForm( User aux)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.connessioni = new List<SocketConnection>();
            this.currServ = -1;
            this.user = aux;
            this.Text += " - USER: \"" + aux.Username + "\" - HOST : " + Dns.GetHostName() + " - IP Address : " + 
                SocketConnection.MyIpInfo() + " - Port Number: " + SocketConnection.localPort.ToString();
            this.AltezzaForm = this.Height;
            this.BaseForm = this.Width;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;

            // Resize "Elenco Label"
            float aux = (this.ElencoLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.ElencoLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            int X = (this.ElencoLabel.Location.X * this.Width) / this.BaseForm;
            int Y = (this.ElencoLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.ElencoLabel.Location = new Point(X, Y);
            this.ElencoLabel.Height = (this.ElencoLabel.Height * this.Height) / this.AltezzaForm;
            this.ElencoLabel.Width = (this.ElencoLabel.Width * this.Width) / this.BaseForm;  

            // Resize "GroupBox Conf."
            aux = (this.groupBox1.Font.Size * this.Height) / this.AltezzaForm;
            this.groupBox1.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Height = ( this.groupBox1.Height * this.Height ) / this.AltezzaForm;
            this.groupBox1.Width = ( this.groupBox1.Width * this.Width ) / this.BaseForm;

            // Resize "GroupBox STATO"
            aux = (this.groupBox2.Font.Size * this.Height) / this.AltezzaForm;
            this.groupBox2.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Height = (this.groupBox2.Height * this.Height) / this.AltezzaForm;
            this.groupBox2.Width = (this.groupBox2.Width * this.Width) / this.BaseForm;

            // Resize ListBox
            this.listBox1.Height = (this.listBox1.Height * this.Height) / this.AltezzaForm;
            this.listBox1.Width = (this.listBox1.Width * this.Width) / this.BaseForm;                      
            X = (this.listBox1.Location.X * this.Width) / this.BaseForm;
            Y = (this.listBox1.Location.Y * this.Height) / this.AltezzaForm;
            this.listBox1.Location = new Point(X, Y);

            // Riposizionamento Bottone Connetti
            aux = (this.ConnectButton.Font.Size * this.Height) / this.AltezzaForm;
            this.ConnectButton.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.Height = (this.ConnectButton.Height * this.Height) / this.AltezzaForm;
            this.ConnectButton.Width = (this.ConnectButton.Width * this.Width) / this.BaseForm;
            X = (this.ConnectButton.Location.X * this.Width ) / this.BaseForm;
            Y = (this.ConnectButton.Location.Y * this.Height) / this.AltezzaForm;
            this.ConnectButton.Location = new Point(X, Y);

            // Riposizionamento Bottone Disconnetti
            aux = (this.DisconnectButton.Font.Size * this.Height) / this.AltezzaForm;
            this.DisconnectButton.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectButton.Height = (this.DisconnectButton.Height * this.Height) / this.AltezzaForm;
            this.DisconnectButton.Width = (this.DisconnectButton.Width * this.Width) / this.BaseForm;
            X = (this.DisconnectButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.DisconnectButton.Location.Y * this.Height) / this.AltezzaForm;
            this.DisconnectButton.Location = new Point(X, Y);

            // Riposizionamento Bottone Information
            this.InfoButton.Height = (this.InfoButton.Height * this.Height) / this.AltezzaForm;
            this.InfoButton.Width = (this.InfoButton.Width * this.Width) / this.BaseForm;
            X = (this.InfoButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.InfoButton.Location.Y * this.Height) / this.AltezzaForm;
            this.InfoButton.Location = new Point(X, Y);

            // Resize elementi dentro il groupbox di configurazione
            // Resise ConfigLabel
            aux = (this.ConfigLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.ConfigLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.ConfigLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.ConfigLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.ConfigLabel.Location = new Point(X, Y);
            // Resize Ip Addr label
            aux = (this.IPAddressLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.IPAddressLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.IPAddressLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.IPAddressLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.IPAddressLabel.Location = new Point(X, Y);
            // Resize IP TextBox
            aux = (this.IPAddressTextBox.Font.Size * this.Height) / this.AltezzaForm;
            this.IPAddressTextBox.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.IPAddressTextBox.Location.X * this.Width) / this.BaseForm;
            Y = (this.IPAddressTextBox.Location.Y * this.Height) / this.AltezzaForm;
            this.IPAddressTextBox.Location = new Point(X, Y);
            this.IPAddressTextBox.Width = (this.IPAddressTextBox.Width * this.Width) / this.BaseForm;
            // Resize Porta Label
            aux = (this.PortaLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.PortaLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.PortaLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.PortaLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.PortaLabel.Location = new Point(X, Y);
            // Resize Porta TextBox
            aux = (this.PortaTextBox.Font.Size * this.Height) / this.AltezzaForm;
            this.PortaTextBox.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.PortaTextBox.Location.X * this.Width) / this.BaseForm;
            Y = (this.PortaTextBox.Location.Y * this.Height) / this.AltezzaForm;
            this.PortaTextBox.Location = new Point(X, Y);
            this.PortaTextBox.Width = (this.PortaTextBox.Width * this.Width) / this.BaseForm;

            this.AddButton.Height = (this.AddButton.Height * this.Height) / this.AltezzaForm;
            this.AddButton.Width = (this.AddButton.Width * this.Width) / this.BaseForm;
            X = (this.AddButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.AddButton.Location.Y * this.Height) / this.AltezzaForm;
            this.AddButton.Location = new Point(X, Y);

            aux = (this.RemoveButton.Font.Size * this.Height) / this.AltezzaForm;
            this.RemoveButton.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveButton.Height = ( this.RemoveButton.Height * this.Height) / this.AltezzaForm;
            this.RemoveButton.Width = (this.RemoveButton.Width * this.Width) / this.BaseForm;
            X = (this.RemoveButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.RemoveButton.Location.Y * this.Height) / this.AltezzaForm;
            this.RemoveButton.Location = new Point(X, Y);

            // Resize Ip Addr label
            aux = (this.HostNameLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.HostNameLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.HostNameLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.HostNameLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.HostNameLabel.Location = new Point(X, Y);
            // Resize IP TextBox
            aux = (this.HostNameTextBox.Font.Size * this.Height) / this.AltezzaForm;
            this.HostNameTextBox.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.HostNameTextBox.Location.X * this.Width) / this.BaseForm;
            Y = (this.HostNameTextBox.Location.Y * this.Height) / this.AltezzaForm;
            this.HostNameTextBox.Location = new Point(X, Y);
            this.HostNameTextBox.Width = (this.HostNameTextBox.Width * this.Width) / this.BaseForm;

            this.IPAddressTextBox.Focus();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            int aux;

            if ( this.IPAddressTextBox.Text == "" && this.PortaTextBox.Text == "" && this.HostNameTextBox.Text == "") {
                MessageBox.Show("Specifica la configurazione del server prima de cliccare su tasto \"Add\"!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.IPAddressTextBox.Focus();
                return;
            }
            else if (this.HostNameTextBox.Text == "") {
                MessageBox.Show("Specifica il nome del dispositivo Server!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.HostNameTextBox.Focus();
                return;
            }
            else if (this.IPAddressTextBox.Text == "") {
                MessageBox.Show("Specifica un indirizzo IP per il Server!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.IPAddressTextBox.Focus();
                return;
            }
            else if (this.PortaTextBox.Text == "") {
                MessageBox.Show("Specifica un numero di Porta valido per il Server!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.IPAddressTextBox.Focus();
                return;
            }

            // Controllo Numero di porta
            if (!Int32.TryParse(this.PortaTextBox.Text, out aux) || Int32.Parse(this.PortaTextBox.Text) < 1024 || Int32.Parse(this.PortaTextBox.Text) > 65535) {
                MessageBox.Show("Il numero di Porta specificato non è corretto!!\nDeve trattarsi di un numero intero compreso tra 1024 e 65535!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PortaTextBox.Clear();
                this.PortaTextBox.Focus();
                return;
            }

            // Controllo validità indirizzo IP
            if (!verificaIPAddres(this.IPAddressTextBox.Text)) {
                MessageBox.Show("L'indirizzo IP inserito non è corretto!!\n" +
                    "Deve presentarsi nel seguente formato: xxx.xxx.xxx.xxx ,\ndove \"xxx\" è un numero compreso tra 0 e 255!!", 
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.IPAddressTextBox.Clear();
                this.IPAddressTextBox.Focus();
                return;
            }

            this.listBox1.Items.Add( this.HostNameTextBox.Text + " -  IP: " + this.IPAddressTextBox.Text + " , PORTA: " + this.PortaTextBox.Text );
            this.connessioni.Add(new SocketConnection(this.IPAddressTextBox.Text, Convert.ToInt32(this.PortaTextBox.Text)));
            this.PortaTextBox.Clear();
            this.IPAddressTextBox.Clear();
            this.HostNameTextBox.Clear();
            this.HostNameTextBox.Focus();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone Connetti!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected())
                this.connessioni[this.currServ].StartClientConnection();
            else
                MessageBox.Show("La connessione con il server è già attiva!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone Rimuovi!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
                this.connessioni.RemoveAt(this.currServ);
            }
            else
                MessageBox.Show("Disconnetti il Server prima di rimuoverlo dalla Lista!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
        }

        // La funzione controlla la correttezza di un indirizzo IP e restituisce true se è corretto.
        private bool verificaIPAddres(string addr) 
        {
            string[] nums = addr.Split('.');

            if (nums.Length != 4)
                return false;

            int aux, res;

            for (int i = 0; i < nums.Length; i++) {

                if (!Int32.TryParse(this.PortaTextBox.Text, out res))
                    return false;

                aux = Convert.ToInt32(nums[i]);

                if (aux < 0 || aux > 255)
                    return false;
            }

            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(this.listBox1.Location.X, this.listBox1.Location.Y, this.listBox1.Width, this.listBox1.Height);
            Pen p = new Pen(Color.White, 6);
            g.DrawRectangle(p, r);
        }


    }
}
