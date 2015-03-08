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
            this.toolStripMenuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PacketsHandlerbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Server Started . . .";
            this.notifyIcon1.BalloonTipTitle = "Click to Open the Controlo Panel";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Ready to connect . . .";
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
            this.toolStripMenuItemLogOut,
            this.toolStripMenuItemCloseForm,
            this.toolStripMenuItemCloseMenu});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(144, 136);
            this.contextMenuStrip.MouseEnter += new System.EventHandler(this.ChangeCursor);
            // 
            // toolStripMenuItemOpenConsole
            // 
            this.toolStripMenuItemOpenConsole.Name = "toolStripMenuItemOpenConsole";
            this.toolStripMenuItemOpenConsole.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemOpenConsole.Text = "Apri Console";
            this.toolStripMenuItemOpenConsole.Click += new System.EventHandler(this.MainFormShow);
            // 
            // toolStripMenuItemConnetti
            // 
            this.toolStripMenuItemConnetti.Name = "toolStripMenuItemConnetti";
            this.toolStripMenuItemConnetti.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemConnetti.Text = "Connetti";
            this.toolStripMenuItemConnetti.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // toolStripMenuItemDisconnetti
            // 
            this.toolStripMenuItemDisconnetti.Name = "toolStripMenuItemDisconnetti";
            this.toolStripMenuItemDisconnetti.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemDisconnetti.Text = "Disconnetti";
            // 
            // toolStripMenuItemLogOut
            // 
            this.toolStripMenuItemLogOut.Name = "toolStripMenuItemLogOut";
            this.toolStripMenuItemLogOut.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemLogOut.Text = "LogOut";
            this.toolStripMenuItemLogOut.Click += new System.EventHandler(this.toolStripMenuItemLogOut_Click);
            // 
            // toolStripMenuItemCloseForm
            // 
            this.toolStripMenuItemCloseForm.Name = "toolStripMenuItemCloseForm";
            this.toolStripMenuItemCloseForm.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemCloseForm.Text = "Chiudi";
            this.toolStripMenuItemCloseForm.Click += new System.EventHandler(this.MainFormClose);
            // 
            // toolStripMenuItemCloseMenu
            // 
            this.toolStripMenuItemCloseMenu.Name = "toolStripMenuItemCloseMenu";
            this.toolStripMenuItemCloseMenu.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemCloseMenu.Text = "Chiudi Menù";
            this.toolStripMenuItemCloseMenu.Click += new System.EventHandler(this.MenuClose);
            // 
            // PacketsHandlerbackgroundWorker
            // 
            this.PacketsHandlerbackgroundWorker.WorkerReportsProgress = true;
            this.PacketsHandlerbackgroundWorker.WorkerSupportsCancellation = true;
            this.PacketsHandlerbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PacketsHandlerbackgroundWorker_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(374, 132);
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

        }

        #endregion

        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenConsole;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnetti;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisconnetti;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseMenu;
        protected internal System.ComponentModel.BackgroundWorker PacketsHandlerbackgroundWorker;
    }
}