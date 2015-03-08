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
using System.Runtime.InteropServices;

namespace ProgettoPDS_SERVER
{
    public partial class MainForm : Form
    {
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
            notifyIcon1.ShowBalloonTip(1000);
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

            this.notifyIcon1.ShowBalloonTip(2);
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

                this.notifyIcon1.ShowBalloonTip(2);

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

        private void PacketsHandlerbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] data;
            String comando;
            bool work = true;
            while (work)//ricevo i pachetti del client 
            {
                try
                {
                    data = new byte[45];
                    Sconnection.Passiv.Receive(data);//password ricevuta
                    comando = Encoding.ASCII.GetString(data);
                    comando = comando.Substring(0, comando.IndexOf('\0'));
                    Thread t = null;

                    String[] Data = comando.Split(ApplicationConstants.SEPARATOR);

                    if (Data[0] == ApplicationConstants.MOUSECODE)
                    {
                        t = new Thread(MainForm.MouseThreadProc);
                    }
                    if (Data[0] == ApplicationConstants.KEYBOARDCODE)
                    {
                        t = new Thread(MainForm.KeyBoardThreadProc);
                    }
                    if (Data[0] == ApplicationConstants.CLIPBOARDCODE)
                    {
                        t = new Thread(MainForm.ClipBoardThreadProc);
                    }

                    if(t!=null)
                        t.Start(Data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    DialogResult res = MessageBox.Show("Errore critico, vuoi chiudere la connessione?","ECCEZIONE",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);

                    if (res == DialogResult.OK)
                    {
                        Sconnection.SockDisconnect();
                        work = false;
                    }
                    
                }
            }
        }

        public static void MouseThreadProc(object data)
        {
            double X = 0.0, Y = 0.0;
            String[] MouseData = data as String[];

            if (MouseData[2] != null)
                X = Convert.ToDouble(MouseData[2], NumberFormatInfo.CurrentInfo);
            if (MouseData[3] != null)
                Y = Convert.ToDouble(MouseData[3], NumberFormatInfo.CurrentInfo);

            int PosX = (int)X * Screen.PrimaryScreen.WorkingArea.Width;
            int PosY = (int)Y * Screen.PrimaryScreen.WorkingArea.Height;

            SetCursorPos(PosX, PosY);
        }
        public static void KeyBoardThreadProc(object data)
        { }
        public static void ClipBoardThreadProc(object data)
        { }

        [DllImport("user32.dll",  CallingConvention=CallingConvention.Cdecl)]
        static extern bool SetCursorPos(int X, int Y);

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sconnection.DrawBorders();
        }

    }
}
