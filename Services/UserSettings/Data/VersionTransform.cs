﻿/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json.Linq;
using System;

namespace Xarial.AppLaunchKit.Services.UserSettings.Data
{
    public class VersionTransform
    {
        public Version From { get; private set; }
        public Version To { get; private set; }

        private Func<JToken, JToken> m_Transform;

        public VersionTransform(Version from, Version to, Func<JToken, JToken> transform)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            m_Transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public JToken Transform(JToken input)
        {
            return m_Transform.Invoke(input);
        }
    }
}