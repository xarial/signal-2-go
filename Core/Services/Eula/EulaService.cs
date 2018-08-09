/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Helpers;
using Xarial.AppLaunchKit.Properties;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Eula.Exceptions;
using Xarial.AppLaunchKit.Services.Eula.UI;

namespace Xarial.AppLaunchKit.Services.Eula
{
    public class EulaService : BaseService<EulaAttribute>, IEulaService
    {
        public event Action EulaSigned;
        public event Action EulaRejected;
        public event Action EulaSkipped;
        
        private string m_EulaContent;
        private string m_EulaFile;

        internal string EulaContent
        {
            get
            {
                return m_EulaContent;
            }
        }

        internal string EulaFile
        {
            get
            {
                return m_EulaFile;
            }
        }

        public EulaService()
        {
        }

        internal EulaService(string workDir, EulaAttribute bindingAtt)
        {
            Init(null, workDir, bindingAtt);
        }

        public bool ValidateEulaSigned(out string eulaContent)
        {
            eulaContent = m_EulaContent;

            return IsEulaSigned(m_EulaFile);
        }

        public void SaveEulaConfirmation()
        {
            try
            {
                var dir = Path.GetDirectoryName(m_EulaFile);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (var file = File.Create(m_EulaFile))
                {
                    JsonSerializer.Serialize(new EulaAgreementData()
                    {
                        IsAgreed = true,
                        TimeStamp = DateTime.Now
                    }, file);
                }
            }
            catch
            {
            }
        }

        private bool IsEulaSigned(string eulaFilePath)
        {
            try
            {
                if (File.Exists(eulaFilePath))
                {
                    using (var fileStream = File.OpenRead(eulaFilePath))
                    {
                        var eulaData = JsonSerializer.Deserialize<EulaAgreementData>(fileStream);

                        if (eulaData != null)
                        {
                            return eulaData.IsAgreed;
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        protected override void Init(Assembly assm, string workDir, EulaAttribute bindingAtt)
        {
            if (bindingAtt == null)
            {
                throw new ArgumentNullException(nameof(bindingAtt));
            }

            m_EulaContent = bindingAtt.RtfContent;

            if (string.IsNullOrEmpty(m_EulaContent))
            {
                throw new EulaContentException("Eula content is empty");
            }

            m_EulaFile = Path.Combine(workDir, Settings.Default.EulaAgreementFileName);
        }

        public override async Task Start()
        {
            await RunAsyncInCurrentSynchronizationContext(CheckEula);
        }

        private void CheckEula()
        {
            string eulaContent;

            if (!ValidateEulaSigned(out eulaContent))
            {
                var dlg = CreateDialog<WinFormServiceDialog<EulaForm>>(
                    "End User License Agreement for {0}");

                dlg.Form.EulaContent = eulaContent;

                if (dlg.Show() == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveEulaConfirmation();
                    EulaSigned?.Invoke();
                }
                else
                {
                    EulaRejected?.Invoke();
                    throw new EulaNotAgreedException();
                }
            }
            else
            {
                EulaSkipped?.Invoke();
            }
        }
    }
}
