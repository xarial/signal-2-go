/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using IdentityModel.OidcClient;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Auth.Oidc.Exceptions;
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
                
                var loginRes = await Login(client, state, true);

                if (loginRes.User == null)
                {
                    loginRes = await Login(client, state, false);
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

        private async Task<LoginResult> Login(OidcClient client, AuthorizeState state, bool silent)
        {
            try
            {
                var dlg = this.CreateDialog<WinFormServiceDialog<LoginForm>>("Login to {0}");

                var res = await dlg.Form.LoginAsync(
                    state.StartUrl + (silent ? "&prompt=none" : ""),
                    state.RedirectUri, !silent);

                if (string.IsNullOrEmpty(res.Response))
                {
                    throw new NullReferenceException("Cancelled by the user");
                }

                return await client.ProcessResponseAsync(res.Response, state);
            }
            catch(Exception ex)
            {
                return new LoginResult(ex.Message);
            }
        }

        protected override void Init(Assembly assm, string workDir, AuthOidcAttribute bindingAtt)
        {
            m_Authority = bindingAtt.Authority;
            m_ClientId = bindingAtt.ClientId;
            m_ClientSecret = bindingAtt.ClientSecret;
            m_Scope = bindingAtt.Scope;
            m_LoadProfile = bindingAtt.LoadProfile;
            m_RedirectUrl = bindingAtt.RedirectUrl;
        }
    }
}
