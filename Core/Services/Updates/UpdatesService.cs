/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using System.Threading.Tasks;
using Xarial.Signal2Go.Base.Services;
using Xarial.Signal2Go.Common;
using Xarial.Signal2Go.Helpers;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.Updates.Exceptions;
using Xarial.Signal2Go.Services.Updates.UI;

namespace Xarial.Signal2Go.Services.Updates
{
    public class UpdatesService : BaseService<UpdatesUrlAttribute>, IUpdatesService
    {
        public event UpdateAvailableDelegate UpdateAvailable;
        public event Action UpdatesCheckCompleted;
        public event Action UpdatesCheckFailed;

        internal string ServerUrl { get; private set; }
        internal Version Version { get; private set; }

        private readonly HttpMessageHandler m_MsgHandler;

        public UpdatesService()
        {
            m_MsgHandler = new HttpClientHandler();
        }

        internal UpdatesService(Assembly appAssm, UpdatesUrlAttribute bindingAtt, HttpMessageHandler handler)
        {
            m_MsgHandler = handler;
            Init(appAssm, "", bindingAtt);
        }

        private void SetWebSettings()
        {
            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
            ServicePointManager.Expect100Continue = false;

            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;
        }

        internal async Task<Tuple<Version, string, string>> GetUpdatesAsync()
        {
            try
            {

                SetWebSettings();

                using (var webClient = new HttpClient(m_MsgHandler))
                {
                    var data = await webClient.GetStreamAsync(ServerUrl);

                    return GetUpdateInfoIfAvailable(data);
                }
            }
            catch(Exception ex)
            {
                UpdatesCheckFailed?.Invoke();
                throw new UpdatesCheckException(ex);
            }
        }

        public async Task<Tuple<Version, string, string>> CheckForUpdatesAsync()
        {
            var updates = await GetUpdatesAsync();

            if (updates != null)
            {
                var dlg = CreateDialog<WinFormServiceDialog<UpgradeForm>>(
                            "Updates for {0}");

                dlg.Form.SetData(updates.Item1, updates.Item2, updates.Item3);

                dlg.Show();
            }

            return updates;
        }

        private Tuple<Version, string, string> GetUpdateInfoIfAvailable(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var latestVersInfo = (LatestVersionInfo)new JsonSerializer().Deserialize(streamReader, typeof(LatestVersionInfo));
                var latestVer = new Version(latestVersInfo.Version);

                if (Version < latestVer)
                {
                    UpdateAvailable?.Invoke(latestVer, latestVersInfo.WhatsNewUrl, latestVersInfo.UpgradeUrl);

                    return new Tuple<Version, string, string>(latestVer, latestVersInfo.WhatsNewUrl,
                        latestVersInfo.UpgradeUrl);
                }
                else
                {
                    UpdatesCheckCompleted?.Invoke();
                    return null;
                }
            }
        }
        
        public override Task StartAsync()
        {
            var checkForUpdatesTask = CheckForUpdatesAsync();
            checkForUpdatesTask.ConfigureAwait(false);
            return checkForUpdatesTask;
        }

        protected override void Init(Assembly assm, string workDir, UpdatesUrlAttribute bindingAtt)
        {
            if (bindingAtt == null)
            {
                throw new ArgumentNullException(nameof(bindingAtt));
            }
            
            ServerUrl = bindingAtt.Url;

            if (string.IsNullOrEmpty(ServerUrl)
                || !Uri.IsWellFormedUriString(ServerUrl, UriKind.Absolute))
            {
                throw new CheckForUpdatesDataException("Specified updates server url is not of correct format");
            }

            Version = assm.GetName().Version;

            if (Version == new Version(0, 0, 0, 0))
            {
                throw new CheckForUpdatesDataException(
                    $"Assembly {assm.FullName} must be decorated with {typeof(AssemblyVersionAttribute).FullName} attribute");
            }
        }
    }
}
