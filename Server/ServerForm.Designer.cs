namespace Server
{
    partial class ServerForm
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
            this.conslole = new System.Windows.Forms.RichTextBox();
            this.localip = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.internetip = new System.Windows.Forms.TextBox();
            this.stopServer = new System.Windows.Forms.Button();
            this.startServer = new System.Windows.Forms.Button();
            this.filesDirPath = new System.Windows.Forms.TextBox();
            this.lableFD = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // conslole
            // 
            this.conslole.BackColor = System.Drawing.Color.White;
            this.conslole.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.conslole.ForeColor = System.Drawing.Color.Black;
            this.conslole.Location = new System.Drawing.Point(12, 100);
            this.conslole.Name = "conslole";
            this.conslole.ReadOnly = true;
            this.conslole.ShowSelectionMargin = true;
            this.conslole.Size = new System.Drawing.Size(613, 227);
            this.conslole.TabIndex = 0;
            this.conslole.Text = "";
            // 
            // localip
            // 
            this.localip.BackColor = System.Drawing.Color.White;
            this.localip.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.localip.Location = new System.Drawing.Point(12, 12);
            this.localip.Name = "localip";
            this.localip.ReadOnly = true;
            this.localip.Size = new System.Drawing.Size(243, 38);
            this.localip.TabIndex = 1;
            // 
            // port
            // 
            this.port.BackColor = System.Drawing.Color.White;
            this.port.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.port.ForeColor = System.Drawing.Color.Black;
            this.port.Location = new System.Drawing.Point(508, 12);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(117, 38);
            this.port.TabIndex = 2;
            this.port.TextChanged += new System.EventHandler(this.port_TextChanged);
            // 
            // internetip
            // 
            this.internetip.BackColor = System.Drawing.Color.White;
            this.internetip.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.internetip.Location = new System.Drawing.Point(261, 12);
            this.internetip.Name = "internetip";
            this.internetip.ReadOnly = true;
            this.internetip.Size = new System.Drawing.Size(243, 38);
            this.internetip.TabIndex = 3;
            // 
            // stopServer
            // 
            this.stopServer.Location = new System.Drawing.Point(332, 333);
            this.stopServer.Name = "stopServer";
            this.stopServer.Size = new System.Drawing.Size(293, 41);
            this.stopServer.TabIndex = 4;
            this.stopServer.Text = "Stop";
            this.stopServer.UseVisualStyleBackColor = true;
            this.stopServer.Click += new System.EventHandler(this.stopServer_Click);
            // 
            // startServer
            // 
            this.startServer.Location = new System.Drawing.Point(12, 333);
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(304, 41);
            this.startServer.TabIndex = 5;
            this.startServer.Text = "Start";
            this.startServer.UseVisualStyleBackColor = true;
            this.startServer.Click += new System.EventHandler(this.startServer_Click);
            // 
            // filesDirPath
            // 
            this.filesDirPath.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filesDirPath.Location = new System.Drawing.Point(145, 59);
            this.filesDirPath.Name = "filesDirPath";
            this.filesDirPath.ReadOnly = true;
            this.filesDirPath.Size = new System.Drawing.Size(399, 32);
            this.filesDirPath.TabIndex = 6;
            // 
            // lableFD
            // 
            this.lableFD.AutoSize = true;
            this.lableFD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lableFD.Location = new System.Drawing.Point(12, 61);
            this.lableFD.Name = "lableFD";
            this.lableFD.Size = new System.Drawing.Size(127, 24);
            this.lableFD.TabIndex = 7;
            this.lableFD.Text = "Files directory";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(550, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 382);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lableFD);
            this.Controls.Add(this.filesDirPath);
            this.Controls.Add(this.startServer);
            this.Controls.Add(this.stopServer);
            this.Controls.Add(this.internetip);
            this.Controls.Add(this.port);
            this.Controls.Add(this.localip);
            this.Controls.Add(this.conslole);
            this.MaximumSize = new System.Drawing.Size(653, 420);
            this.MinimumSize = new System.Drawing.Size(653, 420);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox conslole;
        private System.Windows.Forms.TextBox localip;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox internetip;
        private System.Windows.Forms.Button stopServer;
        private System.Windows.Forms.Button startServer;
        private System.Windows.Forms.TextBox filesDirPath;
        private System.Windows.Forms.Label lableFD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timer1;
    }
}