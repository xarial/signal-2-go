/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;

namespace Xarial.AppLaunchKit.Base.Services
{
    public delegate void UpdateAvailableDelegate(Version version, string whatsNewUrl, string upgradeUrl);

    public interface IUpdatesService : IService
    {
        event UpdateAvailableDelegate UpdateAvailable;
        event Action UpdatesCheckCompleted;
        event Action UpdatesCheckFailed;
    }
}
