/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Xarial.Signal2Go.Base;

namespace Xarial.Signal2Go.Common
{
    internal static class WinApi
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }

    public class WinFormServiceDialog<TForm> : IServiceDialog
            where TForm : Form, new()
    {
        public TForm Form { get; private set; }

        private AppInfo m_AppInfo;

        void IServiceDialog.Show()
        {
            Show();
        }

        public DialogResult Show()
        {
            return Form.ShowDialog(m_AppInfo.WindowWrapper);
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            WinApi.SetParent(Form.Handle, m_AppInfo.WindowWrapper.Handle);
        }

        public void Init(AppInfo appInfo, string titleFormat = "")
        {
            m_AppInfo = appInfo;

            Form = new TForm()
            {
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable,
                Icon = appInfo.Icon,
                Text = string.IsNullOrEmpty(titleFormat)
                    ? appInfo.Title
                    : string.Format(titleFormat, appInfo.Title)
            };
            
            Form.HandleCreated += OnHandleCreated;
        }
    }
}
