/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Helpers;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Updates.Exceptions;
using Xarial.AppLaunchKit.Services.Updates.UI;

namespace Xarial.AppLaunchKit.Services.Updates
{
    public class UpdatesService : BaseService<UpdatesUrlAttribute>, IUpdatesService
    {
        public event UpdateAvailableDelegate UpdateAvailable;
        public event Action UpdatesCheckCompleted;
        public event Action UpdatesCheckFailed;
        
        private string m_ServerUrl;

        private Version m_Version;
        
        internal string ServerUrl
        {
            get
            {
                return m_ServerUrl;
            }
        }

        internal Version Version
        {
            get
            {
                return m_Version;
            }
        }

        public UpdatesService()
        {
        }

        internal UpdatesService(Assembly appAssm, UpdatesUrlAttribute bindingAtt)
        {
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

        internal Tuple<Version, string, string> GetUpdates()
        {
            Tuple<Version, string, string> res = null;

            SetWebSettings();

            using (var webClient = new WebClient())
            {
                var data = webClient.DownloadData(m_ServerUrl);

                res = GetUpdateInfoIfAvailable(data);

                if (res != null)
                {
                    UpdateAvailable?.Invoke(res.Item1, res.Item2, res.Item3);

                    return res;
                }
                else
                {
                    UpdatesCheckCompleted?.Invoke();
                }
            }

            return res;
        }

        public Tuple<Version, string, string> CheckForUpdates()
        {
            Tuple<Version, string, string> updates = null;

            try
            {
                updates = GetUpdates();

                if (updates != null)
                {
                    var dlg = CreateDialog<WinFormServiceDialog<UpgradeForm>>(
                                "Updates for {0}");

                    dlg.Form.SetData(updates.Item1, updates.Item2, updates.Item3);

                    dlg.Show();
                }
            }
            catch(Exception ex)
            {
                UpdatesCheckFailed?.Invoke();
                throw new UpdatesCheckException(ex);
            }

            return updates;
        }

        public async Task CheckForUpdatesAsync()
        {
            await RunAsyncInCurrentSynchronizationContext(
                () => CheckForUpdates());
        }

        private Tuple<Version, string, string> GetUpdateInfoIfAvailable(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var latestVersInfo = JsonSerializer.Deserialize<LatestVersionInfo>(stream);
                var latestVer = new Version(latestVersInfo.Version);

                if (m_Version < latestVer)
                {
                    return new Tuple<Version, string, string>(latestVer, latestVersInfo.WhatsNewUrl,
                        latestVersInfo.UpgradeUrl);
                }
                else
                {
                    return null;
                }
            }
        }
        
        public override async Task Start()
        {
            await CheckForUpdatesAsync();
        }

        protected override void Init(Assembly assm, string workDir, UpdatesUrlAttribute bindingAtt)
        {
            if (bindingAtt == null)
            {
                throw new ArgumentNullException(nameof(bindingAtt));
            }
            
            m_ServerUrl = bindingAtt.Url;

            if (string.IsNullOrEmpty(m_ServerUrl)
                || !Uri.IsWellFormedUriString(m_ServerUrl, UriKind.Absolute))
            {
                throw new CheckForUpdatesDataException("Specified updates server url is not of correct format");
            }

            m_Version = assm.GetName().Version;

            if (m_Version == new Version(0, 0, 0, 0))
            {
                throw new CheckForUpdatesDataException(
                    $"Assembly {assm.FullName} must be decorated with {typeof(AssemblyVersionAttribute).FullName} attribute");
            }
        }
    }
}
