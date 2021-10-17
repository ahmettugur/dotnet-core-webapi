using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ATCommon.Utilities
{
    public class AppSettingsHelper
    {
        protected AppSettingsHelper()
        {
            
        }
        public static string GetAppSettings(string key)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json");

            IConfigurationRoot Configuration = builder.Build();

            var value = "";

            try
            {
                value = Configuration.GetSection($"AppSettings:{key}").Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return value;
        }
        public static string GetConnectionString(string connectionString)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json");

            IConfigurationRoot Configuration = builder.Build();

            var value = "";
            try
            {
                value = Configuration.GetConnectionString(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return value;
        }
    }
}
