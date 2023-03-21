using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Logger
{
    public class LogWriter
    {
        private readonly ILogger<LogWriter> _logger;
        private readonly IHttpContextAccessor _httpContext;
        public LogWriter(ILogger<LogWriter> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext;
        }

        public void WriteLog(string messageLog, LogType logType, Exception exceptionLog = null)
        {
            messageLog = $"\n Time: {DateTime.Now.ToString()}. \n UserIdentifier: {_httpContext.HttpContext.Connection.RemoteIpAddress.ToString()} \n Message: {messageLog}";
            MainLogWriter(messageLog, logType, exceptionLog);
        }

        private void MainLogWriter(string messageLog, LogType logType, Exception? exceptionLog = null)
        {
            switch (logType)
            {
                case LogType.LOG_DEBUG:
                    _logger.LogDebug(exceptionLog, messageLog);
                    break;
                case LogType.LOG_INFORMATION:
                    _logger.LogInformation(messageLog);
                    break;
                case LogType.LOG_ERROR:
                    _logger.LogError(messageLog);
                    break;
            }
        }
    }

    public enum LogType
    {
        LOG_DEBUG = 1,
        LOG_INFORMATION = 2,
        LOG_ERROR = 3
    }
}
