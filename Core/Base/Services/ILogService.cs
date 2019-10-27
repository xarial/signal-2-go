/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;

namespace Xarial.Signal2Go.Base.Services
{
    public interface ILogService : IService
    {
        void LogMessage(string msg);
        void LogException(Exception ex);
        void LogWarning(string warn);
        void LogError(string err);
    }
}
