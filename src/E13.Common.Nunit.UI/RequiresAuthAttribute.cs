using E13.Common.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Nunit.UI
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequiresAuthAttribute : Attribute, IApplyToContext
    {
        public void ApplyToContext(TestExecutionContext context)
        {
            LaunchSettingsHelper.EnsureEnvironmentVariables();
        }
    }
}
