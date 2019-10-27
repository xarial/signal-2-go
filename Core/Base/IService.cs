/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System.ComponentModel;
using System.Threading.Tasks;
using Xarial.Signal2Go.Components;

namespace Xarial.Signal2Go.Base
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IService
    {
        void Init(AppInfo appInfo, ServiceBindingAttribute bindingAtt);
        Task StartAsync();
    }

    public interface IService<TSrvBindingAtt> : IService
        where TSrvBindingAtt : ServiceBindingAttribute
    {
        void Init(AppInfo appInfo, TSrvBindingAtt bindingAtt);
    }
}
