using System;
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
using System.Threading;

namespace ProgettoPDS_CLIENT
{
    public partial class MainForm : Form
    {
        private User user;
        private List<SocketConnection> connessioni;
        private List<Server> servers;
        private int AltezzaForm, BaseForm, currServ, CountServerConnected;
        private MouseCord mouse;
        private static Mutex mut = new Mutex();

        public MainForm( User aux)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            this.connessioni = new List<SocketConnection>();
            this.servers = new List<Server>();
            this.currServ = -1;
            this.user = aux;
            this.CountServerConnected = 0;
            this.mouse = new MouseCord(Cursor.Position.X, Cursor.Position.Y);
            this.Text += " - USER: \"" + aux.Username + "\" - HOST : " + Dns.GetHostName() + " - IP Address : " + 
                SocketConnection.MyIpInfo() + " - Port Number: " + SocketConnection.localPort.ToString();
            this.AltezzaForm = this.Height;
            this.BaseForm = this.Width;
            this.Ridimensiona();
            this.HostNameTextBox.Focus();
        }


        private void Ridimensiona() 
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
            this.groupBox1.Height = (this.groupBox1.Height * this.Height) / this.AltezzaForm;
            this.groupBox1.Width = (this.groupBox1.Width * this.Width) / this.BaseForm;

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
            X = (this.ConnectButton.Location.X * this.Width) / this.BaseForm;
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
            this.RemoveButton.Height = (this.RemoveButton.Height * this.Height) / this.AltezzaForm;
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

            aux = (this.StartButton.Font.Size * this.Height) / this.AltezzaForm;
            this.StartButton.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Height = (this.StartButton.Height * this.Height) / this.AltezzaForm;
            this.StartButton.Width = (this.StartButton.Width * this.Width) / this.BaseForm;
            X = (this.StartButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.StartButton.Location.Y * this.Height) / this.AltezzaForm;
            this.StartButton.Location = new Point(X, Y);

