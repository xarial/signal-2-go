namespace Xarial.Signal2Go.Services.Updates.UI
{
    partial class UpgradeForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lnkWhatsNew = new System.Windows.Forms.LinkLabel();
            this.lnkGetNewVersion = new System.Windows.Forms.LinkLabel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblMessage, 0, 0);
            this.tlpMain.Controls.Add(this.lnkWhatsNew, 0, 1);
            this.tlpMain.Controls.Add(this.lnkGetNewVersion, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(302, 202);
            this.tlpMain.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(118, 65);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(65, 17);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnkWhatsNew
            // 
            this.lnkWhatsNew.AutoSize = true;
            this.lnkWhatsNew.Location = new System.Drawing.Point(5, 153);
            this.lnkWhatsNew.Margin = new System.Windows.Forms.Padding(5);
            this.lnkWhatsNew.Name = "lnkWhatsNew";
            this.lnkWhatsNew.Size = new System.Drawing.Size(82, 17);
            this.lnkWhatsNew.TabIndex = 1;
            this.lnkWhatsNew.TabStop = true;
            this.lnkWhatsNew.Text = "What\'s New";
            this.lnkWhatsNew.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnWhatsNewLinkClicked);
            // 
            // lnkGetNewVersion
            // 
            this.lnkGetNewVersion.AutoSize = true;
            this.lnkGetNewVersion.Location = new System.Drawing.Point(5, 180);
            this.lnkGetNewVersion.Margin = new System.Windows.Forms.Padding(5);
            this.lnkGetNewVersion.Name = "lnkGetNewVersion";
            this.lnkGetNewVersion.Size = new System.Drawing.Size(114, 17);
            this.lnkGetNewVersion.TabIndex = 2;
            this.lnkGetNewVersion.TabStop = true;
            this.lnkGetNewVersion.Text = "Get New Version";
            this.lnkGetNewVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnGetNewVersionLinkClicked);
            // 
            // UpgradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 202);
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpgradeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpgradeForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkWhatsNew;
        private System.Windows.Forms.LinkLabel lnkGetNewVersion;
    }
}