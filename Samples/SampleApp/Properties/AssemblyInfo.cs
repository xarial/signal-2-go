using SampleApp;
using SampleApp.Properties;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Xarial.Signal2Go.Attributes;
using Xarial.Signal2Go.Services.Attributes;

[assembly: AssemblyTitle("SampleApp")]
[assembly: AssemblyDescription("Sample application demonstrating the use of Signal2Go services")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Xarial")]
[assembly: AssemblyProduct("Signal2Go Example")]
[assembly: AssemblyCopyright("Copyright © Xarial  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("69bf95c6-d0b7-446b-8b10-ce634d778593")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: ExternalProcess("Aasdsda")]

[assembly: UpdatesUrl(typeof(UpdatesServerMock), nameof(UpdatesServerMock.UpdateUrl))]

[assembly: Eula(typeof(Resources), nameof(Resources.test_eula))]
[assembly: Log("Xarial", "SampleApplication", true, false)]
[assembly: UserSettings("Settings", false, typeof(CustomUserSettingsBackwardCompatibility))]
[assembly: About(typeof(Resources), nameof(Resources.test_eula),
    nameof(Resources.test_licenses), nameof(Resources.app_logo))]

[assembly: ApplicationInfo(typeof(Resources), Environment.SpecialFolder.ApplicationData,
    nameof(Resources.WorkDir), nameof(Resources.AppTitle), nameof(Resources.app_icon))] 