/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Linq;
using Xarial.Signal2Go.Components;
using Xarial.Signal2Go.Reflection;
using Xarial.Signal2Go.Services.UserSettings;

namespace Xarial.Signal2Go.Services.Attributes
{
    public class UserSettingsAttribute : ServiceBindingAttribute
    {
        public string StorageName { get; private set; }

        public Type[] VersTransformers { get; private set; }

        /// <summary>
        /// Indicates if default settings should be created in case of error in deserialization
        /// </summary>
        /// <remarks>If this setting is false and there is an error while reading the settings
        /// <see cref="InvalidSettingsException"/> otherwise new default value will be created.
        /// If there is an error while writing <see cref="StoringSettingsFailureException"/> exception is thrown</remarks>
        public bool ThrowOnError { get; private set; }

        public UserSettingsAttribute(params Type[] versTransformers) : this("", true, versTransformers)
        {
        }

        public UserSettingsAttribute(Type resourceType, string storageNameResourceName, 
            bool throwOnError, params Type[] versTransformers)
            : this(ResourceHelper.GetResource<string>(resourceType, storageNameResourceName),
                  throwOnError, versTransformers)
        {
        }

        public UserSettingsAttribute(string storageName, bool throwOnError, params Type[] versTransformers)
        {
            var invalidTransformers = versTransformers?.Where(
                t => !typeof(IUserSettingsVersionsTransformer).IsAssignableFrom(t));

            if (invalidTransformers.Any())
            {
                throw new InvalidCastException(
                    $"{string.Join(", ", invalidTransformers.Select(i => i.Name))} must implement {nameof(IUserSettingsVersionsTransformer)} interface");
            }

            StorageName = storageName;
            ThrowOnError = throwOnError;
            VersTransformers = versTransformers;
        }
    }
}
