/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.AppLaunchKit.Base.Services
{
    public interface ILogService : IService
    {
        void LogMessage(string msg);
        void LogException(Exception ex);
        void LogWarning(string warn);
        void LogError(string err);
    }
}
