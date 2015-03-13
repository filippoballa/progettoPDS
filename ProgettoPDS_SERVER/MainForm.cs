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

namespace ProgettoPDS_SERVER
{
    public partial class MainForm : Form
    {
        static bool work = true;
        //public delegate void Handler(Cursor c);
        //public Handler myHandler;
        //PopUpWindow top = new PopUpWindow(new Panel(),"t");
        private SocketConnection Sconnection = null;
        private static int port = 2000;
        public int Port { get { return port; } set { port = value; } }
        private const int toolTipTimeOut = 200;
        public int ToolTipTimeOut { get { return toolTipTimeOut; } }
        

        public MainForm(User u)
        {
            InitializeComponent();

            //aggancio l'Handler per poter fare una safe invoke
            //this.myHandler = new Handler(ChangeCursor);

            //scrivo il nome dell'utente nel form
            this.Text = "SERVER - "+ u.Username;
            //avvio un message box di benvenuto
            MessageBox.Show("Bentornato " + u.Name + " " + u.Surname + "!","BENTORNATO!",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //lancio la notify icon
            this.notifyIcon1.ShowBalloonTip(ToolTipTimeOut);
            //rendo il form invisibile
            this.ShowInTaskbar = false;

            //inizializzo la classe per gestire la connessione
            if (this.Sconnection==null)
                this.Sconnection = new SocketConnection(Port);

            //aggiungo nel form l'IP corrente
            this.Text+=" - IP : " + Sconnection.GetMyIp();
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

        /*public void PopUpShow(object sender, EventArgs e)
        {
            //this.top.Show(0,0);
        }
        public void PopUpHide(object sender, EventArgs e)
        {
            //this.top.Hide();
        }*/

        /// <summary>
        /// Click sul pulsante 'Disconnetti' del menu: chiude la connessione.
        /// </summary>
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            EndBackgroundWorker();          
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

        //non so se serve ??
        private void ChangeCursor(Cursor c )
        {
            this.Cursor = c;
        }


        /// <summary>
        /// Click sul pulsante 'Console' del menu: mostra il form che funge da console.
        /// </summary>
        private void MainFormShow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;          
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

        #region async recive [DEPRECATED]
        private void PacketsHandlerbackgroundWorker_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            byte[] data;
            work = true;

            while (work)//ricevo i pachetti del client 
            {
                try
                {
                    data = new byte[128];
                    ApplicationConstants.Data StructData;

                    StructData.sock = Sconnection.Passiv;
                    StructData.data = data;
                    StructData.BufferSize = data.Length;

                    Sconnection.Passiv.BeginReceive(data, 0, data.Length, 0, new AsyncCallback(ReceiveCallaback), StructData);
                }
                catch (Exception ex)
                {
                    ExceptionManagement(ex);  
                }
            }
        }

        private void ReceiveCallaback(IAsyncResult ar)
        {
            String comandi;
            try
            {
                ApplicationConstants.Data StructData = (ApplicationConstants.Data) ar.AsyncState;

                int bytesRead = StructData.sock.EndReceive(ar);

                //ricevo i dati come stream di byte quindi devo ricostruirmi i caratteri ricevuti e poi 'splittarli'
                comandi = Encoding.ASCII.GetString(StructData.data);
                comandi = comandi.Substring(0, comandi.IndexOf('\0'));
                
                String[] Data = comandi.Split(ApplicationConstants.SEPARATOR);

                //il ciclo for serve a capire se ho ricevuto più comandi in un solo invio
                for(int i = 0; i<Data.Length; i++)
                {
                    Thread t = null;
                    //controllo il counter dei thread, se ne ho già lanciati MAXTHREAD mi metto in attesa
                    while (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                        ThreadHandler.mrMaxThreads.WaitOne();

                    String s = Data[i];
                    String[] d;

                    //caso ricezione mouse
                    if (s == ApplicationConstants.MOUSECODE)
                    {
                        d = new String[4];
                        d[0] = s;
                        d[1] = Data[i + 1];
                        d[2] = Data[i + 2];
                        d[3] = Data[i + 3];

                        i += 3;

                        t = new Thread(ThreadHandler.MouseThreadProc);
                    }
                    //caso ricezione keyboard
                    else if (s == ApplicationConstants.KEYBOARDCODE)
                    {
                        d = new String[3];
                        d[0] = s;
                        d[1] = Data[i + 1];
                        d[2] = Data[i + 2];

                        i += 2;

                        t = new Thread(ThreadHandler.KeyBoardThreadProc);
                    }
                    //caso ricezione cliboard
                    else if (s == ApplicationConstants.CLIPBOARDCODE)
                    { 
                        //da gestire la costruzione del pacchetto
                        d = new String[1];
                        d[0] = s;

                        t = new Thread(ThreadHandler.ClipBoardThreadProc);
                    }

                    //se è stato creato un thread lo lancio e aggiorno il counter e la condition variable se ho lanciato il 20esimo
                    if (t != null)
                    {
                        t.Start(Data);
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

                            ThreadHandler.Queue.Enqueue(d);

                            if (ThreadHandler.Queue.Count == 1)
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

                            t = new Thread(ThreadHandler.KeyBoardThreadProc);
                        }
                        //caso ricezione cliboard
                        else if (s == ApplicationConstants.CLIPBOARDCODE)
                        {
                            //da gestire la costruzione del pacchetto
                            d = new String[1];
                            d[0] = s;

                            t = new Thread(ThreadHandler.ClipBoardThreadProc);
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
                EndBackgroundWorker();
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

        public void ChangeLabelStato(string stato)
        {
            this.labelStato.Text = stato;
        }
    }
}
