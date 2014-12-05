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
    public partial class MainForm : Form
    {
        private User user;
        private SocketConnection connessione;
        private int AltezzaForm, BaseForm;

        public MainForm( User aux)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.user = aux;
            this.Text += " - " + aux.Username;
            this.AltezzaForm = this.Height;
            this.BaseForm = this.Width;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this.connessione = new SocketConnection(this.IPAddressTextBox.Text, Convert.ToInt32(this.PortaTextBox.Text));
            this.connessione.StartClientConnection();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Resize "Elenco Label"
            float aux = (this.ElencoLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.ElencoLabel.Font = new System.Drawing.Font("Comic Sans MS", aux - 1, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            int X = (this.ElencoLabel.Location.X * this.Width) / this.BaseForm;
            int Y = (this.ElencoLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.ElencoLabel.Location = new Point(X, Y);
            this.ElencoLabel.Height = (this.ElencoLabel.Height * this.Height) / this.AltezzaForm;
            this.ElencoLabel.Width = (this.ElencoLabel.Width * this.Width) / this.BaseForm;  

            // Resize "GroupBox Conf."
            this.groupBox1.Height = ( this.groupBox1.Height * this.Height ) / this.AltezzaForm;
            this.groupBox1.Width = ( this.groupBox1.Width * this.Width ) / this.BaseForm;

            // Resize "GroupBox STATO"
            this.groupBox2.Height = (this.groupBox2.Height * this.Height) / this.AltezzaForm;
            this.groupBox2.Width = (this.groupBox2.Width * this.Width) / this.BaseForm;

            // Resize ListBox
            this.listBox1.Height = (this.listBox1.Height * this.Height) / this.AltezzaForm;
            this.listBox1.Width = (this.listBox1.Width * this.Width) / this.BaseForm;                      
            X = (this.listBox1.Location.X * this.Width) / this.BaseForm;
            Y = (this.listBox1.Location.Y * this.Height) / this.AltezzaForm;
            this.listBox1.Location = new Point(X, Y);

            // Riposizionamento Bottone Connetti
            this.ConnectButton.Height = (this.ConnectButton.Height * this.Height) / this.AltezzaForm;
            this.ConnectButton.Width = (this.ConnectButton.Width * this.Width) / this.BaseForm;
            X = (this.ConnectButton.Location.X * this.Width ) / this.BaseForm;
            Y = (this.ConnectButton.Location.Y * this.Height) / this.AltezzaForm;
            this.ConnectButton.Location = new Point(X, Y);

            // Riposizionamento Bottone Disconnetti
            this.DisconnectButton.Height = (this.DisconnectButton.Height * this.Height) / this.AltezzaForm;
            this.DisconnectButton.Width = (this.DisconnectButton.Width * this.Width) / this.BaseForm;
            X = (this.DisconnectButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.DisconnectButton.Location.Y * this.Height) / this.AltezzaForm;
            this.DisconnectButton.Location = new Point(X, Y);

            // Riposizionamento Bottone Information
            this.InfoButton.Height = (this.InfoButton.Height * this.Height) / this.AltezzaForm;
            this.InfoButton.Height += 5;
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

            this.AddButton.Height = (this.AddButton.Height * this.Height) / this.AltezzaForm;
            this.AddButton.Width = (this.AddButton.Width * this.Width) / this.BaseForm;
            X = (this.AddButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.AddButton.Location.Y * this.Height) / this.AltezzaForm;
            this.AddButton.Location = new Point(X, Y);

            this.IPAddressTextBox.Focus();
        }


    }
}
