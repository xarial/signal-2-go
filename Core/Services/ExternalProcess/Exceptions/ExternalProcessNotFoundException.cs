/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.Signal2Go.Services.ExternalProcess.Exceptions
{
    public class ExternalProcessNotFoundException : FileNotFoundException
    {
        public ExternalProcessNotFoundException(string appPath) 
            : this(appPath, null)
        {
        }

        public ExternalProcessNotFoundException(string appPath, Exception innerException)
            : base("External process service application cannot be found", appPath, innerException)
        {
        }
    }
}
