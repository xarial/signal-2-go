/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

namespace Xarial.AppLaunchKit.Services.Auth.Oidc.UI
{
    public class LoginData
    {
        public bool IsSucceeded { get; private set; }
        public string Response { get; private set; }

        public LoginData(bool isSucceeded, string response = "")
        {
            IsSucceeded = isSucceeded;
            Response = response;
        }
    }
}
