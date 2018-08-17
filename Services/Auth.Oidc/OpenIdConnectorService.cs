/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using IdentityModel.OidcClient;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Auth.Oidc.Data;
using Xarial.AppLaunchKit.Services.Auth.Oidc.Exceptions;
using Xarial.AppLaunchKit.Services.Auth.Oidc.Properties;
using Xarial.AppLaunchKit.Services.Auth.Oidc.UI;

namespace Xarial.AppLaunchKit.Services.Auth.Oidc
{
    public class OpenIdConnectorService : BaseService<AuthOidcAttribute>, IOpenIdConnectorService
    {
        public event LoggedInDelegate LoggedIn;
        public event LoginFailedDelegate LoginFailed;
        
        private string m_Authority;
        private string m_ClientId;
        private string m_ClientSecret;
        private string m_Scope;
        private bool m_LoadProfile;
        private string m_RedirectUrl;

        private string m_AuthDataFile;

        public OpenIdConnectorService()
        {   
        }
        
        public override async Task Start()
        {
            await LoginAsync();
        }

        public async Task LoginAsync()
        {
            try
            {
                var opts = new OidcClientOptions
                {
                    Authority = m_Authority,
                    ClientId = m_ClientId,
                    ClientSecret = m_ClientSecret,
                    RedirectUri = m_RedirectUrl,
                    Scope = m_Scope,
                    LoadProfile = m_LoadProfile,
                    Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                    ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
                    Policy = new Policy()
                    {
                        RequireAuthorizationCodeHash = false,
                        RequireAccessTokenHash = false
                    }
                };

                var client = new OidcClient(opts);

                var loginRequest = new LoginRequest();
                
                var state = await client.PrepareLoginAsync();

                var authData = GetUserData();

                LoginResult loginRes = null;

                if (authData.StayLoggedIn)
                {
                    loginRes = await Login(client, state, true, authData.StayLoggedIn);
                }

                if (loginRes?.User == null)
                {
                    loginRes = await Login(client, state, false, authData.StayLoggedIn);
                }
                
                if (loginRes.User != null)
                {
                    LoggedIn?.Invoke(loginRes.User.Identity?.Name);
                }
                else
                {
                    throw new Exception(loginRes.Error);
                }
            }
            catch(Exception ex)
            {
                LoginFailed?.Invoke();
                throw new LoginFailedException(ex);
            }
        }

        private async Task<LoginResult> Login(OidcClient client, AuthorizeState state, bool silent, bool stayLoggedIn)
        {
            try
            {
                var dlg = this.CreateDialog<WinFormServiceDialog<LoginForm>>("Login to {0}");

                dlg.Form.SetData(stayLoggedIn);

                var dlgRes = await dlg.Form.LoginAsync(
                    state.StartUrl + (silent ? "&prompt=none" : ""),
                    state.RedirectUri, !silent);
                
                if (string.IsNullOrEmpty(dlgRes.Response))
                {
                    throw new NullReferenceException("Cancelled by the user");
                }

                var res = await client.ProcessResponseAsync(dlgRes.Response, state);

                if (res.User != null)
                {
                    CacheUserData(dlgRes.StayLoggedIn);
                }

                return res;
            }
            catch(Exception ex)
            {
                return new LoginResult(ex.Message);
            }
        }

        private void CacheUserData(bool stayLoggedIn)
        {
            try
            {
                Helpers.JsonSerializer.SerializeToFile(new AuthData()
                {
                    StayLoggedIn = stayLoggedIn
                }, m_AuthDataFile);
            }
            catch
            {
            }
        }

        private AuthData GetUserData()
        {
            AuthData authData = null;

            try
            {
                authData = Helpers.JsonSerializer.DeserializeFromFile<AuthData>(m_AuthDataFile);
            }
            catch
            {
            }

            if (authData == null)
            {
                authData = new AuthData();
            }

            return authData;
        }

        protected override void Init(Assembly assm, string workDir, AuthOidcAttribute bindingAtt)
        {
            m_Authority = bindingAtt.Authority;
            m_ClientId = bindingAtt.ClientId;
            m_ClientSecret = bindingAtt.ClientSecret;
            m_Scope = bindingAtt.Scope;
            m_LoadProfile = bindingAtt.LoadProfile;
            m_RedirectUrl = bindingAtt.RedirectUrl;

            m_AuthDataFile = Path.Combine(workDir, Settings.Default.AuthDataFileName);
        }
    }
}
