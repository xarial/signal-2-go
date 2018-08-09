/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Drawing;
using Xarial.AppLaunchKit.Components;
using Xarial.AppLaunchKit.Reflection;

namespace Xarial.AppLaunchKit.Services.Attributes
{
    public class AboutAttribute : ServiceBindingAttribute
    {
        public string Eula { get; private set; }
        public string Licenses { get; private set; }
        public Image Logo { get; private set; }

        public AboutAttribute() : this("", "")
        {
        }

        public AboutAttribute(string eula, string licenses)
            : this(eula, licenses, null)
        {
            
        }

        public AboutAttribute(Type resourceType, string logoResourceName)
            : this ("", "", ResourceHelper.GetResource<Image>(resourceType, logoResourceName))
        {
        }

        public AboutAttribute(Type resourceType, string eulaResourceName,
            string licensesResourceName, string logoResourceName)
            : this(ResourceHelper.GetResource<string>(resourceType, eulaResourceName),
                  ResourceHelper.GetResource<string>(resourceType, licensesResourceName),
                  ResourceHelper.GetResource<Image>(resourceType, logoResourceName))
        {
        }

        private AboutAttribute(string eula, string licenses, Image logo)
        {
            Eula = eula;
            Licenses = licenses;
            Logo = logo;
        }
    }
}
