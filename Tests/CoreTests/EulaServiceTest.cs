using CoreTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.Eula;
using Xarial.Signal2Go.Services.Eula.Exceptions;

namespace CoreTests
{
    [TestClass]
    public class EulaServiceTest
    {
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

            var srv1 = new EulaService(workDir, new EulaAttribute("EULA Content"));
                        
            Assert.AreEqual("EULA Content", srv1.EulaContent);
            
            Assert.ThrowsException<EulaContentException>(() => new EulaService(workDir, new EulaAttribute("")));
        }

        [TestMethod]
        public void TestValidateEulaSigned()
        {
            var workDir = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString());

            var srv1 = new EulaService(workDir, new EulaAttribute("EULA Content"));
            
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
            var srv1 = new EulaService(workDir, new EulaAttribute("EULA Content"));
            
            srv1.SaveEulaConfirmation();

            var eulaFile = Path.Combine(workDir, "eula.json");
            
            Assert.IsTrue(File.Exists(eulaFile));

            var eula = JsonConvert.DeserializeObject<EulaAgreementData>(File.ReadAllText(eulaFile));

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
