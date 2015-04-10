using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace ProgettoPDS_SERVER
{
    public partial class MainForm : Form
    {
        #region Attributes

        static bool work = true;
        private const int minheight = 165;
        private const int maxheight = 250;
        private SocketConnection Sconnection = null;

        private int port = 2000;
        private string client;
        public int Port { get { return port; } set { port = value; } }
        private const int toolTipTimeOut = 1;
        public int ToolTipTimeOut { get { return toolTipTimeOut; } }
        private User u = null;
        private const int passwordlenght = 8;
        private static System.Media.SoundPlayer player;
        Graphics g;
        #endregion

        #region Delegates per le modifiche del form da parte di altri thread
        public delegate void LabelStatoChanged(ApplicationConstants.Stato stato, string client);
        public LabelStatoChanged LabelStatoChangedDelegate;
        public delegate void closeProgressBar();
        public closeProgressBar closeProgressBarDelegate;
        public delegate void startProgressBar(int max);
        public startProgressBar startProgressBarDelegate;
        public delegate void doStepProgressBar();
        public doStepProgressBar doStepProgressBarDelegate;
        public delegate void SetCliboardData(string statoCB, object data);
        public SetCliboardData SetCliboardDataDelegate;
        public delegate object GetCliboardData();
        public GetCliboardData GetCliboardDataDelegate;
        public delegate void DrawBorders(Brush color);
        public DrawBorders DrawBordersDelegate;
        public delegate void ClipboardChanghed();
        public ClipboardChanghed ClipboardChanghedDelegate;
        #endregion

        #region Constructor
        public MainForm(User u)
        {
            //delegate
            LabelStatoChangedDelegate = new LabelStatoChanged(LabelStatoChangedMethod);
            closeProgressBarDelegate = new closeProgressBar(closeProgressBarMethod);
            startProgressBarDelegate = new startProgressBar(startProgressBarMethod);
            doStepProgressBarDelegate = new doStepProgressBar(doStepProgressBarMethod);
            SetCliboardDataDelegate = new SetCliboardData(SetCliboardDataMethod);
            GetCliboardDataDelegate = new GetCliboardData(GetClipboardDataMethod);
            DrawBordersDelegate = new DrawBorders(DrawBordersMethod);
            ClipboardChanghedDelegate = new ClipboardChanghed(ClipboardChanghedMethod);
            
            InitializeComponent();

            this.Height = minheight;
            this.u = u;
            //scrivo il nome dell'utente nel form
            this.Text = "SERVER - "+ u.Username;

            //stato clipBoard
            ClipboardChanghedMethod();

            //lancio la notify icon
            this.notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "SERVER AVVIATO", "Benvenuto "+u.Name+" "+u.Surname, ToolTipIcon.Info);
            //rendo il form invisibile
            this.ShowInTaskbar = false;

            //inizializzo la classe per gestire la connessione
            if (Sconnection == null)
                Sconnection = new SocketConnection(Port);

            //aggiungo nel form l'IP corrente
            this.Text += " - IP : " + Sconnection.GetMyIp();
        }

        #endregion

        #region Connessione
        /// <summary>
        /// Click sul pulsante 'Connetti' del menu: avvia la connessione.
        /// </summary>
        private void button_connect_Click(object sender, EventArgs e)
        {
            if (labelStato.Text != ApplicationConstants.Stato.CONNESSO.ToString())
            {
                Sconnection.StartListening(this);

                notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "In attesa di connessioni dal Client.", ToolTipIcon.Info);
            } 
        }

        /// <summary>
        /// Click sul pulsante 'Disconnetti' del menu: chiude la connessione.
        /// </summary>
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (PacketsHandlerbackgroundWorker.IsBusy)
                work = false;
            else
            {
                Sconnection.SockDisconnect();
            } 
        }

        /// <summary>
        /// Metodo per cambiare lo 'stato' della connessione
        /// </summary>
        /// <param name="stato"></param>
        /// <param name="client"></param>
        public void LabelStatoChangedMethod(ApplicationConstants.Stato stato, string client)
        {
            pictureBoxLoader.Visible = false;
            this.client = client;
            this.labelStato.Text = stato.ToString();

            if (stato == ApplicationConstants.Stato.CONNESSO)
            {
                DrawBordersMethod(Brushes.Red);
                this.labelStato.ForeColor = Color.LawnGreen;
                this.labelConnectedClient.Text = client;
            }
            else if (stato == ApplicationConstants.Stato.IN_ATTESA)
            {
                DrawBordersMethod(Brushes.Yellow);
                this.labelStato.ForeColor = Color.MediumTurquoise;
                this.labelConnectedClient.Text = "-";
                pictureBoxLoader.Visible = true;
            }
            else
            {
                DrawBordersMethod(Brushes.Transparent);
                this.labelStato.ForeColor = Color.DarkRed;
                this.labelConnectedClient.Text = "-";
            }

        }
        #endregion
       
        #region Main Form
        /// <summary>
        /// Metodo chiamato quando il form si sta chiudendo, da la possibilita di chiudere oppure no.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ApplicationConstants.RES != DialogResult.Yes)
            {
                DialogResult res = MessageBox.Show("Vuoi chiudere l'applicazione?", "CHIUSURA IN CORSO", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (res == DialogResult.OK)
                {
                    e.Cancel = false;
                    notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Applicazione in Back Ground.", ToolTipIcon.Info);
                }
                else
                {
                    e.Cancel = true;
                }

                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            else
            {
                if (PacketsHandlerbackgroundWorker.IsBusy)
                    work = false;
                else
                    Sconnection.SockDisconnect();
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
            Clipboard.Clear();
        }
        /// <summary>
        /// Click sul pulsante 'Chiudi Applicazione' del menu: chiude l'applicazione.
        /// </summary>
        private void MainFormClose(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click sul pulsante 'Console' del menu: mostra il form che funge da console.
        /// </summary>
        private void MainFormShow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        #endregion

        #region PacketsHandler backgroundWorker

        /// <summary>
        /// Work del PacketsHandlerbackgroundWorker, gestione dei pacchetti ricevuti e smistamento nei thread corrispondenti alla funzionalità(mouse, keyboard, clipboard).
        /// </summary>
        private void PacketsHandlerbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] data;
            work = true;

            while (work)//ricevo i pachetti del client 
            {
                try
                {
                    data = new byte[128];
                    Sconnection.Passiv.Receive(data);

                    String comandi;

                    //ricevo i dati come stream di byte quindi devo ricostruirmi i caratteri ricevuti e poi 'splittarli'
                    comandi = Encoding.ASCII.GetString(data);
                    comandi = comandi.Substring(0, comandi.IndexOf('\0'));

                    String[] Data = comandi.Split(ApplicationConstants.SEPARATOR);

                    //il ciclo for serve a capire se ho ricevuto più comandi in un solo invio
                    for (int i = 0; i < Data.Length; i++)
                    {
                        Thread t = null;
                        //controllo il counter dei thread, se ne ho già lanciati MAXTHREAD mi metto in attesa
                        while (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                            ThreadHandler.mrMaxThreads.WaitOne();

                        String s = Data[i];
                        object[] d = null;

                        //caso ricezione mouse
                        if (s == ApplicationConstants.MOUSECODE)
                        {
                            //[M]-[EVENTKEY]-[X]-[Y]
                            d = new String[4];
                            d[0] = s;
                            d[1] = Data[i + 1];
                            d[2] = Data[i + 2];
                            d[3] = Data[i + 3];

                            i += 3;

                            ThreadHandler.MouseQueue.Enqueue((String[])d);

                            //se il pacchetto è l'unico in coda creo il thread
                            if (ThreadHandler.MouseQueue.Count == 1)
                            {
                                t = new Thread(ThreadHandler.MouseThreadProc);
                            }
                        }
                        //caso ricezione keyboard
                        else if (s == ApplicationConstants.KEYBOARDCODE)
                        {
                            //[K]-[EVENTKEY]-[KEYCODE]
                            d = new String[3];
                            d[0] = s;
                            d[1] = Data[i + 1];
                            d[2] = Data[i + 2];

                            i += 2;

                            ThreadHandler.KeyBoardQueue.Enqueue((String[])d);

                            //se il pacchetto è l'unico in coda creo il thread
                            if (ThreadHandler.KeyBoardQueue.Count == 1)
                            {
                                t = new Thread(ThreadHandler.KeyBoardThreadProc);
                            }
                            
                        }
                        //caso ricezione cliboard
                        else if (s == ApplicationConstants.CLIPBOARDCODE)
                        {
                            //[C]-[EVENTKEY]
                            d = new object[4];
                            d[0] = s;
                            d[1] = d[1] = Data[i + 1];
                            //campi aggiuntivi per passare dati diversi da string al thread della clipboard 
                            this.Invoke(ClipboardChanghedDelegate);
                            d[2] = this;
                            d[3] = this.Sconnection;
                            
                            i += 1;

                            if((string)d[1]==ApplicationConstants.CLIPQUITCODE)//ho ricevuto un messaggio 'CLOSE' sulla clipboard
                            {
                                ThreadHandler.ClipBoardWork = false;
                                MessageBox.Show("Chiusura trasferimento in corso da parte del client", "CHIUSURA IN CORSO");
                            }
                            else
                            {
                                ThreadHandler.ClipBoardWork = true;
                                t = new Thread(ThreadHandler.ClipBoardThreadProc);
                            }
                            
                        }
                        else if(s==ApplicationConstants.QUITCODE)
                        {
                            work = false;
                            break;
                        }
                        else if(s==ApplicationConstants.START)
                        {
                            //creo stato controllato
                            DrawBordersMethod(Brushes.LawnGreen);
                        }
                        else if (s == ApplicationConstants.END)
                        {
                            //creo stato non controllato
                            DrawBordersMethod(Brushes.Red);
                        }

                        //se è stato creato un thread lo lancio e aggiorno il counter e la condition variable se ho lanciato il 20esimo
                        if (t != null && d !=null )
                        {
                            t.Start(d);
                            ThreadHandler.ThreadCounter++;

                            if (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                                ThreadHandler.mrMaxThreads.Reset();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManagement(ex);
                }
            }
            EndBackgroundWorker();
        }
        /// <summary>
        /// Chiusura del PacketsHandlerbackgroundWorker, disconnessione e terminazione BW.
        /// </summary>
        private void EndBackgroundWorker()
        {
            Sconnection.SockDisconnect();

            if (PacketsHandlerbackgroundWorker.IsBusy)
                PacketsHandlerbackgroundWorker.CancelAsync();
        }
        #endregion
      
        #region Altre funzioni
        /// <summary>
        /// Click sul pulsante 'Chiudi Menu' del menu: nasconde il menu.
        /// </summary>
        private void MenuClose(object sender, EventArgs e)
        {
            this.contextMenuStrip.Hide();
        }
        /// <summary>
        /// Nasconde il form e non lo mostra nella task bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBoxNascondi_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Applicazione in Back Ground.", ToolTipIcon.Info);
            this.ShowInTaskbar = true; 
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false; 
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationConstants.RES = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// Click sulla Notify Icon: mostra il menu.
        /// </summary>
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.contextMenuStrip.Show(Cursor.Position);
        }
        #endregion

        #region Porta

        /// <summary>
        /// Set della nuova porta di ascolto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPort_Click(object sender, EventArgs e)
        {
            if ((int)this.numericUpDownPort.Value != SocketConnection.trasfport)
            {
                this.Port = (int)this.numericUpDownPort.Value;

                //inizializzo la classe per gestire la connessione
                if (Sconnection.Stato == ApplicationConstants.Stato.DISCONNESSO)
                {
                    Sconnection = new SocketConnection(Port);
                    PanelSetPort.Visible = false;
                    this.Height = minheight;
                    notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Porta cambiata con successo!", ToolTipIcon.Info);
                }
                else
                    notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Impossibile cambiare la porta, connessione in corso.", ToolTipIcon.Warning);
            }
            else
            {
                MessageBox.Show("Impossibile usare la porta " + SocketConnection.trasfport + ", è dedicata alle operazioni sulla Clipboard!");
            }


        }
        //cambio porta
        private void impostaPortaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //port
            numericUpDownPort.Value = port;

            if (panelChangePassword.Visible)
            {
                panelChangePassword.Visible = false;
            }
            else
            {
                this.Height = maxheight;
            }
            this.PanelSetPort.Visible = true;
        }

        //chiusura pannello porta
        private void buttonClosePanelSetPort_Click(object sender, EventArgs e)
        {
            PanelSetPort.Visible = false;
            this.Height = minheight;
        }
        #endregion

        #region Password

        //Apertura pannello Password
        private void cambioPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PanelSetPort.Visible)
            {
                PanelSetPort.Visible = false;
            }
            else
            {
                this.Height = maxheight;
            }
            this.panelChangePassword.Visible = true;
        }

        //chiusura pannello password
        private void buttonClosePanelChangePassword_Click(object sender, EventArgs e)
        {
            panelChangePassword.Visible = false;
            this.Height = minheight;
        }
        /// <summary>
        /// metodo per cambiare la password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            if (textBoxVpassword.Text == "" )
            {
                 MessageBox.Show("Errore, inserisci la vecchia password!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(textBoxNpassword.Text == "")
            {
                 MessageBox.Show("Errore, inserisci la nuova password!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SHA1 shaM = new SHA1Managed();

                if( shaM.ComputeHash(Encoding.ASCII.GetBytes(textBoxVpassword.Text)) == Encoding.ASCII.GetBytes(u.Password))
                {
                    MessageBox.Show("Errore, la vecchia password non corrisponde!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                     if(textBoxNpassword.Text.Length > passwordlenght)
                     {
                        //imposta nuova password
                        XmlManager mng = new XmlManager('U');

                        if (mng.ModifyPwdUser(u.Username, textBoxNpassword.Text))
                        {
                            u.Password = shaM.ComputeHash(Encoding.ASCII.GetBytes(textBoxNpassword.Text)).ToString();
                            textBoxNpassword.Text = "";
                            textBoxVpassword.Text = "";
                            this.buttonClosePanelSetPort_Click(null, null);
                        }
                        else
                            MessageBox.Show("Errore, impossibile modificare la password!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Errore, inserire una password con almeno "+passwordlenght+" caratteri!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }
        #endregion

        #region Audio Player
        private void buttonPlayAudio_Click(object sender, EventArgs e)
        {
             System.IO.Stream aus = Clipboard.GetAudioStream();

                    player = new System.Media.SoundPlayer(aus);
                    try
                    {
                           player.Play();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
        }

        private void buttonStopAudio_Click(object sender, EventArgs e)
        {
            try
            {
                player.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ProgressBar
        public void startProgressBarMethod(int max)
        {
            buttonHideProg.Visible = true;
            panelChangePassword.Visible = false;
            PanelSetPort.Visible = false;
            this.Height = minheight + progressBarClipboard.Height;
           
            this.progressBarClipboard.Visible = true;

            progressBarClipboard.Maximum = max;
            progressBarClipboard.Minimum = 1;
            progressBarClipboard.Value = 1;
            progressBarClipboard.Step = 1;
        }
        public void doStepProgressBarMethod()
        {
            progressBarClipboard.PerformStep();
            int percent = (int)(((double)progressBarClipboard.Value / (double)progressBarClipboard.Maximum) * 100);
            progressBarClipboard.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBarClipboard.Width / 2 - 10, progressBarClipboard.Height / 2 - 7));
            
        }
        public void closeProgressBarMethod()
        {
            if(progressBarClipboard.Maximum>progressBarClipboard.Value)
            {
                for (int i = progressBarClipboard.Value; i <= progressBarClipboard.Maximum; i++)
                    progressBarClipboard.PerformStep();
            }

            this.Height = minheight;

            this.progressBarClipboard.Visible = false;
            buttonHideProg.Visible = false;
        }

        //chiude il trasferimento dei dati [non utilizzato]
        private void buttonCloseProg_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(" Vuoi davvero chiudere la trasmissione?", "CANCELLAZIONE OPERAZIONE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (res == DialogResult.OK)
            {
                ThreadHandler.ClipBoardWork = false;
            }
        }

        //nasconde la progressBar
        private void buttonHideShowProg_Click(object sender, EventArgs e)
        {
            if(this.progressBarClipboard.Visible)
            {
                buttonHideProg.BackgroundImage = new Bitmap(@"..\..\images\darrow.png");
                this.progressBarClipboard.Visible = false;
                this.Height = minheight;
            }
            else
            {
                buttonHideProg.BackgroundImage = new Bitmap(@"..\..\images\uarrow.png");
                this.Height = minheight + progressBarClipboard.Height;
                this.progressBarClipboard.Visible = true;
            }   
        }
        #endregion

        #region Clipboard
        // OTTENGO IL CONTENUTO DELLA CLIPBOARD
        private object GetClipboardDataMethod()
        {

            if (Clipboard.ContainsAudio())
            {
                return Clipboard.GetAudioStream();
            }
            else if (Clipboard.ContainsImage())
            {
                return Clipboard.GetImage();
            }
            else if (Clipboard.ContainsText())
            {
                return Clipboard.GetText();
            }
            else if (Clipboard.ContainsFileDropList())
            {
                return Clipboard.GetFileDropList();
            }

            return null;
        }

        // IMPOSTO IL CONTENUTO DELLA CLIPBOARD
        private void SetCliboardDataMethod(string statoCB, object data)
        {
            switch (statoCB)
            {

                case "AUDIO":
                    Clipboard.SetAudio((Stream)data);
                    break;
                case "IMMAGINE":
                    Clipboard.SetImage((Image)data);
                    break;
                case "TEXT":
                    Clipboard.SetText((string)data);
                    break;
                case "FILE_DROP":
                    Clipboard.SetFileDropList((System.Collections.Specialized.StringCollection)data);
                    break;
                default :
                    Clipboard.Clear();
                    break;    
            }
            ClipboardChanghedMethod();
            infoDatiToolStripMenuItem_Click(null, null);

        }

        //pulizia della Clipboard
        private void pulisciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            ClipboardChanghedMethod();
            infoDatiToolStripMenuItem_Click(null, null);
        }

        //chiusura pannello Info Clipboard
        private void buttonClosePanelInfoCB_Click(object sender, EventArgs e)
        {
            groupBoxInfo.Width += panelInfoCB.Width + groupBoxInfo.Margin.Left;
            panelInfoCB.Visible = false;
        }

        //apertura pannello Info ClipBoard
        private void infoDatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Retrieves the data from the clipboard.
            richTextBoxCB.Visible = false;
            pictureBoxCB.Visible = false;
            buttonPlayAudio.Visible = false;
            buttonStopAudio.Visible = false;

            IDataObject d = System.Windows.Forms.Clipboard.GetDataObject();
            if (d != null)
            {
                if (Clipboard.ContainsAudio())
                {
                    buttonPlayAudio.Visible = true;
                    buttonStopAudio.Visible = true;
                    labelTipoCB.Text = ApplicationConstants.StatoClipBoard.AUDIO.ToString();
                }
                else if (Clipboard.ContainsImage())
                {
                    pictureBoxCB.Visible = true;
                    labelTipoCB.Text = ApplicationConstants.StatoClipBoard.IMMAGINE.ToString();

                    Image i = Clipboard.GetImage();

                    pictureBoxCB.Image = i;
                }
                else if (Clipboard.ContainsText())
                {
                    richTextBoxCB.Visible = true;
                    labelTipoCB.Text = ApplicationConstants.StatoClipBoard.TEXT.ToString();
                    string text = Clipboard.GetText();

                    richTextBoxCB.Text = text;
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    richTextBoxCB.Visible = true;
                    labelTipoCB.Text = ApplicationConstants.StatoClipBoard.FILE_DROP.ToString();

                    System.Collections.Specialized.StringCollection s = Clipboard.GetFileDropList();

                    richTextBoxCB.Text = "";
                    for (int i = 0; i < s.Count; i++)
                    {
                        richTextBoxCB.Text += s[i] + Environment.NewLine;
                    }
                }
                else
                {
                    labelTipoCB.Text = ApplicationConstants.StatoClipBoard.VUOTA.ToString();
                }
            }
            else
            {
                //clipboard data object null
            }

            if (!panelInfoCB.Visible)
            {
                groupBoxInfo.Width -= panelInfoCB.Width + groupBoxInfo.Margin.Left;
                panelInfoCB.Visible = true;
            }

            ClipboardChanghedMethod();
        }

        //controllo stato clipboard
        private void ClipboardChanghedMethod()
        {
            // Controllo stato clipboard
            ApplicationConstants.StatoClipBoard s = ApplicationConstants.StatoClipBoard.VUOTA;
            labelTipoCB.Text = s.ToString();

            if (Clipboard.ContainsAudio())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
                labelTipoCB.Text = ApplicationConstants.StatoClipBoard.AUDIO.ToString();
            }
            else if (Clipboard.ContainsImage())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
                labelTipoCB.Text = ApplicationConstants.StatoClipBoard.IMMAGINE.ToString();
            }
            else if (Clipboard.ContainsText())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
                labelTipoCB.Text = ApplicationConstants.StatoClipBoard.TEXT.ToString();
            }
            else if (Clipboard.ContainsFileDropList())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
                labelTipoCB.Text = ApplicationConstants.StatoClipBoard.FILE_DROP.ToString();
            }

            this.labelClipboardState.Text = s.ToString();

            if (s == ApplicationConstants.StatoClipBoard.VUOTA)
                this.labelClipboardState.ForeColor = Color.DarkRed;
            else
                this.labelClipboardState.ForeColor = Color.LawnGreen;
        }
        #endregion

        #region Graphics
        public void DrawBordersMethod(Brush color)
        {
            IntPtr desktop = GetDC(IntPtr.Zero);

            if (color == Brushes.Transparent)
            {
                SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                g = Graphics.FromHdc(desktop);
  
                int border = 20;
                g.FillRectangle(Brushes.Black, (Screen.PrimaryScreen.Bounds.Width / 2) - border - 1, border - 1, border + 2, border + 2);
                g.FillRectangle(color, (Screen.PrimaryScreen.Bounds.Width / 2) - border, border, border, border);
 
            }
            ReleaseDC(IntPtr.Zero, desktop);
        }
        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll", EntryPoint = "ReleaseDC", SetLastError = true)]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        #endregion
 
/*
 *      Gestione di un'eccezione. Messaggio di errore e possibilita' di chiudere la connessione.
 */
        private void ExceptionManagement(Exception ex)
        {
            DialogResult res = MessageBox.Show("\"" + ex.Message + "\" Errore critico, vuoi chiudere la connessione?", "ECCEZIONE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (res == DialogResult.OK)
            {
                work = false;
            }
        }

        [DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
    }
    
}
