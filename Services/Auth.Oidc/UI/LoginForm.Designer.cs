namespace Xarial.Signal2Go.Services.Auth.Oidc.UI
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.chkStayLoggedIn = new System.Windows.Forms.CheckBox();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlBrowser
            // 
            this.ctrlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlBrowser.Location = new System.Drawing.Point(3, 3);
            this.ctrlBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ctrlBrowser.Name = "ctrlBrowser";
            this.ctrlBrowser.Size = new System.Drawing.Size(763, 665);
            this.ctrlBrowser.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.ctrlBrowser, 0, 0);
            this.tlpMain.Controls.Add(this.chkStayLoggedIn, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(769, 698);
            this.tlpMain.TabIndex = 1;
            // 
            // chkStayLoggedIn
            // 
            this.chkStayLoggedIn.AutoSize = true;
            this.chkStayLoggedIn.Location = new System.Drawing.Point(3, 674);
            this.chkStayLoggedIn.Name = "chkStayLoggedIn";
            this.chkStayLoggedIn.Size = new System.Drawing.Size(125, 21);
            this.chkStayLoggedIn.TabIndex = 1;
            this.chkStayLoggedIn.Text = "Stay Logged In";
            this.chkStayLoggedIn.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 698);
            this.Controls.Add(this.tlpMain);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser ctrlBrowser;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.CheckBox chkStayLoggedIn;
    }
}