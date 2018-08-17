/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

namespace Xarial.AppLaunchKit.Base
{
    public interface IServiceDialog
    {
        void Init(AppInfo appInfo, string titleFormat = "");
        void Show();
    }
}
