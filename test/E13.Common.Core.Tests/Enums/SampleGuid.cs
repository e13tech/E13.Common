using E13.Common.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Tests.Enums
{
    public enum SampleGuid
    {
        [Guid("81CEB3CE-4BC9-477D-843B-6125179C70A2")] A,
        [Guid("9DA6A76B-C338-40FD-8B55-67B82651A4D6")] B,
#pragma warning disable CA2243 // Attribute string literals should parse correctly - supressing because on of the tests is to confirm that this throws an exception at runtime
        [Guid("Not a Guid")] Invalid
#pragma warning restore CA2243 // Attribute string literals should parse correctly
    }
}
