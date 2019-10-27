/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xarial.Signal2Go.Base.Services;
using Xarial.Signal2Go.Common;
using Xarial.Signal2Go.Helpers;
using Xarial.Signal2Go.Properties;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.Eula;
using Xarial.Signal2Go.Services.Eula.Exceptions;
using Xarial.Signal2Go.Services.Eula.UI;

namespace Xarial.Signal2Go.Services.Eula
{
    public class EulaService : BaseService<EulaAttribute>, IEulaService
    {
        public event Action EulaSigned;
        public event Action EulaRejected;
        public event Action EulaSkipped;

        internal string EulaContent { get; private set; }

        internal string EulaFile { get; private set; }

        public EulaService()
        {
        }

        internal EulaService(string workDir, EulaAttribute bindingAtt)
        {
            Init(null, workDir, bindingAtt);
        }

        public bool ValidateEulaSigned(out string eulaContent)
        {
            eulaContent = EulaContent;

            return IsEulaSigned(EulaFile);
        }

        public void SaveEulaConfirmation()
        {
            try
            {
                File.WriteAllText(EulaFile,
                    JsonConvert.SerializeObject(new EulaAgreementData()
                    {
                        IsAgreed = true,
                        TimeStamp = DateTime.Now
                    }));
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
                    var eulaData = JsonConvert.DeserializeObject<EulaAgreementData>(File.ReadAllText(eulaFilePath));

                    if (eulaData != null)
                    {
                        return eulaData.IsAgreed;
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

            EulaContent = bindingAtt.RtfContent;

            if (string.IsNullOrEmpty(EulaContent))
            {
                throw new EulaContentException("Eula content is empty");
            }

            EulaFile = Path.Combine(workDir, Settings.Default.EulaAgreementFileName);
        }

        public override async Task StartAsync()
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
