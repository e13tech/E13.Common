using E13.Common.Domain.Specifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace E13.Common.Domain.Tests.Specifications
{
    public class LogicalNotTests
    {
        [Test]
        public void LeftTrue_RightTrue_True()
        {
            var s = new TrueSpecification();

            var result = s.LogicalNot().IsSatisfiedBy(null);

            result.Should().BeFalse();
        }
        [Test]
        public void LeftFalse_RightTrue_False()
        {
            var s = new FalseSpecification();

            var result = s.LogicalNot().IsSatisfiedBy(null);

            result.Should().BeTrue();
        }
    }
}
