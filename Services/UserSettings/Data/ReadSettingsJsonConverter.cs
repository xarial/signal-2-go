/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Services.UserSettings.Attributes;

namespace Xarial.AppLaunchKit.Services.UserSettings.Data
{
    public class ReadSettingsJsonConverter : SettingsJsonConverter
    {
        private IUserSettingsVersionsTransformer m_Transformer;

        internal ReadSettingsJsonConverter(Type settsType, IUserSettingsVersionsTransformer transformer)
            : base(settsType, true, false)
        {
            m_Transformer = transformer;
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.ReadFrom(reader);
            
            var versToken = jToken.SelectToken(VERSION_NODE_NAME);

            Version settsVers;

            if (versToken == null)
            {
                settsVers = new Version();
            }
            else
            {
                settsVers = Version.Parse(versToken.Value<string>());
            }

            if (m_LatestVersion > settsVers)
            {
                foreach (var tr in m_Transformer
                    .Where(t => t.From >= settsVers && t.To <= m_LatestVersion)
                    .OrderBy(t => t.From))
                {
                    jToken = tr.Transform(jToken);
                }
            }

            return jToken.ToObject(objectType, GetSerializer(serializer));
        }
    }
}
