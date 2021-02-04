using System.IO;
using FluentAssertions;
using System;
using NUnit.Framework;

namespace E13.Common.Nunit.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Test]
        public void ParentDirectory_ValidParent_Found()
        {
            var c = Directory.GetCurrentDirectory();
            // the default working directory for a unit test execution should be somewhere under a folder for the project with this assembly name
            var t = c.ParentDirectory("E13.Common.Nunit.Tests");

            t.Should().NotBeNullOrWhiteSpace();
        }
        [Test]
        public void ParentDirectory_RandomGuid_ThrowsException()
        {
            var c = Directory.GetCurrentDirectory();
            // using a random new Guid to represent a parent directory that should not exist
            Action a = () => c.ParentDirectory(Guid.NewGuid().ToString());

            a.Should()
                .Throw<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("parent");
        }
        [Test]
        public void ParentSiblingDirectory_ValidParent_Found()
        {
            var c = Directory.GetCurrentDirectory();
            // Checking for a parent sibling of src since all non-test projects are in there
            var t = c.ParentSiblingDirectory("src");

            t.Should().NotBeNullOrWhiteSpace();
        }
        [Test]
        public void ParentSiblingDirectory_RandomGuid_ThrowsException()
        {
            var c = Directory.GetCurrentDirectory();
            // using a random new Guid to represent a parent sibling directory that should not exist
            Action a = () => c.ParentSiblingDirectory(Guid.NewGuid().ToString());

            a.Should()
                .Throw<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("sibling");
        }
    }
}
