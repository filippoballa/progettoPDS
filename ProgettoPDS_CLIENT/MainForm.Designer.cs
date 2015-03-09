namespace ProgettoPDS_CLIENT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MouseBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.HostNameLabel = new System.Windows.Forms.Label();
            this.HostNameTextBox = new System.Windows.Forms.TextBox();
            this.ConfigLabel = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.PortaTextBox = new System.Windows.Forms.TextBox();
            this.PortaLabel = new System.Windows.Forms.Label();
            this.IPAddressLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ElencoLabel = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.InfoButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.ActionPanel = new System.Windows.Forms.Panel();
            this.EscapeLabel = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.ActionServerLabel = new System.Windows.Forms.Label();
            this.KeyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.BackButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ActionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // MouseBackgroundWorker
            // 
            this.MouseBackgroundWorker.WorkerReportsProgress = true;
            this.MouseBackgroundWorker.WorkerSupportsCancellation = true;
            this.MouseBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MouseBackgroundWorker_DoWork);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.pictureBox2);
            this.MainPanel.Controls.Add(this.ConnectButton);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Controls.Add(this.groupBox2);
            this.MainPanel.Controls.Add(this.ElencoLabel);
            this.MainPanel.Controls.Add(this.listBox1);
            this.MainPanel.Controls.Add(this.DisconnectButton);
            this.MainPanel.Controls.Add(this.InfoButton);
            this.MainPanel.Controls.Add(this.pictureBox1);
            this.MainPanel.Controls.Add(this.RemoveButton);
            this.MainPanel.Controls.Add(this.StartButton);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1071, 608);
            this.MainPanel.TabIndex = 14;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 608);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.ForestGreen;
            this.ConnectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConnectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ConnectButton.FlatAppearance.BorderSize = 3;
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectButton.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.ForeColor = System.Drawing.Color.White;
            this.ConnectButton.Location = new System.Drawing.Point(128, 492);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(121, 34);
            this.ConnectButton.TabIndex = 24;
            this.ConnectButton.Text = "CONNETTI";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.HostNameLabel);
            this.groupBox1.Controls.Add(this.HostNameTextBox);
            this.groupBox1.Controls.Add(this.ConfigLabel);
            this.groupBox1.Controls.Add(this.AddButton);
            this.groupBox1.Controls.Add(this.IPAddressTextBox);
            this.groupBox1.Controls.Add(this.PortaTextBox);
            this.groupBox1.Controls.Add(this.PortaLabel);
            this.groupBox1.Controls.Add(this.IPAddressLabel);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(586, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 343);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurazione Server";
            // 
            // HostNameLabel
            // 
            this.HostNameLabel.AutoSize = true;
            this.HostNameLabel.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HostNameLabel.Location = new System.Drawing.Point(15, 107);
            this.HostNameLabel.Name = "HostNameLabel";
            this.HostNameLabel.Size = new System.Drawing.Size(105, 21);
            this.HostNameLabel.TabIndex = 7;
            this.HostNameLabel.Text = "Host Name :";
            // 
            // HostNameTextBox
            // 
            this.HostNameTextBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.HostNameTextBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HostNameTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.HostNameTextBox.Location = new System.Drawing.Point(132, 106);
            this.HostNameTextBox.Name = "HostNameTextBox";
            this.HostNameTextBox.Size = new System.Drawing.Size(176, 26);
            this.HostNameTextBox.TabIndex = 1;
            // 
            // ConfigLabel
            // 
            this.ConfigLabel.AutoSize = true;
            this.ConfigLabel.Font = new System.Drawing.Font("Comic Sans MS", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigLabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.ConfigLabel.Location = new System.Drawing.Point(32, 45);
            this.ConfigLabel.Name = "ConfigLabel";
            this.ConfigLabel.Size = new System.Drawing.Size(289, 22);
            this.ConfigLabel.TabIndex = 5;
            this.ConfigLabel.Text = "Inserisci i Parametri di Configurazione";
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.Firebrick;
            this.AddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddButton.FlatAppearance.BorderColor = System.Drawing.Color.Wheat;
            this.AddButton.FlatAppearance.BorderSize = 3;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Location = new System.Drawing.Point(132, 252);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(123, 62);
            this.AddButton.TabIndex = 4;
            this.AddButton.Text = "Add Server \r\nTo List!";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.IPAddressTextBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPAddressTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.IPAddressTextBox.Location = new System.Drawing.Point(132, 150);
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(176, 26);
            this.IPAddressTextBox.TabIndex = 2;
            // 
            // PortaTextBox
            // 
            this.PortaTextBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.PortaTextBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortaTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.PortaTextBox.Location = new System.Drawing.Point(132, 192);
            this.PortaTextBox.Name = "PortaTextBox";
            this.PortaTextBox.Size = new System.Drawing.Size(176, 26);
            this.PortaTextBox.TabIndex = 3;
            // 
            // PortaLabel
            // 
            this.PortaLabel.AutoSize = true;
            this.PortaLabel.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortaLabel.Location = new System.Drawing.Point(41, 193);
            this.PortaLabel.Name = "PortaLabel";
            this.PortaLabel.Size = new System.Drawing.Size(62, 21);
            this.PortaLabel.TabIndex = 3;
            this.PortaLabel.Text = "Porta :";
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPAddressLabel.Location = new System.Drawing.Point(15, 151);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(111, 21);
            this.IPAddressLabel.TabIndex = 2;
            this.IPAddressLabel.Text = "Indirizzo IP :";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.BackColor = System.Drawing.Color.Sienna;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(586, 402);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 100);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "STATO SISTEMA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Non Connesso con Nessun Server al momento!!";
            // 
            // ElencoLabel
            // 
            this.ElencoLabel.Font = new System.Drawing.Font("Comic Sans MS", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElencoLabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.ElencoLabel.Location = new System.Drawing.Point(133, 36);
            this.ElencoLabel.Name = "ElencoLabel";
            this.ElencoLabel.Size = new System.Drawing.Size(294, 66);
            this.ElencoLabel.TabIndex = 21;
            this.ElencoLabel.Text = "Elenco dei Dispositivi ( SERVER ) \r\nComandabili momentaneamente";
            this.ElencoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.SlateGray;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 23;
            this.listBox1.Location = new System.Drawing.Point(128, 107);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(303, 345);
            this.listBox1.TabIndex = 20;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.Maroon;
            this.DisconnectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisconnectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DisconnectButton.FlatAppearance.BorderSize = 3;
            this.DisconnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectButton.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectButton.ForeColor = System.Drawing.Color.White;
            this.DisconnectButton.Location = new System.Drawing.Point(128, 548);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(121, 34);
            this.DisconnectButton.TabIndex = 19;
            this.DisconnectButton.Text = "DISCONNETTI";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // InfoButton
            // 
            this.InfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.InfoButton.FlatAppearance.BorderSize = 4;
            this.InfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoButton.Image = ((System.Drawing.Image)(resources.GetObject("InfoButton.Image")));
            this.InfoButton.Location = new System.Drawing.Point(808, 537);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(66, 58);
            this.InfoButton.TabIndex = 17;
            this.InfoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.InfoButton.UseVisualStyleBackColor = true;
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(971, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 608);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.Olive;
            this.RemoveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RemoveButton.FlatAppearance.BorderColor = System.Drawing.Color.Wheat;
            this.RemoveButton.FlatAppearance.BorderSize = 3;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveButton.ForeColor = System.Drawing.Color.White;
            this.RemoveButton.Location = new System.Drawing.Point(272, 509);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(126, 55);
            this.RemoveButton.TabIndex = 15;
            this.RemoveButton.Text = "Remove Server From List";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.DarkCyan;
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.StartButton.FlatAppearance.BorderSize = 4;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartButton.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.ForeColor = System.Drawing.Color.White;
            this.StartButton.Location = new System.Drawing.Point(660, 537);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(103, 56);
            this.StartButton.TabIndex = 14;
            this.StartButton.Text = "Start && Use";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ActionPanel
            // 
            this.ActionPanel.BackColor = System.Drawing.Color.Snow;
            this.ActionPanel.Controls.Add(this.EscapeLabel);
            this.ActionPanel.Controls.Add(this.pictureBox4);
            this.ActionPanel.Controls.Add(this.pictureBox3);
            this.ActionPanel.Controls.Add(this.ActionServerLabel);
            this.ActionPanel.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.ActionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionPanel.Location = new System.Drawing.Point(0, 0);
            this.ActionPanel.Name = "ActionPanel";
            this.ActionPanel.Size = new System.Drawing.Size(1071, 608);
            this.ActionPanel.TabIndex = 26;
            this.ActionPanel.Visible = false;
            // 
            // EscapeLabel
            // 
            this.EscapeLabel.AutoSize = true;
            this.EscapeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.EscapeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EscapeLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EscapeLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.EscapeLabel.Location = new System.Drawing.Point(300, 561);
            this.EscapeLabel.Name = "EscapeLabel";
            this.EscapeLabel.Size = new System.Drawing.Size(400, 20);
            this.EscapeLabel.TabIndex = 3;
            this.EscapeLabel.Text = "Premere il tasto \"ESC\" per tornare alla schermata precedente...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox4.Location = new System.Drawing.Point(971, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(100, 608);
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 608);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // ActionServerLabel
            // 
            this.ActionServerLabel.AutoSize = true;
            this.ActionServerLabel.Font = new System.Drawing.Font("Comic Sans MS", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionServerLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.ActionServerLabel.Location = new System.Drawing.Point(217, 18);
            this.ActionServerLabel.Name = "ActionServerLabel";
            this.ActionServerLabel.Size = new System.Drawing.Size(495, 36);
            this.ActionServerLabel.TabIndex = 0;
            this.ActionServerLabel.Text = "Stai Comandando il seguente Server: ";
            // 
            // KeyBackgroundWorker
            // 
            this.KeyBackgroundWorker.WorkerReportsProgress = true;
            this.KeyBackgroundWorker.WorkerSupportsCancellation = true;
            this.KeyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.KeyBackgroundWorker_DoWork);
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackColor = System.Drawing.Color.Linen;
            this.InfoPanel.Controls.Add(this.BackButton);
            this.InfoPanel.Controls.Add(this.label1);
            this.InfoPanel.Controls.Add(this.pictureBox6);
            this.InfoPanel.Controls.Add(this.pictureBox5);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPanel.Location = new System.Drawing.Point(0, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(1071, 608);
            this.InfoPanel.TabIndex = 27;
            this.InfoPanel.Visible = false;
            // 
            // BackButton
            // 
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BackButton.FlatAppearance.BorderSize = 0;
            this.BackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Linen;
            this.BackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Linen;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Image = ((System.Drawing.Image)(resources.GetObject("BackButton.Image")));
            this.BackButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BackButton.Location = new System.Drawing.Point(106, 547);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(118, 59);
            this.BackButton.TabIndex = 3;
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(214, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(627, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "INFORMAZIONI SULL\'UTILIZZO DELLA SCHERMATA PRINCIPALE";
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.BackgroundImage")));
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox6.Location = new System.Drawing.Point(0, 0);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(100, 608);
            this.pictureBox6.TabIndex = 1;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox5.Location = new System.Drawing.Point(971, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(100, 608);
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Sienna;
            this.ClientSize = new System.Drawing.Size(1071, 608);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.ActionPanel);
            this.Controls.Add(this.InfoPanel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progetto PDS - Main Form";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ActionPanel.ResumeLayout(false);
            this.ActionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker MouseBackgroundWorker;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label HostNameLabel;
        private System.Windows.Forms.TextBox HostNameTextBox;
        private System.Windows.Forms.Label ConfigLabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox IPAddressTextBox;
        private System.Windows.Forms.TextBox PortaTextBox;
        private System.Windows.Forms.Label PortaLabel;
        private System.Windows.Forms.Label IPAddressLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ElencoLabel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button InfoButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Panel ActionPanel;
        private System.Windows.Forms.Label ActionServerLabel;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label EscapeLabel;
        private System.ComponentModel.BackgroundWorker KeyBackgroundWorker;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button BackButton;
    }
}