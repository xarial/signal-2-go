using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoreTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.Signal2Go.Exceptions;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.Updates;
using Xarial.Signal2Go.Services.Updates.Exceptions;

namespace CoreTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string m_Dir;

        public MockHttpMessageHandler(string dir)
        {
            m_Dir = dir;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                Content = new StreamContent(File.OpenRead(
                    Path.Combine(m_Dir, Path.GetFileName(request.RequestUri.AbsolutePath))))
            });
        }
    }

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

            var srv1 = new UpdatesService(mock1, new UpdatesUrlAttribute("https://www.xarial.net/test"), new MockHttpMessageHandler(""));
            
            var appMock4 = m_ServHelper.MockAppAssembly(null);
            var appMock5 = m_ServHelper.MockAppAssembly(new Version(2, 1, 3, 5));

            var srv2 = new UpdatesService(appMock5, new UpdatesUrlAttribute("https://www.xarial.net/test"), new MockHttpMessageHandler(""));
            
            Assert.AreEqual("https://www.xarial.net/test", srv1.ServerUrl);
            Assert.AreEqual(new Version(2, 1, 3, 5), srv2.Version);

            Assert.ThrowsException<CheckForUpdatesDataException>(() => new UpdatesService(appMock4, new UpdatesUrlAttribute("https://www.xarial.net/test"), new MockHttpMessageHandler("")));
            Assert.ThrowsException<CheckForUpdatesDataException>(() => new UpdatesService(appMock5, new UpdatesUrlAttribute("invalid url"), new MockHttpMessageHandler("")));
        }

        [TestMethod]
        public async Task TestGetUpdates()
        {
            var srv = CreateUpgradeServer(Resources.mock_serv);

            var app1 = m_ServHelper.MockAppAssembly(new Version(1, 0));
            var app2 = m_ServHelper.MockAppAssembly(new Version(1, 2, 0, 0));
            var app3 = m_ServHelper.MockAppAssembly(new Version(1, 2, 1, 0));
            var app4 = m_ServHelper.MockAppAssembly(new Version(1, 2, 1, 0));

            var srv1 = new UpdatesService(app1, new UpdatesUrlAttribute(srv.Item2), srv.Item1);
            var srv2 = new UpdatesService(app2, new UpdatesUrlAttribute(srv.Item2), srv.Item1);
            var srv3 = new UpdatesService(app3, new UpdatesUrlAttribute(srv.Item2), srv.Item1);
            var srv4 = new UpdatesService(app4, new UpdatesUrlAttribute(srv.Item2 + "_invalid"), srv.Item1);

            var res1 = await srv1.GetUpdatesAsync();
            var res2 = await srv2.GetUpdatesAsync();
            var res3 = await srv3.GetUpdatesAsync();
            
            Assert.AreEqual(new Version(1, 2, 1, 0), res1.Item1);
            Assert.AreEqual("https://www.xarial-app4.com/whats-new", res1.Item2);
            Assert.AreEqual("https://www.xarial-app4.com/upgrade", res1.Item3);
            Assert.AreEqual(new Version(1, 2, 1, 0), res2.Item1);
            Assert.AreEqual("https://www.xarial-app4.com/whats-new", res2.Item2);
            Assert.AreEqual("https://www.xarial-app4.com/upgrade", res2.Item3);
            Assert.IsNull(res3);

            await Assert.ThrowsExceptionAsync<UpdatesCheckException>(async () => await srv4.GetUpdatesAsync());
        }

        internal Tuple<HttpMessageHandler, string> CreateUpgradeServer(byte[] data)
        {
            var filePath = Path.Combine(m_ServHelper.WorkingDir, Guid.NewGuid() + ".json");
            
            File.WriteAllBytes(filePath, data);

            var testUpgradeUrl = $"https://testsrv/{Path.GetFileName(filePath)}";

            return new Tuple<HttpMessageHandler, string>(
                new MockHttpMessageHandler(Path.GetDirectoryName(filePath)),
                testUpgradeUrl);
        }
    }
}
