/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.AppLaunchKit.Services.UserSettings.Data
{
    public interface IUserSettingsVersionsTransformer : IEnumerable<VersionTransform>
    {
        Type SettingType { get; }
    }

    public class BaseUserSettingsVersionsTransformer<TSettings> : IUserSettingsVersionsTransformer
    {
        public Type SettingType => typeof(TSettings);

        private List<VersionTransform> m_Transformers;

        public BaseUserSettingsVersionsTransformer(params VersionTransform[] transformers)
        {
            m_Transformers = new List<VersionTransform>();

            if (transformers != null)
            {
                m_Transformers.AddRange(transformers);
            }
        }

        protected void Add(Version from, Version to, Func<JToken, JToken> transform)
        {
            m_Transformers.Add(new VersionTransform(from, to, transform));
        }

        public IEnumerator<VersionTransform> GetEnumerator()
        {
            return m_Transformers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Transformers.GetEnumerator();
        }
    }
}
