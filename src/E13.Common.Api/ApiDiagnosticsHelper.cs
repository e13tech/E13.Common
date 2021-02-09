using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace E13.Common.Api
{
    public static class ApiDiagnosticsHelper
    {
        public static object GetDiagnosticInfo(IConfiguration config, bool isDevelopmentEnv, string configKey)
        {
            if (isDevelopmentEnv)
            {
                return new
                {
                    AzureAd = config.AzureAd(),
                    AssemblyVersion = Assembly.GetEntryAssembly().GetName().Version,
                    ConnectionStrings = config.ConnectionStrings(configKey),
                };
            }
            else
            {
                return new
                {
                    AssemblyVersion = Assembly.GetEntryAssembly().GetName().Version,
                };
            }
        }
    }
}
