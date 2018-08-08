namespace Xarial.AppLaunchKit.Services.Auth.Oidc.UI
{
    partial class LoginForm
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
            this.ctrlBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // ctrlBrowser
            // 
            this.ctrlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlBrowser.Location = new System.Drawing.Point(0, 0);
            this.ctrlBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ctrlBrowser.Name = "ctrlBrowser";
            this.ctrlBrowser.Size = new System.Drawing.Size(769, 698);
            this.ctrlBrowser.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 698);
            this.Controls.Add(this.ctrlBrowser);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser ctrlBrowser;
    }
}