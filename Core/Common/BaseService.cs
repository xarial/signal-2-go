/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Threading;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base;
using Xarial.AppLaunchKit.Components;

namespace Xarial.AppLaunchKit.Common
{
    public abstract class BaseService<TSrvBindingAtt> : IService<TSrvBindingAtt>
        where TSrvBindingAtt : ServiceBindingAttribute
    {
        protected AppInfo m_AppInfo;

        public void Init(AppInfo appInfo, ServiceBindingAttribute bindingAtt)
        {
            Init(appInfo, bindingAtt as TSrvBindingAtt);
        }

        public void Init(AppInfo appInfo, TSrvBindingAtt bindingAtt)
        {
            m_AppInfo = appInfo;
            Init(m_AppInfo.ApplicationType, m_AppInfo.WorkDir, bindingAtt);
        }

        public virtual Task Start()
        {
            return Task.CompletedTask;
        }

        protected virtual void Init(Type appType, string workDir, TSrvBindingAtt bindingAtt)
        {
        }

        protected async Task RunAsyncInCurrentSynchronizationContext(Action action)
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            await Task.Factory.StartNew(
                () => action(), new CancellationToken(),
                TaskCreationOptions.None, scheduler);
        }

        protected TDialog CreateDialog<TDialog>(string titleFormat = "")
            where TDialog : IServiceDialog, new()
        {
            var dlg = new TDialog();
            dlg.Init(m_AppInfo, titleFormat);
            return dlg;
        }
    }
}
