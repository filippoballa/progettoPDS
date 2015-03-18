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
using System.Xml;

namespace ProgettoPDS_CLIENT
{
    public partial class MainForm : Form
    {
        #region Variables

        public delegate void Handler();
        public Handler myHandler;
        private User user;
        private List<SocketConnection> connessioni;
        private List<Server> servers;
        private int AltezzaForm, BaseForm, currServ, CountServerConnected;
        private static Mutex mut = new Mutex();
        private MouseHook mouseHook = new MouseHook();
        private KeyboardHook keyboardHook = new KeyboardHook();
        private static ManualResetEvent mreMouse = new ManualResetEvent(false);
        private static ManualResetEvent mreKeyboard = new ManualResetEvent(false);
        private MouseCord mc;
        private XmlManager mng;

        #endregion

        #region Constructor

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
            this.mng = new XmlManager();
            this.myHandler = new Handler(RefreshLabel); 
            this.currServ = -1;
            this.user = aux;
            this.CountServerConnected = 0;
            this.Text += " - USER: \"" + aux.Username + "\" - HOST : " + Dns.GetHostName() + " - IP Address : " + 
                SocketConnection.MyIpInfo() + " - Port Number: " + SocketConnection.localPort.ToString();
            this.AltezzaForm = this.Height;
            this.BaseForm = this.Width;
            this.Ridimensiona();
            this.HostNameTextBox.Focus();
            this.mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            this.keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            this.mouseHook.Click += new MouseEventHandler(mouseHook_DoubleClick);
            this.mouseHook.DoubleClick += new MouseEventHandler(mouseHook_DoubleClick);
        }

        #endregion

        #region Ridimensiona

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

            // Riposizionamento Bottone "Save Configuration Server"
            aux = (this.SaveServButton.Font.Size * this.Height) / this.AltezzaForm;
            this.SaveServButton.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveServButton.Height = (this.SaveServButton.Height * this.Height) / this.AltezzaForm;
            this.SaveServButton.Width = (this.SaveServButton.Width * this.Width) / this.BaseForm;
            X = (this.SaveServButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.SaveServButton.Location.Y * this.Height) / this.AltezzaForm;
            this.SaveServButton.Location = new Point(X, Y);

            // Riposizionamento Bottone "Load Configuration Server"
            aux = (this.LoadConfButton.Font.Size * this.Height) / this.AltezzaForm;
            this.LoadConfButton.Height = (this.LoadConfButton.Height * this.Height) / this.AltezzaForm;
            this.LoadConfButton.Width = (this.LoadConfButton.Width * this.Width) / this.BaseForm;
            X = (this.LoadConfButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.LoadConfButton.Location.Y * this.Height) / this.AltezzaForm;
            this.LoadConfButton.Location = new Point(X, Y);

            // Riposizionamento Bottone "Clear Configuration Server"
            aux = (this.ClearCacheButton.Font.Size * this.Height) / this.AltezzaForm;
            this.ClearCacheButton.Height = (this.ClearCacheButton.Height * this.Height) / this.AltezzaForm;
            this.ClearCacheButton.Width = (this.ClearCacheButton.Width * this.Width) / this.BaseForm;
            X = (this.ClearCacheButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.ClearCacheButton.Location.Y * this.Height) / this.AltezzaForm;
            this.ClearCacheButton.Location = new Point(X, Y);

            // Riposizionamento Bottone Disconnetti
            aux = (this.DisconnectButton.Font.Size * this.Height) / this.AltezzaForm;
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

        #endregion

