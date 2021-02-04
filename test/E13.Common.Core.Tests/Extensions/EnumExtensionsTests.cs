using System;
using NUnit;
using FluentAssertions;
using E13.Common.Core.Attributes;
using NUnit.Framework;
using E13.Common.Core.Tests.Enums;

namespace E13.Common.Core.Tests.Extensions
{
    public class EnumExtensionsTests
    {
        [Test]
        public void AsDisplay_EnumA_CorrectValue()
        {
            SampleDisplay.A.AsDisplay()
                .Should().Be("ValueA Display Text");
        }

        [Test]
        public void AsDisplay_EnumB_CorrectValue()
        {
            SampleDisplay.B.AsDisplay()
                .Should().Be("ValueB Display Text");
        }

        [Test]
        public void AsEnum_A_CorrectValue()
        {
            SampleGuid.A.AsGuid()
                .Should().Be(Guid.Parse("81CEB3CE-4BC9-477D-843B-6125179C70A2"));
        }

        [Test]
        public void AsEnum_B_CorrectValue()
        {
            SampleGuid.B.AsGuid()
                .Should().Be(Guid.Parse("9DA6A76B-C338-40FD-8B55-67B82651A4D6"));
        }

        [Test]
        public void AsEnum_Invalid_ThrowsExceptions()
        {
            Action a = () => SampleGuid.Invalid.AsGuid();

            a.Should().Throw<FormatException>();
        }
    }
}
