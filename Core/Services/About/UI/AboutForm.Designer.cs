namespace Xarial.AppLaunchKit.Services.About.UI
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.llpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tlpProduct = new System.Windows.Forms.TableLayoutPanel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtLicenses = new System.Windows.Forms.TextBox();
            this.btnEula = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.llpMain.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.tlpProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgLogo.Image = ((System.Drawing.Image)(resources.GetObject("imgLogo.Image")));
            this.imgLogo.Location = new System.Drawing.Point(4, 4);
            this.imgLogo.Margin = new System.Windows.Forms.Padding(4);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(133, 146);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgLogo.TabIndex = 12;
            this.imgLogo.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(2, 2);
            this.lblName.Margin = new System.Windows.Forms.Padding(2);
            this.lblName.MaximumSize = new System.Drawing.Size(0, 21);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(414, 21);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Product Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCopyright.Location = new System.Drawing.Point(2, 52);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(2);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(414, 17);
            this.lblCopyright.TabIndex = 0;
            this.lblCopyright.Text = "Copyright";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(451, 310);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(116, 30);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "&OK";
            // 
            // llpMain
            // 
            this.llpMain.ColumnCount = 1;
            this.llpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.llpMain.Controls.Add(this.tlpHeader, 0, 0);
            this.llpMain.Controls.Add(this.txtLicenses, 0, 1);
            this.llpMain.Controls.Add(this.btnEula, 0, 2);
            this.llpMain.Controls.Add(this.btnOk, 0, 3);
            this.llpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llpMain.Location = new System.Drawing.Point(12, 11);
            this.llpMain.Name = "llpMain";
            this.llpMain.RowCount = 4;
            this.llpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.llpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.llpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.llpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.llpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.llpMain.Size = new System.Drawing.Size(571, 344);
            this.llpMain.TabIndex = 26;
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpHeader.Controls.Add(this.imgLogo, 0, 0);
            this.tlpHeader.Controls.Add(this.tlpProduct, 1, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(3, 3);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(565, 154);
            this.tlpHeader.TabIndex = 27;
            // 
            // tlpProduct
            // 
            this.tlpProduct.ColumnCount = 1;
            this.tlpProduct.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProduct.Controls.Add(this.lblName, 0, 0);
            this.tlpProduct.Controls.Add(this.lblVersion, 0, 1);
            this.tlpProduct.Controls.Add(this.lblCopyright, 0, 2);
            this.tlpProduct.Controls.Add(this.txtDescription, 0, 3);
            this.tlpProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProduct.Location = new System.Drawing.Point(144, 3);
            this.tlpProduct.Name = "tlpProduct";
            this.tlpProduct.RowCount = 4;
            this.tlpProduct.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProduct.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProduct.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProduct.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProduct.Size = new System.Drawing.Size(418, 148);
            this.tlpProduct.TabIndex = 13;
            // 
            // lblVersion
            // 
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point(2, 27);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(2);
            this.lblVersion.MaximumSize = new System.Drawing.Size(0, 21);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(414, 21);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(3, 74);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(412, 71);
            this.txtDescription.TabIndex = 20;
            // 
            // txtLicenses
            // 
            this.txtLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLicenses.Location = new System.Drawing.Point(3, 163);
            this.txtLicenses.Multiline = true;
            this.txtLicenses.Name = "txtLicenses";
            this.txtLicenses.ReadOnly = true;
            this.txtLicenses.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLicenses.Size = new System.Drawing.Size(565, 101);
            this.txtLicenses.TabIndex = 27;
            // 
            // btnEula
            // 
            this.btnEula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEula.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEula.Location = new System.Drawing.Point(4, 271);
            this.btnEula.Margin = new System.Windows.Forms.Padding(4);
            this.btnEula.Name = "btnEula";
            this.btnEula.Size = new System.Drawing.Size(193, 30);
            this.btnEula.TabIndex = 27;
            this.btnEula.Text = "License Agreement";
            this.btnEula.Click += new System.EventHandler(this.OnEulaClick);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 366);
            this.Controls.Add(this.llpMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutForm";
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.llpMain.ResumeLayout(false);
            this.llpMain.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpProduct.ResumeLayout(false);
            this.tlpProduct.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TableLayoutPanel llpMain;
        private System.Windows.Forms.Button btnEula;
        private System.Windows.Forms.TextBox txtLicenses;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.TableLayoutPanel tlpProduct;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtDescription;
    }
}
