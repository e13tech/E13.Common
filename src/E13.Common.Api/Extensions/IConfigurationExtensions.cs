using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace E13.Common.Api
{
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// This method returns the AzureAD configuration information that is set on the
        /// current micro-service
        /// </summary>
        /// <param name="config">configuration for the current micro-service</param>
        /// <returns></returns>
        public static Dictionary<string, string> AzureAd(this IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new Dictionary<string, string>()
            {
                {"TenantId" , config["AzureAd:TenantId"] },
                {"ClientId" , config["AzureAd:ClientId"] },
                {"ClientSecret" , config["AzureAd:ClientSecret"] },
            };
        }

        /// <summary>
        /// This method returns the connection strings that is set on the
        /// current micro-service
        /// </summary>
        /// <param name="config">configuration object for the current micro-service</param>
        /// <param name="configKey">configuration key for the micro-service's database connection string</param>
        /// <returns></returns>
        public static Dictionary<string, string> ConnectionStrings(this IConfiguration config, string configKey)
        {
            return new Dictionary<string, string>()
            {
                { configKey , config.GetConnectionString(configKey) },
            };
        }
    }
}
