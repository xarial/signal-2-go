/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Drawing;
using System.IO;
using Xarial.Signal2Go.Attributes;
using Xarial.Signal2Go.Properties;
using Xarial.Signal2Go.Base;
using Xarial.Signal2Go.Components;
using Xarial.Signal2Go.Services.Eula;
using Xarial.Signal2Go.Services.Updates;
using System.Threading.Tasks;
using Xarial.Signal2Go.Services.Logger;
using System.Reflection;
using System.Linq;
using Xarial.Signal2Go.Services.About;
using Xarial.Signal2Go.Services.ExternalProcess;
using Xarial.Signal2Go.Services.UserSettings;

namespace Xarial.Signal2Go
{
    public delegate bool ErrorHandlerDelegate(Exception ex);

    public class ServicesManager
    {
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
                  typeof(SystemEventLogService),
                  typeof(AboutApplicationService),
                  typeof(ExternalProcessService),
                  typeof(UserSettingsService))
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

        public Task StartServicesAsync()
        {
            var allSrvTasks = Task.WhenAll(m_ServiceLocator.GetServices()
                .Select(s =>
                {
                    var srvTask = s.StartAsync();
                    srvTask.ConfigureAwait(false);
                    return srvTask;
                }));

            allSrvTasks.ConfigureAwait(false);

            return allSrvTasks;
        }
    }
}
