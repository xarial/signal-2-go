using CoreTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xarial.Signal2Go.Reflection;

namespace CoreTests
{
    [TestClass]
    public class ResourceHelperTest
    {
        #region Mocks

        public static class ResourceMock
        {
            public class SubClass
            {
                public class SubClass2
                {
                    public string Val { get; set; } = "AAA";
                }

                public SubClass2 Cl2 { get; set; } = new SubClass2();
            }

            public static SubClass Cl1 { get; set; } = new SubClass();
        }

        #endregion

        [TestMethod]
        public void TestGetResource()
        {
            var r1 = ResourceHelper.GetResource<string>(typeof(Resources), nameof(Resources.TestString));
            var r2 = ResourceHelper.GetResource<string>(typeof(Settings), nameof(Settings.Default) + "." + nameof(Settings.TestUserSettings));
            var r3 = ResourceHelper.GetResource<string>(typeof(Settings), nameof(Settings.Default) + "." + nameof(Settings.TestAppSettings));
            var r4 = ResourceHelper.GetResource<string>(typeof(ResourceMock), "Cl1.Cl2.Val");

            Assert.AreEqual("Test", r1);
            Assert.AreEqual("Test2", r2);
            Assert.AreEqual("Test1", r3);
            Assert.AreEqual("AAA", r4);
            Assert.ThrowsException<NullReferenceException>(() => ResourceHelper.GetResource<string>(typeof(ResourceMock), "Cl2.Cl2.Val"));
        }
    }
}
