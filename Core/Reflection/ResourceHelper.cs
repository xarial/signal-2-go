/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Reflection;

namespace Xarial.Signal2Go.Reflection
{
    public static class ResourceHelper
    {
        public static T GetResource<T>(Type resType, string resName)
        {
            return (T)GetValue(null, resType, resName.Split('.'));
        }

        public static object GetValue(object obj, Type type, string[] prpsPath)
        {
            foreach (var prpName in prpsPath)
            {
                var prp = type.GetProperty(prpName,
                    BindingFlags.NonPublic | BindingFlags.Public
                    | BindingFlags.Static | BindingFlags.Instance);

                if (prp == null)
                {
                    throw new NullReferenceException($"Resource '{prpName}' is missing in '{type.Name}'");
                }

                obj = prp.GetValue(obj, null);

                if (obj != null)
                {
                    type = obj.GetType();
                }
            }

            return obj;
        }
    }
}
