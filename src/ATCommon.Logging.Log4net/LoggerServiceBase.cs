using System;
using System.IO;
using System.Reflection;
using System.Xml;
using ATCommon.Logging.Contracts;
using log4net;
using log4net.Repository;

namespace ATCommon.Logging.Log4net
{
    public class LoggerServiceBase:ICommonLogger
    {
        private readonly ILog _log;
        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            var t = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
            xmlDocument.Load(File.OpenRead(t));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
            typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name);
        }


        bool IsFatalEnabled => _log.IsFatalEnabled;
        bool IsWarnEnabled => _log.IsWarnEnabled;
        bool IsInfoEnabled => _log.IsInfoEnabled;
        bool IsDebugEnabled => _log.IsDebugEnabled;
        bool IsErrorEnabled => _log.IsErrorEnabled;
        public void Info(object logMessage)
        {
            if (IsInfoEnabled)
                _log.Info(logMessage);
        }
        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
                _log.Debug(logMessage);
        }
        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
                _log.Warn(logMessage);
        }
        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
                _log.Fatal(logMessage);
        }
        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
                _log.Error(logMessage);
        }

        public void Log(LogMethodParameter logMethodParameter)
        {
            if (IsInfoEnabled)
                _log.Info(logMethodParameter);
        }
    }
}
