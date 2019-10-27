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
using System.Threading.Tasks;

namespace Xarial.Signal2Go.Services.ExternalProcess.Exceptions
{
    public class ExternalProcessStartException : Exception
    {
        public ExternalProcessStartException(Exception innerException)
            : base("Failed to start external service", innerException)
        {
        }
    }
}
