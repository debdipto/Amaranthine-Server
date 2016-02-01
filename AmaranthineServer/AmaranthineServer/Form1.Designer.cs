namespace AmaranthineServer
{
    partial class frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            this.menu_MenuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAmaranthineServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_MasterLogReader = new System.Windows.Forms.Button();
            this.flow_ListOfClients = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_ServerOnOff = new System.Windows.Forms.Button();
            this.lbl_ServerSettings = new System.Windows.Forms.Label();
            this.txtb_ServerSettings = new System.Windows.Forms.TextBox();
            this.rtxtb_ServerDisplay = new System.Windows.Forms.RichTextBox();
            this.txtb_WatcherLogs = new System.Windows.Forms.TextBox();
            this.lbl_WatcherDisplay = new System.Windows.Forms.Label();
            this.btn_ArchiveLog = new System.Windows.Forms.Button();
            this.btn_ClearWatcherLog = new System.Windows.Forms.Button();
            this.menu_MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_MenuBar
            // 
            this.menu_MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu_MenuBar.Location = new System.Drawing.Point(0, 0);
            this.menu_MenuBar.Name = "menu_MenuBar";
            this.menu_MenuBar.Size = new System.Drawing.Size(1000, 24);
            this.menu_MenuBar.TabIndex = 0;
            this.menu_MenuBar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverPropertiesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // serverPropertiesToolStripMenuItem
            // 
            this.serverPropertiesToolStripMenuItem.Name = "serverPropertiesToolStripMenuItem";
            this.serverPropertiesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.serverPropertiesToolStripMenuItem.Text = "Server &Properties";
            this.serverPropertiesToolStripMenuItem.Click += new System.EventHandler(this.serverPropertiesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutAmaranthineServerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutAmaranthineServerToolStripMenuItem
            // 
            this.aboutAmaranthineServerToolStripMenuItem.Name = "aboutAmaranthineServerToolStripMenuItem";
            this.aboutAmaranthineServerToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.aboutAmaranthineServerToolStripMenuItem.Text = "&About Amaranthine Server";
            this.aboutAmaranthineServerToolStripMenuItem.Click += new System.EventHandler(this.aboutAmaranthineServerToolStripMenuItem_Click);
            // 
            // btn_MasterLogReader
            // 
            this.btn_MasterLogReader.Location = new System.Drawing.Point(246, 35);
            this.btn_MasterLogReader.Name = "btn_MasterLogReader";
            this.btn_MasterLogReader.Size = new System.Drawing.Size(147, 23);
            this.btn_MasterLogReader.TabIndex = 14;
            this.btn_MasterLogReader.Text = "Open Master Log Reader";
            this.btn_MasterLogReader.UseVisualStyleBackColor = true;
            // 
            // flow_ListOfClients
            // 
            this.flow_ListOfClients.AutoScroll = true;
            this.flow_ListOfClients.BackColor = System.Drawing.SystemColors.Info;
            this.flow_ListOfClients.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flow_ListOfClients.Dock = System.Windows.Forms.DockStyle.Right;
            this.flow_ListOfClients.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flow_ListOfClients.Location = new System.Drawing.Point(784, 24);
            this.flow_ListOfClients.Name = "flow_ListOfClients";
            this.flow_ListOfClients.Size = new System.Drawing.Size(216, 689);
            this.flow_ListOfClients.TabIndex = 13;
            // 
            // btn_ServerOnOff
            // 
            this.btn_ServerOnOff.Location = new System.Drawing.Point(6, 35);
            this.btn_ServerOnOff.Name = "btn_ServerOnOff";
            this.btn_ServerOnOff.Size = new System.Drawing.Size(107, 23);
            this.btn_ServerOnOff.TabIndex = 12;
            this.btn_ServerOnOff.Text = "Turn Server ON";
            this.btn_ServerOnOff.UseVisualStyleBackColor = true;
            this.btn_ServerOnOff.Click += new System.EventHandler(this.btn_ServerOnOff_Click);
            // 
            // lbl_ServerSettings
            // 
            this.lbl_ServerSettings.AutoSize = true;
            this.lbl_ServerSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ServerSettings.Location = new System.Drawing.Point(12, 71);
            this.lbl_ServerSettings.Name = "lbl_ServerSettings";
            this.lbl_ServerSettings.Size = new System.Drawing.Size(144, 16);
            this.lbl_ServerSettings.TabIndex = 16;
            this.lbl_ServerSettings.Text = "Current Server Settings";
            // 
            // txtb_ServerSettings
            // 
            this.txtb_ServerSettings.BackColor = System.Drawing.SystemColors.Info;
            this.txtb_ServerSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtb_ServerSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_ServerSettings.Location = new System.Drawing.Point(6, 99);
            this.txtb_ServerSettings.Multiline = true;
            this.txtb_ServerSettings.Name = "txtb_ServerSettings";
            this.txtb_ServerSettings.ReadOnly = true;
            this.txtb_ServerSettings.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtb_ServerSettings.Size = new System.Drawing.Size(355, 162);
            this.txtb_ServerSettings.TabIndex = 15;
            // 
            // rtxtb_ServerDisplay
            // 
            this.rtxtb_ServerDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtb_ServerDisplay.BackColor = System.Drawing.Color.Black;
            this.rtxtb_ServerDisplay.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtxtb_ServerDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtb_ServerDisplay.ForeColor = System.Drawing.SystemColors.Info;
            this.rtxtb_ServerDisplay.Location = new System.Drawing.Point(6, 267);
            this.rtxtb_ServerDisplay.Name = "rtxtb_ServerDisplay";
            this.rtxtb_ServerDisplay.ReadOnly = true;
            this.rtxtb_ServerDisplay.Size = new System.Drawing.Size(760, 434);
            this.rtxtb_ServerDisplay.TabIndex = 18;
            this.rtxtb_ServerDisplay.Text = "";
            // 
            // txtb_WatcherLogs
            // 
            this.txtb_WatcherLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_WatcherLogs.BackColor = System.Drawing.SystemColors.Info;
            this.txtb_WatcherLogs.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtb_WatcherLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_WatcherLogs.Location = new System.Drawing.Point(367, 99);
            this.txtb_WatcherLogs.Multiline = true;
            this.txtb_WatcherLogs.Name = "txtb_WatcherLogs";
            this.txtb_WatcherLogs.ReadOnly = true;
            this.txtb_WatcherLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtb_WatcherLogs.Size = new System.Drawing.Size(399, 162);
            this.txtb_WatcherLogs.TabIndex = 19;
            // 
            // lbl_WatcherDisplay
            // 
            this.lbl_WatcherDisplay.AutoSize = true;
            this.lbl_WatcherDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WatcherDisplay.Location = new System.Drawing.Point(286, 71);
            this.lbl_WatcherDisplay.Name = "lbl_WatcherDisplay";
            this.lbl_WatcherDisplay.Size = new System.Drawing.Size(91, 16);
            this.lbl_WatcherDisplay.TabIndex = 20;
            this.lbl_WatcherDisplay.Text = "Watcher Logs";
            // 
            // btn_ArchiveLog
            // 
            this.btn_ArchiveLog.Location = new System.Drawing.Point(119, 35);
            this.btn_ArchiveLog.Name = "btn_ArchiveLog";
            this.btn_ArchiveLog.Size = new System.Drawing.Size(121, 23);
            this.btn_ArchiveLog.TabIndex = 21;
            this.btn_ArchiveLog.Text = "Archive (clear) Log";
            this.btn_ArchiveLog.UseVisualStyleBackColor = true;
            this.btn_ArchiveLog.Click += new System.EventHandler(this.btn_ArchiveLog_Click);
            // 
            // btn_ClearWatcherLog
            // 
            this.btn_ClearWatcherLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ClearWatcherLog.Location = new System.Drawing.Point(654, 68);
            this.btn_ClearWatcherLog.Name = "btn_ClearWatcherLog";
            this.btn_ClearWatcherLog.Size = new System.Drawing.Size(112, 23);
            this.btn_ClearWatcherLog.TabIndex = 22;
            this.btn_ClearWatcherLog.Text = "Clear Watcher Log";
            this.btn_ClearWatcherLog.UseVisualStyleBackColor = true;
            this.btn_ClearWatcherLog.Click += new System.EventHandler(this.btn_ClearWatcherLog_Click);
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 713);
            this.Controls.Add(this.btn_ClearWatcherLog);
            this.Controls.Add(this.btn_ArchiveLog);
            this.Controls.Add(this.lbl_WatcherDisplay);
            this.Controls.Add(this.txtb_WatcherLogs);
            this.Controls.Add(this.rtxtb_ServerDisplay);
            this.Controls.Add(this.txtb_ServerSettings);
            this.Controls.Add(this.btn_MasterLogReader);
            this.Controls.Add(this.lbl_ServerSettings);
            this.Controls.Add(this.flow_ListOfClients);
            this.Controls.Add(this.btn_ServerOnOff);
            this.Controls.Add(this.menu_MenuBar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu_MenuBar;
            this.Name = "frm_Main";
            this.Text = "Amaranthine Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Main_FormClosing);
            this.Load += new System.EventHandler(this.frm_Main_Load);
            this.menu_MenuBar.ResumeLayout(false);
            this.menu_MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu_MenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAmaranthineServerToolStripMenuItem;
        private System.Windows.Forms.Button btn_MasterLogReader;
        private System.Windows.Forms.FlowLayoutPanel flow_ListOfClients;
        private System.Windows.Forms.Button btn_ServerOnOff;
        private System.Windows.Forms.Label lbl_ServerSettings;
        private System.Windows.Forms.TextBox txtb_ServerSettings;
        private System.Windows.Forms.RichTextBox rtxtb_ServerDisplay;
        private System.Windows.Forms.TextBox txtb_WatcherLogs;
        private System.Windows.Forms.Label lbl_WatcherDisplay;
        private System.Windows.Forms.Button btn_ArchiveLog;
        private System.Windows.Forms.Button btn_ClearWatcherLog;
    }
}

