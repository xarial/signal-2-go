/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xarial.Signal2Go.Services.Updates.UI
{
    public partial class UpgradeForm : Form
    {
        private Version m_Version;
        private string m_WhatsNewUrl;
        private string m_UpgradeUrl;

        public UpgradeForm()
        {
            InitializeComponent();
        }

        internal void SetData (Version version, string whatsNewUrl, string upgradeUrl)
        {
            m_Version = version;
            m_WhatsNewUrl = whatsNewUrl;
            m_UpgradeUrl = upgradeUrl;

            lblMessage.Text = $"New version {version} is available. Please follow links below for more information";
        }
        
        private void OnWhatsNewLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(m_WhatsNewUrl);
        }

        private void OnGetNewVersionLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(m_UpgradeUrl);
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
            }
        }
    }
}
