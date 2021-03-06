﻿using System;
using System.Diagnostics;
using ATCommon.Logging.Contracts;
using Newtonsoft.Json;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.DependencyResolvers.Autofac;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Business.Logging
{

    public class DatabaseLogger : ICommonLogger
    {
        private ILogService _logService;
        public void Log(LogMethodParameter logMethodParameter)
        {
            _logService = InstanceFactory.GetInstance<ILogService>();


            if (logMethodParameter == null)
            {
                throw new Exception("Log method parameter cannot be null");
            }
    
            if (string.IsNullOrWhiteSpace(logMethodParameter.Message))
            {
                logMethodParameter.Message = JsonConvert.SerializeObject(logMethodParameter.LogDetail, Formatting.Indented);
            }

            try
            {

                Log log = new Log
                {
                    CreateDate = DateTime.Now,
                    LogName = logMethodParameter.LogName,
                    LogDetail = logMethodParameter.Message
                };

                _logService.Add(log);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
