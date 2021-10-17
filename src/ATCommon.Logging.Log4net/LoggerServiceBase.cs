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
        private const string LOG_XML_FILE= "log4net.config";
        private const string XML_NODE= "log4net";


        protected LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            var log4netXmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,LOG_XML_FILE);
            xmlDocument.Load(File.OpenRead(log4netXmlPath));

            var loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
            typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument[XML_NODE]);

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
