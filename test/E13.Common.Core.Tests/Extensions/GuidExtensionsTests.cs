using E13.Common.Core.Attributes;
using E13.Common.Core.Tests.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Tests.Extensions
{
    public class GuidExtensionsTests
    {
        [Test]
        public void AsEnum_A_Matches()
        {
            var arranged = Guid.Parse("81CEB3CE-4BC9-477D-843B-6125179C70A2");

            arranged.AsEnum<SampleGuid>()
                .Should().Be(SampleGuid.A);
        }

        [Test]
        public void AsEnum_B_Matches()
        {
            var arranged = Guid.Parse("9DA6A76B-C338-40FD-8B55-67B82651A4D6");

            arranged.AsEnum<SampleGuid>()
                .Should().Be(SampleGuid.B);
        }
    }
}
