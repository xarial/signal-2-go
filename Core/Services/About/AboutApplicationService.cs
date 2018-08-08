/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Services.About.UI;
using Xarial.AppLaunchKit.Services.Attributes;
using System.Reflection;
using Xarial.AppLaunchKit.Base;
using Xarial.AppLaunchKit.Base.Services;
using System.Drawing;

namespace Xarial.AppLaunchKit.Services.About
{
    public class AboutApplicationService : BaseService<AboutAttribute>, IAboutApplicationService
    {
        private string m_Eula;
        private string m_Licenses;
        private Image m_Logo;

        protected override void Init(Type appType, string workDir, AboutAttribute bindingAtt)
        {
            m_Eula = bindingAtt.Eula;
            m_Licenses = bindingAtt.Licenses;
            m_Logo = bindingAtt.Logo;
        }

        public void ShowAboutForm()
        {
            var dlg = CreateDialog<WinFormServiceDialog<AboutForm>>("About {0}");

            var assm = m_AppInfo.ApplicationType.Assembly;

            var name = "";
            var version = "";
            var copyright = "";
            var description = "";

            assm.WithAttribute<AssemblyProductAttribute>(p => name = p.Product);
            version = assm.GetName().Version.ToString();
            assm.WithAttribute<AssemblyCopyrightAttribute>(c => copyright = c.Copyright);
            assm.WithAttribute<AssemblyDescriptionAttribute>(d => description = d.Description);

            dlg.Form.SetData(name, version, copyright, description, m_Eula, m_Licenses, m_Logo);

            dlg.Show();
        }
    }
}
