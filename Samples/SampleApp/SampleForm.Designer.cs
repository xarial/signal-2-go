namespace SampleApp
{
    partial class SampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleForm));
            this.pnlUpdates = new System.Windows.Forms.FlowLayoutPanel();
            this.lblUpdates1 = new System.Windows.Forms.Label();
            this.lnkServer = new System.Windows.Forms.LinkLabel();
            this.lblUpdates2 = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEula = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEula1 = new System.Windows.Forms.Label();
            this.lnkEulaCache = new System.Windows.Forms.LinkLabel();
            this.lblEula2 = new System.Windows.Forms.Label();
            this.pnlUserSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.lblUserSettings1 = new System.Windows.Forms.Label();
            this.lnkUserSettings = new System.Windows.Forms.LinkLabel();
            this.lblUserMessage = new System.Windows.Forms.Label();
            this.lblUserSettings2 = new System.Windows.Forms.Label();
            this.pnlLog = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLog1 = new System.Windows.Forms.Label();
            this.lnkEventLog = new System.Windows.Forms.LinkLabel();
            this.lblLog2 = new System.Windows.Forms.Label();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.pnlAuth = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAuth1 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblAuth2 = new System.Windows.Forms.Label();
            this.pnlUpdates.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.pnlEula.SuspendLayout();
            this.pnlUserSettings.SuspendLayout();
            this.pnlLog.SuspendLayout();
            this.pnlAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUpdates
            // 
            this.pnlUpdates.Controls.Add(this.lblUpdates1);
            this.pnlUpdates.Controls.Add(this.lnkServer);
            this.pnlUpdates.Controls.Add(this.lblUpdates2);
            this.pnlUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUpdates.Location = new System.Drawing.Point(4, 148);
            this.pnlUpdates.Margin = new System.Windows.Forms.Padding(4);
            this.pnlUpdates.Name = "pnlUpdates";
            this.pnlUpdates.Size = new System.Drawing.Size(505, 64);
            this.pnlUpdates.TabIndex = 0;
            // 
            // lblUpdates1
            // 
            this.lblUpdates1.AutoSize = true;
            this.lblUpdates1.Location = new System.Drawing.Point(4, 0);
            this.lblUpdates1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpdates1.Name = "lblUpdates1";
            this.lblUpdates1.Size = new System.Drawing.Size(197, 17);
            this.lblUpdates1.TabIndex = 0;
            this.lblUpdates1.Text = "Updates server is emulated at";
            // 
            // lnkServer
            // 
            this.lnkServer.AutoSize = true;
            this.lnkServer.Location = new System.Drawing.Point(209, 0);
            this.lnkServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkServer.Name = "lnkServer";
            this.lnkServer.Size = new System.Drawing.Size(157, 17);
            this.lnkServer.TabIndex = 1;
            this.lnkServer.TabStop = true;
            this.lnkServer.Text = "App Data Server Folder";
            this.lnkServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnServerLinkClicked);
            // 
            // lblUpdates2
            // 
            this.lblUpdates2.AutoSize = true;
            this.lblUpdates2.Location = new System.Drawing.Point(4, 17);
            this.lblUpdates2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpdates2.Name = "lblUpdates2";
            this.lblUpdates2.Size = new System.Drawing.Size(488, 34);
            this.lblUpdates2.TabIndex = 2;
            this.lblUpdates2.Text = "New available version is set to 2.0.0.0. Modify the AssemblyVersionAttribute and " +
    "change the version to change the behavior of update";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlUpdates, 0, 2);
            this.tlpMain.Controls.Add(this.pnlEula, 0, 1);
            this.tlpMain.Controls.Add(this.pnlUserSettings, 0, 3);
            this.tlpMain.Controls.Add(this.pnlLog, 0, 4);
            this.tlpMain.Controls.Add(this.lnkAbout, 0, 5);
            this.tlpMain.Controls.Add(this.pnlAuth, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(4);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(513, 457);
            this.tlpMain.TabIndex = 1;
            // 
            // pnlEula
            // 
            this.pnlEula.Controls.Add(this.lblEula1);
            this.pnlEula.Controls.Add(this.lnkEulaCache);
            this.pnlEula.Controls.Add(this.lblEula2);
            this.pnlEula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEula.Location = new System.Drawing.Point(4, 76);
            this.pnlEula.Margin = new System.Windows.Forms.Padding(4);
            this.pnlEula.Name = "pnlEula";
            this.pnlEula.Size = new System.Drawing.Size(505, 64);
            this.pnlEula.TabIndex = 1;
            // 
            // lblEula1
            // 
            this.lblEula1.AutoSize = true;
            this.lblEula1.Location = new System.Drawing.Point(4, 0);
            this.lblEula1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEula1.Name = "lblEula1";
            this.lblEula1.Size = new System.Drawing.Size(198, 17);
            this.lblEula1.TabIndex = 1;
            this.lblEula1.Text = "Signed EULA data is stored at";
            // 
            // lnkEulaCache
            // 
            this.lnkEulaCache.AutoSize = true;
            this.lnkEulaCache.Location = new System.Drawing.Point(210, 0);
            this.lnkEulaCache.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkEulaCache.Name = "lnkEulaCache";
            this.lnkEulaCache.Size = new System.Drawing.Size(114, 17);
            this.lnkEulaCache.TabIndex = 2;
            this.lnkEulaCache.TabStop = true;
            this.lnkEulaCache.Text = "EULA Cache File";
            this.lnkEulaCache.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnEulaCacheLinkClicked);
            // 
            // lblEula2
            // 
            this.lblEula2.AutoSize = true;
            this.lblEula2.Location = new System.Drawing.Point(4, 17);
            this.lblEula2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEula2.Name = "lblEula2";
            this.lblEula2.Size = new System.Drawing.Size(403, 17);
            this.lblEula2.TabIndex = 3;
            this.lblEula2.Text = "Delete the file and restart application to show EULA form again";
            // 
            // pnlUserSettings
            // 
            this.pnlUserSettings.Controls.Add(this.lblUserSettings1);
            this.pnlUserSettings.Controls.Add(this.lnkUserSettings);
            this.pnlUserSettings.Controls.Add(this.lblUserMessage);
            this.pnlUserSettings.Controls.Add(this.lblUserSettings2);
            this.pnlUserSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUserSettings.Location = new System.Drawing.Point(3, 219);
            this.pnlUserSettings.Name = "pnlUserSettings";
            this.pnlUserSettings.Size = new System.Drawing.Size(507, 66);
            this.pnlUserSettings.TabIndex = 2;
            // 
            // lblUserSettings1
            // 
            this.lblUserSettings1.AutoSize = true;
            this.lblUserSettings1.Location = new System.Drawing.Point(3, 0);
            this.lblUserSettings1.Name = "lblUserSettings1";
            this.lblUserSettings1.Size = new System.Drawing.Size(204, 17);
            this.lblUserSettings1.TabIndex = 0;
            this.lblUserSettings1.Text = "This is a user setting read from";
            // 
            // lnkUserSettings
            // 
            this.lnkUserSettings.AutoSize = true;
            this.lnkUserSettings.Location = new System.Drawing.Point(213, 0);
            this.lnkUserSettings.Name = "lnkUserSettings";
            this.lnkUserSettings.Size = new System.Drawing.Size(119, 17);
            this.lnkUserSettings.TabIndex = 1;
            this.lnkUserSettings.TabStop = true;
            this.lnkUserSettings.Text = "User Settings File";
            this.lnkUserSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnUserSettingsLinkClicked);
            // 
            // lblUserMessage
            // 
            this.lblUserMessage.AutoSize = true;
            this.lblUserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserMessage.Location = new System.Drawing.Point(338, 0);
            this.lblUserMessage.Name = "lblUserMessage";
            this.lblUserMessage.Size = new System.Drawing.Size(90, 17);
            this.lblUserMessage.TabIndex = 2;
            this.lblUserMessage.Text = "<Message>";
            // 
            // lblUserSettings2
            // 
            this.lblUserSettings2.AutoSize = true;
            this.lblUserSettings2.Location = new System.Drawing.Point(3, 17);
            this.lblUserSettings2.Name = "lblUserSettings2";
            this.lblUserSettings2.Size = new System.Drawing.Size(496, 51);
            this.lblUserSettings2.TabIndex = 3;
            this.lblUserSettings2.Text = resources.GetString("lblUserSettings2.Text");
            // 
            // pnlLog
            // 
            this.pnlLog.Controls.Add(this.lblLog1);
            this.pnlLog.Controls.Add(this.lnkEventLog);
            this.pnlLog.Controls.Add(this.lblLog2);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLog.Location = new System.Drawing.Point(3, 291);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Size = new System.Drawing.Size(507, 66);
            this.pnlLog.TabIndex = 4;
            // 
            // lblLog1
            // 
            this.lblLog1.AutoSize = true;
            this.lblLog1.Location = new System.Drawing.Point(3, 0);
            this.lblLog1.Name = "lblLog1";
            this.lblLog1.Size = new System.Drawing.Size(175, 17);
            this.lblLog1.TabIndex = 3;
            this.lblLog1.Text = "Application log is written to";
            // 
            // lnkEventLog
            // 
            this.lnkEventLog.AutoSize = true;
            this.lnkEventLog.Location = new System.Drawing.Point(184, 0);
            this.lnkEventLog.Name = "lnkEventLog";
            this.lnkEventLog.Size = new System.Drawing.Size(129, 17);
            this.lnkEventLog.TabIndex = 4;
            this.lnkEventLog.TabStop = true;
            this.lnkEventLog.Text = "System Events Log";
            this.lnkEventLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnEventLogLinkClicked);
            // 
            // lblLog2
            // 
            this.lblLog2.AutoSize = true;
            this.lblLog2.Location = new System.Drawing.Point(3, 17);
            this.lblLog2.Name = "lblLog2";
            this.lblLog2.Size = new System.Drawing.Size(473, 34);
            this.lblLog2.TabIndex = 5;
            this.lblLog2.Text = "Administrative privileges are required on a first start in order to create log so" +
    "urce";
            // 
            // lnkAbout
            // 
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.Location = new System.Drawing.Point(3, 360);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(57, 17);
            this.lnkAbout.TabIndex = 5;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "About...";
            this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnAboutLinkClicked);
            // 
            // pnlAuth
            // 
            this.pnlAuth.Controls.Add(this.lblAuth1);
            this.pnlAuth.Controls.Add(this.lblUserName);
            this.pnlAuth.Controls.Add(this.lblAuth2);
            this.pnlAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAuth.Location = new System.Drawing.Point(3, 3);
            this.pnlAuth.Name = "pnlAuth";
            this.pnlAuth.Size = new System.Drawing.Size(507, 66);
            this.pnlAuth.TabIndex = 6;
            // 
            // lblAuth1
            // 
            this.lblAuth1.AutoSize = true;
            this.lblAuth1.Location = new System.Drawing.Point(3, 0);
            this.lblAuth1.Name = "lblAuth1";
            this.lblAuth1.Size = new System.Drawing.Size(167, 17);
            this.lblAuth1.TabIndex = 0;
            this.lblAuth1.Text = "Currently logged in user: ";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(176, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(20, 17);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "...";
            // 
            // lblAuth2
            // 
            this.lblAuth2.AutoSize = true;
            this.lblAuth2.Location = new System.Drawing.Point(3, 17);
            this.lblAuth2.Name = "lblAuth2";
            this.lblAuth2.Size = new System.Drawing.Size(430, 17);
            this.lblAuth2.TabIndex = 2;
            this.lblAuth2.Text = "Modify project user settings to set the auth service connection data";
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 457);
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SampleForm";
            this.Text = "Sample Application";
            this.pnlUpdates.ResumeLayout(false);
            this.pnlUpdates.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.pnlEula.ResumeLayout(false);
            this.pnlEula.PerformLayout();
            this.pnlUserSettings.ResumeLayout(false);
            this.pnlUserSettings.PerformLayout();
            this.pnlLog.ResumeLayout(false);
            this.pnlLog.PerformLayout();
            this.pnlAuth.ResumeLayout(false);
            this.pnlAuth.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlUpdates;
        private System.Windows.Forms.Label lblUpdates1;
        private System.Windows.Forms.LinkLabel lnkServer;
        private System.Windows.Forms.Label lblUpdates2;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblEula1;
        private System.Windows.Forms.FlowLayoutPanel pnlEula;
        private System.Windows.Forms.LinkLabel lnkEulaCache;
        private System.Windows.Forms.Label lblEula2;
        private System.Windows.Forms.FlowLayoutPanel pnlUserSettings;
        private System.Windows.Forms.Label lblUserSettings1;
        private System.Windows.Forms.LinkLabel lnkUserSettings;
        private System.Windows.Forms.FlowLayoutPanel pnlLog;
        private System.Windows.Forms.Label lblLog1;
        private System.Windows.Forms.LinkLabel lnkEventLog;
        private System.Windows.Forms.Label lblLog2;
        private System.Windows.Forms.Label lblUserMessage;
        private System.Windows.Forms.Label lblUserSettings2;
        private System.Windows.Forms.LinkLabel lnkAbout;
        private System.Windows.Forms.FlowLayoutPanel pnlAuth;
        private System.Windows.Forms.Label lblAuth1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblAuth2;
    }
}

