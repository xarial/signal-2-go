namespace Xarial.AppLaunchKit.Services.Eula.UI
{
    partial class EulaForm
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
            this.btnAgree = new System.Windows.Forms.Button();
            this.txtEula = new System.Windows.Forms.RichTextBox();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.btnAgree, 0, 1);
            this.tlpMain.Controls.Add(this.txtEula, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(437, 395);
            this.tlpMain.TabIndex = 0;
            // 
            // btnAgree
            // 
            this.btnAgree.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgree.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAgree.Location = new System.Drawing.Point(157, 346);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(123, 46);
            this.btnAgree.TabIndex = 0;
            this.btnAgree.Text = "Agree";
            this.btnAgree.UseVisualStyleBackColor = true;
            // 
            // txtEula
            // 
            this.txtEula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEula.Location = new System.Drawing.Point(3, 3);
            this.txtEula.Name = "txtEula";
            this.txtEula.Size = new System.Drawing.Size(431, 337);
            this.txtEula.TabIndex = 1;
            this.txtEula.Text = "";
            // 
            // EulaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 395);
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EulaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EulaForm";
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnAgree;
        private System.Windows.Forms.RichTextBox txtEula;
    }
}