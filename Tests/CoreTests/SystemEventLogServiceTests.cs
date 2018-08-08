using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.AppLaunchKit.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Services.Attributes;
using System.Diagnostics;

namespace Xarial.AppLaunchKit.Services.Log.Tests
{
    [TestClass]
    public class SystemEventLogServiceTests
    {
        [TestMethod]
        public void LogTest()
        {
            var srv = new SystemEventLogService(
                new LogAttribute("Xarial.Test", "SystemEventLogService", true, true));

            var entries = new List<EventLogEntry>();

            var start = DateTime.Now;

            var varMsg = Guid.NewGuid().ToString();

            var initalEntriesCount = srv.EventLog.Entries.Count;

            srv.LogError("Some Error" + varMsg);
            srv.LogWarning("Some Warning" + varMsg);
            srv.LogMessage("Some Message" + varMsg);
            srv.LogException(new Exception("Some Exception" + varMsg, new Exception("Some Internal Exception")));
            
            var e0 = srv.EventLog.Entries[srv.EventLog.Entries.Count - 4];
            var e1 = srv.EventLog.Entries[srv.EventLog.Entries.Count - 3];
            var e2 = srv.EventLog.Entries[srv.EventLog.Entries.Count - 2];
            var e3 = srv.EventLog.Entries[srv.EventLog.Entries.Count - 1];

            Assert.AreEqual(4, srv.EventLog.Entries.Count - initalEntriesCount);

            Assert.AreEqual("Some Error" + varMsg, e0.Message);
            Assert.AreEqual(EventLogEntryType.Error, e0.EntryType);
            Assert.AreEqual("Some Warning" + varMsg, e1.Message);
            Assert.AreEqual(EventLogEntryType.Warning, e1.EntryType);
            Assert.AreEqual("Some Message" + varMsg, e2.Message);
            Assert.AreEqual(EventLogEntryType.Information, e2.EntryType);
            Assert.AreEqual($"Some Exception{varMsg}\r\n\r\n\r\nSome Internal Exception\r\n\r\n\r\n", e3.Message);
            Assert.AreEqual(EventLogEntryType.Error, e3.EntryType);
        }
    }
}