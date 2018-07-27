namespace Client
{
    partial class ClientForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ipTextbox = new System.Windows.Forms.TextBox();
            this.portTextbox = new System.Windows.Forms.TextBox();
            this.conslole = new System.Windows.Forms.RichTextBox();
            this.showFiles = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipTextbox
            // 
            this.ipTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ipTextbox.Location = new System.Drawing.Point(12, 12);
            this.ipTextbox.Name = "ipTextbox";
            this.ipTextbox.Size = new System.Drawing.Size(243, 38);
            this.ipTextbox.TabIndex = 1;
            this.ipTextbox.TextChanged += new System.EventHandler(this.ipTextbox_TextChanged);
            // 
            // portTextbox
            // 
            this.portTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.portTextbox.Location = new System.Drawing.Point(261, 12);
            this.portTextbox.Name = "portTextbox";
            this.portTextbox.Size = new System.Drawing.Size(117, 38);
            this.portTextbox.TabIndex = 5;
            this.portTextbox.TextChanged += new System.EventHandler(this.portTextbox_TextChanged);
            // 
            // conslole
            // 
            this.conslole.BackColor = System.Drawing.Color.White;
            this.conslole.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.conslole.Location = new System.Drawing.Point(12, 56);
            this.conslole.MaximumSize = new System.Drawing.Size(366, 185);
            this.conslole.MinimumSize = new System.Drawing.Size(366, 185);
            this.conslole.Name = "conslole";
            this.conslole.ReadOnly = true;
            this.conslole.ShowSelectionMargin = true;
            this.conslole.Size = new System.Drawing.Size(366, 185);
            this.conslole.TabIndex = 6;
            this.conslole.Text = "";
            // 
            // showFiles
            // 
            this.showFiles.Location = new System.Drawing.Point(12, 247);
            this.showFiles.Name = "showFiles";
            this.showFiles.Size = new System.Drawing.Size(366, 35);
            this.showFiles.TabIndex = 8;
            this.showFiles.Text = "show servers files";
            this.showFiles.UseVisualStyleBackColor = true;
            this.showFiles.Click += new System.EventHandler(this.showFiles_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 290);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(389, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 312);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.showFiles);
            this.Controls.Add(this.conslole);
            this.Controls.Add(this.portTextbox);
            this.Controls.Add(this.ipTextbox);
            this.MaximumSize = new System.Drawing.Size(405, 350);
            this.MinimumSize = new System.Drawing.Size(405, 350);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ipTextbox;
        private System.Windows.Forms.TextBox portTextbox;
        private System.Windows.Forms.RichTextBox conslole;
        private System.Windows.Forms.Button showFiles;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}