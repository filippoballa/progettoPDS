using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;

namespace ProgettoPDS_SERVER
{
    public partial class MainForm : Form
    {
        //attributi

        static bool work = true;
        private const int minheight = 160;
        private const int maxheight = 250;
        private SocketConnection Sconnection = null;
        private static int port = 2000;
        public int Port { get { return port; } set { port = value; } }
        private const int toolTipTimeOut = 2;
        public int ToolTipTimeOut { get { return toolTipTimeOut; } }
        private User u = null;
        private const int passwordlenght = 8;

        //delegate per le modifiche allo stato nel form da parte di altri thread
        public delegate void LabelStatoChanged(ApplicationConstants.Stato stato, string client);
        public LabelStatoChanged LabelStatoChangedDelegate;
        
        public MainForm(User u)
        {
            //delegate
            LabelStatoChangedDelegate = new LabelStatoChanged(LabelStatoChangedMethod);

            InitializeComponent();

            this.Height = minheight;
            this.u = u;
            //scrivo il nome dell'utente nel form
            this.Text = "SERVER - "+ u.Username;
          
            //stato clipBoard
            ClipboardChanghed();

            //lancio la notify icon
            this.notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "SERVER AVVIATO", "Benvenuto "+u.Name+" "+u.Surname, ToolTipIcon.Info);
            //rendo il form invisibile
            this.ShowInTaskbar = false;

            //inizializzo la classe per gestire la connessione
            if (this.Sconnection == null)
                this.Sconnection = new SocketConnection(Port);

            //aggiungo nel form l'IP corrente
            this.Text += " - IP : " + Sconnection.GetMyIp();
        }

