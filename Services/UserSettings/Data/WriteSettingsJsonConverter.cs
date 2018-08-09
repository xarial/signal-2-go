/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Xarial.AppLaunchKit.Services.UserSettings.Data
{
    public class WriteSettingsJsonConverter : SettingsJsonConverter
    {
        internal WriteSettingsJsonConverter(Type settsType) 
            : base(settsType, false, true)
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jObject = JObject.FromObject(value, GetSerializer(serializer));

            jObject.Add(VERSION_NODE_NAME, m_LatestVersion.ToString());

            jObject.WriteTo(writer);
        }
    }
}
