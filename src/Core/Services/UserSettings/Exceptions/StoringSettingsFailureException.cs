/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;

namespace Xarial.Signal2Go.Services.UserSettings.Exceptions
{
    public class StoringSettingsFailureException : InvalidOperationException
    {
        public StoringSettingsFailureException(Exception innerException)
            : base("Storing settings error", innerException)
        {
        }
    }
}
