/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Xarial.AppLaunchKit.Base.Services;
using Xarial.AppLaunchKit.Common;
using Xarial.AppLaunchKit.Services.Attributes;
using Xarial.AppLaunchKit.Services.Logger.Exceptions;

namespace Xarial.AppLaunchKit.Services.Logger
{
    public class SystemEventLogService : BaseService<LogAttribute>, ILogService
    {
        private string m_Source;
        private string m_LogName;

        internal EventLog EventLog { get; private set; }

        private bool m_ThrowOnError;

        public SystemEventLogService()
        {
        }

        internal SystemEventLogService(LogAttribute bindingAtt)
        {
            Init(null, "", bindingAtt);
        }

        public void LogMessage(string msg)
        {
            WriteLog(msg, EventLogEntryType.Information);
        }

        public void LogException(Exception ex)
        {
            var error = new StringBuilder();
            GetFullExceptionError(ex, error);

            LogError(error.ToString());
        }

        private void GetFullExceptionError(Exception ex, StringBuilder error)
        {
            if (ex != null)
            {
                error.AppendLine(ex.Message);
                error.AppendLine(ex.StackTrace);
                error.AppendLine();

                GetFullExceptionError(ex.InnerException, error);
            }
        }

        public void LogWarning(string warn)
        {
            WriteLog(warn, EventLogEntryType.Warning);
        }

        public void LogError(string err)
        {
            WriteLog(err, EventLogEntryType.Error);
        }

        private void WriteLog(string message, EventLogEntryType type)
        {
            try
            {
                EventLog.WriteEntry(message, type);
            }
            catch(Exception ex)
            {
                if (m_ThrowOnError)
                {
                    throw new LogException("Error writing the log", ex);
                }
            }
        }

        protected override void Init(Assembly assm, string workDir, LogAttribute bindingAtt)
        {
            m_Source = bindingAtt.SourceName;
            m_LogName = bindingAtt.LogName;
            m_ThrowOnError = bindingAtt.ThrowOnError;

            if (bindingAtt.CreateStore)
            {
                try
                {
                    var createSource = false;

                    if (EventLog.SourceExists(m_Source))
                    {
                        if (EventLog.LogNameFromSourceName(m_Source, ".") != m_LogName)
                        {
                            EventLog.DeleteEventSource(m_Source);
                            createSource = true;
                        }
                    }
                    else
                    {
                        createSource = true;
                    }

                    if (createSource)
                    {
                        EventLog.CreateEventSource(m_Source, m_LogName);
                    }
                }
                catch(Exception ex)
                {
                    if (m_ThrowOnError)
                    {
                        throw new LogException("Failed to create log source", ex);
                    }
                }
            }

            EventLog = new EventLog(m_LogName)
            {
                Source = m_Source
            };
        }
    }
}
