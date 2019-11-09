using System;
using System.Collections.Generic;
using System.Reflection;
using ATCommon.Aspect.Contracts.Interception;
using ATCommon.Logging.Contracts;

namespace ATCommon.Aspect.Logging
{
    public class AfterLogAspect: InterceptionAttribute, IAfterInterception
    {
                private readonly Type _loggerType;
        private readonly ICommonLogger _logger;

        public AfterLogAspect(Type loggerType)
        {

            _loggerType = loggerType;

            if (!typeof(ICommonLogger).IsAssignableFrom(_loggerType))
            {
                throw new Exception("Wrong logger type");
            }

            _logger = (ICommonLogger)Activator.CreateInstance(_loggerType);
        }
        public void OnAfter(AfterMethodArgs afterMethodArgs)
        {
            LogMethodParameter logMethodParameter = null;

            ParameterInfo[] parameterInfos = afterMethodArgs.MethodInfo.GetParameters();
            List<LogDetailParameter> logParameters = new List<LogDetailParameter>();
            for (int i = 0; i < afterMethodArgs.Arguments.Length; i++)
            {
                if (afterMethodArgs.Arguments[i] != null)
                {
                    var parameter = afterMethodArgs.Arguments[i].GetType().BaseType.BaseType == typeof(System.Linq.Expressions.LambdaExpression)
                                    ? afterMethodArgs.Arguments[i].ToString()
                                    : afterMethodArgs.Arguments[i];

                    logParameters.Add(new LogDetailParameter
                    {
                        Name = parameterInfos[i].Name,
                        Type = parameterInfos[i].ParameterType.Name,
                        Value = parameter
                    });
                }
                else
                {
                    logParameters.Add(new LogDetailParameter
                    {
                        Name = parameterInfos[i].Name,
                        Type = parameterInfos[i].ParameterType.Name,
                        Value = null
                    });
                }

            }

            LogDetail logDetail = new LogDetail
            {
                ClassName = afterMethodArgs.MethodInfo.DeclaringType.FullName,
                MethodName = afterMethodArgs.MethodInfo.Name,
                Parameters = logParameters,
                Result = afterMethodArgs.Value
            };

            logMethodParameter = new LogMethodParameter()
            {
                LogName = afterMethodArgs.MethodInfo.Name,
                LogDetail = logDetail
            };

            _logger.Log(logMethodParameter);
        }
    
    }
}
