using E13.Common.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Nunit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IntegrationAttribute : Attribute, IApplyToTest
    {
        protected static readonly bool InMemory = Environment.GetEnvironmentVariable("InMemory").DefaultParse(false);

        public void ApplyToTest(Test test)
        {
            ArgumentNullException.ThrowIfNull(test);

            LaunchSettingsHelper.EnsureEnvironmentVariables();

            // If this test is running in memory then skip the test
            if (InMemory)
                test.RunState = RunState.Ignored; 
        }
    }
}
