namespace AmaranthineServer
{
    partial class UserCtrl_SettingsEntry
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_SettingsName = new System.Windows.Forms.Label();
            this.txtb_SettingsValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_SettingsName
            // 
            this.lbl_SettingsName.AutoSize = true;
            this.lbl_SettingsName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SettingsName.Location = new System.Drawing.Point(3, 19);
            this.lbl_SettingsName.Name = "lbl_SettingsName";
            this.lbl_SettingsName.Size = new System.Drawing.Size(51, 20);
            this.lbl_SettingsName.TabIndex = 0;
            this.lbl_SettingsName.Text = "label1";
            // 
            // txtb_SettingsValue
            // 
            this.txtb_SettingsValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_SettingsValue.Location = new System.Drawing.Point(327, 21);
            this.txtb_SettingsValue.Name = "txtb_SettingsValue";
            this.txtb_SettingsValue.Size = new System.Drawing.Size(306, 20);
            this.txtb_SettingsValue.TabIndex = 1;
            // 
            // UserCtrl_SettingsEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtb_SettingsValue);
            this.Controls.Add(this.lbl_SettingsName);
            this.MaximumSize = new System.Drawing.Size(645, 60);
            this.Name = "UserCtrl_SettingsEntry";
            this.Size = new System.Drawing.Size(645, 60);
            this.Load += new System.EventHandler(this.UserCtrl_SettingsEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_SettingsName;
        private System.Windows.Forms.TextBox txtb_SettingsValue;
    }
}
