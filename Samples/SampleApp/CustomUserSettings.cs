using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Services.UserSettings.Attributes;
using Xarial.AppLaunchKit.Services.UserSettings.Data;

namespace SampleApp
{
    public class CustomUserSettingsBackwardCompatibility : BaseUserSettingsVersionsTransformer<CustomUserSettings>
    {
        public CustomUserSettingsBackwardCompatibility()
        {
            Add(new Version(1, 0), new Version(2, 0), 
                t => 
                {
                    var prop = t.Children<JProperty>().First(p => p.Name == "MessageV1");

                    prop.Replace(new JProperty("MessageV2", prop.Value));
                    return t;
                });

            Add(new Version(2, 0), new Version(3, 0), t =>
            {
                var prop = t.Children<JProperty>().First(p => p.Name == "MessageV2");

                prop.Replace(new JProperty("Message", prop.Value));
                return t;
            });
        }
    }

    [UserSettingVersion("3.0")]
    public class CustomUserSettings
    {
        public string Message { get; set; } = "Sample Message";
    }
}
