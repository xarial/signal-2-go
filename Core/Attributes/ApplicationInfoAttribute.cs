/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Drawing;
using Xarial.AppLaunchKit.Reflection;

namespace Xarial.AppLaunchKit.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ApplicationInfoAttribute : Attribute
    {
        public string WorkingDirectory { get; private set; }
        public string Title { get; private set; }
        public Icon Icon { get; private set; }

        public ApplicationInfoAttribute(string workDir, string appTitle, string iconPath)
            : this(workDir, appTitle, Icon.ExtractAssociatedIcon(iconPath))
        {
        }

        public ApplicationInfoAttribute(Environment.SpecialFolder workDirSpecFolder, string workDirRelPath, string appTitle, string iconPath)
            : this(System.IO.Path.Combine(Environment.GetFolderPath(workDirSpecFolder), workDirRelPath), appTitle, iconPath)
        {
        }

        public ApplicationInfoAttribute(Type resourceType, string workDirResName, string appTitleResName, string iconResName)
            : this(ResourceHelper.GetResource<string>(resourceType, workDirResName),
                  ResourceHelper.GetResource<string>(resourceType, appTitleResName),
                  ResourceHelper.GetResource<Icon>(resourceType, iconResName))
        {
        }

        public ApplicationInfoAttribute(Type resourceType, Environment.SpecialFolder workDirSpecFolder,
            string relWorkDirResName, string appTitleResName, string iconResName)
            : this(resourceType, relWorkDirResName, appTitleResName, iconResName)
        {
            WorkingDirectory = System.IO.Path.Combine(Environment.GetFolderPath(workDirSpecFolder), 
                ResourceHelper.GetResource<string>(resourceType, relWorkDirResName));
        }

        private ApplicationInfoAttribute(string workDir, string appTitle, Icon icon)
        {
            WorkingDirectory = workDir;
            Title = appTitle;
            Icon = icon;
        }
    }
}
