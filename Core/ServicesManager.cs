/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Drawing;
using System.IO;
using Xarial.AppLaunchKit.Attributes;
using Xarial.AppLaunchKit.Properties;
using Xarial.AppLaunchKit.Base;
using Xarial.AppLaunchKit.Components;
using Xarial.AppLaunchKit.Services.Eula;
using Xarial.AppLaunchKit.Services.Updates;
using System.Threading.Tasks;
using System.Threading;
using Xarial.AppLaunchKit.Services.Logger;
using System.Reflection;

namespace Xarial.AppLaunchKit
{
    public delegate bool ErrorHandlerDelegate(Exception ex);

    public class ServicesManager
    {
        public event Action ServicesLaunchCompleted;
        public event Action ServicesLaunchTerminated;

        public event ErrorHandlerDelegate HandleError;

        private readonly AppInfo m_AppInfo;

        private readonly ServiceLocator m_ServiceLocator;

        public TService GetService<TService>()
            where TService : IService
        {
            return m_ServiceLocator.GetService<TService>();
        }

        public ServicesManager(Assembly assm, IntPtr parentWnd)
            : this(assm, parentWnd,
                  typeof(EulaService),
                  typeof(UpdatesService),
                  typeof(SystemEventLogService))
        {
        }

        public ServicesManager(Assembly assm, IntPtr parentWnd, params Type[] servicesTypes)
        {
            if (assm == null)
            {
                throw new ArgumentNullException(nameof(assm));
            }

            ApplicationInfoAttribute appInfoAtt;

            string appWordDir = "";
            string appTitle = "";
            Icon appIcon = null;

            if (assm.TryGetAttribute(out appInfoAtt))
            {
                appWordDir = appInfoAtt.WorkingDirectory;
                appTitle = appInfoAtt.Title;
                appIcon = appInfoAtt.Icon;
            }

            if (string.IsNullOrEmpty(appWordDir))
            {
                appWordDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    Settings.Default.DefaultAppDir);
            }

            m_AppInfo = new AppInfo(assm,
                new WindowWrapper(parentWnd), appTitle, appIcon, appWordDir);

            m_ServiceLocator = new ServiceLocator(m_AppInfo, servicesTypes);
        }

        public void StartServices()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(
                () => StartServicesAsync(), new CancellationToken(),
                TaskCreationOptions.None, scheduler);
        }

        public async Task StartServicesAsync()
        {
            foreach (var srv in m_ServiceLocator.GetServices())
            {
                try
                {
                    await srv.Start();
                }
                catch (Exception ex)
                {
                    if (HandleError?.Invoke(ex) == false)
                    {
                        ServicesLaunchTerminated?.Invoke();
                        return;
                    }
                }
            }

            ServicesLaunchCompleted?.Invoke();
        }
    }
}
