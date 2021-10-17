﻿using ATCommon.Aspect.Contracts.Interception;
using ATCommon.Caching.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATCommon.Aspect.Caching
{
    public class CacheRemoveAspectAttribute : InterceptionAttribute, IAfterInterception
    {
        private readonly ICacheManager _cacheManager;
        public CacheRemoveAspectAttribute(Type cacheType)
        {

            if (!typeof(ICacheManager).IsAssignableFrom(cacheType))
            {
                throw new ArgumentException("Wrong caching type");
            }

            _cacheManager = (ICacheManager)Activator.CreateInstance(cacheType);
        }
        public void OnAfter(AfterMethodArgs afterMethodArgs)
        {
            string key = $"{afterMethodArgs.MethodInfo.DeclaringType.FullName}.{afterMethodArgs.MethodInfo.Name}";
            if (_cacheManager.IsExist(key))
            {
                _cacheManager.Remove(key);
            }
        }
    }
}
