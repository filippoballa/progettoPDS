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
            MessageBox.Show("Benvenuto " + u.Name + " " + u.Surname + "!");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1000);
            this.ShowInTaskbar = false;
            this.Sconnection = new SocketConnection(port);
            this.Text+=" -IP : "+Sconnection.GetMyIp();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
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
            if (this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;
            //Show the form
            this.Show();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;
            //Show the form
            this.Show();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (Sconnection.StartListening())
                PopUpShow(null, null);
        }

        private void PopUpShow(object sender, EventArgs e)
        {
            //int border = PopUpWindow.border;
            this.top.Show(0,0);
            //this.right.Show(new Point(Screen.PrimaryScreen.Bounds.Width - border, border));
            //this.left.Show(new Point(0, border));
            //this.bottom.Show(new Point(0, Screen.PrimaryScreen.Bounds.Height - border));
        }

    }
}
