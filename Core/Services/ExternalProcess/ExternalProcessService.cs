/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.ExternalProcess.Exceptions;

namespace Xarial.AppLaunchKit.Services.External
{
    public class ExternalProcessService : BaseService<ExternalProcessAttribute>, IExternalProcessService
    {
        private string m_AppPath;
        private string m_Arg;
        private Assembly m_AppAssm;

        internal ExternalProcessService(Assembly appAssm, string workDir, ExternalProcessAttribute bindingAtt)
        {
            Init(appAssm, workDir, bindingAtt);
        }

        protected override void Init(Assembly appAssm, string workDir, ExternalProcessAttribute bindingAtt)
        {
            m_AppAssm = appAssm;

            m_AppPath = bindingAtt.ApplicationPath;
            
            m_Arg = bindingAtt.Args;
        }

        public override Task Start()
        {
            return RunAsyncInCurrentSynchronizationContext(StartApplication);
        }

        public void StartApplication()
        {
            string appPath = m_AppPath;

            try
            {
                if (!Path.IsPathRooted(appPath))
                {
                    appPath = Path.Combine(Path.GetDirectoryName(m_AppAssm.Location), appPath);
                }
            }
            catch(Exception ex)
            {
                throw new ExternalProcessNotFoundException(appPath, ex);
            }
            
            if (!File.Exists(appPath))
            {
                throw new ExternalProcessNotFoundException(appPath);
            }

            try
            {
                Process.Start(appPath, m_Arg);
            }
            catch(Exception ex)
            {
                throw new ExternalProcessStartException(ex);
            }
        }
    }
}
