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
            this.LogOutButton = new System.Windows.Forms.Button();
            this.ClearCacheButton = new System.Windows.Forms.Button();
            this.LoadConfButton = new System.Windows.Forms.Button();
            this.SaveServButton = new System.Windows.Forms.Button();
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
            this.ComandiGroupBox = new System.Windows.Forms.GroupBox();
            this.ComandLabel = new System.Windows.Forms.Label();
            this.EscapeLabel = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.ActionServerLabel = new System.Windows.Forms.Label();
            this.KeyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.ClipPictureBox = new System.Windows.Forms.PictureBox();
            this.MousePictureBox = new System.Windows.Forms.PictureBox();
            this.KeyPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BackButton = new System.Windows.Forms.Button();
            this.InfoContentLabel = new System.Windows.Forms.Label();
            this.ClipboardSendBW = new System.ComponentModel.BackgroundWorker();
            this.ClipboardRequestBW = new System.ComponentModel.BackgroundWorker();
            this.ContentClipboardPanel = new System.Windows.Forms.Panel();
            this.StopAudioButton = new System.Windows.Forms.Button();
            this.PlayAudioButton = new System.Windows.Forms.Button();
            this.RichTextBox = new System.Windows.Forms.RichTextBox();
            this.ImageClipboardPictureBox = new System.Windows.Forms.PictureBox();
            this.TypeClipboardLabel = new System.Windows.Forms.Label();
            this.TitleContentClipLabel = new System.Windows.Forms.Label();
            this.ProgressBarPanel = new System.Windows.Forms.Panel();
            this.PercentageLabel = new System.Windows.Forms.Label();
            this.ClipboardProgressBar = new System.Windows.Forms.ProgressBar();
            this.AvanzClipLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ActionPanel.SuspendLayout();
            this.ComandiGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClipPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MousePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.ContentClipboardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageClipboardPictureBox)).BeginInit();
            this.ProgressBarPanel.SuspendLayout();
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
            this.MainPanel.Controls.Add(this.LogOutButton);
            this.MainPanel.Controls.Add(this.ClearCacheButton);
            this.MainPanel.Controls.Add(this.LoadConfButton);
            this.MainPanel.Controls.Add(this.SaveServButton);
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
            // LogOutButton
            // 
            this.LogOutButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LogOutButton.BackgroundImage")));
            this.LogOutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LogOutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogOutButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LogOutButton.FlatAppearance.BorderSize = 4;
            this.LogOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogOutButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogOutButton.Location = new System.Drawing.Point(865, 535);
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.Size = new System.Drawing.Size(66, 58);
            this.LogOutButton.TabIndex = 29;
            this.LogOutButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LogOutButton.UseVisualStyleBackColor = true;
            this.LogOutButton.Click += new System.EventHandler(this.LogOutButton_Click);
            // 
            // ClearCacheButton
            // 
            this.ClearCacheButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ClearCacheButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearCacheButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ClearCacheButton.FlatAppearance.BorderSize = 3;
            this.ClearCacheButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearCacheButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearCacheButton.ForeColor = System.Drawing.Color.White;
            this.ClearCacheButton.Location = new System.Drawing.Point(461, 237);
            this.ClearCacheButton.Name = "ClearCacheButton";
            this.ClearCacheButton.Size = new System.Drawing.Size(83, 61);
            this.ClearCacheButton.TabIndex = 28;
            this.ClearCacheButton.Text = "Clear Configuration Cache";
            this.ClearCacheButton.UseVisualStyleBackColor = false;
            this.ClearCacheButton.Click += new System.EventHandler(this.ClearCacheButton_Click);
            // 
            // LoadConfButton
            // 
            this.LoadConfButton.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.LoadConfButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadConfButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LoadConfButton.FlatAppearance.BorderSize = 3;
            this.LoadConfButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadConfButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadConfButton.ForeColor = System.Drawing.Color.White;
            this.LoadConfButton.Location = new System.Drawing.Point(461, 145);
            this.LoadConfButton.Name = "LoadConfButton";
            this.LoadConfButton.Size = new System.Drawing.Size(83, 61);
            this.LoadConfButton.TabIndex = 27;
            this.LoadConfButton.Text = "Load Configuration Saved";
            this.LoadConfButton.UseVisualStyleBackColor = false;
            this.LoadConfButton.Click += new System.EventHandler(this.LoadConfButton_Click);
            // 
            // SaveServButton
            // 
            this.SaveServButton.BackColor = System.Drawing.Color.Olive;
            this.SaveServButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveServButton.FlatAppearance.BorderColor = System.Drawing.Color.Wheat;
            this.SaveServButton.FlatAppearance.BorderSize = 3;
            this.SaveServButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveServButton.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveServButton.ForeColor = System.Drawing.Color.White;
            this.SaveServButton.Location = new System.Drawing.Point(281, 542);
            this.SaveServButton.Name = "SaveServButton";
            this.SaveServButton.Size = new System.Drawing.Size(136, 53);
            this.SaveServButton.TabIndex = 25;
            this.SaveServButton.Text = "Save Configuration Server";
            this.SaveServButton.UseVisualStyleBackColor = false;
            this.SaveServButton.Click += new System.EventHandler(this.SaveServButton_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(586, 17);
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
            this.ConfigLabel.Size = new System.Drawing.Size(289, 21);
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
            this.groupBox2.Location = new System.Drawing.Point(586, 398);
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
            this.listBox1.HorizontalScrollbar = true;
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
            this.InfoButton.Location = new System.Drawing.Point(767, 535);
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
            this.RemoveButton.Location = new System.Drawing.Point(281, 475);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(136, 55);
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
            this.StartButton.Location = new System.Drawing.Point(609, 537);
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
            this.ActionPanel.Controls.Add(this.label2);
            this.ActionPanel.Controls.Add(this.ProgressBarPanel);
            this.ActionPanel.Controls.Add(this.ContentClipboardPanel);
            this.ActionPanel.Controls.Add(this.ComandiGroupBox);
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
            // ComandiGroupBox
            // 
            this.ComandiGroupBox.Controls.Add(this.ComandLabel);
            this.ComandiGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComandiGroupBox.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComandiGroupBox.ForeColor = System.Drawing.Color.DarkRed;
            this.ComandiGroupBox.Location = new System.Drawing.Point(117, 135);
            this.ComandiGroupBox.Name = "ComandiGroupBox";
            this.ComandiGroupBox.Size = new System.Drawing.Size(374, 334);
            this.ComandiGroupBox.TabIndex = 4;
            this.ComandiGroupBox.TabStop = false;
            this.ComandiGroupBox.Text = "Legenda dei Comandi";
            this.ComandiGroupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ComandiGroupBox_Paint);
            // 
            // ComandLabel
            // 
            this.ComandLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComandLabel.AutoSize = true;
            this.ComandLabel.Font = new System.Drawing.Font("Calibri", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComandLabel.Location = new System.Drawing.Point(34, 65);
            this.ComandLabel.Name = "ComandLabel";
            this.ComandLabel.Size = new System.Drawing.Size(277, 204);
            this.ComandLabel.TabIndex = 0;
            this.ComandLabel.Text = resources.GetString("ComandLabel.Text");
            // 
            // EscapeLabel
            // 
            this.EscapeLabel.AutoSize = true;
            this.EscapeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.EscapeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EscapeLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EscapeLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.EscapeLabel.Location = new System.Drawing.Point(254, 561);
            this.EscapeLabel.Name = "EscapeLabel";
            this.EscapeLabel.Size = new System.Drawing.Size(546, 20);
            this.EscapeLabel.TabIndex = 3;
            this.EscapeLabel.Text = "Premere la combinazione  di tasti \"Shift + Esc\" per tornare alla schermata preced" +
    "ente...";
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
            this.ActionServerLabel.Location = new System.Drawing.Point(679, 18);
            this.ActionServerLabel.Name = "ActionServerLabel";
            this.ActionServerLabel.Size = new System.Drawing.Size(0, 36);
            this.ActionServerLabel.TabIndex = 0;
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
            this.InfoPanel.Controls.Add(this.ClipPictureBox);
            this.InfoPanel.Controls.Add(this.MousePictureBox);
            this.InfoPanel.Controls.Add(this.KeyPictureBox);
            this.InfoPanel.Controls.Add(this.pictureBox6);
            this.InfoPanel.Controls.Add(this.pictureBox5);
            this.InfoPanel.Controls.Add(this.label1);
            this.InfoPanel.Controls.Add(this.BackButton);
            this.InfoPanel.Controls.Add(this.InfoContentLabel);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPanel.Location = new System.Drawing.Point(0, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(1071, 608);
            this.InfoPanel.TabIndex = 27;
            this.InfoPanel.Visible = false;
            // 
            // ClipPictureBox
            // 
            this.ClipPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClipPictureBox.BackgroundImage")));
            this.ClipPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClipPictureBox.Location = new System.Drawing.Point(824, 410);
            this.ClipPictureBox.Name = "ClipPictureBox";
            this.ClipPictureBox.Size = new System.Drawing.Size(123, 109);
            this.ClipPictureBox.TabIndex = 14;
            this.ClipPictureBox.TabStop = false;
            // 
            // MousePictureBox
            // 
            this.MousePictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MousePictureBox.BackgroundImage")));
            this.MousePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MousePictureBox.Location = new System.Drawing.Point(814, 259);
            this.MousePictureBox.Name = "MousePictureBox";
            this.MousePictureBox.Size = new System.Drawing.Size(139, 85);
            this.MousePictureBox.TabIndex = 13;
            this.MousePictureBox.TabStop = false;
            // 
            // KeyPictureBox
            // 
            this.KeyPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("KeyPictureBox.BackgroundImage")));
            this.KeyPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.KeyPictureBox.Location = new System.Drawing.Point(824, 87);
            this.KeyPictureBox.Name = "KeyPictureBox";
            this.KeyPictureBox.Size = new System.Drawing.Size(123, 85);
            this.KeyPictureBox.TabIndex = 12;
            this.KeyPictureBox.TabStop = false;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(229, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(627, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "INFORMAZIONI SULL\'UTILIZZO DELLA SCHERMATA PRINCIPALE";
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
            // InfoContentLabel
            // 
            this.InfoContentLabel.BackColor = System.Drawing.Color.Linen;
            this.InfoContentLabel.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoContentLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.InfoContentLabel.Location = new System.Drawing.Point(119, 79);
            this.InfoContentLabel.Name = "InfoContentLabel";
            this.InfoContentLabel.Padding = new System.Windows.Forms.Padding(8, 8, 2, 2);
            this.InfoContentLabel.Size = new System.Drawing.Size(672, 466);
            this.InfoContentLabel.TabIndex = 11;
            this.InfoContentLabel.Text = resources.GetString("InfoContentLabel.Text");
            this.InfoContentLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.InfoContentLabel_Paint);
            // 
            // ClipboardSendBW
            // 
            this.ClipboardSendBW.WorkerReportsProgress = true;
            this.ClipboardSendBW.WorkerSupportsCancellation = true;
            this.ClipboardSendBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClipboardSendBW_DoWork);
            // 
            // ClipboardRequestBW
            // 
            this.ClipboardRequestBW.WorkerReportsProgress = true;
            this.ClipboardRequestBW.WorkerSupportsCancellation = true;
            this.ClipboardRequestBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClipboardRequestBW_DoWork);
            // 
            // ContentClipboardPanel
            // 
            this.ContentClipboardPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ContentClipboardPanel.BackgroundImage")));
            this.ContentClipboardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentClipboardPanel.Controls.Add(this.label3);
            this.ContentClipboardPanel.Controls.Add(this.StopAudioButton);
            this.ContentClipboardPanel.Controls.Add(this.PlayAudioButton);
            this.ContentClipboardPanel.Controls.Add(this.RichTextBox);
            this.ContentClipboardPanel.Controls.Add(this.ImageClipboardPictureBox);
            this.ContentClipboardPanel.Controls.Add(this.TypeClipboardLabel);
            this.ContentClipboardPanel.Controls.Add(this.TitleContentClipLabel);
            this.ContentClipboardPanel.Location = new System.Drawing.Point(551, 135);
            this.ContentClipboardPanel.Name = "ContentClipboardPanel";
            this.ContentClipboardPanel.Size = new System.Drawing.Size(328, 191);
            this.ContentClipboardPanel.TabIndex = 16;
            this.ContentClipboardPanel.Visible = false;
            // 
            // StopAudioButton
            // 
            this.StopAudioButton.BackColor = System.Drawing.Color.Transparent;
            this.StopAudioButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StopAudioButton.BackgroundImage")));
            this.StopAudioButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StopAudioButton.Location = new System.Drawing.Point(151, 102);
            this.StopAudioButton.Name = "StopAudioButton";
            this.StopAudioButton.Size = new System.Drawing.Size(50, 50);
            this.StopAudioButton.TabIndex = 12;
            this.StopAudioButton.UseVisualStyleBackColor = false;
            this.StopAudioButton.Visible = false;
            // 
            // PlayAudioButton
            // 
            this.PlayAudioButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayAudioButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayAudioButton.BackgroundImage")));
            this.PlayAudioButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayAudioButton.Location = new System.Drawing.Point(64, 102);
            this.PlayAudioButton.Name = "PlayAudioButton";
            this.PlayAudioButton.Size = new System.Drawing.Size(50, 50);
            this.PlayAudioButton.TabIndex = 11;
            this.PlayAudioButton.UseVisualStyleBackColor = false;
            this.PlayAudioButton.Visible = false;
            // 
            // RichTextBox
            // 
            this.RichTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.RichTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBox.Location = new System.Drawing.Point(17, 87);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.Size = new System.Drawing.Size(277, 88);
            this.RichTextBox.TabIndex = 4;
            this.RichTextBox.Text = "";
            this.RichTextBox.Visible = false;
            // 
            // ImageClipboardPictureBox
            // 
            this.ImageClipboardPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ImageClipboardPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ImageClipboardPictureBox.Location = new System.Drawing.Point(94, 92);
            this.ImageClipboardPictureBox.Name = "ImageClipboardPictureBox";
            this.ImageClipboardPictureBox.Size = new System.Drawing.Size(140, 82);
            this.ImageClipboardPictureBox.TabIndex = 3;
            this.ImageClipboardPictureBox.TabStop = false;
            // 
            // TypeClipboardLabel
            // 
            this.TypeClipboardLabel.AutoSize = true;
            this.TypeClipboardLabel.BackColor = System.Drawing.Color.Transparent;
            this.TypeClipboardLabel.Font = new System.Drawing.Font("Calibri", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeClipboardLabel.ForeColor = System.Drawing.Color.Navy;
            this.TypeClipboardLabel.Location = new System.Drawing.Point(119, 59);
            this.TypeClipboardLabel.Name = "TypeClipboardLabel";
            this.TypeClipboardLabel.Size = new System.Drawing.Size(0, 15);
            this.TypeClipboardLabel.TabIndex = 2;
            // 
            // TitleContentClipLabel
            // 
            this.TitleContentClipLabel.AutoSize = true;
            this.TitleContentClipLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleContentClipLabel.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleContentClipLabel.Location = new System.Drawing.Point(64, 21);
            this.TitleContentClipLabel.Name = "TitleContentClipLabel";
            this.TitleContentClipLabel.Size = new System.Drawing.Size(187, 20);
            this.TitleContentClipLabel.TabIndex = 1;
            this.TitleContentClipLabel.Text = "Contenuto della Clipboard";
            this.TitleContentClipLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProgressBarPanel
            // 
            this.ProgressBarPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProgressBarPanel.BackgroundImage")));
            this.ProgressBarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgressBarPanel.Controls.Add(this.PercentageLabel);
            this.ProgressBarPanel.Controls.Add(this.ClipboardProgressBar);
            this.ProgressBarPanel.Controls.Add(this.AvanzClipLabel);
            this.ProgressBarPanel.Location = new System.Drawing.Point(551, 346);
            this.ProgressBarPanel.Name = "ProgressBarPanel";
            this.ProgressBarPanel.Size = new System.Drawing.Size(328, 123);
            this.ProgressBarPanel.TabIndex = 29;
            this.ProgressBarPanel.Visible = false;
            // 
            // PercentageLabel
            // 
            this.PercentageLabel.AutoSize = true;
            this.PercentageLabel.BackColor = System.Drawing.Color.Transparent;
            this.PercentageLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PercentageLabel.Location = new System.Drawing.Point(244, 82);
            this.PercentageLabel.Name = "PercentageLabel";
            this.PercentageLabel.Size = new System.Drawing.Size(29, 18);
            this.PercentageLabel.TabIndex = 7;
            this.PercentageLabel.Text = "0 %";
            // 
            // ClipboardProgressBar
            // 
            this.ClipboardProgressBar.Location = new System.Drawing.Point(29, 80);
            this.ClipboardProgressBar.Name = "ClipboardProgressBar";
            this.ClipboardProgressBar.Size = new System.Drawing.Size(205, 23);
            this.ClipboardProgressBar.TabIndex = 6;
            // 
            // AvanzClipLabel
            // 
            this.AvanzClipLabel.AutoSize = true;
            this.AvanzClipLabel.BackColor = System.Drawing.Color.Transparent;
            this.AvanzClipLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvanzClipLabel.Location = new System.Drawing.Point(36, 16);
            this.AvanzClipLabel.Name = "AvanzClipLabel";
            this.AvanzClipLabel.Size = new System.Drawing.Size(261, 38);
            this.AvanzClipLabel.TabIndex = 1;
            this.AvanzClipLabel.Text = "Avanzamento dell\'operazione richiesta \r\nsulla Clipboard!!";
            this.AvanzClipLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(194, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 36);
            this.label2.TabIndex = 30;
            this.label2.Text = "Stai Comandando il seguente Server: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(24, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "TYPE OF DATA :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Sienna;
            this.ClientSize = new System.Drawing.Size(1071, 608);
            this.Controls.Add(this.ActionPanel);
            this.Controls.Add(this.MainPanel);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
            this.ComandiGroupBox.ResumeLayout(false);
            this.ComandiGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClipPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MousePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ContentClipboardPanel.ResumeLayout(false);
            this.ContentClipboardPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageClipboardPictureBox)).EndInit();
            this.ProgressBarPanel.ResumeLayout(false);
            this.ProgressBarPanel.PerformLayout();
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
        private System.Windows.Forms.Button ClearCacheButton;
        private System.Windows.Forms.Button LoadConfButton;
        private System.Windows.Forms.Button SaveServButton;
        private System.ComponentModel.BackgroundWorker ClipboardSendBW;
        private System.ComponentModel.BackgroundWorker ClipboardRequestBW;
        private System.Windows.Forms.Button LogOutButton;
        private System.Windows.Forms.GroupBox ComandiGroupBox;
        private System.Windows.Forms.Label ComandLabel;
        private System.Windows.Forms.Label InfoContentLabel;
        private System.Windows.Forms.PictureBox ClipPictureBox;
        private System.Windows.Forms.PictureBox MousePictureBox;
        private System.Windows.Forms.PictureBox KeyPictureBox;
        private System.Windows.Forms.Panel ProgressBarPanel;
        private System.Windows.Forms.Label PercentageLabel;
        private System.Windows.Forms.ProgressBar ClipboardProgressBar;
        private System.Windows.Forms.Label AvanzClipLabel;
        private System.Windows.Forms.Panel ContentClipboardPanel;
        private System.Windows.Forms.Button StopAudioButton;
        private System.Windows.Forms.Button PlayAudioButton;
        private System.Windows.Forms.RichTextBox RichTextBox;
        private System.Windows.Forms.PictureBox ImageClipboardPictureBox;
        private System.Windows.Forms.Label TypeClipboardLabel;
        private System.Windows.Forms.Label TitleContentClipLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}