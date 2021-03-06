﻿/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Drawing;
using System.Reflection;
using Xarial.AppLaunchKit.Components;

namespace Xarial.AppLaunchKit.Base
{
    public class AppInfo
    {
        public Assembly Assembly { get; private set; }
        internal WindowWrapper WindowWrapper { get; private set; }
        public string Title { get; private set; }
        public Icon Icon { get; private set; }
        public string WorkDir { get; private set; }

        internal AppInfo(Assembly appAssm, WindowWrapper windowWrapper, string title, Icon icon, string workDir)
        {
            Assembly = appAssm;
            WindowWrapper = windowWrapper;
            Title = title;
            Icon = icon;
            WorkDir = workDir;
        }
    }
}
