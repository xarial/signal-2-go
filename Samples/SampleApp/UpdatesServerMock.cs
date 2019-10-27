using SampleApp.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Xarial.Signal2Go.Services.Updates;

namespace SampleApp
{
    internal static class UpdatesServerMock
    {
        internal static string UpdateUrl { get; private set; }

        internal static string WorkDir { get; private set; }

        static UpdatesServerMock()
        {
            WorkDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                Resources.WorkDir, "UpdatesServer");

            if (!Directory.Exists(WorkDir))
            {
                Directory.CreateDirectory(WorkDir);
            }

            var upgUrl = Path.Combine(WorkDir, "upgrade.txt");
            File.WriteAllText(upgUrl, "Upgrade");

            var whatsNewUrl = Path.Combine(WorkDir, "whats-new.txt");
            File.WriteAllText(whatsNewUrl, "What's New");
            
            var latestVers = new LatestVersionInfo()
            {
                Version = $"2.0.0.0",
                UpgradeUrl = upgUrl,
                WhatsNewUrl = whatsNewUrl
            };

            var filePath = Path.Combine(WorkDir, "latest-version.json");
            UpdateUrl = "file:///" + filePath.Replace('\\', '/');
            
            using (var str = File.Create(filePath))
            {
                var ser = new DataContractJsonSerializer(typeof(LatestVersionInfo));
                ser.WriteObject(str, latestVers);
            }
        }
    }
}
