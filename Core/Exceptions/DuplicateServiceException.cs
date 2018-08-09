/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;

namespace Xarial.AppLaunchKit.Exceptions
{
    public class DuplicateServiceException : ServiceLocatorException
    {
        public DuplicateServiceException(Type serviceType)
            : base($"Duplicate service {serviceType.Name}")
        {
        }
    }
}