        private void ClipboardChanghed()
        {
             // Retrieves the data from the clipboard.
            ApplicationConstants.StatoClipBoard s = ApplicationConstants.StatoClipBoard.VUOTA;
          
            if(Clipboard.ContainsAudio())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
            }
            else if(Clipboard.ContainsImage())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
            }
            else if(Clipboard.ContainsText())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
            }
            else if(Clipboard.ContainsFileDropList())
            {
                s = ApplicationConstants.StatoClipBoard.PIENA;
            }
                
            this.labelClipboardState.Text = s.ToString();

            if(s==ApplicationConstants.StatoClipBoard.VUOTA)
                this.labelClipboardState.ForeColor = Color.DarkRed;
            else
                this.labelClipboardState.ForeColor = Color.LawnGreen;
        }

        /// <summary>
        /// Click sulla Notify Icon: mostra il menu.
        /// </summary>
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.contextMenuStrip.Show(Cursor.Position);
        }

        /// <summary>
        /// Click sul pulsante 'Connetti' del menu: avvia la connessione.
        /// </summary>
        private void button_connect_Click(object sender, EventArgs e)
        {
            Sconnection.StartListening(this);

            notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "In attesa di connessioni dal Client.", ToolTipIcon.Info);
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
                if (Sconnection.Stato == ApplicationConstants.Stato.DISCONNESSO)
                    notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Deve ancora essere creata una connessione.", ToolTipIcon.Warning);
                else
                    notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Eri ancora in attesa di connessioni.", ToolTipIcon.Warning);

                Thread.Sleep(1000);
                Sconnection.Stato = ApplicationConstants.Stato.DISCONNESSO;
            } 
        }

        /// <summary>
        /// Click sul pulsante 'Chiudi Applicazione' del menu: chiude l'applicazione.
        /// </summary>
        private void MainFormClose(object sender, EventArgs e)
        {
               this.Close();
        }

        /// <summary>
        /// Click sul pulsante 'Chiudi Menu' del menu: nasconde il menu.
        /// </summary>
        private void MenuClose(object sender, EventArgs e)
        {
            this.contextMenuStrip.Hide();
        }

        /// <summary>
        /// Click sul pulsante 'Console' del menu: mostra il form che funge da console.
        /// </summary>
        private void MainFormShow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// Metodo chiamato quando il form si sta chiudendo, da la possibilita di chiudere oppure no.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Vuoi chiudere l'applicazione?", "CHIUSURA IN CORSO", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (res == DialogResult.OK)
            {
                e.Cancel = false;
                notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Applicazione in Back Ground.", ToolTipIcon.Info);
            }   
            else
                e.Cancel = true;

            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// Click sul pulsante 'disegna' del menu: disegna un bordo.
        /// </summary>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sconnection.DrawBorders();
        }

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
                        String[] d = null;

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

                            ThreadHandler.MouseQueue.Enqueue(d);

                            //se il pacchetto è l'unico in coda creao il thread
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

                            ThreadHandler.KeyBoardQueue.Enqueue(d);

                            //se il pacchetto è l'unico in coda creao il thread
                            if (ThreadHandler.KeyBoardQueue.Count == 1)
                            {
                                t = new Thread(ThreadHandler.KeyBoardThreadProc);
                            }
                            
                        }
                        //caso ricezione cliboard
                        else if (s == ApplicationConstants.CLIPBOARDCODE)
                        {
                            //[C]-[EVENTKEY]
                            d = new String[2];
                            d[0] = s;
                            d[1] = d[1] = Data[i + 1]; ;

                            i += 1;

                            t = new Thread(ThreadHandler.ClipBoardThreadProc);
                        }
                        else if(s==ApplicationConstants.QUITCODE)
                        {
                            work = false;
                            break;
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

        #endregion
        /// <summary>
        /// Gestione di un'eccezione. Messaggio di errore e possibilita' di chiudere la connessione.
        /// </summary>
        private void ExceptionManagement(Exception ex)
        {
            DialogResult res = MessageBox.Show("\""+ex.Message+"\" Errore critico, vuoi chiudere la connessione?", "ECCEZIONE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (res == DialogResult.OK)
            {
                //EndBackgroundWorker();
                work = false;
            }
        }
        /// <summary>
        /// Chiusura del PacketsHandlerbackgroundWorker, disconnessione e terminazione BW.
        /// </summary>
        private void EndBackgroundWorker()
        {
            Sconnection.SockDisconnect();

            if(PacketsHandlerbackgroundWorker.IsBusy)
                PacketsHandlerbackgroundWorker.CancelAsync();
        }
        /// <summary>
        /// Metodo per cambiare lo 'stato' della connessione
        /// </summary>
        /// <param name="stato"></param>
        /// <param name="client"></param>
        public void LabelStatoChangedMethod(ApplicationConstants.Stato stato, string client)
        {
            this.labelStato.Text = stato.ToString();

            if (stato == ApplicationConstants.Stato.CONNESSO)
            {
                this.labelStato.ForeColor = Color.LawnGreen;
                this.labelConnectedClient.Text = client;
            }
            else if (stato == ApplicationConstants.Stato.IN_ATTESA)
            {
                this.labelStato.ForeColor = Color.Black;
                this.labelConnectedClient.Text = "-";
            }  
            else
            {
                this.labelStato.ForeColor = Color.DarkRed;
                this.labelConnectedClient.Text = "-";
            }
 
        }
        /// <summary>
        /// Set della nuova porta di ascolto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPort_Click(object sender, EventArgs e)
        {
            this.Port = (int)this.numericUpDownPort.Value;

            //inizializzo la classe per gestire la connessione
            if (this.Sconnection.Stato == ApplicationConstants.Stato.DISCONNESSO)
            {
                this.Sconnection = new SocketConnection(Port);
                PanelSetPort.Visible = false;
                this.Height = minheight;
                notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Porta cambiata con successo!", ToolTipIcon.Info);
            }
            else
                notifyIcon1.ShowBalloonTip(ToolTipTimeOut, "INFO STATO", "Impossibile cambiare la porta, connessione in corso.", ToolTipIcon.Warning);

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

        private void impostaPortaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(panelChangePassword.Visible)
            {
                panelChangePassword.Visible = false;
            }
            else
            {
                this.Height = maxheight;
            }
            this.PanelSetPort.Visible = true;    
        }

        private void buttonClosePanelSetPort_Click(object sender, EventArgs e)
        {
            PanelSetPort.Visible = false;
            this.Height = minheight;
        }
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
            panelChangePassword.Visible = true;
        }

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

        private void infoDatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Retrieves the data from the clipboard.
            IDataObject d = System.Windows.Forms.Clipboard.GetDataObject();
            if(d != null)
            {
                if(Clipboard.ContainsAudio())
                { }
                else if(Clipboard.ContainsImage())
                { }
                else if(Clipboard.ContainsText())
                { }
                else if(Clipboard.ContainsFileDropList())
                { }
                else
                { 
                    //formato sconosciuto
                }
            }
            else
            {
                //clipboard vuota
            }
        }

        private void pulisciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            ClipboardChanghed();
        }
    }
}
