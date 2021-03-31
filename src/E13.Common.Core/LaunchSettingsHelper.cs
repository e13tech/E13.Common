using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E13.Common.Core
{
    /// <summary>
    /// Helpers used to help getting settings primarily for unit test execution
    /// </summary>
    public static class LaunchSettingsHelper
    {
        private static bool _evLoaded;

        /// <summary>
        /// 
        /// </summary>
        public static void EnsureEnvironmentVariables()
        {
            if (_evLoaded) return;

            using var file = File.OpenText(Path.Combine("Properties", "launchSettings.json"));
            using var reader = new JsonTextReader(file);

            var jObject = JObject.Load(reader);

            var variables = jObject
                .GetValue("profiles", StringComparison.InvariantCultureIgnoreCase)
                //select a proper profile here
                .SelectMany(profiles => profiles.Children())
                .SelectMany(profile => profile.Children<JProperty>())
                .Where(prop => prop.Name == "environmentVariables")
                .SelectMany(prop => prop.Value.Children<JProperty>())
                .ToList();

            variables.ForEach(v =>
            {
                if (Environment.GetEnvironmentVariable(v.Name) == null)
                {
                    Environment.SetEnvironmentVariable(v.Name, v.Value.ToString());
                }
            });
            _evLoaded = true;
        }
    }
}
