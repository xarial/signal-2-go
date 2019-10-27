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
using System.Windows.Forms;

namespace Xarial.Signal2Go.Services.Eula.UI
{
    public partial class EulaForm : Form
    {
        public EulaForm()
        {
            InitializeComponent();
        }

        internal string EulaContent
        {
            set
            {
                txtEula.Rtf = value;
            }
        }
    }
}
