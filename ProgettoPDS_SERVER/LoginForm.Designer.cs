namespace ProgettoPDS_SERVER
{
    partial class LoginForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registraUtenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiudiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.indirizzoIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informazioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.NotaLabel = new System.Windows.Forms.Label();
            this.RegistraPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RegistraButton = new System.Windows.Forms.Button();
            this.PwdRegTextBox = new System.Windows.Forms.TextBox();
            this.UserRegTextBox = new System.Windows.Forms.TextBox();
            this.CognomeRegTextBox = new System.Windows.Forms.TextBox();
            this.NameRegTextBox = new System.Windows.Forms.TextBox();
            this.PwdRegLabel = new System.Windows.Forms.Label();
            this.UserRegLabel = new System.Windows.Forms.Label();
            this.CognomeRegLabel = new System.Windows.Forms.Label();
            this.NameRegLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TitleInfoLabel = new System.Windows.Forms.Label();
            this.cambiaPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangePasswordPanel = new System.Windows.Forms.Panel();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ChangeUserTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ChangePwdTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.RegistraPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.InfoPanel.SuspendLayout();
            this.ChangePasswordPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuStrip1.BackgroundImage")));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginPageToolStripMenuItem,
            this.registraUtenteToolStripMenuItem,
            this.cambiaPasswordToolStripMenuItem,
            this.chiudiToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loginPageToolStripMenuItem
            // 
            this.loginPageToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.loginPageToolStripMenuItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.loginPageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loginPageToolStripMenuItem.Image")));
            this.loginPageToolStripMenuItem.Name = "loginPageToolStripMenuItem";
            this.loginPageToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.loginPageToolStripMenuItem.Text = "Login Page";
            this.loginPageToolStripMenuItem.Click += new System.EventHandler(this.loginPageToolStripMenuItem_Click);
            // 
            // registraUtenteToolStripMenuItem
            // 
            this.registraUtenteToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.registraUtenteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registraUtenteToolStripMenuItem.Image")));
            this.registraUtenteToolStripMenuItem.Name = "registraUtenteToolStripMenuItem";
            this.registraUtenteToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.registraUtenteToolStripMenuItem.Text = "Registra Utente";
            this.registraUtenteToolStripMenuItem.Click += new System.EventHandler(this.registraUtenteToolStripMenuItem_Click);
            // 
            // chiudiToolStripMenuItem
            // 
            this.chiudiToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.chiudiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("chiudiToolStripMenuItem.Image")));
            this.chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            this.chiudiToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.chiudiToolStripMenuItem.Text = "Chiudi";
            this.chiudiToolStripMenuItem.Click += new System.EventHandler(this.chiudiToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indirizzoIPToolStripMenuItem,
            this.informazioniToolStripMenuItem});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // indirizzoIPToolStripMenuItem
            // 
            this.indirizzoIPToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.indirizzoIPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indirizzoIPToolStripMenuItem.Image")));
            this.indirizzoIPToolStripMenuItem.Name = "indirizzoIPToolStripMenuItem";
            this.indirizzoIPToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.indirizzoIPToolStripMenuItem.Text = "Indirizzo IP";
            this.indirizzoIPToolStripMenuItem.Click += new System.EventHandler(this.indirizzoIPToolStripMenuItem_Click);
            // 
            // informazioniToolStripMenuItem
            // 
            this.informazioniToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.informazioniToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("informazioniToolStripMenuItem.Image")));
            this.informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
            this.informazioniToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.informazioniToolStripMenuItem.Text = "Informazioni";
            this.informazioniToolStripMenuItem.Click += new System.EventHandler(this.informazioniToolStripMenuItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainPanel.BackgroundImage")));
            this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainPanel.Controls.Add(this.UsernameLabel);
            this.MainPanel.Controls.Add(this.PasswordLabel);
            this.MainPanel.Controls.Add(this.LoginButton);
            this.MainPanel.Controls.Add(this.pictureBox1);
            this.MainPanel.Controls.Add(this.UsernameTextBox);
            this.MainPanel.Controls.Add(this.PasswordTextBox);
            this.MainPanel.Controls.Add(this.NotaLabel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(632, 346);
            this.MainPanel.TabIndex = 11;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.UsernameLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.ForeColor = System.Drawing.Color.White;
            this.UsernameLabel.Location = new System.Drawing.Point(17, 50);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(101, 19);
            this.UsernameLabel.TabIndex = 17;
            this.UsernameLabel.Text = "USERNAME :";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.PasswordLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.ForeColor = System.Drawing.Color.White;
            this.PasswordLabel.Location = new System.Drawing.Point(17, 101);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(115, 19);
            this.PasswordLabel.TabIndex = 16;
            this.PasswordLabel.Text = "PASSWORD :  ";
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.SlateGray;
            this.LoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.LoginButton.FlatAppearance.BorderSize = 4;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.Font = new System.Drawing.Font("Comic Sans MS", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.Location = new System.Drawing.Point(152, 178);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 48);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "LOGIN";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(454, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(115, 115);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.BackColor = System.Drawing.Color.Silver;
            this.UsernameTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTextBox.Location = new System.Drawing.Point(152, 49);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(254, 24);
            this.UsernameTextBox.TabIndex = 1;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BackColor = System.Drawing.Color.Silver;
            this.PasswordTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(152, 100);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(254, 24);
            this.PasswordTextBox.TabIndex = 2;
            // 
            // NotaLabel
            // 
            this.NotaLabel.AutoSize = true;
            this.NotaLabel.BackColor = System.Drawing.Color.Transparent;
            this.NotaLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NotaLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotaLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.NotaLabel.Location = new System.Drawing.Point(39, 270);
            this.NotaLabel.Name = "NotaLabel";
            this.NotaLabel.Padding = new System.Windows.Forms.Padding(2);
            this.NotaLabel.Size = new System.Drawing.Size(497, 48);
            this.NotaLabel.TabIndex = 11;
            this.NotaLabel.Text = resources.GetString("NotaLabel.Text");
            // 
            // RegistraPanel
            // 
            this.RegistraPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RegistraPanel.BackgroundImage")));
            this.RegistraPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RegistraPanel.Controls.Add(this.groupBox1);
            this.RegistraPanel.Controls.Add(this.label2);
            this.RegistraPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegistraPanel.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegistraPanel.Location = new System.Drawing.Point(0, 24);
            this.RegistraPanel.Name = "RegistraPanel";
            this.RegistraPanel.Size = new System.Drawing.Size(632, 346);
            this.RegistraPanel.TabIndex = 19;
            this.RegistraPanel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.RegistraButton);
            this.groupBox1.Controls.Add(this.PwdRegTextBox);
            this.groupBox1.Controls.Add(this.UserRegTextBox);
            this.groupBox1.Controls.Add(this.CognomeRegTextBox);
            this.groupBox1.Controls.Add(this.NameRegTextBox);
            this.groupBox1.Controls.Add(this.PwdRegLabel);
            this.groupBox1.Controls.Add(this.UserRegLabel);
            this.groupBox1.Controls.Add(this.CognomeRegLabel);
            this.groupBox1.Controls.Add(this.NameRegLabel);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(33, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 228);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credenziali";
            // 
            // RegistraButton
            // 
            this.RegistraButton.BackColor = System.Drawing.Color.SeaGreen;
            this.RegistraButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegistraButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.RegistraButton.FlatAppearance.BorderSize = 3;
            this.RegistraButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegistraButton.Location = new System.Drawing.Point(393, 96);
            this.RegistraButton.Name = "RegistraButton";
            this.RegistraButton.Size = new System.Drawing.Size(89, 38);
            this.RegistraButton.TabIndex = 8;
            this.RegistraButton.Text = "REGISTRA";
            this.RegistraButton.UseVisualStyleBackColor = false;
            this.RegistraButton.Click += new System.EventHandler(this.RegistraButton_Click);
            // 
            // PwdRegTextBox
            // 
            this.PwdRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.PwdRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PwdRegTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.PwdRegTextBox.Location = new System.Drawing.Point(118, 161);
            this.PwdRegTextBox.MaxLength = 15;
            this.PwdRegTextBox.Name = "PwdRegTextBox";
            this.PwdRegTextBox.PasswordChar = '*';
            this.PwdRegTextBox.Size = new System.Drawing.Size(210, 22);
            this.PwdRegTextBox.TabIndex = 7;
            // 
            // UserRegTextBox
            // 
            this.UserRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.UserRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserRegTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.UserRegTextBox.Location = new System.Drawing.Point(117, 122);
            this.UserRegTextBox.MaxLength = 30;
            this.UserRegTextBox.Name = "UserRegTextBox";
            this.UserRegTextBox.Size = new System.Drawing.Size(211, 22);
            this.UserRegTextBox.TabIndex = 6;
            // 
            // CognomeRegTextBox
            // 
            this.CognomeRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.CognomeRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CognomeRegTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.CognomeRegTextBox.Location = new System.Drawing.Point(117, 81);
            this.CognomeRegTextBox.MaxLength = 30;
            this.CognomeRegTextBox.Name = "CognomeRegTextBox";
            this.CognomeRegTextBox.Size = new System.Drawing.Size(211, 22);
            this.CognomeRegTextBox.TabIndex = 5;
            // 
            // NameRegTextBox
            // 
            this.NameRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.NameRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameRegTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.NameRegTextBox.Location = new System.Drawing.Point(117, 37);
            this.NameRegTextBox.MaxLength = 30;
            this.NameRegTextBox.Name = "NameRegTextBox";
            this.NameRegTextBox.Size = new System.Drawing.Size(211, 22);
            this.NameRegTextBox.TabIndex = 4;
            // 
            // PwdRegLabel
            // 
            this.PwdRegLabel.AutoSize = true;
            this.PwdRegLabel.Location = new System.Drawing.Point(17, 163);
            this.PwdRegLabel.Name = "PwdRegLabel";
            this.PwdRegLabel.Size = new System.Drawing.Size(86, 17);
            this.PwdRegLabel.TabIndex = 3;
            this.PwdRegLabel.Text = "PASSWORD :";
            // 
            // UserRegLabel
            // 
            this.UserRegLabel.AutoSize = true;
            this.UserRegLabel.Location = new System.Drawing.Point(17, 124);
            this.UserRegLabel.Name = "UserRegLabel";
            this.UserRegLabel.Size = new System.Drawing.Size(87, 17);
            this.UserRegLabel.TabIndex = 2;
            this.UserRegLabel.Text = "USERNAME :";
            // 
            // CognomeRegLabel
            // 
            this.CognomeRegLabel.AutoSize = true;
            this.CognomeRegLabel.Location = new System.Drawing.Point(19, 83);
            this.CognomeRegLabel.Name = "CognomeRegLabel";
            this.CognomeRegLabel.Size = new System.Drawing.Size(80, 17);
            this.CognomeRegLabel.TabIndex = 1;
            this.CognomeRegLabel.Text = "COGNOME :";
            // 
            // NameRegLabel
            // 
            this.NameRegLabel.AutoSize = true;
            this.NameRegLabel.Location = new System.Drawing.Point(19, 39);
            this.NameRegLabel.Name = "NameRegLabel";
            this.NameRegLabel.Size = new System.Drawing.Size(55, 17);
            this.NameRegLabel.TabIndex = 0;
            this.NameRegLabel.Text = "NOME :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(174, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "INSERISCI LE TUE CREDENZIALI";
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InfoPanel.BackgroundImage")));
            this.InfoPanel.Controls.Add(this.label4);
            this.InfoPanel.Controls.Add(this.label3);
            this.InfoPanel.Controls.Add(this.TitleInfoLabel);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoPanel.Location = new System.Drawing.Point(0, 24);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(632, 346);
            this.InfoPanel.TabIndex = 20;
            this.InfoPanel.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(19, 53);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(2);
            this.label4.Size = new System.Drawing.Size(588, 245);
            this.label4.TabIndex = 11;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(420, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Realized by Davide B. , Filippo B.  2014 ©";
            // 
            // TitleInfoLabel
            // 
            this.TitleInfoLabel.AutoSize = true;
            this.TitleInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleInfoLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleInfoLabel.ForeColor = System.Drawing.Color.White;
            this.TitleInfoLabel.Location = new System.Drawing.Point(161, 14);
            this.TitleInfoLabel.Name = "TitleInfoLabel";
            this.TitleInfoLabel.Size = new System.Drawing.Size(297, 23);
            this.TitleInfoLabel.TabIndex = 1;
            this.TitleInfoLabel.Text = "Informazioni sulla schermata di Login";
            // 
            // cambiaPasswordToolStripMenuItem
            // 
            this.cambiaPasswordToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.cambiaPasswordToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cambiaPasswordToolStripMenuItem.Image")));
            this.cambiaPasswordToolStripMenuItem.Name = "cambiaPasswordToolStripMenuItem";
            this.cambiaPasswordToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.cambiaPasswordToolStripMenuItem.Text = "Cambia Password";
            this.cambiaPasswordToolStripMenuItem.Click += new System.EventHandler(this.cambiaPasswordToolStripMenuItem_Click);
            // 
            // ChangePasswordPanel
            // 
            this.ChangePasswordPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ChangePasswordPanel.BackgroundImage")));
            this.ChangePasswordPanel.Controls.Add(this.ChangePwdTextBox);
            this.ChangePasswordPanel.Controls.Add(this.label6);
            this.ChangePasswordPanel.Controls.Add(this.ChangeUserTextBox);
            this.ChangePasswordPanel.Controls.Add(this.label5);
            this.ChangePasswordPanel.Controls.Add(this.label1);
            this.ChangePasswordPanel.Controls.Add(this.ChangeButton);
            this.ChangePasswordPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangePasswordPanel.Location = new System.Drawing.Point(0, 24);
            this.ChangePasswordPanel.Name = "ChangePasswordPanel";
            this.ChangePasswordPanel.Size = new System.Drawing.Size(632, 346);
            this.ChangePasswordPanel.TabIndex = 21;
            this.ChangePasswordPanel.Visible = false;
            // 
            // ChangeButton
            // 
            this.ChangeButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ChangeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChangeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ChangeButton.FlatAppearance.BorderSize = 3;
            this.ChangeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeButton.Font = new System.Drawing.Font("Comic Sans MS", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeButton.ForeColor = System.Drawing.Color.White;
            this.ChangeButton.Location = new System.Drawing.Point(247, 290);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(108, 35);
            this.ChangeButton.TabIndex = 7;
            this.ChangeButton.Text = "CHANGE!!";
            this.ChangeButton.UseVisualStyleBackColor = false;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(172, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "MODIFICA LA TUA PASSWORD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(88, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(421, 36);
            this.label5.TabIndex = 9;
            this.label5.Text = "Inserisci nella textbox qui sotto lo \"username\" usato abitualmente \r\nper accedere" +
    " all\'applicazione.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChangeUserTextBox
            // 
            this.ChangeUserTextBox.BackColor = System.Drawing.Color.DimGray;
            this.ChangeUserTextBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeUserTextBox.Location = new System.Drawing.Point(171, 107);
            this.ChangeUserTextBox.Name = "ChangeUserTextBox";
            this.ChangeUserTextBox.Size = new System.Drawing.Size(248, 26);
            this.ChangeUserTextBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(88, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(439, 36);
            this.label6.TabIndex = 11;
            this.label6.Text = "Inserisci nella textbox qui sotto la nuova password che sarà associata \r\nallo use" +
    "rname inserito sopra.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChangePwdTextBox
            // 
            this.ChangePwdTextBox.BackColor = System.Drawing.Color.DimGray;
            this.ChangePwdTextBox.Font = new System.Drawing.Font("Comic Sans MS", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangePwdTextBox.Location = new System.Drawing.Point(176, 233);
            this.ChangePwdTextBox.Name = "ChangePwdTextBox";
            this.ChangePwdTextBox.Size = new System.Drawing.Size(248, 26);
            this.ChangePwdTextBox.TabIndex = 12;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(632, 370);
            this.Controls.Add(this.InfoPanel);
            this.Controls.Add(this.ChangePasswordPanel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.RegistraPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progetto PDS - SERVER Login";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.RegistraPanel.ResumeLayout(false);
            this.RegistraPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            this.ChangePasswordPanel.ResumeLayout(false);
            this.ChangePasswordPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registraUtenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chiudiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem informazioniToolStripMenuItem;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label NotaLabel;
        private System.Windows.Forms.Panel RegistraPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button RegistraButton;
        private System.Windows.Forms.TextBox PwdRegTextBox;
        private System.Windows.Forms.TextBox UserRegTextBox;
        private System.Windows.Forms.TextBox CognomeRegTextBox;
        private System.Windows.Forms.TextBox NameRegTextBox;
        private System.Windows.Forms.Label PwdRegLabel;
        private System.Windows.Forms.Label UserRegLabel;
        private System.Windows.Forms.Label CognomeRegLabel;
        private System.Windows.Forms.Label NameRegLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem indirizzoIPToolStripMenuItem;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label TitleInfoLabel;
        private System.Windows.Forms.ToolStripMenuItem cambiaPasswordToolStripMenuItem;
        private System.Windows.Forms.Panel ChangePasswordPanel;
        private System.Windows.Forms.TextBox ChangeUserTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.TextBox ChangePwdTextBox;
        private System.Windows.Forms.Label label6;
    }
}

