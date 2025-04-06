using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace E13.Common.Api
{
    public static class ApiDiagnosticsHelper
    {
        public static object GetDiagnosticInfo(IConfiguration config, bool isDevelopmentEnv, string configKey)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblyVersion = entryAssembly?.GetName().Version;

            if (isDevelopmentEnv)
            {
                return new
                {
                    AzureAd = config.AzureAd(),
                    AssemblyVersion = assemblyVersion,
                    ConnectionStrings = config.ConnectionStrings(configKey),
                };
            }
            else
            {
                return new
                {
                    AssemblyVersion = assemblyVersion,
                };
            }
        }
    }
}
