/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.UserSettings.Data;
using Xarial.AppLaunchKit.Services.UserSettings.Exceptions;

namespace Xarial.AppLaunchKit.Services.UserSettings
{
    public class UserSettingsService : BaseService<UserSettingsAttribute>, IUserSettingsService
    {
        private string m_SettingsRepository;

        private Dictionary<Type, JsonConverter> m_ReadConverters;

        private bool m_ThrowOnError;

        public UserSettingsService()
        {
        }

        internal UserSettingsService(string workDir, UserSettingsAttribute bindingAtt)
        {
            Init(null, workDir, bindingAtt);
        }

        public T ReadSettings<T>(string nameOrPath = "")
        {
            try
            {
                JsonConverter converter;

                var settsFile = GetSettingsFileName<T>(nameOrPath);

                var fileExist = File.Exists(settsFile);

                string settsData = null;

                if (fileExist)
                {
                    settsData = File.ReadAllText(settsFile);
                }

                if (m_ReadConverters.TryGetValue(typeof(T), out converter))
                {
                    if (string.IsNullOrEmpty(settsData))
                    {
                        settsData = new Newtonsoft.Json.Linq.JObject().ToString();
                    }

                    return JsonConvert.DeserializeObject<T>(settsData, converter);
                }
                else
                {
                    if (!fileExist)
                    {
                        throw new FileNotFoundException(settsFile);
                    }

                    return JsonConvert.DeserializeObject<T>(settsData);
                }
            }
            catch(Exception ex)
            {
                if (m_ThrowOnError)
                {
                    throw new InvalidSettingsException(ex);
                }
                else
                {
                    return Activator.CreateInstance<T>();
                }
            }
        }

        public void StoreSettings<T>(T setts, string nameOrPath = "")
        {
            try
            {
                var settsFile = GetSettingsFileName<T>(nameOrPath);

                var settsDir = Path.GetDirectoryName(settsFile);

                if (!Directory.Exists(settsDir))
                {
                    Directory.CreateDirectory(settsDir);
                }
                
                var settsData = JsonConvert.SerializeObject(setts, new WriteSettingsJsonConverter(typeof(T)));

                File.WriteAllText(settsFile, settsData);
            }
            catch(Exception ex)
            {
                if (m_ThrowOnError)
                {
                    throw new StoringSettingsFailureException(ex);
                }
            }
        }

        private string GetSettingsFileName<T>(string nameOrPath)
        {
            if (string.IsNullOrEmpty(nameOrPath))
            {
                nameOrPath = typeof(T).FullName;
            }

            if (Path.IsPathRooted(nameOrPath))
            {
                return nameOrPath;
            }
            else
            {
                return Path.Combine(m_SettingsRepository, nameOrPath + ".setts");
            }
        }

        protected override void Init(Assembly assm, string workDir, UserSettingsAttribute bindingAtt)
        {
            if (!string.IsNullOrEmpty(bindingAtt.StorageName))
            {
                if (Path.IsPathRooted(bindingAtt.StorageName))
                {
                    m_SettingsRepository = bindingAtt.StorageName;
                }
                else
                {
                    m_SettingsRepository = Path.Combine(workDir, bindingAtt.StorageName);
                }
            }
            else
            {
                m_SettingsRepository = bindingAtt.StorageName;
            }

            m_ThrowOnError = bindingAtt.ThrowOnError;

            m_ReadConverters = new Dictionary<Type, JsonConverter>();

            if (bindingAtt.VersTransformers != null)
            {
                foreach (var transType in bindingAtt.VersTransformers)
                {
                    var trans = Activator.CreateInstance(transType) as IUserSettingsVersionsTransformer;

                    if (trans.SettingType != null)
                    {
                        if (!m_ReadConverters.ContainsKey(trans.SettingType))
                        {
                            m_ReadConverters.Add(trans.SettingType, new ReadSettingsJsonConverter(trans.SettingType, trans));
                        }
                        else
                        {
                            throw new ArgumentException(
                                $"Duplicate transformer for {trans.SettingType.Name} settings");
                        }
                    }
                    else
                    {
                        throw new NullReferenceException(
                            $"Type of settings are not defined via {nameof(trans.SettingType)} in {transType.FullName} transformer");
                    }
                }
            }
        }
    }
}