        #region Connected Button

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1) {
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone Connetti!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {

                if (this.connessioni[this.currServ].IsDisconnect)
                    this.connessioni[this.currServ] = new SocketConnection(servers[this.currServ].IPAddr, servers[this.currServ].Porta, this.user, this);

                this.connessioni[this.currServ].StartClientConnection();

            }
            else
                MessageBox.Show("La connessione con il server è già attiva!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Disconnected Button

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
            this.connessioni[this.currServ].IsDisconnect = true;
            SocketConnection.isBindLocal = false;
            this.CountServerConnected--;

            if (this.CountServerConnected == 0)
                this.label5.Text = "Non Connesso con Nessun Server al momento!!";
            else {
                string[] aux = this.label5.Text.Split(',');
                int index = -1;

                aux[0] = aux[0].Substring(aux[0].LastIndexOf(' '), aux[0].Length - aux[0].LastIndexOf(' '));

                if (aux[0] == this.servers[this.currServ].HostName)
                    index = 0;
                else {

                    for (int i = 1; i < aux.Length; i++) {

                        if ( aux[i] == this.servers[this.currServ].HostName) {
                            index = i;
                            break;
                        }
                    }
                }

                this.label5.Text = "Connessioni attive : ";

                for (int i = 0; i < aux.Length; i++) {

                    if (i != index) {

                        if (i == 0)
                            this.label5.Text += " " + this.servers[this.currServ].HostName;
                        else
                            this.label5.Text += ", " + this.servers[this.currServ].HostName;
                    }
                }
            }

        }

        #endregion

        #region Other Buttons and Form's Functions

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < this.connessioni.Count; i++) {
                if (this.connessioni[i].IsConnected())
                    this.connessioni[i].SockClose();
            }

        }

        private void RefreshLabel()
        {
            if (this.connessioni[this.currServ].IsConnected() && this.CountServerConnected == 0) {
                this.label5.Text = "Connesioni Attive: " + servers[this.currServ].HostName;
                this.CountServerConnected++;
            }
            else if (this.connessioni[this.currServ].IsConnected() && this.CountServerConnected >= 1) {
                this.label5.Text += " , " + servers[this.currServ].HostName;
                this.CountServerConnected++;
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

            this.ActionServerLabel.Text += this.servers[this.currServ].HostName;
            this.ActionPanel.Visible = true;
            this.MainPanel.Visible = false;
            this.mc = new MouseCord(Cursor.Position.X, Cursor.Position.Y);
            this.mouseHook.Install();
            this.keyboardHook.Install();
             

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
            if (!SocketConnection.verificaIPAddres(this.IPAddressTextBox.Text)) {
                MessageBox.Show("L'indirizzo IP inserito non è corretto!!\n" +
                    "Deve presentarsi nel seguente formato: xxx.xxx.xxx.xxx ,\ndove \"xxx\" è un numero compreso tra 0 e 255!!",
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.IPAddressTextBox.Clear();
                this.IPAddressTextBox.Focus();
                return;
            }

            Server s = new Server(this.HostNameTextBox.Text, this.IPAddressTextBox.Text, Convert.ToInt32(this.PortaTextBox.Text));
            Predicate<Server> ipFinder = (Server p) => { return p.IPAddr == this.IPAddressTextBox.Text; };
            Predicate<Server> portFinder = (Server p) => { return p.Porta == Convert.ToInt32(this.PortaTextBox.Text); };
            
            if ( this.servers.Exists(ipFinder) && this.servers.Exists(portFinder) ) 
                MessageBox.Show("Il server specificato è già presente in elenco!!",
                    "AVVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else {
                this.connessioni.Add(new SocketConnection(this.IPAddressTextBox.Text, Convert.ToInt32(this.PortaTextBox.Text), this.user, this));
                this.servers.Add(s);
                this.listBox1.Items.Add(this.servers.Last().ToString());
            }
                

            this.PortaTextBox.Clear();
            this.IPAddressTextBox.Clear();
            this.HostNameTextBox.Clear();
            this.HostNameTextBox.Focus();

        }

        private void SaveServButton_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona prima il Server di cui desideri salvarne la configurazione",
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.mng.AddNewServer(this.servers[this.listBox1.SelectedIndex]);

            MessageBox.Show("Configurazione Salvata con Successo!!", "AVVISO",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadConfButton_Click(object sender, EventArgs e)
        {
            XmlNodeList xmlnodes = this.mng.documentTwo.GetElementsByTagName("MYSERVER");

            for (int i = 0; i < xmlnodes.Count; i++)
            {
                Server aux = new Server();
                aux.HostName = xmlnodes[i].ChildNodes.Item(0).InnerText;
                aux.IPAddr = xmlnodes[i].ChildNodes.Item(1).InnerText;
                aux.Porta = Convert.ToInt32(xmlnodes[i].ChildNodes.Item(2).InnerText);

                this.listBox1.Items.Add(aux.ToString());
                this.servers.Add(aux);
                this.connessioni.Add(new SocketConnection(aux.IPAddr, aux.Porta, this.user, this));
            }
        }

        private void ClearCacheButton_Click(object sender, EventArgs e)
        {
            this.mng.RemoveServers();
        }

        #endregion

        #region Keyboard Management

        private void keyboardHook_KeyUp( object sender, KeyEventArgs e ) 
        {
            if (this.ActionPanel.Visible && this.keyboardHook.IsStarted ) {

                if ( e.Shift && e.KeyCode == Keys.Escape ) {
                    this.ActionPanel.Visible = false;
                    this.ActionServerLabel.Text = "Stai Comandando il seguente Server: ";
                    this.MainPanel.Visible = true;
                    this.HostNameTextBox.Focus();
                    this.mouseHook.Unistall();
                    this.keyboardHook.Unistall();

                    // gestione BWs!!
                    if( this.MouseBackgroundWorker.IsBusy )
                        this.MouseBackgroundWorker.CancelAsync();

                    if( this.KeyBackgroundWorker.IsBusy )
                        this.KeyBackgroundWorker.CancelAsync();

                }
                else if ( e.Alt && e.KeyCode == Keys.PageUp ) {
                    int index = this.currServ;

                    do { 
                        this.currServ++;

                        if( this.currServ > ( this.connessioni.Count - 1 ) )
                            this.currServ = 0;

                        if( this.connessioni[this.currServ].IsConnected() ) {
                            this.ActionServerLabel.Text = "Stai Comandando il seguente Server: " + this.servers[this.currServ].HostName;
                            break;
                        }
                    } while( this.currServ != index );

                    if( this.currServ == index )
                        MessageBox.Show("Al momento sei connesso solo con il server : " + this.servers[this.currServ].HostName,
                            "AVVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if ( e.Alt && e.KeyCode == Keys.PageDown ) {
                    int index = this.currServ;

                    do {
                        this.currServ--;

                        if (this.currServ < 0)
                            this.currServ = this.connessioni.Count - 1;

                        if (this.connessioni[this.currServ].IsConnected()) {
                            this.ActionServerLabel.Text = "Stai Comandando il seguente Server: " + this.servers[this.currServ].HostName;
                            break;
                        }
                    } while (this.currServ != index);

                    if (this.currServ == index)
                        MessageBox.Show("Al momento sei connesso solo con il server : " + this.servers[this.currServ].HostName,
                            "AVVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (e.Alt && e.KeyCode == Keys.R ) { 

                    // Invio Richiesta Clipboard Server
                    // TODO

                }
                else if (e.Alt && e.KeyCode == Keys.S) { 

                    // Imposto la Clipboard del server con quella attuale
                    // TODO

                }
                else {

                    if (this.KeyBackgroundWorker.IsBusy)
                        mreKeyboard.WaitOne();

                    this.KeyBackgroundWorker.RunWorkerAsync(e);

                    mreKeyboard.Reset();

                }
            }
        }

        private void KeyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            KeyEventArgs key = (KeyEventArgs)e.Argument;

            // Costruzione Pacchetto
            string aux = "K-";
            bool mod = true;

            if (key.Control && key.Alt && key.Shift)
                aux += KeyboardHook.Modificatore.CAS.ToString() + "-";
            else if (key.Control && key.Alt)
                aux += KeyboardHook.Modificatore.CA.ToString() + "-";
            else if (key.Control && key.Shift)
                aux += KeyboardHook.Modificatore.CS.ToString() + "-";
            else if (key.Alt && key.Shift )
                aux += KeyboardHook.Modificatore.AS.ToString() + "-";
            else if( key.Control )
                aux += KeyboardHook.Modificatore.C.ToString() + "-";
            else if( key.Alt )
                aux += KeyboardHook.Modificatore.A.ToString() + "-";
            else if (key.Shift)
                aux += KeyboardHook.Modificatore.S.ToString() + "-";
            else {            
                mod = false;
                aux += "SNGKEY-" + key.KeyValue.ToString("X") + "-";
            }

            if (mod)
                aux += key.KeyValue.ToString("X") + "-";

            byte[] pdu = Encoding.ASCII.GetBytes(aux);

            // Invio PDU relativa alla tastiera
            try{
                mut.WaitOne();
                this.connessioni[this.currServ].Sock.Send(pdu);
            }
            finally {
                mut.ReleaseMutex();
            }

            mreKeyboard.Set();
            
        }

        #endregion

        #region Mouse Management

        private void mouseHook_DoubleClick(object sender, MouseEventArgs e) 
        {
            MouseManagement(e);
        }

        private void mouseHook_MouseClick(object sender, MouseEventArgs e) 
        {
            MouseManagement(e);
        }

        private void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            MouseManagement(e);
        }

        private void MouseManagement( MouseEventArgs e ) 
        {
            if (this.mouseHook.IsStarted && this.ActionPanel.Visible) {

                if( this.MouseBackgroundWorker.IsBusy)
                    mreMouse.WaitOne();

                mreMouse.Reset();

                this.MouseBackgroundWorker.RunWorkerAsync(e);

            }   
        }

        private void MouseBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            MouseEventArgs mouse = (MouseEventArgs)e.Argument;
           
            // Costruzione Pacchetto
            string aux;

            if (mouse.Clicks == 0) {   
                // Se le coordinate non sono state modificate esco dal BW 
                if (!mc.aggiornaCord(mouse.X, mouse.Y))
                    aux = "";
                else
                    aux = "M-MOVE";
            }
            else if (mouse.Clicks == 1) {
                if (mouse.Button == MouseButtons.Left)
                    aux = "M-LCLK";
                else if (mouse.Button == MouseButtons.Right)
                    aux = "M-RCLK";
                else
                    aux = "M-MCLK";
            }
            else {

                if (mouse.Button == MouseButtons.Left)
                    aux = "M-LDBCLK";
                else if (mouse.Button == MouseButtons.Right)
                    aux = "M-RDBCLK";
                else
                    aux = "M-MDBCLK";
            }

            if (aux != "") {
                int res = (Cursor.Position.X * 1000) / Screen.PrimaryScreen.WorkingArea.Width;
                aux += "-" + res.ToString();
                res = (Cursor.Position.Y * 1000) / Screen.PrimaryScreen.WorkingArea.Height;
                aux += "-" + res.ToString() + "-";
                byte[] pdu = Encoding.ASCII.GetBytes(aux);

                // Invio Pacchetto
                try {
                    mut.WaitOne();
                    this.connessioni[this.currServ].Sock.Send(pdu);
                }
                finally{
                    mut.ReleaseMutex();
                }
            }

            mreMouse.Set();
                                                                           
        }

        #endregion

    }
}
