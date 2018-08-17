using System;
using System.IO;
using CoreTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.AppLaunchKit.Exceptions;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Updates;
using Xarial.AppLaunchKit.Services.Updates.Exceptions;

namespace CoreTests
{
    [TestClass]
    public class CheckForUpdatesServiceTest
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
            var mock1 = m_ServHelper.MockAppAssembly(new Version(1, 0));

            var srv1 = new UpdatesService(mock1, new UpdatesUrlAttribute("https://www.xarial.net/test"));
            
            var appMock4 = m_ServHelper.MockAppAssembly(null);
            var appMock5 = m_ServHelper.MockAppAssembly(new Version(2, 1, 3, 5));

            var srv2 = new UpdatesService(appMock5, new UpdatesUrlAttribute("https://www.xarial.net/test"));
            
            Assert.AreEqual("https://www.xarial.net/test", srv1.ServerUrl);
            Assert.AreEqual(new Version(2, 1, 3, 5), srv2.Version);

            Assert.ThrowsException<CheckForUpdatesDataException>(() => new UpdatesService(appMock4, new UpdatesUrlAttribute("https://www.xarial.net/test")));
            Assert.ThrowsException<CheckForUpdatesDataException>(() => new UpdatesService(appMock5, new UpdatesUrlAttribute("invalid url")));
        }

        [TestMethod]
        public void TestGetUpdates()
        {
            var upgradeUrl = CreateUpgradeServer(Resources.mock_serv);

            var app1 = m_ServHelper.MockAppAssembly(new Version(1, 0));
            var app2 = m_ServHelper.MockAppAssembly(new Version(1, 2, 0, 0));
            var app3 = m_ServHelper.MockAppAssembly(new Version(1, 2, 1, 0));
            var app4 = m_ServHelper.MockAppAssembly(new Version(1, 2, 1, 0));

            var srv1 = new UpdatesService(app1, new UpdatesUrlAttribute(upgradeUrl));
            var srv2 = new UpdatesService(app2, new UpdatesUrlAttribute(upgradeUrl));
            var srv3 = new UpdatesService(app3, new UpdatesUrlAttribute(upgradeUrl));
            var srv4 = new UpdatesService(app4, new UpdatesUrlAttribute(upgradeUrl + "_invalid"));

            var res1 = srv1.GetUpdates();
            var res2 = srv2.GetUpdates();
            var res3 = srv3.GetUpdates();
            
            Assert.AreEqual(new Version(1, 2, 1, 0), res1.Item1);
            Assert.AreEqual("https://www.xarial-app4.com/whats-new", res1.Item2);
            Assert.AreEqual("https://www.xarial-app4.com/upgrade", res1.Item3);
            Assert.AreEqual(new Version(1, 2, 1, 0), res2.Item1);
            Assert.AreEqual("https://www.xarial-app4.com/whats-new", res2.Item2);
            Assert.AreEqual("https://www.xarial-app4.com/upgrade", res2.Item3);
            Assert.IsNull(res3);
            Assert.ThrowsException<UpdatesCheckException>(() => srv4.CheckForUpdates());
        }

        internal string CreateUpgradeServer(byte[] data)
        {
            var filePath = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid() + ".json");
            var testUpgradeUrl = "file:///" + filePath.Replace('\\', '/');
            File.WriteAllBytes(filePath, data);

            return testUpgradeUrl;
        }
    }
}
