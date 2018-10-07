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
using Xarial.AppLaunchKit.Components;
using Xarial.AppLaunchKit.Reflection;

namespace Xarial.AppLaunchKit.Services.Attributes
{
    public class ExternalProcessAttribute : ServiceBindingAttribute
    {
        public string ApplicationPath { get; private set; }
        public string Args { get; private set; }
        
        public ExternalProcessAttribute(string appPath, string args = "")
        {
            ApplicationPath = appPath;
            Args = args;
        }

        public ExternalProcessAttribute(Type resourceType, string appPathResName, string argsResName = "")
            : this(ResourceHelper.GetResource<string>(resourceType, appPathResName),
                  !string.IsNullOrEmpty(argsResName) 
                  ? ResourceHelper.GetResource<string>(resourceType, argsResName) 
                  : "")
        {
        }
    }
}
