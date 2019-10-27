/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System.Reflection;

namespace Xarial.Signal2Go.Exceptions
{
    public class ServicesNotAttachedException : ServiceLocatorException
    {
        public ServicesNotAttachedException(Assembly appAssm)
            : base($"No services attached to the application {appAssm.GetName().Name}")
        {
        }
    }
}
