/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

namespace Xarial.AppLaunchKit.Base.Services
{
    public interface IUserSettingsService : IService
    {
        T ReadSettings<T>(string name = "");
        void StoreSettings<T>(T setts, string name = "");
    }
}
