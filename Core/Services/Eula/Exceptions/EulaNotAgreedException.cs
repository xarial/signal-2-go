﻿/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xarial.AppLaunchKit.Services.Eula.Exceptions
{
    public class EulaNotAgreedException : Exception
    {
        public EulaNotAgreedException() : base("Eula not agreed")
        {
        }
    }
}
