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
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace ProgettoPDS_CLIENT
{
    public partial class MainForm : Form
    {

        #region Variables And Delegate

        public delegate void Handler();
        public delegate object[] GetClipboardDataDelegate();
        public delegate void SetClipboardDataDelegate(object[] d);   
        public delegate void startProgressBarDelegate(int dim);        
        public delegate void doStepProgressBarDelegate();
        public delegate void closeProgressBarDelegate();
        public delegate void ResetDelegate();

        public ResetDelegate resetCallback;
        public Handler myHandler;
        private GetClipboardDataDelegate getClip;
        private SetClipboardDataDelegate setClip;
        public startProgressBarDelegate startProgressBar;
        public doStepProgressBarDelegate doStepProgressBar;
        public closeProgressBarDelegate closeProgressBar;

        private User user;
        private List<SocketConnection> connessioni;
        private List<Server> servers;
        private int AltezzaForm, BaseForm, currServ, CountServerConnected;
        private static Mutex mut = new Mutex();
        private MouseHook mouseHook = new MouseHook();
        private KeyboardHook keyboardHook = new KeyboardHook();
        private Queue<MouseEventArgs> MouseQueue;
        private Queue<KeyEventArgs> KeyQueue;
        private MouseCord mc;
        private XmlManager mng;
        private bool isDialog;
        private SoundPlayer player; 

        #endregion

        #region Constructor

        public MainForm(User aux )
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            this.isDialog = true;
            this.connessioni = new List<SocketConnection>();
            this.servers = new List<Server>();
            this.MouseQueue = new Queue<MouseEventArgs>();
            this.KeyQueue = new Queue<KeyEventArgs>();
            this.mng = new XmlManager();
            this.myHandler = new Handler(RefreshLabel);
            this.getClip = new GetClipboardDataDelegate(GetClipboardData);
            this.setClip = new SetClipboardDataDelegate(SetCliboardData);
            this.currServ = -1;
            this.user = aux;
            this.CountServerConnected = 0;
            this.Text += " - USER: \"" + aux.Username + "\" - HOST : " + Dns.GetHostName() + " - IP Address : " + 
                SocketConnection.MyIpInfo();
            this.AltezzaForm = this.Height;
            this.BaseForm = this.Width;
            this.Ridimensiona();
            this.HostNameTextBox.Focus();
            this.mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            this.keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            this.mouseHook.Click += new MouseEventHandler(mouseHook_DoubleClick);
            this.mouseHook.DoubleClick += new MouseEventHandler(mouseHook_DoubleClick);
            ModifyProgressBarColor.SetState(this.ClipboardProgressBar, 2);
            this.startProgressBar = new startProgressBarDelegate(startProgressBarMethod);
            this.doStepProgressBar = new doStepProgressBarDelegate(doStepProgressBarMethod);
            this.closeProgressBar = new closeProgressBarDelegate(closeProgressBarMethod);
            this.resetCallback = new ResetDelegate(ResetConnection);
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

            // Resize "ComandLabel"
            aux = (this.ComandLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.ComandLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComandLabel.Height = (this.ComandLabel.Height * this.Height) / this.AltezzaForm;
            this.ComandLabel.Width = (this.ComandLabel.Width * this.Width) / this.BaseForm;

            // Resize "Type Clipboard label"
            aux = (this.TypeClipboardLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.TypeClipboardLabel.Font = new System.Drawing.Font("Calibri", aux, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeClipboardLabel.Height = (this.TypeClipboardLabel.Height * this.Height) / this.AltezzaForm;
            this.TypeClipboardLabel.Width = (this.TypeClipboardLabel.Width * this.Width) / this.BaseForm;

            // Resize "GroupBox Conf."
            aux = (this.groupBox1.Font.Size * this.Height) / this.AltezzaForm;
            this.groupBox1.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Height = (this.groupBox1.Height * this.Height) / this.AltezzaForm;
            this.groupBox1.Width = (this.groupBox1.Width * this.Width) / this.BaseForm;

            // Resize "ProgressBarPanel"
            X = (this.ProgressBarPanel.Location.X * this.Width) / this.BaseForm;
            Y = (this.ProgressBarPanel.Location.Y * this.Height) / this.AltezzaForm;  
            this.ProgressBarPanel.Location = new Point(X,Y);
            this.ProgressBarPanel.Height = (this.ProgressBarPanel.Height * this.Height) / this.AltezzaForm;
            this.ProgressBarPanel.Width = (this.ProgressBarPanel.Width * this.Width ) / this.BaseForm;

            // Resize Clipboard progress Bar
            X = (this.ClipboardProgressBar.Location.X * this.Width) / this.BaseForm;
            Y = (this.ClipboardProgressBar.Location.Y * this.Height) / this.AltezzaForm;
            this.ClipboardProgressBar.Location = new Point(X, Y);
            this.ClipboardProgressBar.Height = (this.ClipboardProgressBar.Height * this.Height) / this.AltezzaForm;
            this.ClipboardProgressBar.Width = (this.ClipboardProgressBar.Width * this.Width) / this.BaseForm;

            // Resize "GroupBox Comandi"
            aux = (this.ComandiGroupBox.Font.Size * this.Height) / this.AltezzaForm;
            this.ComandiGroupBox.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComandiGroupBox.Height = (this.ComandiGroupBox.Height * this.Height) / this.AltezzaForm;
            this.ComandiGroupBox.Width = (this.ComandiGroupBox.Width * this.Width) / this.BaseForm;
            X = (this.ComandiGroupBox.Location.X * this.Width) / this.BaseForm;
            Y = (this.ComandiGroupBox.Location.Y * this.Height) / this.AltezzaForm;
            this.ComandiGroupBox.Location = new Point(X, Y);

            // Resize "ContentClipboardPanel"
            X = (this.ContentClipboardPanel.Location.X * this.Width) / this.BaseForm;
            Y = (this.ContentClipboardPanel.Location.Y * this.Height) / this.AltezzaForm;
            this.ContentClipboardPanel.Location = new Point(X, Y);
            this.ContentClipboardPanel.Height = (this.ContentClipboardPanel.Height * this.Height) / this.AltezzaForm;
            this.ContentClipboardPanel.Width = (this.ContentClipboardPanel.Width * this.Width) / this.BaseForm;

            // Resize "ComandLabel"
            aux = (this.TitleContentClipLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.TitleContentClipLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleContentClipLabel.Height = (this.TitleContentClipLabel.Height * this.Height) / this.AltezzaForm;
            this.TitleContentClipLabel.Width = (this.TitleContentClipLabel.Width * this.Width) / this.BaseForm;

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

            // Resize ImageClipboardPictureBox 
            this.ImageClipboardPictureBox.Height = (this.ImageClipboardPictureBox.Height * this.Height) / this.AltezzaForm;
            this.ImageClipboardPictureBox.Width = (this.ImageClipboardPictureBox.Width * this.Width) / this.BaseForm;
            X = (this.ImageClipboardPictureBox.Location.X * this.Width) / this.BaseForm;
            Y = (this.ImageClipboardPictureBox.Location.Y * this.Height) / this.AltezzaForm;
            this.ImageClipboardPictureBox.Location = new Point(X, Y);


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
            this.DisconnectButton.Font = new System.Drawing.Font("Comic Sans MS", aux - 1, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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

            // Riposizionamento Bottone "LogOut"
            this.LogOutButton.Height = (this.LogOutButton.Height * this.Height) / this.AltezzaForm;
            this.LogOutButton.Width = (this.LogOutButton.Width * this.Width) / this.BaseForm;
            X = (this.LogOutButton.Location.X * this.Width) / this.BaseForm;
            Y = (this.LogOutButton.Location.Y * this.Height) / this.AltezzaForm;
            this.LogOutButton.Location = new Point(X, Y);

            // Resize " AvanzClipLabel "
            aux = (this.AvanzClipLabel.Font.Size * this.Height) / this.AltezzaForm;
            this.AvanzClipLabel.Font = new System.Drawing.Font("Comic Sans MS", aux, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.AvanzClipLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.AvanzClipLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.AvanzClipLabel.Location = new Point(X, Y);

            // Resize " Percentage Label "
            aux = ( this.PercentageLabel.Font.Size * this.Height ) / this.AltezzaForm;
            this.PercentageLabel.Font = new System.Drawing.Font("Calibri", aux, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            X = (this.PercentageLabel.Location.X * this.Width) / this.BaseForm;
            Y = (this.PercentageLabel.Location.Y * this.Height) / this.AltezzaForm;
            this.PercentageLabel.Location = new Point(X, Y);

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

        private void ComandiGroupBox_Paint(object sender, PaintEventArgs e)
        {
            
            GroupBox box = (GroupBox)sender;
            Graphics gfx = e.Graphics;
            Pen p = new Pen(Color.DarkRed, 2);
            gfx.DrawLine(p, 0, 16, 0, box.Height);
            gfx.DrawLine(p, 0, 16, 10, 16);
            gfx.DrawLine(p, 250, 16, box.Width, 16);
            gfx.DrawLine(p, box.Width, 16, box.Width, box.Height);
            gfx.DrawLine(p, 0, box.Height, box.Width, box.Height);  
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

            if (!this.connessioni[this.currServ].IsConnected())
                this.connessioni[this.currServ].StartClientConnection();
            else
                MessageBox.Show("La connessione con il server è già attiva!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Disconnected Button

        private void ResetDisconnectLabel()
        { 
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

                        if (aux[i] == this.servers[this.currServ].HostName) {
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

            QuitPacket();

            this.connessioni[this.currServ].SockClose();
            this.connessioni[this.currServ].CloseClipSock();
            this.connessioni[this.currServ].Stato = SocketConnection.STATO.DISCONESSO;

            ResetDisconnectLabel();
        }

        #endregion

        #region Methods Packets

        private void ResetConnection()
        {
            this.connessioni[this.currServ].SockClose();
            this.connessioni[this.currServ].CloseClipSock();
            this.connessioni[this.currServ].Stato = SocketConnection.STATO.DISCONESSO;

            if ( this.ActionPanel.Visible ) {
                this.ActionPanel.Visible = false;
                this.MainPanel.Visible = true;

                if ( this.keyboardHook.IsStarted )
                    this.keyboardHook.Unistall();

                if (this.mouseHook.IsStarted)
                    this.mouseHook.Unistall();

                ResetDisconnectLabel();

            }
        }

        private void SendPacket(byte[] pdu)
        {
            try {
                mut.WaitOne();

                if( this.connessioni[this.currServ].IsConnected() )
                    this.connessioni[this.currServ].Sock.Send(pdu);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(this.resetCallback);
            }
            finally {
                mut.ReleaseMutex();
            }
        }

        private void QuitPacket() 
        {
            byte[] pdu = Encoding.ASCII.GetBytes("QUIT-");
            SendPacket(pdu);
        }

        #endregion

        #region Other Buttons and Form's Functions

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            Program.res = DialogResult.Yes;
            this.isDialog = false;
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < this.connessioni.Count; i++) {

                if (this.connessioni[i].IsConnected()) {
                    QuitPacket();
                    this.connessioni[i].SockClose();
                    this.connessioni[i].CloseClipSock();
                }
            }

            if (this.keyboardHook.IsStarted )
                this.keyboardHook.Unistall();

            if (this.mouseHook.IsStarted)
                this.mouseHook.Unistall();

            if( this.isDialog )
                Program.res = MessageBox.Show("Desideri tornare alla schermata di Login? ",
                    "AVVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MessageBox.Show("Seleziona un server prima di cliccare sul bottone \"Start&Use\"!!", 
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.currServ = this.listBox1.SelectedIndex;

            if (!this.connessioni[this.currServ].IsConnected()) {
                MessageBox.Show("Il server non è connesso!!", "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ( !SocketConnection.SocketConnected(this.connessioni[this.currServ].Sock) ) {
                MessageBox.Show("La connesione con l'host remota è caduta a causa di un evento\nimprevisto!! Occorre riconnettersi al Server!",
                    "ERRORE!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetDisconnectLabel();
                this.connessioni[this.currServ].SockClose();
                this.connessioni[this.currServ].CloseClipSock();
                this.connessioni[this.currServ].Stato = SocketConnection.STATO.DISCONESSO;
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

            if( this.mng.AddNewServer(this.servers[this.listBox1.SelectedIndex]) )
                MessageBox.Show("Configurazione Salvata con Successo!!", "AVVISO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadConfButton_Click(object sender, EventArgs e)
        {
            XmlNodeList xmlnodes = this.mng.documentTwo.GetElementsByTagName("MYSERVER");
            bool res = false;

            if (xmlnodes.Count == 0) {
                MessageBox.Show("La cache delle configurazioni è vuota!! Aggiungi un server\nalla lista e poi clicca sul bottone\"Save...\" per inserirlo in cache", 
                    "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < xmlnodes.Count; i++)
            {
                Server aux = new Server();
                aux.HostName = xmlnodes[i].ChildNodes.Item(0).InnerText;
                aux.IPAddr = xmlnodes[i].ChildNodes.Item(1).InnerText;
                aux.Porta = Convert.ToInt32(xmlnodes[i].ChildNodes.Item(2).InnerText);

                if ( !this.listBox1.Items.Contains(aux.ToString()) ) {
                    res = true;
                    this.listBox1.Items.Add(aux.ToString());
                    this.servers.Add(aux);
                    this.connessioni.Add(new SocketConnection(aux.IPAddr, aux.Porta, this.user, this));
                }
            }

            if (!res)
                MessageBox.Show("Configurazioni della Cache già Caricate in Lista!!", "AVVISO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ClearCacheButton_Click(object sender, EventArgs e)
        {
            this.mng.RemoveServers();
            MessageBox.Show("La cache è stata svuotata con successo!!", "AVVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearClipboardContentPanel() 
        {
            this.ContentClipboardPanel.Visible = false;
            this.ImageClipboardPictureBox.Visible = false;
            this.TypeClipboardLabel.Text = "TYPE OF DATA :";
            this.RichTextBox.Visible = false;

            if( player != null )
                this.player.Stop();

            this.PlayAudioButton.Visible = false;
            this.StopAudioButton.Visible = false;
            this.player = null;
        }

        private void SetClipboardContentPanel( object[] data ) 
        {
            switch (data[0].ToString()) {

                case "IMMAGINE":
                    this.ImageClipboardPictureBox.Visible = true;
                    this.TypeClipboardLabel.Text += " IMAGE";
                    this.ImageClipboardPictureBox.BackgroundImage = (Image)data[1];
                    break;

                case "TEXT":
                    this.TypeClipboardLabel.Text += " TEXT";
                    this.RichTextBox.Visible = true;
                    this.RichTextBox.Clear();
                    this.RichTextBox.AppendText((string)data[1]);
                    break;

                case "AUDIO":
                    this.TypeClipboardLabel.Text += " AUDIO";
                    this.PlayAudioButton.Visible = true;
                    this.StopAudioButton.Visible = true;
                    this.player = new SoundPlayer((Stream)data[1]);
                    break;

                case "FILE_DROP":
                    this.TypeClipboardLabel.Text += " FILE DROP";
                    this.RichTextBox.Visible = true;
                    this.RichTextBox.Clear();
                    StringCollection strColl = (StringCollection)data[1];

                    for (int i = 0; i < strColl.Count; i++)
                        this.RichTextBox.AppendText(strColl[i] + "\n");

                    break;

                case "VUOTA" :
                    this.TypeClipboardLabel.Text += " VUOTA!";
                    break;
            }

        }

        private void PlayAudioButton_Click(object sender, EventArgs e)
        {
            if (this.player != null)
                this.player.Play();
        }

        private void StopAudioButton_Click(object sender, EventArgs e)
        {
            if (this.player != null)
                this.player.Stop();
        }

        #endregion

        #region Keyboard Management

        private void keyboardHook_KeyUp( object sender, KeyEventArgs e ) 
        {
            if (!GlobalHook.IsActive(this.Handle))
                return;

            if (this.ActionPanel.Visible && this.keyboardHook.IsStarted ) {

                if ( e.Shift && e.KeyCode == Keys.Escape ) {
                    this.ActionPanel.Visible = false;
                    this.ActionServerLabel.Text = "Stai Comandando il seguente Server: ";
                    this.MainPanel.Visible = true;
                    this.HostNameTextBox.Focus();
                    this.mouseHook.Unistall();
                    this.keyboardHook.Unistall();
                    ClearClipboardContentPanel();

                    if( this.MouseBackgroundWorker.IsBusy )
                        this.MouseBackgroundWorker.CancelAsync();

                    if( this.KeyBackgroundWorker.IsBusy )
                        this.KeyBackgroundWorker.CancelAsync();

                    this.KeyQueue.Clear();
                    this.MouseQueue.Clear();

                    bool trovato = false;

                    if ( this.ClipboardSendBW.IsBusy ) {
                        this.ClipboardSendBW.CancelAsync();
                        trovato = true;
                    }

                    if (this.ClipboardSendBW.IsBusy) {
                        this.ClipboardSendBW.CancelAsync();
                        trovato = true;
                    }

                    if (trovato)
                        this.ProgressBarPanel.Visible = false;
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
                else if (e.Alt && e.KeyCode == Keys.C ) {

                    if ( this.ClipboardRequestBW.IsBusy )
                        this.ClipboardRequestBW.CancelAsync();

                    if ( this.ClipboardSendBW.IsBusy )
                        this.ClipboardSendBW.CancelAsync();                    

                }
                else if (e.Alt && e.KeyCode == Keys.R) {
                    if (!this.ClipboardRequestBW.IsBusy && !this.ClipboardSendBW.IsBusy)
                        this.ClipboardRequestBW.RunWorkerAsync();
                    else
                        MessageBox.Show("E' in corso il completamento della richiesta precedente relativa alla Clipboard.", "AVVISO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (e.Alt && e.KeyCode == Keys.S) {

                    if (!this.ClipboardSendBW.IsBusy && !this.ClipboardRequestBW.IsBusy)
                        this.ClipboardSendBW.RunWorkerAsync();
                    else
                        MessageBox.Show("E' in corso il completamento della richiesta precedente relativa alla Clipboard.", "AVVISO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (e.Alt && e.KeyCode == Keys.M) { 
                    
                    if (this.ContentClipboardPanel.Visible)
                        ClearClipboardContentPanel();
                    else {
                        this.ContentClipboardPanel.Visible = true;

                        object[] data = (object[])this.Invoke(this.getClip);

                        SetClipboardContentPanel(data);                        
                    }
                        
                }
                else {

                    this.KeyQueue.Enqueue(e);

                    if (!this.KeyBackgroundWorker.IsBusy)
                        this.KeyBackgroundWorker.RunWorkerAsync();

                }
            }
        }

        private void KeyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this.KeyQueue.Count != 0)
            {
                KeyEventArgs key = this.KeyQueue.Dequeue();

                string aux = "K-";
                bool mod = true;

                if (key.Control && key.Alt && key.Shift)
                    aux += KeyboardHook.Modificatore.CAS.ToString() + "-";
                else if (key.Control && key.Alt)
                    aux += KeyboardHook.Modificatore.CA.ToString() + "-";
                else if (key.Control && key.Shift)
                    aux += KeyboardHook.Modificatore.CS.ToString() + "-";
                else if (key.Alt && key.Shift)
                    aux += KeyboardHook.Modificatore.AS.ToString() + "-";
                else if (key.Control)
                    aux += KeyboardHook.Modificatore.C.ToString() + "-";
                else if (key.Alt)
                    aux += KeyboardHook.Modificatore.A.ToString() + "-";
                else if (key.Shift)
                    aux += KeyboardHook.Modificatore.S.ToString() + "-";
                else
                {
                    mod = false;
                    aux += "SNGKEY-" + key.KeyValue.ToString("X") + "-";
                }

                if (mod)
                    aux += key.KeyValue.ToString("X") + "-";

                byte[] pdu = Encoding.ASCII.GetBytes(aux);

                SendPacket(pdu);

            } 
            
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
            if (!GlobalHook.IsActive(this.Handle))
                return;
           
            if (this.mouseHook.IsStarted && this.ActionPanel.Visible) {

                if ( e.Clicks == 0 ) {
                    if(mc.aggiornaCord(e.X, e.Y))                
                        this.MouseQueue.Enqueue(e);
                }
                else
                    this.MouseQueue.Enqueue(e);

                if (!this.MouseBackgroundWorker.IsBusy)
                    this.MouseBackgroundWorker.RunWorkerAsync();

            }   
        }

        private void MouseBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this.MouseQueue.Count != 0) 
            {

                MouseEventArgs mouse = this.MouseQueue.Dequeue();

                string aux;

                if (mouse.Clicks == 0)                    
                    aux = "M-MOVE";
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

                int res = (Cursor.Position.X * 1000) / Screen.PrimaryScreen.WorkingArea.Width;
                aux += "-" + res.ToString();
                res = (Cursor.Position.Y * 1000) / Screen.PrimaryScreen.WorkingArea.Height;
                aux += "-" + res.ToString() + "-";
                byte[] pdu = Encoding.ASCII.GetBytes(aux);

                SendPacket(pdu);
                
            } 
                                                                           
        }

        #endregion

        #region Clipboard Management

        private void ClipboardPacket(string eventType) 
        {
            string aux = "C-" + eventType + "-";
            byte[] pdu = Encoding.ASCII.GetBytes(aux);
            SendPacket(pdu);           
        }

        private void ClipboardSendBW_DoWork(object sender, DoWorkEventArgs e)
        {
            ClipboardPacket("SET_CLIP");

            object[] data = (object[])this.Invoke(this.getClip);
            string aux, resp;
            byte[] pdu, buff;

            switch (data[0].ToString()) { 
            
                case "AUDIO":
                    aux = "AUDIO-";
                    Stream stream = (Stream)data[1];
                    buff = new byte[stream.Length];
                    stream.Read(buff, 0, (int)stream.Length);
                    aux += buff.Length.ToString(); 
                    pdu = Encoding.ASCII.GetBytes(aux);
                    SendClipboardInfo(pdu);
                    pdu = ReceiveClipboardInfo();
                    resp = Encoding.ASCII.GetString(pdu);
                    resp = resp.Substring(0, resp.IndexOf('\0'));

                    if (resp == "+OK") 
                        SendClipboardData(buff, buff.Length);

                    break;

                case "FILE_DROP":
                    aux = "FILE_DROP-";
                    StringCollection filenames = (StringCollection)data[1];
                    object[] arr = new object[3];
                    List<byte[]> filesData = new List<byte[]>();

                    for (int i = 0; i < filenames.Count; i++) {
                        byte[] f = File.ReadAllBytes(filenames[i]);
                        filesData.Add(f);
                        filenames[i] = Path.GetFileName(filenames[i]);
                    }

                    arr[0] = filenames.Count;
                    arr[1] = filenames;
                    arr[2] = filesData;

                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, arr);
                    buff = ms.ToArray();

                    aux += buff.Length.ToString();

                    pdu = Encoding.ASCII.GetBytes(aux);
                    SendClipboardInfo(pdu);
                    pdu = ReceiveClipboardInfo();
                    resp = Encoding.ASCII.GetString(pdu);
                    resp = resp.Substring(0, resp.IndexOf('\0'));

                    if (resp == "+OK") 
                        SendClipboardData(buff, buff.Length);

                    break;

                case "IMMAGINE":
                    aux = "IMMAGINE-";
                    Image img = (Image)data[1];                    
                    ImageConverter converter = new ImageConverter();
                    buff = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    

                    aux += buff.Length.ToString();

                    pdu = Encoding.ASCII.GetBytes(aux);
                    SendClipboardInfo(pdu);
                    pdu = ReceiveClipboardInfo();
                    resp = Encoding.ASCII.GetString(pdu);
                    resp = resp.Substring(0, resp.IndexOf('\0'));

                    if (resp == "+OK") 
                        SendClipboardData(buff, buff.Length);

                    break;

                case "TEXT":
                    aux = "TEXT-";
                    string text = (string)data[1];
                    buff = Encoding.ASCII.GetBytes(text);
                    aux += buff.Length.ToString();
                    pdu = Encoding.ASCII.GetBytes(aux);
                    SendClipboardInfo(pdu);
                    pdu = ReceiveClipboardInfo();
                    resp = Encoding.ASCII.GetString(pdu);
                    resp = resp.Substring(0, resp.IndexOf('\0'));

                    if (resp == "+OK") 
                        SendClipboardData(buff, buff.Length);
                    
                    break;

                case "VUOTA":
                    aux = "VUOTA-0";
                    pdu = Encoding.ASCII.GetBytes(aux);
                    SendClipboardInfo(pdu);
                    pdu = ReceiveClipboardInfo();
                    break;
            } 

        }

        private void ClipboardRequestBW_DoWork(object sender, DoWorkEventArgs e)
        {
            ClipboardPacket("GET_CLIP");
            byte[] pdu = ReceiveClipboardInfo();
            string resp = Encoding.ASCII.GetString(pdu), aux;
            resp = resp.Substring(0, resp.IndexOf('\0'));
            object[] data = new object[2];

            string[] parametersInfo = resp.Split('-');

            if (parametersInfo[0] == "VUOTA") {
                data[0] = "VUOTA";
                data[1] = null;
                this.Invoke(this.setClip, new object[] { data });
                return;
            }

            Array.Clear(pdu, 0, pdu.Length);
            aux = "+OK";
            pdu = Encoding.ASCII.GetBytes(aux);
            SendClipboardInfo(pdu);
            pdu = ReceiveClipboardData(Convert.ToInt32(parametersInfo[1]));

            if (pdu == null)
                return;
            else {

                switch (parametersInfo[0]) {

                    case "AUDIO":
                        Stream stream = new MemoryStream(pdu);
                        data[0] = "AUDIO";
                        data[1] = stream;
                        break;

                    case "IMMAGINE":
                        ImageConverter ic = new ImageConverter();
                        Image img = (Image)ic.ConvertFrom(pdu);
                        data[0] = "IMMAGINE";
                        data[1] = img;
                        break;

                    case "FILE_DROP":
                        MemoryStream ms = new MemoryStream(pdu);
                        BinaryFormatter bf = new BinaryFormatter();
                        object o = bf.Deserialize(ms);
                        object[] arr = new object[3];

                        arr = (object[])o;

                        int NFiles = (int)arr[0];
                        List<byte[]> files = (List<byte[]>)arr[2];
                        StringCollection strColl = (StringCollection)arr[1];

                        for (int i = 0; i < NFiles; i++) {
                            strColl[i] = Program.tempPath + strColl[i];
                            File.WriteAllBytes(strColl[i], files[i]);
                        }

                        data[0] = "FILE_DROP";
                        data[1] = strColl;

                        break;

                    case "TEXT":
                        aux = Encoding.ASCII.GetString(pdu);
                        data[0] = "TEXT";
                        data[1] = aux;
                        break;

                    default:
                        aux = "-ERR";
                        pdu = Encoding.ASCII.GetBytes(aux);
                        SendClipboardInfo(pdu);
                        break;

                }

                this.Invoke(this.setClip, new object[] { data });

            }

        }

        private void SendClipboardInfo(byte[] pdu)
        {
            try {

                if( this.connessioni[this.currServ].IsConnected() )
                    this.connessioni[this.currServ].ClipSock.Send(pdu);
            }
            catch( Exception e) {
                MessageBox.Show(e.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(this.resetCallback);
            }
        }

        private byte[] ReceiveClipboardInfo()
        {

            byte[] pdu = new byte[60];

            try {

                if (this.connessioni[this.currServ].IsConnected())
                    this.connessioni[this.currServ].ClipSock.Receive(pdu);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(this.resetCallback);
            }

            return pdu;
        }

        // OTTENGO IL CONTENUTO DELLA CLIPBOARD
        private object[] GetClipboardData()
        {
            object[] data = new object[2];

            if( Clipboard.ContainsAudio() ) {
                data[0] = "AUDIO";
                data[1] = Clipboard.GetAudioStream();
            }
            else if( Clipboard.ContainsImage() ) {
                data[0] = "IMMAGINE";
                data[1] = Clipboard.GetImage();
            }
            else if ( Clipboard.ContainsText() )  {
                data[0] = "TEXT";
                data[1] = Clipboard.GetText();
            }
            else if (Clipboard.ContainsFileDropList()) {
                data[0] = "FILE_DROP";
                data[1] = Clipboard.GetFileDropList();
            }
            else {
                data[0] = "VUOTA";
                data[1] = null;
            }

            return data;                
        }

        // IMPOSTO IL CONTENUTO DELLA CLIPBOARD
        private void SetCliboardData(object[] data) 
        {
            switch (data[0].ToString()) {
 
                case "AUDIO":
                    Clipboard.SetAudio( (Stream)data[1] );
                    break;
                case "IMMAGINE":
                    Clipboard.SetImage( (Image)data[1] );
                    break;
                case "TEXT":
                    Clipboard.SetText((string)data[1]);
                    break;
                case "FILE_DROP":
                    Clipboard.SetFileDropList((StringCollection)data[1]);
                    break;
                case "VUOTA":
                    Clipboard.Clear();
                    break;
            }
        }

        private void SendClipboardData( byte[] buff, int dim)
        {
            this.Invoke(this.startProgressBar, dim);   
            int dataSended = 0;

            while (dataSended < dim) {

                if (this.ClipboardSendBW.CancellationPending) {
                    string aux = "C-CLOSE-";
                    SendPacket(Encoding.ASCII.GetBytes(aux));
                    break;
                }

                byte[] d = new byte[SocketConnection.SBufSizeClipSock];

                for ( int i = dataSended; (i < SocketConnection.SBufSizeClipSock + dataSended ) && i < dim; i++)
                    d[i - dataSended] = buff[i];

                try {

                    if (this.connessioni[this.currServ].IsConnected())
                        this.connessioni[this.currServ].ClipSock.Send(d);
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Invoke(this.resetCallback);
                }

                dataSended += d.Length;
                this.Invoke(this.doStepProgressBar); 
            }

            this.Invoke(this.closeProgressBar);
        }

        private byte[] ReceiveClipboardData( int dim )
        {
            this.Invoke(this.startProgressBar, dim);

            int dataReceived = 0;
            byte[] rbuff = new byte[dim], data;

            while (dataReceived < dim) {

                if (this.ClipboardRequestBW.CancellationPending) {
                    Array.Clear(rbuff, 0, rbuff.Length);
                    rbuff = null;
                    string aux = "C-CLOSE-";
                    SendPacket(Encoding.ASCII.GetBytes(aux));
                    break;
                }

                data = new byte[SocketConnection.RBufSizeClipSock];

                try {

                    if (this.connessioni[this.currServ].IsConnected())
                        this.connessioni[this.currServ].ClipSock.Receive(data);
                }
                catch (Exception e) {
                    MessageBox.Show( e.Message, "ANOMALY", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    this.Invoke(this.resetCallback);
                }

                for (int i = dataReceived; i < SocketConnection.RBufSizeClipSock + dataReceived && i < dim; i++)
                    rbuff[i] = data[i - dataReceived];

                dataReceived += data.Length;
                this.Invoke(this.doStepProgressBar); 
            }

            this.Invoke(this.closeProgressBar);

            return rbuff;
        }

        #endregion

        #region ProgressBar Methods

        public void startProgressBarMethod(int dim)
        {
            if (!this.ProgressBarPanel.Visible)
                this.ProgressBarPanel.Visible = true;

            this.ClipboardProgressBar.Maximum = dim;
            this.ClipboardProgressBar.Minimum = 0;
            this.ClipboardProgressBar.Value = 0;
            this.ClipboardProgressBar.Step = SocketConnection.SBufSizeClipSock;
        }

        public void doStepProgressBarMethod()
        {
            this.ClipboardProgressBar.PerformStep();
            int aux = (this.ClipboardProgressBar.Value * 100) / this.ClipboardProgressBar.Maximum;
            this.PercentageLabel.Text = aux.ToString() + " %";
        }

        private void closeProgressBarMethod()
        {
            while (this.ClipboardProgressBar.Maximum > this.ClipboardProgressBar.Value) {
                doStepProgressBarMethod();
                Thread.Sleep(10);

            }

            this.ProgressBarPanel.Visible = false;
        }

        #endregion
        
    }
}
