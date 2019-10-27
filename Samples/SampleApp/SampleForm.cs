﻿using SampleApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xarial.Signal2Go;
using Xarial.Signal2Go.Attributes;
using Xarial.Signal2Go.Base.Services;
using Xarial.Signal2Go.Services.About;
using Xarial.Signal2Go.Services.Attributes;
using Xarial.Signal2Go.Services.Eula;
using Xarial.Signal2Go.Services.Eula.Exceptions;
using Xarial.Signal2Go.Services.Logger;
using Xarial.Signal2Go.Services.Updates;
using Xarial.Signal2Go.Services.Updates.Exceptions;
using Xarial.Signal2Go.Services.UserSettings;

namespace SampleApp
{   
    public partial class SampleForm : Form
    {
        private ServicesManager m_Kit;

        public SampleForm()
        {
            InitializeComponent();

            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }

            m_Kit = new ServicesManager(this.GetType().Assembly, this.Handle);

            LoadServices();
        }

        private async void LoadServices()
        {
            Task svcTask = null;

            try
            {
                svcTask = m_Kit.StartServicesAsync();
                await svcTask;
            }
            catch
            {
                foreach (var ex in svcTask.Exception.InnerExceptions)
                {
                    if (ex is UpdatesCheckException)
                    {
                        MessageBox.Show("Failed to check for updates");
                    }
                    else if (ex is EulaNotAgreedException)
                    {
                        MessageBox.Show("EULA not signed");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            m_Kit.GetService<ILogService>().LogMessage("Starting the application");

            var setts = m_Kit.GetService<IUserSettingsService>().ReadSettings<CustomUserSettings>("user");
            lblUserMessage.Text = setts.Message;
            m_Kit.GetService<IUserSettingsService>().StoreSettings(setts, "user");
        }

        private void OnServicesLaunchTerminated()
        {
            m_Kit.GetService<ILogService>().LogError("Services terminated");
        }

        private void OnServicesLaunchCompleted()
        {
            m_Kit.GetService<ILogService>().LogMessage("Services launched");
        }
        
        private void OnServerLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Kit.GetService<ILogService>().LogMessage("Server Link Clicked");

            OpenFolder(UpdatesServerMock.WorkDir);
        }

        private void OnEulaCacheLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Kit.GetService<ILogService>().LogMessage("Eula Link Clicked");

            SelectFile(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Resources.WorkDir, "eula.json"));
        }

        private void OnUserSettingsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectFile(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Resources.WorkDir, "Settings", "user.setts"));
        }

        private void SelectFile(string filePath)
        {
            Process.Start("explorer.exe",
                $"/select, \"{filePath}\"");
        }

        private void OpenFolder(string dirPath)
        {
            Process.Start("explorer.exe",
                dirPath);
        }

        private void OnEventLogLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("eventvwr", "/c:Xarial");
        }

        private void OnAboutLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Kit.GetService<IAboutApplicationService>().ShowAboutForm();
        }
    }
}
