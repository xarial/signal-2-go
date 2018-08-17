/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using Xarial.AppLaunchKit.Components;
using Xarial.AppLaunchKit.Reflection;

namespace Xarial.AppLaunchKit.Services.Attributes
{
    public class EulaAttribute : ServiceBindingAttribute
    {
        public string RtfContent { get; private set; }

        public EulaAttribute(string rtfContent)
        {
            RtfContent = rtfContent;
        }

        public EulaAttribute(Type resourceType, string resourceName)
            : this(ResourceHelper.GetResource<string>(resourceType, resourceName))
        {
        }
    }
}
