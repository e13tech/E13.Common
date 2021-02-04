using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Nunit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RegressionAttribute : CategoryAttribute { }
}
