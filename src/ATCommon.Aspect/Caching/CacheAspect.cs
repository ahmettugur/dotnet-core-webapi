﻿using ATCommon.Aspect.Contracts.Interception;
using ATCommon.Caching.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATCommon.Aspect.Caching
{
    public class CacheAspectAttribute : InterceptionAttribute, IBeforeInterception, IAfterInterception
    {
        private readonly Type _returnType;
        private readonly int _expireAsMinute;
        private readonly ICacheManager _cacheManager;

        public CacheAspectAttribute(Type cacheType, Type returnType, int expireAsMinute = 60)
        {

            if (!typeof(ICacheManager).IsAssignableFrom(cacheType))
            {
                throw new ArgumentException("Wrong caching type");
            }
            _returnType = returnType;
            _expireAsMinute = expireAsMinute;
            _cacheManager = (ICacheManager)Activator.CreateInstance(cacheType);

        }
        public object OnBefore(BeforeMethodArgs beforeMethodArgs)
        {
            string key = $"{beforeMethodArgs.MethodInfo.DeclaringType.FullName}.{beforeMethodArgs.MethodInfo.Name}";

            object data = null;
            if (!_cacheManager.IsExist(key)) return data;
            var cacheData = _cacheManager.Get<object>(key);
            if (cacheData == null)
            {
                _cacheManager.Remove(key);
                return null;
            }
            data = JsonConvert.DeserializeObject(cacheData.ToString(), _returnType);

            return data;
        }

        public void OnAfter(AfterMethodArgs afterMethodArgs)
        {
            string key = $"{afterMethodArgs.MethodInfo.DeclaringType.FullName}.{afterMethodArgs.MethodInfo.Name}";
            if (_cacheManager.IsExist(key)) return;
            if (afterMethodArgs.Value == null) return;
            if (afterMethodArgs.Value.ToString() != "[]" && !string.IsNullOrEmpty(afterMethodArgs.Value.ToString()))
            {
                _cacheManager.Add(key, afterMethodArgs.Value, _expireAsMinute);
            }
        }
    }
}
