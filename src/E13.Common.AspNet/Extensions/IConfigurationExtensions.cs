using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// This doesn't strictly require the IConfiguration object but it was a convenient
        /// extension point and it will likely make sense to add other extensions to this
        /// interface
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool RunningInMemory(this IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(config);

            //return config.GetValue<bool>("InMemory");


            return Environment.GetEnvironmentVariable("InMemory").DefaultParse(true);
        }
    }
}
