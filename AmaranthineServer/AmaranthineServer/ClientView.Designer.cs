namespace AmaranthineServer
{
    partial class ClientView
    {
        private ClientDetails clientDetails;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private string userName;

    
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientView));
            this.btn_Clear = new System.Windows.Forms.Button();
            this.txtb_ClientDetailsViewer = new System.Windows.Forms.TextBox();
            this.btn_DisconnectClient = new System.Windows.Forms.Button();
            this.txtb_Display = new System.Windows.Forms.TextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(3, 403);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 35);
            this.btn_Clear.TabIndex = 7;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // txtb_ClientDetailsViewer
            // 
            this.txtb_ClientDetailsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_ClientDetailsViewer.BackColor = System.Drawing.SystemColors.Menu;
            this.txtb_ClientDetailsViewer.Location = new System.Drawing.Point(589, 11);
            this.txtb_ClientDetailsViewer.Multiline = true;
            this.txtb_ClientDetailsViewer.Name = "txtb_ClientDetailsViewer";
            this.txtb_ClientDetailsViewer.ReadOnly = true;
            this.txtb_ClientDetailsViewer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_ClientDetailsViewer.Size = new System.Drawing.Size(251, 383);
            this.txtb_ClientDetailsViewer.TabIndex = 6;
            // 
            // btn_DisconnectClient
            // 
            this.btn_DisconnectClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DisconnectClient.Location = new System.Drawing.Point(457, 403);
            this.btn_DisconnectClient.Name = "btn_DisconnectClient";
            this.btn_DisconnectClient.Size = new System.Drawing.Size(125, 35);
            this.btn_DisconnectClient.TabIndex = 5;
            this.btn_DisconnectClient.Text = "Disconnect Client";
            this.btn_DisconnectClient.UseVisualStyleBackColor = true;
            this.btn_DisconnectClient.Click += new System.EventHandler(this.btn_DisconnectClient_Click);
            // 
            // txtb_Display
            // 
            this.txtb_Display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_Display.BackColor = System.Drawing.Color.Black;
            this.txtb_Display.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtb_Display.ForeColor = System.Drawing.SystemColors.Info;
            this.txtb_Display.Location = new System.Drawing.Point(3, 11);
            this.txtb_Display.Multiline = true;
            this.txtb_Display.Name = "txtb_Display";
            this.txtb_Display.ReadOnly = true;
            this.txtb_Display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_Display.Size = new System.Drawing.Size(579, 383);
            this.txtb_Display.TabIndex = 4;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // ClientView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 448);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.txtb_ClientDetailsViewer);
            this.Controls.Add(this.btn_DisconnectClient);
            this.Controls.Add(this.txtb_Display);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientView";
            this.Text = "ClientView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TextBox txtb_ClientDetailsViewer;
        private System.Windows.Forms.Button btn_DisconnectClient;
        private System.Windows.Forms.TextBox txtb_Display;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}