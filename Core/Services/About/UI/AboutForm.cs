/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xarial.AppLaunchKit.Services.About.UI
{
    partial class AboutForm : Form
    {
        private string m_Eula;

        public AboutForm()
        {
            InitializeComponent();
        }

        internal void SetData(string name, string version, string copyright, string description,
            string eula, string licenses, Image logo)
        {
            lblName.Text = name;
            lblVersion.Text = $"Version: {version}";
            lblCopyright.Text = copyright;
            txtDescription.Text = description;

            imgLogo.Image = logo;

            m_Eula = eula;

            btnEula.Visible = !string.IsNullOrEmpty(m_Eula);
            txtLicenses.Text = licenses;
        }

        private void OnEulaClick(object sender, EventArgs e)
        {
            var eulaForm = new Form()
            {
                Text = "End User License Agreement",
                Icon = this.Icon,
                StartPosition = FormStartPosition.CenterParent
            };

            eulaForm.Controls.Add(new RichTextBox()
            {
                ReadOnly = true,
                Rtf = m_Eula,
                Dock = DockStyle.Fill,
                ScrollBars = RichTextBoxScrollBars.Both
            });

            eulaForm.ShowDialog(this);
        }
    }
}
