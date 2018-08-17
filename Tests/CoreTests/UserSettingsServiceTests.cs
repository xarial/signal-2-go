using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.AppLaunchKit.Services.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Services.Attributes;
using CoreTests;
using System.IO;
using Xarial.AppLaunchKit.Services.UserSettings.Attributes;
using CoreTests.Properties;
using Xarial.AppLaunchKit.Services.UserSettings.Data;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace Xarial.AppLaunchKit.Services.UserSettings.Tests
{
    [TestClass]
    public class UserSettingsServiceTests
    {
        #region Mocks

        public class SettsMock1
        {
            public string Field1 { get; set; }
            public double Field2 { get; set; }
        }

        [UserSettingVersion("2.1.0")]
        public class SettsMock2
        {
            public string Field1 { get; set; }
            public double Field3 { get; set; }
            public bool Field4 { get; set; }
        }

        public class SettsMockTransformer : IUserSettingsVersionsTransformer
        {
            public Type SettingType => typeof(SettsMock2);

            private List<VersionTransform> m_Transforms;

            public SettsMockTransformer()
            {
                m_Transforms = new List<VersionTransform>();
                m_Transforms.Add(new VersionTransform(
                    new Version(),
                    new Version("2.1.0"),
                    t =>
                    {
                        var field2 = t.Children<JProperty>().First(p => p.Name == "Field2");

                        field2.Replace(new JProperty("Field3", (field2 as JProperty).Value));
                        return t;
                    }));
            }

            public IEnumerator<VersionTransform> GetEnumerator()
            {
                return m_Transforms.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return m_Transforms.GetEnumerator();
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
        public void ReadSettingsTest()
        {
            var srv1 = new UserSettingsService(m_ServHelper.WorkingDir,
                new UserSettingsAttribute("SettsStorage", true));

            var srv2 = new UserSettingsService(m_ServHelper.WorkingDir,
                new UserSettingsAttribute("SettsStorage", true, typeof(SettsMockTransformer)));

            Directory.CreateDirectory(Path.Combine(m_ServHelper.WorkingDir, "SettsStorage"));

            var settsFile1 = Path.Combine(m_ServHelper.WorkingDir, "SettsStorage", "mock1.setts");
            var settsFile2 = Path.Combine(m_ServHelper.WorkingDir, "SettsStorage", "mock2.setts");

            File.WriteAllBytes(settsFile1, Resources.mock1);
            File.WriteAllBytes(settsFile2, Resources.mock2);

            var setts1 = srv1.ReadSettings<SettsMock1>("mock1");
            var setts2 = srv1.ReadSettings<SettsMock1>("mock2");
            var setts3 = srv1.ReadSettings<SettsMock2>("mock1");
            var setts4 = srv1.ReadSettings<SettsMock2>("mock2");

            var setts5 = srv2.ReadSettings<SettsMock1>("mock1");
            var setts6 = srv2.ReadSettings<SettsMock1>("mock2");
            var setts7 = srv2.ReadSettings<SettsMock2>("mock1");
            var setts8 = srv2.ReadSettings<SettsMock2>("mock2");

            Assert.AreEqual("AAA", setts1.Field1);
            Assert.AreEqual(10, setts1.Field2);
            Assert.AreEqual("BBB", setts2.Field1);
            Assert.AreEqual(default(double), setts2.Field2);
            Assert.AreEqual("AAA", setts3.Field1);
            Assert.AreEqual(default(double), setts3.Field3);
            Assert.AreEqual(default(bool), setts3.Field4);
            Assert.AreEqual("BBB", setts4.Field1);
            Assert.AreEqual(12.5, setts4.Field3);
            Assert.AreEqual(true, setts4.Field4);

            Assert.AreEqual("AAA", setts5.Field1);
            Assert.AreEqual(10, setts5.Field2);
            Assert.AreEqual("BBB", setts6.Field1);
            Assert.AreEqual(default(double), setts6.Field2);
            Assert.AreEqual("AAA", setts7.Field1);
            Assert.AreEqual(10, setts7.Field3);
            Assert.AreEqual(default(bool), setts7.Field4);
            Assert.AreEqual("BBB", setts8.Field1);
            Assert.AreEqual(12.5, setts8.Field3);
            Assert.AreEqual(true, setts8.Field4);
        }

        [TestMethod]
        public void WriteSettingsTest()
        {
            var srv = new UserSettingsService(m_ServHelper.WorkingDir,
                new UserSettingsAttribute("SettsStorage", true));

            var setts1 = new SettsMock1()
            {
                Field1 = "AAA",
                Field2 = 10
            };

            var setts2 = new SettsMock2()
            {
                Field1 = "BBB",
                Field3 = 12.5,
                Field4 = true
            };
            
            var settsFile1 = Path.Combine(m_ServHelper.WorkingDir, "SettsStorage", "mock1.setts");
            var settsFile2 = Path.Combine(m_ServHelper.WorkingDir, "SettsStorage", "mock2.setts");

            srv.StoreSettings(setts1, "mock1");
            srv.StoreSettings(setts2, "mock2");

            Assert.IsTrue(File.Exists(settsFile1));
            Assert.IsTrue(File.Exists(settsFile2));
            Assert.AreEqual("{\"Field1\":\"AAA\",\"Field2\":10.0,\"__version\":\"0.0\"}", File.ReadAllText(settsFile1));
            Assert.AreEqual("{\"Field1\":\"BBB\",\"Field3\":12.5,\"Field4\":true,\"__version\":\"2.1.0\"}", File.ReadAllText(settsFile2));
        }
    }
}