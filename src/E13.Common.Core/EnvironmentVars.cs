using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Core
{
    /// <summary>
    /// Environment variables used by E13.Common
    /// </summary>
    public static class EnvironmentVars
    {
        /// <summary>
        /// Checks if the InMemory environment variable is set to true
        /// </summary>
        /// <returns>false if "InMemory" has a value of "false" or is not set, true otherwise</returns>
        public static bool IsRunningInMemory()
        {
            var inMemoryEVar = Environment.GetEnvironmentVariable("InMemory");

            if (string.IsNullOrEmpty(inMemoryEVar))
                return false;

            
            return inMemoryEVar.DefaultParse(false);
        }
    }
}
