/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Services.UserSettings.Attributes;

namespace Xarial.AppLaunchKit.Services.UserSettings.Data
{
    public abstract class SettingsJsonConverter : JsonConverter
    {
        protected string VERSION_NODE_NAME = "__version";

        protected Type m_SettsType;
        protected Version m_LatestVersion;

        private bool m_CanRead;
        private bool m_CanWrite;

        protected SettingsJsonConverter(Type settsType, bool canRead, bool canWrite)
        {
            m_SettsType = settsType;

            UserSettingVersionAttribute versAtt;

            if (settsType.TryGetAttribute(out versAtt))
            {
                m_LatestVersion = versAtt.Version;
            }
            else
            {
                m_LatestVersion = new Version();
            }

            m_CanRead = canRead;
            m_CanWrite = canWrite;
        }

        public override bool CanRead => m_CanRead;

        public override bool CanWrite => m_CanWrite;

        public override bool CanConvert(Type objectType)
        {
            return objectType == m_SettsType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected JsonSerializer GetSerializer(JsonSerializer baseSer)
        {
            if (baseSer.Converters?.Contains(this) == true)
            {
                baseSer.Converters.Remove(this);
            }

            return baseSer;
        }
    }
}
