using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Base;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Components;
using Xarial.AppLaunchKit.Exceptions;

namespace CoreTests
{
    [TestClass]
    public class ServiceLocatorTest
    {
        #region Mocks

        public class SrvAtt1 : ServiceBindingAttribute
        {
        }

        public class SrvAtt2 : ServiceBindingAttribute
        {
        }

        public class SrvAtt3 : ServiceBindingAttribute
        {
        }

        public class SrvAtt4 : ServiceBindingAttribute
        {
        }

        public class NonSrvAtt : Attribute
        {
        }

        public interface IServiceMock<TSrvBindingAtt> : IService<TSrvBindingAtt>
            where TSrvBindingAtt : ServiceBindingAttribute
        {
        }

        public class ServiceMock : BaseService<SrvAtt1>, IServiceMock<SrvAtt1>
        {
            public override Task Start()
            {
                throw new NotImplementedException();
            }
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
        public void TestConstructor()
        {
            var appMockType1 = m_ServHelper.MockAppAssembly(null, new SrvAtt1());

            var st1 = new Mock<BaseService<SrvAtt1>>().Object.GetType();
            var st2 = new Mock<IService>().Object.GetType();
            var st3 = new Mock<BaseService<SrvAtt2>>().Object.GetType();

            var appMockType2 = m_ServHelper.MockAppAssembly(null, new SrvAtt1());
            var appInfoMock2 = new AppInfo(appMockType2, new WindowWrapper(IntPtr.Zero), "", null, "");

            var appMockType3 = m_ServHelper.MockAppAssembly(null, new NonSrvAtt());
            var appInfoMock3 = new AppInfo(appMockType3, new WindowWrapper(IntPtr.Zero), "", null, "");

            //does not throw
            new ServiceLocator(
                new AppInfo(appMockType1, new WindowWrapper(IntPtr.Zero), "", null, ""),
                typeof(ServiceMock));

            Assert.ThrowsException<DuplicateServiceException>(() => new ServiceLocator(
                appInfoMock2,
                st1, st1));

            Assert.ThrowsException<InvalidServiceException>(() => new ServiceLocator(
                appInfoMock2, st1, st2));

            Assert.ThrowsException<ServiceNotBoundException>(() => new ServiceLocator(
                appInfoMock2, st3));

            Assert.ThrowsException<OverdefinedServiceRegisteredException>(() => new ServiceLocator(
                appInfoMock2, st1, typeof(ServiceMock)));

            Assert.ThrowsException<ServicesNotAttachedException>(() => new ServiceLocator(
                appInfoMock3, st1, typeof(ServiceMock)));
        }

        [TestMethod]
        public void TestGetServices()
        {
            var appMockType1 = m_ServHelper.MockAppAssembly(null,
                new SrvAtt1(), new SrvAtt2(), new SrvAtt3());

            var appInfoMock1 = new AppInfo(appMockType1, new WindowWrapper(IntPtr.Zero), "", null, "");

            var st1 = typeof(ServiceMock);
            var st2 = new Mock<BaseService<SrvAtt2>>().Object.GetType();
            var st3 = new Mock<BaseService<SrvAtt3>>().Object.GetType();
            var st4 = new Mock<BaseService<SrvAtt4>>().Object.GetType();

            var srvLoc = new ServiceLocator(appInfoMock1, st1, st2, st3, st4);

            var srvs = srvLoc.GetServices();

            Assert.AreEqual(3, srvs.Count());
            Assert.IsTrue(srvs.Any(s => s is ServiceMock));
            Assert.IsTrue(srvs.Any(s => s is BaseService<SrvAtt2>));
            Assert.IsTrue(srvs.Any(s => s is BaseService<SrvAtt3>));
            Assert.IsFalse(srvs.Any(s => s is BaseService<SrvAtt4>));
        }

        [TestMethod]
        public void TestGetService()
        {
            var appMockType1 = m_ServHelper.MockAppAssembly(null,
                new SrvAtt1(), new SrvAtt2());

            var appInfoMock1 = new AppInfo(appMockType1, new WindowWrapper(IntPtr.Zero), "", null, "");

            var st1 = typeof(ServiceMock);
            var st2 = new Mock<BaseService<SrvAtt2>>().Object.GetType();
            var st3 = new Mock<BaseService<SrvAtt3>>().Object.GetType();

            var srvLoc = new ServiceLocator(appInfoMock1, st1, st2, st3);

            var s1 = srvLoc.GetService<ServiceMock>();
            var s1_1 = srvLoc.GetService<IService<SrvAtt1>>();
            var s1_2 = srvLoc.GetService<IServiceMock<SrvAtt1>>();

            Assert.IsNotNull(s1);
            Assert.IsNotNull(s1_1);
            Assert.IsNotNull(s1_2);
            Assert.AreEqual(s1, s1_1);
            Assert.AreEqual(s1, s1_2);

            Assert.ThrowsException<ServiceNotSupportedException>(() => srvLoc.GetService<BaseService<SrvAtt3>>());
            Assert.ThrowsException<ServiceNotRegisteredException>(() => srvLoc.GetService<BaseService<SrvAtt4>>());
        }
    }
}
