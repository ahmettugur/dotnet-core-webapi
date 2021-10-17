using ATCommon.Utilities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCommon.Caching.Redis
{
    public class RedisConnectionFactory
    {

        protected RedisConnectionFactory()
        {
         
        }


        private static Lazy<ConnectionMultiplexer> lazyConnection = new(() =>
        {
            var RedisCacheUrl = AppSettingsHelper.GetAppSettings("RedisCacheUrl");
            if (string.IsNullOrEmpty(RedisCacheUrl))
            {
                throw new ArgumentException("appsettings.json > AppSettings > \"RedisCacheUrl\": \"localhost:6379\" bulunamadı.");
            }

            return ConnectionMultiplexer.Connect(RedisCacheUrl);
        });

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public static void DisposeConnection()
        {
            if (lazyConnection.Value.IsConnected)
                lazyConnection.Value.Dispose();
        }
    }
}
