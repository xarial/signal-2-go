/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

namespace Xarial.Signal2Go.Exceptions
{
    public class ServiceNotRegisteredException : ServiceLocatorException
    {
        public ServiceNotRegisteredException() 
            : base($"This service is not registered with {nameof(Components.ServiceLocator)}")
        {
        }
    }
}
