/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using Xarial.AppLaunchKit.Components;
using Xarial.AppLaunchKit.Reflection;

namespace Xarial.AppLaunchKit.Services.Attributes
{
    public class AuthOidcAttribute : ServiceBindingAttribute
    {
        public string Authority { get; private set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string Scope { get; private set; }
        public bool LoadProfile { get; private set; }
        public string RedirectUrl { get; private set; }

        public AuthOidcAttribute(Type resourceType, string authorityResName, string clientIdResName,
            string redirectUrlResName, string clientSecretResName, string scopeResName, string loadProfileResName)
            : this(ResourceHelper.GetResource<string>(resourceType, authorityResName),
                  ResourceHelper.GetResource<string>(resourceType, clientIdResName),
                  ResourceHelper.GetResource<string>(resourceType, redirectUrlResName),
                  ResourceHelper.GetResource<string>(resourceType, clientSecretResName),
                  ResourceHelper.GetResource<string>(resourceType, scopeResName),
                  ResourceHelper.GetResource<bool>(resourceType, loadProfileResName))
        {
        }

        public AuthOidcAttribute(string authority, string clientId,
            string redirectUrl)
            : this(authority, clientId, redirectUrl, "", "", false)
        {
        }

        public AuthOidcAttribute(string authority, string clientId,
            string redirectUrl, string clientSecret, string scope, bool loadProfile)
        {
            Authority = authority;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scope = scope;
            LoadProfile = loadProfile;
            RedirectUrl = redirectUrl;
        }
    }
}
