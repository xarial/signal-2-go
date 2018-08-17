/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;

namespace Xarial.AppLaunchKit.Services.UserSettings.Attributes
{
    public class UserSettingVersionAttribute : Attribute
    {
        public Version Version { get; private set; }

        public UserSettingVersionAttribute(string version)
        {
            Version = new Version(version);
        }
    }
}
