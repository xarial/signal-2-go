/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using Xarial.AppLaunchKit.Base;

namespace Xarial.AppLaunchKit.Exceptions
{
    public class InvalidServiceException : ServiceLocatorException
    {
        public InvalidServiceException(Type serviceType)
            : base($"{serviceType.FullName} must implement {typeof(IService<>).Name}")
        {
        }
    }
}
