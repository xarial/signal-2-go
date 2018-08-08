using CoreTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Attributes;
using Xarial.AppLaunchKit.Exceptions;
using Xarial.AppLaunchKit.Helpers;
using Xarial.AppLaunchKit.Services;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Eula;
using Xarial.AppLaunchKit.Services.Eula.Exceptions;

namespace CoreTests
{
    [TestClass]
    public class EulaServiceTest
    {
        #region Mocks

        [Eula("EULA Content")]
        public class App1Mock
        {
        }

        [Eula("")]
        public class App2Mock
        {
        }

        public class App3Mock
        {
        }

        #endregion

        private ServiceHelper m_ServHelper;

        [TestInitialize]
        public void Init()
        {
            m_ServHelper = new ServiceHelper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            m_ServHelper.Dispose();
        }

        [TestMethod]
        public void TestInitialization()
        {
            var workDir = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString());

            var srv1 = new EulaService(typeof(App1Mock), workDir, ServiceHelper.GetAttribute<EulaAttribute>(typeof(App1Mock)));
                        
            Assert.AreEqual("EULA Content", srv1.EulaContent);
            
            Assert.ThrowsException<EulaContentException>(() => new EulaService(typeof(App2Mock), workDir, ServiceHelper.GetAttribute<EulaAttribute>(typeof(App2Mock))));
        }

        [TestMethod]
        public void TestValidateEulaSigned()
        {
            var workDir = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString());

            var srv1 = new EulaService(typeof(App1Mock), workDir, ServiceHelper.GetAttribute<EulaAttribute>(typeof(App1Mock)));
            
            string eulaContent;
            
            var res1 = srv1.ValidateEulaSigned(out eulaContent);

            CreateEulaFile(workDir, Resources.eula_not_signed);
            var res2 = srv1.ValidateEulaSigned(out eulaContent);

            CreateEulaFile(workDir, Resources.eula_invalid_json);
            var res3 = srv1.ValidateEulaSigned(out eulaContent);

            CreateEulaFile(workDir, Resources.eula_invalid);
            var res4 = srv1.ValidateEulaSigned(out eulaContent);

            CreateEulaFile(workDir, Resources.eula_signed);
            var res5 = srv1.ValidateEulaSigned(out eulaContent);

            Assert.AreEqual("EULA Content", eulaContent);
            Assert.IsFalse(res1);
            Assert.IsFalse(res2);
            Assert.IsFalse(res3);
            Assert.IsFalse(res4);
            Assert.IsTrue(res5);
        }

        [TestMethod]
        public void TestSaveEulaConfirmation()
        {
            var workDir = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString());
            var srv1 = new EulaService(typeof(App1Mock), workDir, ServiceHelper.GetAttribute<EulaAttribute>(typeof(App1Mock)));
            
            srv1.SaveEulaConfirmation();

            var eulaFile = Path.Combine(workDir, "eula.json");
            
            Assert.IsTrue(File.Exists(eulaFile));

            EulaAgreementData eula = null;

            using (var file = File.OpenRead(eulaFile))
            {
                eula = JsonSerializer.Deserialize<EulaAgreementData>(file);
            }

            Assert.IsNotNull(eula);
            Assert.IsTrue(eula.IsAgreed);
            Assert.IsTrue(DateTime.Now.Subtract(eula.TimeStamp).TotalSeconds < 3);
        }

        private void CreateEulaFile(string workDir, byte[] content)
        {
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }

            File.WriteAllBytes(Path.Combine(workDir, "eula.json"), content);
        }
    }
}
