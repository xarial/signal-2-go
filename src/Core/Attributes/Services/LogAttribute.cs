/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using Xarial.Signal2Go.Components;
using Xarial.Signal2Go.Reflection;

namespace Xarial.Signal2Go.Services.Attributes
{
    public class LogAttribute : ServiceBindingAttribute
    {
        public string SourceName { get; private set; }
        public string LogName { get; private set; }
        public bool CreateStore { get; private set; }
        public bool ThrowOnError { get; private set; }

        public LogAttribute(string sourceName) : this("", sourceName, false, false)
        {
        }

        public LogAttribute(string logName, string sourceName, bool createStore, bool throwOnError)
        {
            LogName = logName;
            SourceName = sourceName;
            CreateStore = createStore;
            ThrowOnError = throwOnError;
        }

        public LogAttribute(Type resourceType, string logNameResourceName, string sourceNameResourceName,
             bool createStore, bool throwOnError)
            : this(ResourceHelper.GetResource<string>(resourceType, logNameResourceName),
                  ResourceHelper.GetResource<string>(resourceType, sourceNameResourceName), createStore, throwOnError)
        {
        }
    }
}
