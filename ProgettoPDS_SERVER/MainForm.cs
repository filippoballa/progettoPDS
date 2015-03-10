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
        
        private SocketConnection Sconnection = null;
        const int port = 2000;
        //PopUpWindow top = new PopUpWindow(new Panel(),"t");

        public MainForm(User u)
        {
            InitializeComponent();
            this.Text = "SERVER - "+ u.Username;
            MessageBox.Show("Bentornato " + u.Name + " " + u.Surname + "!","BENTORNATO!",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.ShowBalloonTip(1000);
            this.ShowInTaskbar = false;

            if (this.Sconnection==null)
                this.Sconnection = new SocketConnection(port);

            this.Text+=" - IP : "+Sconnection.GetMyIp();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.contextMenuStrip.Show(Cursor.Position);
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            Sconnection.StartListening(this);

            this.notifyIcon1.BalloonTipText = "In attesa di connessioni dal Client";
            this.notifyIcon1.BalloonTipTitle = "INFO STATO";
            this.notifyIcon1.ShowBalloonTip(1000);
        }

        /*public void PopUpShow(object sender, EventArgs e)
        {
            //this.top.Show(0,0);
        }
        public void PopUpHide(object sender, EventArgs e)
        {
            //this.top.Hide();
        }*/

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Sconnection.SockDisconnect();          
        }

        private void MainFormClose(object sender, EventArgs e)
        {
                this.Close();
        }

        private void MenuClose(object sender, EventArgs e)
        {
            this.contextMenuStrip.Hide();
        }

        private void ChangeCursor(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void MainFormShow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;          
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Vuoi chiudere l'applicazione?", "CHIUSURA IN CORSO", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (res == DialogResult.OK)
            {
                e.Cancel = false;

                this.notifyIcon1.BalloonTipText = "Applicazione in Back Ground.";
                this.notifyIcon1.BalloonTipTitle = "INFO STATO";
                this.notifyIcon1.ShowBalloonTip(1000);
            }
               
            else
                e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
        }

        private void toolStripMenuItemLogOut_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Vuoi fare Log Out?", "LOG OUT IN CORSO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (res == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sconnection.DrawBorders();
        }

#region async recive 
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
                        ThreadHandler.mr.WaitOne();

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
                            ThreadHandler.mr.Reset();
                    }
                }
  
            }
            catch (Exception ex)
            {
                ExceptionManagement(ex);
            }
        }
#endregion
        #region sync recive

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
                            ThreadHandler.mr.WaitOne();

                        String s = Data[i];
                        String[] d = null;

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
                        if (t != null && d !=null )
                        {
                            t.Start(d);
                            ThreadHandler.ThreadCounter++;

                            if (ThreadHandler.ThreadCounter >= ThreadHandler.MAXTHREAD)
                                ThreadHandler.mr.Reset();
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
        private void ExceptionManagement(Exception ex)
        {
            MessageBox.Show(ex.Message);

            DialogResult res = MessageBox.Show("Errore critico, vuoi chiudere la connessione?", "ECCEZIONE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (res == DialogResult.OK)
            {
                Sconnection.SockDisconnect();
                work = false;
            }
        }
        private void EndBackgroundWorker()
        {
            PacketsHandlerbackgroundWorker.CancelAsync();
        }
    }
}
