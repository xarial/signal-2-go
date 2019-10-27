using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.ExternalProcess;
using Xarial.Signal2Go.Services.ExternalProcess.Exceptions;

namespace CoreTests
{
    [TestClass]
    public class ExternalProcessServiceTest
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
        public void TestExternalProcessRelativeApp()
        {
            var testFilePath = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString() + ".txt");
            var content = Guid.NewGuid().ToString();

            var srv = new ExternalProcessService(this.GetType().Assembly,
                m_ServHelper.WorkingDir,
                new ExternalProcessAttribute("ExternalApp.exe", $"\"{testFilePath}\" {content}"));

            srv.StartApplication();

            Thread.Sleep(200);

            Assert.IsTrue(File.Exists(testFilePath));
            Assert.AreEqual(content, File.ReadAllText(testFilePath));
        }

        [TestMethod]
        public void TestExternalProcessApp()
        {
            var testFilePath = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString() + ".txt");
            var content = Guid.NewGuid().ToString();

            var appPath = Path.Combine(m_ServHelper.WorkingDir, "ExternalApp.exe");

            File.Copy(Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location),
                "ExternalApp.exe"), appPath);

            var srv = new ExternalProcessService(this.GetType().Assembly,
                m_ServHelper.WorkingDir,
                new ExternalProcessAttribute(appPath, $"\"{testFilePath}\" {content}"));

            srv.StartApplication();

            Thread.Sleep(200);

            Assert.IsTrue(File.Exists(testFilePath));
            Assert.AreEqual(content, File.ReadAllText(testFilePath));
        }

        [TestMethod]
        public void TestExternalServiceErrors()
        {
            var srv1 = new ExternalProcessService(this.GetType().Assembly,
                m_ServHelper.WorkingDir,
                new ExternalProcessAttribute("MissingApp.exe"));

            var testAppPath = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid().ToString() + ".exe");

            File.WriteAllBytes(testAppPath, new byte[] { 1, 2, 3 });

            var srv2 = new ExternalProcessService(this.GetType().Assembly,
                m_ServHelper.WorkingDir,
                new ExternalProcessAttribute(testAppPath));

            Assert.ThrowsException<ExternalProcessNotFoundException>(() => srv1.StartApplication());
            Assert.ThrowsException<ExternalProcessStartException>(() => srv2.StartApplication());
        }
    }
}
