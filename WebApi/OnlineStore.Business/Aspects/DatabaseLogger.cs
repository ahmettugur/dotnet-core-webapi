using System;
using ATCommon.Logging.Contracts;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.DependencyResolvers.Autofac;

namespace OnlineStore.Business.Aspects
{
    public class DatabaseLogger : ICommonLogger
    {
        public void Log(LogMethodParameter logMethodParameter)
        {
            var ps = InstanceFactory.GetInstance<IProductService>();



        }
    }
}
