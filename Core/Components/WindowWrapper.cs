/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xarial.AppLaunchKit.Components
{
    internal class WindowWrapper : IWin32Window
    {
        public IntPtr Handle
        {
            get;
            private set;
        }

        internal WindowWrapper(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
