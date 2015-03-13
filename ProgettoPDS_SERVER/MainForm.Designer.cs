namespace ProgettoPDS_SERVER
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpenConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConnetti = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisconnetti = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PacketsHandlerbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.labelStato = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Click per aprire il menù.";
            this.notifyIcon1.BalloonTipTitle = "SERVER AVVIATO";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Pronto . . .";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenConsole,
            this.toolStripMenuItemConnetti,
            this.toolStripMenuItemDisconnetti,
            this.toolStripMenuItemCloseForm,
            this.toolStripMenuItemCloseMenu,
            this.toolStripMenuItem1});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 158);
            // 
            // toolStripMenuItemOpenConsole
            // 
            this.toolStripMenuItemOpenConsole.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemOpenConsole.Image")));
            this.toolStripMenuItemOpenConsole.Name = "toolStripMenuItemOpenConsole";
            this.toolStripMenuItemOpenConsole.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemOpenConsole.Text = "Console";
            this.toolStripMenuItemOpenConsole.Click += new System.EventHandler(this.MainFormShow);
            // 
            // toolStripMenuItemConnetti
            // 
            this.toolStripMenuItemConnetti.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemConnetti.Image")));
            this.toolStripMenuItemConnetti.Name = "toolStripMenuItemConnetti";
            this.toolStripMenuItemConnetti.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemConnetti.Text = "Connetti";
            this.toolStripMenuItemConnetti.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // toolStripMenuItemDisconnetti
            // 
            this.toolStripMenuItemDisconnetti.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDisconnetti.Image")));
            this.toolStripMenuItemDisconnetti.Name = "toolStripMenuItemDisconnetti";
            this.toolStripMenuItemDisconnetti.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemDisconnetti.Text = "Disconnetti";
            this.toolStripMenuItemDisconnetti.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // toolStripMenuItemCloseForm
            // 
            this.toolStripMenuItemCloseForm.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCloseForm.Image")));
            this.toolStripMenuItemCloseForm.Name = "toolStripMenuItemCloseForm";
            this.toolStripMenuItemCloseForm.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemCloseForm.Text = "Chiudi Menù";
            this.toolStripMenuItemCloseForm.Click += new System.EventHandler(this.MenuClose);
            // 
            // toolStripMenuItemCloseMenu
            // 
            this.toolStripMenuItemCloseMenu.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCloseMenu.Image")));
            this.toolStripMenuItemCloseMenu.Name = "toolStripMenuItemCloseMenu";
            this.toolStripMenuItemCloseMenu.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemCloseMenu.Text = "Chiudi Applicazione";
            this.toolStripMenuItemCloseMenu.Click += new System.EventHandler(this.MainFormClose);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "disegna";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // PacketsHandlerbackgroundWorker
            // 
            this.PacketsHandlerbackgroundWorker.WorkerReportsProgress = true;
            this.PacketsHandlerbackgroundWorker.WorkerSupportsCancellation = true;
            this.PacketsHandlerbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PacketsHandlerbackgroundWorker_DoWork);
            // 
            // labelStato
            // 
            this.labelStato.AutoSize = true;
            this.labelStato.Location = new System.Drawing.Point(277, 9);
            this.labelStato.Name = "labelStato";
            this.labelStato.Size = new System.Drawing.Size(85, 13);
            this.labelStato.TabIndex = 1;
            this.labelStato.Text = "DISCONNESSO";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(374, 132);
            this.Controls.Add(this.labelStato);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenConsole;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnetti;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisconnetti;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseMenu;
        protected internal System.ComponentModel.BackgroundWorker PacketsHandlerbackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        protected internal System.Windows.Forms.Label labelStato;
    }
}