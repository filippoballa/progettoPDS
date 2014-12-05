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
            //Sconnection.StartListening();
            //PopUpShow();
    
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
            PopUpShow(null,null);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
            PopUpShow(null,null);
            
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            Sconnection.StartListening();
        }

        private void PopUpShow(object sender, EventArgs e)
        {
            //int border = PopUpWindow.border;
            this.top.Show(0,0);
            //this.right.Show(new Point(Screen.PrimaryScreen.Bounds.Width - border, border));
            //this.left.Show(new Point(0, border));
            //this.bottom.Show(new Point(0, Screen.PrimaryScreen.Bounds.Height - border));
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Minimized;

            // Activate the form.
            this.Activate();
            PopUpShow(null, null);
        }

    }
}
