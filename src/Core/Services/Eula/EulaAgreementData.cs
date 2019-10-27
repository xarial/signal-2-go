/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Runtime.Serialization;

namespace Xarial.Signal2Go.Services.Eula
{
    [DataContract]
    public class EulaAgreementData
    {
        [DataMember]
        public bool IsAgreed { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }
    }
}
