using E13.Common.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E13.Common.Nunit
{
    public class LaunchSettingsFixture
    {
        protected LaunchSettingsFixture()
        {
            LaunchSettingsHelper.EnsureEnvironmentVariables();
        }
    }
}
