/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xarial.AppLaunchKit.Base;
using Xarial.AppLaunchKit.Exceptions;

namespace Xarial.AppLaunchKit.Components
{
    internal class ServiceLocator
    {
        private readonly Dictionary<Type, IService> m_Services;

        private readonly AppInfo m_AppInfo;

        private readonly IDictionary<Type, Type> m_ServicesMap;

        internal ServiceLocator(AppInfo appInfo, params Type[] supportedServices)
        {
            if (supportedServices == null)
            {
                throw new ArgumentNullException(nameof(supportedServices));
            }

            m_ServicesMap = new Dictionary<Type, Type>();
            m_AppInfo = appInfo;
            m_Services = new Dictionary<Type, IService>();

            MapServicesToBindingAttributes(supportedServices);

            LoadAppServices(appInfo);
        }

        private void MapServicesToBindingAttributes(Type[] supportedServices)
        {
            foreach (var suppSrv in supportedServices)
            {
                if (suppSrv.IsAssignableToGenericType(typeof(IService<>)))
                {
                    var bindgAttType = suppSrv.GetArgumentsOfGenericType(typeof(IService<>)).First();

                    if (!m_ServicesMap.ContainsKey(suppSrv))
                    {
                        m_ServicesMap.Add(suppSrv, bindgAttType);
                    }
                    else
                    {
                        throw new DuplicateServiceException(suppSrv);
                    }
                }
                else
                {
                    throw new InvalidServiceException(suppSrv);
                }
            }
        }

        private void LoadAppServices(AppInfo appInfo)
        {
            var atts = appInfo.Assembly.GetCustomAttributes(
                            typeof(ServiceBindingAttribute), true)?.Cast<ServiceBindingAttribute>();

            if (atts?.Any() == true)
            {
                foreach (var att in atts)
                {
                    var srvTypesMap = m_ServicesMap.Where(
                        m => m.Value.IsAssignableFrom(att.GetType()));

                    if (srvTypesMap.Count() == 1)
                    {
                        var srvType = srvTypesMap.First().Key;
                        var srv = Activator.CreateInstance(srvType) as IService;
                        srv.Init(appInfo, att);
                        m_Services.Add(srvType, srv);
                    }
                    else if (srvTypesMap.Count() == 0)
                    {
                        throw new ServiceNotBoundException(att.GetType());
                    }
                    else if (srvTypesMap.Count() > 1)
                    {
                        throw new OverdefinedServiceRegisteredException(att.GetType());
                    }
                }
            }
            else
            {
                throw new ServicesNotAttachedException(appInfo.Assembly);
            }
        }
        
        internal TService GetService<TService>()
            where TService : IService
        {
            IService srv;

            if (!LookupInServiceTypesDictionary(m_Services, typeof(TService), out srv))
            {
                Type attType;

                if (LookupInServiceTypesDictionary(m_ServicesMap, typeof(TService), out attType))
                {
                    throw new ServiceNotSupportedException(
                        m_AppInfo.Assembly, typeof(TService), attType);
                }
                else
                {
                    throw new ServiceNotRegisteredException();
                }
            }

            if (!(srv is TService))
            {
                Debug.Assert(false, "Services must be correctly grouped by type");
                throw new ServiceLocatorException("Unexpected invalid type");
            }

            return (TService)srv;
        }

        internal IEnumerable<IService> GetServices()
        {
            return m_Services.Values;
        }

        private static bool LookupInServiceTypesDictionary<TValue>(IDictionary<Type, TValue> dict, Type type, out TValue val)
        {
            if (dict.TryGetValue(type, out val))
            {
                return true;
            }
            else
            {
                var assignableSrvs = dict.Where(t => type.IsAssignableFrom(t.Key));

                if (assignableSrvs.Any())
                {
                    if (assignableSrvs.Count() == 1)
                    {
                        val = assignableSrvs.First().Value;
                        return true;
                    }
                    else
                    {
                        //this might be never possible
                        throw new AmbiguousServiceException(type);
                    }
                }
            }

            return false;
        }
    }
}
