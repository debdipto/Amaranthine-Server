namespace AmaranthineServer
{
    partial class frm_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Settings));
            this.btn_SaveChanges = new System.Windows.Forms.Button();
            this.flow_SettingsDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_warning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_SaveChanges
            // 
            this.btn_SaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SaveChanges.Location = new System.Drawing.Point(13, 465);
            this.btn_SaveChanges.Name = "btn_SaveChanges";
            this.btn_SaveChanges.Size = new System.Drawing.Size(121, 37);
            this.btn_SaveChanges.TabIndex = 1;
            this.btn_SaveChanges.Text = "Save Changes";
            this.btn_SaveChanges.UseVisualStyleBackColor = true;
            this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
            // 
            // flow_SettingsDisplay
            // 
            this.flow_SettingsDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flow_SettingsDisplay.AutoScroll = true;
            this.flow_SettingsDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flow_SettingsDisplay.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flow_SettingsDisplay.Location = new System.Drawing.Point(13, 13);
            this.flow_SettingsDisplay.Name = "flow_SettingsDisplay";
            this.flow_SettingsDisplay.Size = new System.Drawing.Size(672, 446);
            this.flow_SettingsDisplay.TabIndex = 2;
            this.flow_SettingsDisplay.WrapContents = false;
            // 
            // lbl_warning
            // 
            this.lbl_warning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_warning.AutoSize = true;
            this.lbl_warning.ForeColor = System.Drawing.Color.Red;
            this.lbl_warning.Location = new System.Drawing.Point(443, 477);
            this.lbl_warning.Name = "lbl_warning";
            this.lbl_warning.Size = new System.Drawing.Size(242, 13);
            this.lbl_warning.TabIndex = 3;
            this.lbl_warning.Text = "Changes will take effect after restarting the Server";
            // 
            // frm_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(697, 514);
            this.Controls.Add(this.lbl_warning);
            this.Controls.Add(this.flow_SettingsDisplay);
            this.Controls.Add(this.btn_SaveChanges);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_SaveChanges;
        private System.Windows.Forms.FlowLayoutPanel flow_SettingsDisplay;
        private System.Windows.Forms.Label lbl_warning;
    }
}