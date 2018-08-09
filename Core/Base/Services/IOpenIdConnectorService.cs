/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System.Threading.Tasks;

namespace Xarial.AppLaunchKit.Base.Services
{
    public delegate void LoggedInDelegate(string identity);
    public delegate void LoginFailedDelegate();

    public interface IOpenIdConnectorService : IService
    {
        event LoggedInDelegate LoggedIn;
        event LoginFailedDelegate LoginFailed;

        Task LoginAsync();
    }
}
