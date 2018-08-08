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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xarial.AppLaunchKit.Services.Auth.Oidc.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public async Task<LoginData> LoginAsync(string url, string redirectUrl, bool showForm)
        {
            try
            {
                var res = new LoginData(false);

                var signal = new SemaphoreSlim(0, 1);
                
                ctrlBrowser.DocumentCompleted += (s, e) =>
                {
                    if (e.Url.ToString().StartsWith(redirectUrl))
                    {
                        res = new LoginData(true, e.Url.ToString());
                        this.Close();
                    }
                    else if (!showForm)
                    {
                        this.Close();
                    }
                };

                this.FormClosed += (s, e) => signal.Release();
                
                ctrlBrowser.Navigate(url);

                if (showForm)
                {
                    this.Show();
                }

                await signal.WaitAsync();

                return res;
            }
            finally
            {
                this.Close();
            }
        }
    }
}
