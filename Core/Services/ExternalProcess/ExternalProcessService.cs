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
using Xarial.Signal2Go.Base.Services;
using Xarial.Signal2Go.Common;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.ExternalProcess.Exceptions;

namespace Xarial.Signal2Go.Services.ExternalProcess
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

        public override Task StartAsync()
        {
            StartApplication();
            return Task.CompletedTask;
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
