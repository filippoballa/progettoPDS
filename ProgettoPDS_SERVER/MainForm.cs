﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgettoPDS_SERVER
{
    public partial class MainForm : Form
    {
        private SocketConnection Sconnection;
        const int port = 2000;

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
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            Sconnection.StartListening();
        }
    }
}
