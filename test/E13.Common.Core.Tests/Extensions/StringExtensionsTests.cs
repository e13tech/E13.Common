using E13.Common.Core.Tests.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Test]
        public void DefaultParse_ValidTrueString_True()
        {
            "true".DefaultParse()
                .Should().BeTrue();
        }
        [Test]
        public void DefaultParse_ValidFalseString_False()
        {
            "false".DefaultParse()
                .Should().BeFalse();
        }
        [Test]
        public void DefaultParse_EmptyString_False()
        {
            string.Empty.DefaultParse()
                .Should().BeFalse();
        }
        [Test]
        public void DefaultParse_EmptyStringTrueDefault_False()
        {
            string.Empty.DefaultParse(true)
                .Should().BeTrue();
        }
        [Test]
        public void AsEnum_A_Matches()
        {
            var arranged = "ValueA Display Text";

            arranged.AsEnum<SampleDisplay>()
                .Should().Be(SampleDisplay.A);
        }

        [Test]
        public void AsEnum_B_Matches()
        {
            var arranged = "ValueB Display Text";

            arranged.AsEnum<SampleDisplay>()
                .Should().Be(SampleDisplay.B);
        }
    }
}
