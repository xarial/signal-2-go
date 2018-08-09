/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System.Linq;

namespace System.Reflection
{
    public static class AssemblyExtension
    {
        public static bool TryGetAttribute<TAtt>(this Assembly assm, out TAtt att)
            where TAtt : Attribute
        {
            TAtt localAtt = null;

            assm.WithAttribute<TAtt>(a => localAtt = a);

            att = localAtt;

            return localAtt != null;
        }

        public static void WithAttribute<TAtt>(this Assembly assm, Action<TAtt> action)
            where TAtt : Attribute
        {
            if (assm == null)
            {
                throw new ArgumentNullException(nameof(assm));
            }
            
            var custAtts = assm.GetCustomAttributes(typeof(TAtt), true);

            if (custAtts != null && custAtts.Any())
            {
                foreach (TAtt att in custAtts)
                {
                    action.Invoke(att);
                }
            }
        }
    }
}
