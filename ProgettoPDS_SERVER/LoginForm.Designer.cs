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
            this.informazioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RegistraPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PwdRegTextBox = new System.Windows.Forms.TextBox();
            this.UserRegTextBox = new System.Windows.Forms.TextBox();
            this.CognomeRegTextBox = new System.Windows.Forms.TextBox();
            this.NameRegTextBox = new System.Windows.Forms.TextBox();
            this.PwdRegLabel = new System.Windows.Forms.Label();
            this.UserRegLabel = new System.Windows.Forms.Label();
            this.CognomeRegLabel = new System.Windows.Forms.Label();
            this.NameRegLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.RegistraPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(579, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginPageToolStripMenuItem,
            this.registraUtenteToolStripMenuItem,
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
            this.loginPageToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.loginPageToolStripMenuItem.Text = "Login Page";
            this.loginPageToolStripMenuItem.Click += new System.EventHandler(this.loginPageToolStripMenuItem_Click);
            // 
            // registraUtenteToolStripMenuItem
            // 
            this.registraUtenteToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.registraUtenteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registraUtenteToolStripMenuItem.Image")));
            this.registraUtenteToolStripMenuItem.Name = "registraUtenteToolStripMenuItem";
            this.registraUtenteToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.registraUtenteToolStripMenuItem.Text = "Registra Utente";
            this.registraUtenteToolStripMenuItem.Click += new System.EventHandler(this.registraUtenteToolStripMenuItem_Click);
            // 
            // chiudiToolStripMenuItem
            // 
            this.chiudiToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.chiudiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("chiudiToolStripMenuItem.Image")));
            this.chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            this.chiudiToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.chiudiToolStripMenuItem.Text = "Chiudi";
            this.chiudiToolStripMenuItem.Click += new System.EventHandler(this.chiudiToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informazioniToolStripMenuItem});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // informazioniToolStripMenuItem
            // 
            this.informazioniToolStripMenuItem.BackColor = System.Drawing.SystemColors.Info;
            this.informazioniToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("informazioniToolStripMenuItem.Image")));
            this.informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
            this.informazioniToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.informazioniToolStripMenuItem.Text = "Informazioni";
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
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(579, 296);
            this.MainPanel.TabIndex = 11;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.UsernameLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.ForeColor = System.Drawing.Color.White;
            this.UsernameLabel.Location = new System.Drawing.Point(18, 46);
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
            this.PasswordLabel.Location = new System.Drawing.Point(18, 99);
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
            this.LoginButton.Location = new System.Drawing.Point(139, 152);
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
            this.pictureBox1.Location = new System.Drawing.Point(419, 21);
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
            this.UsernameTextBox.Location = new System.Drawing.Point(139, 45);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(254, 24);
            this.UsernameTextBox.TabIndex = 1;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BackColor = System.Drawing.Color.Silver;
            this.PasswordTextBox.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(139, 98);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(254, 24);
            this.PasswordTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(22, 230);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(497, 34);
            this.label1.TabIndex = 11;
            this.label1.Text = "NOTA :  Se stai usando l\'applicativo per la prima volta e quindi non sei ancora i" +
    "scritto nel sistema, \r\n             sei pregato di registrare le tue credenziali" +
    ".";
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
            this.RegistraPanel.Size = new System.Drawing.Size(579, 296);
            this.RegistraPanel.TabIndex = 19;
            this.RegistraPanel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.PwdRegTextBox);
            this.groupBox1.Controls.Add(this.UserRegTextBox);
            this.groupBox1.Controls.Add(this.CognomeRegTextBox);
            this.groupBox1.Controls.Add(this.NameRegTextBox);
            this.groupBox1.Controls.Add(this.PwdRegLabel);
            this.groupBox1.Controls.Add(this.UserRegLabel);
            this.groupBox1.Controls.Add(this.CognomeRegLabel);
            this.groupBox1.Controls.Add(this.NameRegLabel);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(21, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 197);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credenziali";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SeaGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(385, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 38);
            this.button1.TabIndex = 8;
            this.button1.Text = "REGISTRA";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // PwdRegTextBox
            // 
            this.PwdRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.PwdRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PwdRegTextBox.Location = new System.Drawing.Point(118, 143);
            this.PwdRegTextBox.MaxLength = 15;
            this.PwdRegTextBox.Name = "PwdRegTextBox";
            this.PwdRegTextBox.PasswordChar = '*';
            this.PwdRegTextBox.Size = new System.Drawing.Size(210, 22);
            this.PwdRegTextBox.TabIndex = 7;
            // 
            // UserRegTextBox
            // 
            this.UserRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.UserRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserRegTextBox.Location = new System.Drawing.Point(117, 108);
            this.UserRegTextBox.MaxLength = 30;
            this.UserRegTextBox.Name = "UserRegTextBox";
            this.UserRegTextBox.Size = new System.Drawing.Size(211, 22);
            this.UserRegTextBox.TabIndex = 6;
            // 
            // CognomeRegTextBox
            // 
            this.CognomeRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.CognomeRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CognomeRegTextBox.Location = new System.Drawing.Point(117, 71);
            this.CognomeRegTextBox.MaxLength = 30;
            this.CognomeRegTextBox.Name = "CognomeRegTextBox";
            this.CognomeRegTextBox.Size = new System.Drawing.Size(211, 22);
            this.CognomeRegTextBox.TabIndex = 5;
            // 
            // NameRegTextBox
            // 
            this.NameRegTextBox.BackColor = System.Drawing.Color.Tan;
            this.NameRegTextBox.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.PwdRegLabel.Location = new System.Drawing.Point(23, 146);
            this.PwdRegLabel.Name = "PwdRegLabel";
            this.PwdRegLabel.Size = new System.Drawing.Size(84, 17);
            this.PwdRegLabel.TabIndex = 3;
            this.PwdRegLabel.Text = "PASSWORD :";
            // 
            // UserRegLabel
            // 
            this.UserRegLabel.AutoSize = true;
            this.UserRegLabel.Location = new System.Drawing.Point(23, 111);
            this.UserRegLabel.Name = "UserRegLabel";
            this.UserRegLabel.Size = new System.Drawing.Size(84, 17);
            this.UserRegLabel.TabIndex = 2;
            this.UserRegLabel.Text = "USERNAME :";
            // 
            // CognomeRegLabel
            // 
            this.CognomeRegLabel.AutoSize = true;
            this.CognomeRegLabel.Location = new System.Drawing.Point(23, 73);
            this.CognomeRegLabel.Name = "CognomeRegLabel";
            this.CognomeRegLabel.Size = new System.Drawing.Size(79, 17);
            this.CognomeRegLabel.TabIndex = 1;
            this.CognomeRegLabel.Text = "COGNOME :";
            // 
            // NameRegLabel
            // 
            this.NameRegLabel.AutoSize = true;
            this.NameRegLabel.Location = new System.Drawing.Point(23, 37);
            this.NameRegLabel.Name = "NameRegLabel";
            this.NameRegLabel.Size = new System.Drawing.Size(54, 17);
            this.NameRegLabel.TabIndex = 0;
            this.NameRegLabel.Text = "NOME :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(137, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "INSERISCI LE TUE CREDENZIALI";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(579, 320);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.RegistraPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel RegistraPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox PwdRegTextBox;
        private System.Windows.Forms.TextBox UserRegTextBox;
        private System.Windows.Forms.TextBox CognomeRegTextBox;
        private System.Windows.Forms.TextBox NameRegTextBox;
        private System.Windows.Forms.Label PwdRegLabel;
        private System.Windows.Forms.Label UserRegLabel;
        private System.Windows.Forms.Label CognomeRegLabel;
        private System.Windows.Forms.Label NameRegLabel;
        private System.Windows.Forms.Label label2;
    }
}