            // Resize ActionServer label
            aux = (this.ActionServerLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.ActionServerLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.ActionServerLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.ActionServerLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.ActionServerLabel.Location = new Point(X, Y);

            // Resize Escape label
            aux = (this.EscapeLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.EscapeLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.EscapeLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.EscapeLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.EscapeLabel.Location = new Point(X, Y);

            // Riposizionamento Back Button
            this.BackButton.Height = (this.BackButton.Height * this.Height) / this.AltezzaForm;
            this.BackButton.Width = (this.BackButton.Width * this.Width) / this.BaseForm;
            X = (this.BackButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.BackButton.Location.Y * this.Height) / this.AltezzaForm;
            this.BackButton.Location = new Point(X, Y);

            // Resize ActionServer label
            aux = (this.label1.Font.Size * this.Height) / this.AltezzaForm;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.label1.Location.X * this.Width) / this.BaseForm;
            Y = (this.label1.Location.Y * this.Height) / this.AltezzaForm;
            this.label1.Location = new Point(X, Y);
        }


        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(this.listBox1.Location.X, this.listBox1.Location.Y, this.listBox1.Width, this.listBox1.Height);
            Pen p = new Pen(Color.White, 6);
            g.DrawRectangle(p, r);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone Connetti!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {
                this.connessioni[this.currServ].StartClientConnection();

                if ( this.connessioni[this.currServ].IsConnected() && this.CountServerConnected == 0) {
                    this.label5.Text = "Connesioni Attive: " + servers[this.currServ].HostName;
                    this.CountServerConnected++;
                }
                else if (this.connessioni[this.currServ].IsConnected() && this.CountServerConnected >= 1) {
                    this.label5.Text += " , " + servers[this.currServ].HostName;
                    this.CountServerConnected++;
                }
            }
            else
                MessageBox.Show("La connessione con il server è già attiva!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone Disconetti!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {
                MessageBox.Show("Impossbile Disconettere perchè il Server è già disconesso!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.connessioni[this.currServ].SockDisconnect();
            this.CountServerConnected--;

            if (this.CountServerConnected == 0) 
                this.label5.Text = "Non Connesso con Nessun Server al momento!!";

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone \"Start&Use\"!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {
                MessageBox.Show("Il server non è connesso!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.ActionPanel.Visible = true;
            this.MainPanel.Visible = false;
            this.MouseBackgroundWorker.RunWorkerAsync();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone Rimuovi!!", 
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
                this.connessioni.RemoveAt(this.currServ);
                this.servers.RemoveAt(this.currServ);
            }
            else
                MessageBox.Show("Disconnetti il Server prima di rimuoverlo dalla Lista!!", 
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            int aux;

            if (this.IPAddressTextBox.Text == "" && this.PortaTextBox.Text == "" && this.HostNameTextBox.Text == "") {
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
         
            this.connessioni.Add(new SocketConnection(this.IPAddressTextBox.Text, Convert.ToInt32(this.PortaTextBox.Text), this.user));
            this.servers.Add(new Server(this.HostNameTextBox.Text, this.IPAddressTextBox.Text, Convert.ToInt32(this.PortaTextBox.Text)));   
            this.listBox1.Items.Add(this.servers.Last().ToString());
            this.PortaTextBox.Clear();
            this.IPAddressTextBox.Clear();
            this.HostNameTextBox.Clear();
            this.HostNameTextBox.Focus();

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.ActionPanel.Visible) {

                if (e.KeyCode == Keys.Escape) {
                    this.ActionPanel.Visible = false;
                    this.MainPanel.Visible = true;
                    this.HostNameTextBox.Focus();

                    // gestione BWs!!
                    this.MouseBackgroundWorker.CancelAsync();
                    this.KeyBackgroundWorker.CancelAsync();
                }
                else {
                    // Invio PDU legati alla tastiera

                    // Costruzione Pacchetto
                    string aux = "T-";

                    if (e.Control && e.KeyCode == Keys.C)
                        aux += Keys.Control.ToString() + "-" + Keys.C.ToString();
                    else if (e.Control && e.KeyCode == Keys.V)
                        aux += Keys.Control.ToString() + "-" + Keys.V.ToString();
                    else if (e.Control && e.KeyCode == Keys.S)
                        aux += Keys.Control.ToString() + "-" + Keys.S.ToString();
                    else if (e.Control && e.KeyCode == Keys.A)
                        aux += Keys.Control.ToString() + "-" + Keys.A.ToString();
                    else if (e.Control && e.KeyCode == Keys.X)
                        aux += Keys.Control.ToString() + "-" + Keys.X.ToString();
                    else if (!e.Alt && !e.Control && !e.Shift)
                        aux += e.KeyCode.ToString();

                    // Parte il KeyBW per invio pdu al server!!
                    this.KeyBackgroundWorker.RunWorkerAsync(aux);
                    
                }

            }
                
        }

        // La funzione controlla la correttezza di un indirizzo IP e restituisce true se è corretto.
        private bool verificaIPAddres(string addr)
        {
            string[] nums = addr.Split('.');

            if (nums.Length != 4)
                return false;

            int aux, res;

            for (int i = 0; i < nums.Length; i++) {

                if (!Int32.TryParse(nums[i], out res))
                    return false;

                aux = Convert.ToInt32(nums[i]);

                if (aux < 0 || aux > 255)
                    return false;
            }

            return true;
        }

        private void KeyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Invio PDU relativa alla tastiera
            string aux = e.Argument.ToString();
            byte[] pdu = Encoding.ASCII.GetBytes(aux);
            mut.WaitOne();
            this.connessioni[this.currServ].Sock.Send(pdu);
            mut.ReleaseMutex();
        }

        private void MouseBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true) {
                Thread.Sleep(1);
                int result = mouse.aggiornaCordinate(Cursor.Position.X, Cursor.Position.Y);
                this.MouseBackgroundWorker.ReportProgress(result);
            }
        }

        private void MouseBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (Convert.ToBoolean(Convert.ToInt32(e.ProgressPercentage))) {
                // Costruzione Pacchetto
                string aux = "M-";
                aux += Cursor.Position.X / Screen.PrimaryScreen.WorkingArea.Width;
                aux += "-" + Cursor.Position.Y / Screen.PrimaryScreen.WorkingArea.Height;
                byte[] pdu = Encoding.ASCII.GetBytes(aux);
                // Invio Pacchetto
                mut.WaitOne();
                this.connessioni[this.currServ].Sock.Send(pdu);
                mut.ReleaseMutex();
            }
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.InfoPanel.Visible = true;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = true;
            this.InfoPanel.Visible = false;
        }

    }
}
