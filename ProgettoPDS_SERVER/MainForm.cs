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

namespace ProgettoPDS_SERVER
{
    public partial class MainForm : Form
    {
        private SocketConnection Sconnection;
        const int port = 2000;
        PopUpWindow top = new PopUpWindow(new Panel(),"t");
        PopUpWindow right = new PopUpWindow(new Panel(), "r");
        PopUpWindow left = new PopUpWindow(new Panel(), "l");
        PopUpWindow bottom = new PopUpWindow(new Panel(), "b");

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
            this.Sconnection = new SocketConnection(port);
            this.Text+=" - IP : "+Sconnection.GetMyIp();
        }

        /*private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Hide the form.
            this.Hide();
        }
        private void MainForm_DoubleClicked(object sender, EventArgs e)
        {
            //Show the form
            this.Hide();
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                if (this.WindowState != FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Normal;
                }

                this.Show();
            }
           
        }*/

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

        public void PopUpShow(object sender, EventArgs e)
        {
            //int border = PopUpWindow.border;
            this.top.Show(0,0);
            //this.right.Show(new Point(Screen.PrimaryScreen.Bounds.Width - border, border));
            //this.left.Show(new Point(0, border));
            //this.bottom.Show(new Point(0, Screen.PrimaryScreen.Bounds.Height - border));
        }
        public void PopUpHide(object sender, EventArgs e)
        {
            //int border = PopUpWindow.border;
            this.top.Hide();
            //this.right.Show(new Point(Screen.PrimaryScreen.Bounds.Width - border, border));
            //this.left.Show(new Point(0, border));
            //this.bottom.Show(new Point(0, Screen.PrimaryScreen.Bounds.Height - border));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sconnection.SockDisconnect();
            PopUpHide(null, null);
            
        }

        private void MainFormClose(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Sei sicuro di voler chiudere l'applicazione?", "CHIUSURA IN CORSO", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            
            if(res == DialogResult.OK)
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

    }
}
