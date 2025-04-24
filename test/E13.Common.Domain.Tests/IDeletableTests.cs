using FluentAssertions.Common;
using NUnit.Framework;
using System;

namespace E13.Common.Domain.Tests
{
    public class IDeletableTests
    {

        [Test]
        public void IsDeleted_DeletedNull_False()
        {
            IDeletable<string> arranged = new IDeletableSample { Deleted = null };

            arranged.IsDeleted();
        }
    }

    public class IDeletableSample : IDeletable<string>
    {
        public Guid Id { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeletedSource { get; set; }
        public DateTime? Deleted { get; set; }
        
    }
}