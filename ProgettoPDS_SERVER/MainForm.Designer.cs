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
            this.PacketsHandlerbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.labelStato = new System.Windows.Forms.Label();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.buttonSetPort = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelConnectedClient = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemConnessione = new System.Windows.Forms.ToolStripMenuItem();
            this.connettiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnettiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impostaPortaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoDatiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pulisciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambioPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNascondi = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelSetPort = new System.Windows.Forms.Panel();
            this.buttonClosePanelSetPort = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelClipboardState = new System.Windows.Forms.Label();
            this.panelChangePassword = new System.Windows.Forms.Panel();
            this.buttonChangePassword = new System.Windows.Forms.Button();
            this.buttonClosePanelChangePassword = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNpassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxVpassword = new System.Windows.Forms.TextBox();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.panelInfoCB = new System.Windows.Forms.Panel();
            this.labelTipoCB = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonClosePanelInfoCB = new System.Windows.Forms.Button();
            this.richTextBoxCB = new System.Windows.Forms.RichTextBox();
            this.pictureBoxCB = new System.Windows.Forms.PictureBox();
            this.buttonPlayAudio = new System.Windows.Forms.Button();
            this.buttonStopAudio = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.PanelSetPort.SuspendLayout();
            this.panelChangePassword.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.panelInfoCB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCB)).BeginInit();
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
            this.toolStripMenuItemCloseMenu});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 114);
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
            // PacketsHandlerbackgroundWorker
            // 
            this.PacketsHandlerbackgroundWorker.WorkerReportsProgress = true;
            this.PacketsHandlerbackgroundWorker.WorkerSupportsCancellation = true;
            this.PacketsHandlerbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PacketsHandlerbackgroundWorker_DoWork);
            // 
            // labelStato
            // 
            this.labelStato.AutoSize = true;
            this.labelStato.BackColor = System.Drawing.Color.Transparent;
            this.labelStato.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStato.ForeColor = System.Drawing.Color.DarkRed;
            this.labelStato.Location = new System.Drawing.Point(107, 31);
            this.labelStato.Name = "labelStato";
            this.labelStato.Size = new System.Drawing.Size(85, 15);
            this.labelStato.TabIndex = 1;
            this.labelStato.Text = "DISCONNESSO";
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownPort.BackColor = System.Drawing.Color.White;
            this.numericUpDownPort.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPort.Location = new System.Drawing.Point(132, 13);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownPort.TabIndex = 2;
            this.numericUpDownPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownPort.Value = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            // 
            // buttonSetPort
            // 
            this.buttonSetPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetPort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSetPort.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetPort.Location = new System.Drawing.Point(132, 58);
            this.buttonSetPort.Name = "buttonSetPort";
            this.buttonSetPort.Size = new System.Drawing.Size(120, 24);
            this.buttonSetPort.TabIndex = 3;
            this.buttonSetPort.Text = "Set Port";
            this.buttonSetPort.UseVisualStyleBackColor = true;
            this.buttonSetPort.Click += new System.EventHandler(this.buttonSetPort_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "STATO :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(5, 44);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2);
            this.label3.Size = new System.Drawing.Size(53, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "CLIENT :";
            // 
            // labelConnectedClient
            // 
            this.labelConnectedClient.AutoSize = true;
            this.labelConnectedClient.BackColor = System.Drawing.Color.Transparent;
            this.labelConnectedClient.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectedClient.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelConnectedClient.Location = new System.Drawing.Point(107, 48);
            this.labelConnectedClient.Name = "labelConnectedClient";
            this.labelConnectedClient.Size = new System.Drawing.Size(11, 15);
            this.labelConnectedClient.TabIndex = 9;
            this.labelConnectedClient.Text = "-";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuStrip1.BackgroundImage")));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConnessione,
            this.clipboardToolStripMenuItem,
            this.cambioPasswordToolStripMenuItem,
            this.toolStripMenuItemNascondi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 25);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "-";
            // 
            // toolStripMenuItemConnessione
            // 
            this.toolStripMenuItemConnessione.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connettiToolStripMenuItem,
            this.disconnettiToolStripMenuItem,
            this.impostaPortaToolStripMenuItem});
            this.toolStripMenuItemConnessione.Name = "toolStripMenuItemConnessione";
            this.toolStripMenuItemConnessione.Size = new System.Drawing.Size(94, 21);
            this.toolStripMenuItemConnessione.Text = "Connessione";
            // 
            // connettiToolStripMenuItem
            // 
            this.connettiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("connettiToolStripMenuItem.Image")));
            this.connettiToolStripMenuItem.Name = "connettiToolStripMenuItem";
            this.connettiToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.connettiToolStripMenuItem.Text = "Connetti";
            this.connettiToolStripMenuItem.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // disconnettiToolStripMenuItem
            // 
            this.disconnettiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("disconnettiToolStripMenuItem.Image")));
            this.disconnettiToolStripMenuItem.Name = "disconnettiToolStripMenuItem";
            this.disconnettiToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.disconnettiToolStripMenuItem.Text = "Disconnetti";
            this.disconnettiToolStripMenuItem.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // impostaPortaToolStripMenuItem
            // 
            this.impostaPortaToolStripMenuItem.Name = "impostaPortaToolStripMenuItem";
            this.impostaPortaToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.impostaPortaToolStripMenuItem.Text = "Imposta Porta";
            this.impostaPortaToolStripMenuItem.Click += new System.EventHandler(this.impostaPortaToolStripMenuItem_Click);
            // 
            // clipboardToolStripMenuItem
            // 
            this.clipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoDatiToolStripMenuItem,
            this.pulisciToolStripMenuItem});
            this.clipboardToolStripMenuItem.Name = "clipboardToolStripMenuItem";
            this.clipboardToolStripMenuItem.Size = new System.Drawing.Size(78, 21);
            this.clipboardToolStripMenuItem.Text = "Clipboard";
            // 
            // infoDatiToolStripMenuItem
            // 
            this.infoDatiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("infoDatiToolStripMenuItem.Image")));
            this.infoDatiToolStripMenuItem.Name = "infoDatiToolStripMenuItem";
            this.infoDatiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.infoDatiToolStripMenuItem.Text = "Info Dati";
            this.infoDatiToolStripMenuItem.Click += new System.EventHandler(this.infoDatiToolStripMenuItem_Click);
            // 
            // pulisciToolStripMenuItem
            // 
            this.pulisciToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pulisciToolStripMenuItem.Image")));
            this.pulisciToolStripMenuItem.Name = "pulisciToolStripMenuItem";
            this.pulisciToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pulisciToolStripMenuItem.Text = "Pulisci";
            this.pulisciToolStripMenuItem.Click += new System.EventHandler(this.pulisciToolStripMenuItem_Click);
            // 
            // cambioPasswordToolStripMenuItem
            // 
            this.cambioPasswordToolStripMenuItem.Name = "cambioPasswordToolStripMenuItem";
            this.cambioPasswordToolStripMenuItem.Size = new System.Drawing.Size(125, 21);
            this.cambioPasswordToolStripMenuItem.Text = "Cambio Password";
            this.cambioPasswordToolStripMenuItem.Click += new System.EventHandler(this.cambioPasswordToolStripMenuItem_Click);
            // 
            // toolStripMenuItemNascondi
            // 
            this.toolStripMenuItemNascondi.Name = "toolStripMenuItemNascondi";
            this.toolStripMenuItemNascondi.Size = new System.Drawing.Size(75, 21);
            this.toolStripMenuItemNascondi.Text = "Nascondi";
            this.toolStripMenuItemNascondi.Click += new System.EventHandler(this.toolStripTextBoxNascondi_Click);
            // 
            // PanelSetPort
            // 
            this.PanelSetPort.BackColor = System.Drawing.Color.Beige;
            this.PanelSetPort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelSetPort.BackgroundImage")));
            this.PanelSetPort.Controls.Add(this.buttonClosePanelSetPort);
            this.PanelSetPort.Controls.Add(this.numericUpDownPort);
            this.PanelSetPort.Controls.Add(this.buttonSetPort);
            this.PanelSetPort.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelSetPort.Location = new System.Drawing.Point(0, 124);
            this.PanelSetPort.Name = "PanelSetPort";
            this.PanelSetPort.Padding = new System.Windows.Forms.Padding(5);
            this.PanelSetPort.Size = new System.Drawing.Size(384, 87);
            this.PanelSetPort.TabIndex = 11;
            this.PanelSetPort.Visible = false;
            // 
            // buttonClosePanelSetPort
            // 
            this.buttonClosePanelSetPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClosePanelSetPort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClosePanelSetPort.BackgroundImage")));
            this.buttonClosePanelSetPort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClosePanelSetPort.Location = new System.Drawing.Point(355, 5);
            this.buttonClosePanelSetPort.Name = "buttonClosePanelSetPort";
            this.buttonClosePanelSetPort.Padding = new System.Windows.Forms.Padding(5);
            this.buttonClosePanelSetPort.Size = new System.Drawing.Size(24, 23);
            this.buttonClosePanelSetPort.TabIndex = 4;
            this.buttonClosePanelSetPort.UseVisualStyleBackColor = true;
            this.buttonClosePanelSetPort.Click += new System.EventHandler(this.buttonClosePanelSetPort_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(5, 63);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(2);
            this.label4.Size = new System.Drawing.Size(78, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "CLIPBOARD :";
            // 
            // labelClipboardState
            // 
            this.labelClipboardState.AutoSize = true;
            this.labelClipboardState.BackColor = System.Drawing.Color.Transparent;
            this.labelClipboardState.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClipboardState.ForeColor = System.Drawing.Color.DarkRed;
            this.labelClipboardState.Location = new System.Drawing.Point(107, 67);
            this.labelClipboardState.Name = "labelClipboardState";
            this.labelClipboardState.Size = new System.Drawing.Size(45, 15);
            this.labelClipboardState.TabIndex = 13;
            this.labelClipboardState.Text = "VUOTA";
            // 
            // panelChangePassword
            // 
            this.panelChangePassword.BackColor = System.Drawing.Color.Beige;
            this.panelChangePassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelChangePassword.BackgroundImage")));
            this.panelChangePassword.Controls.Add(this.buttonChangePassword);
            this.panelChangePassword.Controls.Add(this.buttonClosePanelChangePassword);
            this.panelChangePassword.Controls.Add(this.label5);
            this.panelChangePassword.Controls.Add(this.textBoxNpassword);
            this.panelChangePassword.Controls.Add(this.label2);
            this.panelChangePassword.Controls.Add(this.textBoxVpassword);
            this.panelChangePassword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChangePassword.Location = new System.Drawing.Point(0, 37);
            this.panelChangePassword.Name = "panelChangePassword";
            this.panelChangePassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelChangePassword.Size = new System.Drawing.Size(384, 87);
            this.panelChangePassword.TabIndex = 1;
            this.panelChangePassword.Visible = false;
            // 
            // buttonChangePassword
            // 
            this.buttonChangePassword.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonChangePassword.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChangePassword.Location = new System.Drawing.Point(132, 59);
            this.buttonChangePassword.Name = "buttonChangePassword";
            this.buttonChangePassword.Size = new System.Drawing.Size(120, 24);
            this.buttonChangePassword.TabIndex = 6;
            this.buttonChangePassword.Text = "Set Password";
            this.buttonChangePassword.UseVisualStyleBackColor = true;
            this.buttonChangePassword.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // 
            // buttonClosePanelChangePassword
            // 
            this.buttonClosePanelChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClosePanelChangePassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClosePanelChangePassword.BackgroundImage")));
            this.buttonClosePanelChangePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClosePanelChangePassword.Location = new System.Drawing.Point(355, 6);
            this.buttonClosePanelChangePassword.Name = "buttonClosePanelChangePassword";
            this.buttonClosePanelChangePassword.Padding = new System.Windows.Forms.Padding(5);
            this.buttonClosePanelChangePassword.Size = new System.Drawing.Size(25, 23);
            this.buttonClosePanelChangePassword.TabIndex = 5;
            this.buttonClosePanelChangePassword.UseVisualStyleBackColor = true;
            this.buttonClosePanelChangePassword.Click += new System.EventHandler(this.buttonClosePanelChangePassword_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(17, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Nuova Password :";
            // 
            // textBoxNpassword
            // 
            this.textBoxNpassword.Location = new System.Drawing.Point(132, 32);
            this.textBoxNpassword.Name = "textBoxNpassword";
            this.textBoxNpassword.Size = new System.Drawing.Size(120, 20);
            this.textBoxNpassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(17, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vecchia Password :";
            // 
            // textBoxVpassword
            // 
            this.textBoxVpassword.Location = new System.Drawing.Point(132, 8);
            this.textBoxVpassword.Name = "textBoxVpassword";
            this.textBoxVpassword.Size = new System.Drawing.Size(120, 20);
            this.textBoxVpassword.TabIndex = 0;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxInfo.Controls.Add(this.labelStato);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.labelClipboardState);
            this.groupBoxInfo.Controls.Add(this.labelConnectedClient);
            this.groupBoxInfo.Controls.Add(this.label4);
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBoxInfo.Location = new System.Drawing.Point(5, 32);
            this.groupBoxInfo.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxInfo.Size = new System.Drawing.Size(375, 87);
            this.groupBoxInfo.TabIndex = 15;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // panelInfoCB
            // 
            this.panelInfoCB.BackColor = System.Drawing.Color.Beige;
            this.panelInfoCB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfoCB.Controls.Add(this.buttonStopAudio);
            this.panelInfoCB.Controls.Add(this.buttonPlayAudio);
            this.panelInfoCB.Controls.Add(this.pictureBoxCB);
            this.panelInfoCB.Controls.Add(this.richTextBoxCB);
            this.panelInfoCB.Controls.Add(this.labelTipoCB);
            this.panelInfoCB.Controls.Add(this.label6);
            this.panelInfoCB.Controls.Add(this.buttonClosePanelInfoCB);
            this.panelInfoCB.Location = new System.Drawing.Point(251, 25);
            this.panelInfoCB.Name = "panelInfoCB";
            this.panelInfoCB.Size = new System.Drawing.Size(130, 96);
            this.panelInfoCB.TabIndex = 20;
            this.panelInfoCB.Visible = false;
            // 
            // labelTipoCB
            // 
            this.labelTipoCB.AutoSize = true;
            this.labelTipoCB.Location = new System.Drawing.Point(39, 10);
            this.labelTipoCB.Name = "labelTipoCB";
            this.labelTipoCB.Size = new System.Drawing.Size(64, 13);
            this.labelTipoCB.TabIndex = 7;
            this.labelTipoCB.Text = "labelTipoCB";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "TIPO:";
            // 
            // buttonClosePanelInfoCB
            // 
            this.buttonClosePanelInfoCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClosePanelInfoCB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClosePanelInfoCB.BackgroundImage")));
            this.buttonClosePanelInfoCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClosePanelInfoCB.Location = new System.Drawing.Point(101, 3);
            this.buttonClosePanelInfoCB.Name = "buttonClosePanelInfoCB";
            this.buttonClosePanelInfoCB.Padding = new System.Windows.Forms.Padding(5);
            this.buttonClosePanelInfoCB.Size = new System.Drawing.Size(24, 23);
            this.buttonClosePanelInfoCB.TabIndex = 5;
            this.buttonClosePanelInfoCB.UseVisualStyleBackColor = true;
            this.buttonClosePanelInfoCB.Click += new System.EventHandler(this.buttonClosePanelInfoCB_Click);
            // 
            // richTextBoxCB
            // 
            this.richTextBoxCB.Location = new System.Drawing.Point(-1, 32);
            this.richTextBoxCB.Name = "richTextBoxCB";
            this.richTextBoxCB.ReadOnly = true;
            this.richTextBoxCB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxCB.Size = new System.Drawing.Size(130, 62);
            this.richTextBoxCB.TabIndex = 8;
            this.richTextBoxCB.Text = "";
            this.richTextBoxCB.Visible = false;
            // 
            // pictureBoxCB
            // 
            this.pictureBoxCB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxCB.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCB.Image")));
            this.pictureBoxCB.Location = new System.Drawing.Point(-1, 32);
            this.pictureBoxCB.Name = "pictureBoxCB";
            this.pictureBoxCB.Size = new System.Drawing.Size(130, 63);
            this.pictureBoxCB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCB.TabIndex = 9;
            this.pictureBoxCB.TabStop = false;
            this.pictureBoxCB.Visible = false;
            // 
            // buttonPlayAudio
            // 
            this.buttonPlayAudio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPlayAudio.BackgroundImage")));
            this.buttonPlayAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPlayAudio.Location = new System.Drawing.Point(73, 46);
            this.buttonPlayAudio.Name = "buttonPlayAudio";
            this.buttonPlayAudio.Size = new System.Drawing.Size(30, 30);
            this.buttonPlayAudio.TabIndex = 10;
            this.buttonPlayAudio.UseVisualStyleBackColor = true;
            this.buttonPlayAudio.Visible = false;
            // 
            // buttonStopAudio
            // 
            this.buttonStopAudio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonStopAudio.BackgroundImage")));
            this.buttonStopAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStopAudio.Location = new System.Drawing.Point(28, 46);
            this.buttonStopAudio.Name = "buttonStopAudio";
            this.buttonStopAudio.Size = new System.Drawing.Size(30, 30);
            this.buttonStopAudio.TabIndex = 11;
            this.buttonStopAudio.UseVisualStyleBackColor = true;
            this.buttonStopAudio.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Beige;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.panelInfoCB);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.panelChangePassword);
            this.Controls.Add(this.PanelSetPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelSetPort.ResumeLayout(false);
            this.panelChangePassword.ResumeLayout(false);
            this.panelChangePassword.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.panelInfoCB.ResumeLayout(false);
            this.panelInfoCB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCB)).EndInit();
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
        protected internal System.Windows.Forms.Label labelStato;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Button buttonSetPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        protected internal System.Windows.Forms.Label labelConnectedClient;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnessione;
        private System.Windows.Forms.ToolStripMenuItem connettiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnettiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNascondi;
        private System.Windows.Forms.ToolStripMenuItem impostaPortaToolStripMenuItem;
        private System.Windows.Forms.Panel PanelSetPort;
        private System.Windows.Forms.Button buttonClosePanelSetPort;
        private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambioPasswordToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelClipboardState;
        private System.Windows.Forms.Panel panelChangePassword;
        private System.Windows.Forms.Button buttonClosePanelChangePassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxNpassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxVpassword;
        private System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.ToolStripMenuItem infoDatiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pulisciToolStripMenuItem;
        private System.Windows.Forms.Panel panelInfoCB;
        private System.Windows.Forms.Button buttonClosePanelInfoCB;
        private System.Windows.Forms.Label labelTipoCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBoxCB;
        private System.Windows.Forms.PictureBox pictureBoxCB;
        private System.Windows.Forms.Button buttonStopAudio;
        private System.Windows.Forms.Button buttonPlayAudio;
    }
}