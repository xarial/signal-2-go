/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xarial.Signal2Go.Base;
using Xarial.Signal2Go.Components;

namespace Xarial.Signal2Go.Common
{
    public abstract class BaseService<TSrvBindingAtt> : IService<TSrvBindingAtt>
        where TSrvBindingAtt : ServiceBindingAttribute
    {
        protected AppInfo m_AppInfo;

        public void Init(AppInfo appInfo, ServiceBindingAttribute bindingAtt)
        {
            Init(appInfo, (TSrvBindingAtt)bindingAtt);
        }

        public void Init(AppInfo appInfo, TSrvBindingAtt bindingAtt)
        {
            m_AppInfo = appInfo;
            Init(m_AppInfo.Assembly, m_AppInfo.WorkDir, bindingAtt);
        }

        public virtual Task StartAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual void Init(Assembly appAssm, string workDir, TSrvBindingAtt bindingAtt)
        {
        }

        //TODO: might need to remove this method later
        protected Task RunAsyncInCurrentSynchronizationContext(Action action)
        {
            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(
                    new System.Windows.Forms.WindowsFormsSynchronizationContext());
            }

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            return Task.Factory.StartNew(
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
