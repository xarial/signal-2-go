/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System.Runtime.Serialization;

namespace Xarial.AppLaunchKit.Services.Updates
{
    [DataContract]
    public class LatestVersionInfo
    {
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string WhatsNewUrl { get; set; }

        [DataMember]
        public string UpgradeUrl { get; set; }
    }
}
