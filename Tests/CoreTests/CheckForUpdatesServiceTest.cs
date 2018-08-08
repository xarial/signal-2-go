using System;
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
        #region Mocks

        [UpdatesUrl("https://www.xarial.net/test")]
        public class App1Mock
        {
        }

        [UpdatesUrl("invalid url")]
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
            var srv1 = new UpdatesService(typeof(App1Mock), ServiceHelper.GetAttribute<UpdatesUrlAttribute>(typeof(App1Mock)));
            
            var appMock4 = m_ServHelper.MockAppType("App4Mock", null, "");
            var appMock5 = m_ServHelper.MockAppType("App5Mock", new Version(2, 1, 3, 5), "https://www.xarial.net/test");

            var srv2 = new UpdatesService(appMock5, ServiceHelper.GetAttribute<UpdatesUrlAttribute>(appMock5));
            
            Assert.AreEqual("https://www.xarial.net/test", srv1.ServerUrl);
            Assert.AreEqual(new Version(2, 1, 3, 5), srv2.Version);

            Assert.ThrowsException<CheckForUpdatesDataException>(() => new UpdatesService(typeof(App2Mock), ServiceHelper.GetAttribute<UpdatesUrlAttribute>(typeof(App2Mock))));
            Assert.ThrowsException<CheckForUpdatesDataException>(() => new UpdatesService(appMock4, ServiceHelper.GetAttribute<UpdatesUrlAttribute>(appMock4)));
        }

        [TestMethod]
        public void TestGetUpdates()
        {
            var upgradeUrl = m_ServHelper.CreateUpgradeServer(Resources.mock_serv);

            var app1 = m_ServHelper.MockAppType("App5Mock", new Version(1, 0), upgradeUrl);
            var app2 = m_ServHelper.MockAppType("App5Mock", new Version(1, 2, 0, 0), upgradeUrl);
            var app3 = m_ServHelper.MockAppType("App5Mock", new Version(1, 2, 1, 0), upgradeUrl);
            var app4 = m_ServHelper.MockAppType("App5Mock", new Version(1, 2, 1, 0), upgradeUrl + "_invalid");

            var srv1 = new UpdatesService(app1, ServiceHelper.GetAttribute<UpdatesUrlAttribute>(app1));
            var srv2 = new UpdatesService(app2, ServiceHelper.GetAttribute<UpdatesUrlAttribute>(app2));
            var srv3 = new UpdatesService(app3, ServiceHelper.GetAttribute<UpdatesUrlAttribute>(app3));
            var srv4 = new UpdatesService(app4, ServiceHelper.GetAttribute<UpdatesUrlAttribute>(app4));

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
    }
}
