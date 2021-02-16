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
            //var arranged = new IDeletableSample { Deleted = null };

            //arranged.IsDeleted();
        }
    }

    public class IDeletableSample : IDeletable
    {
        public string DeletedBy { get; set; }
        public string DeletedSource { get; set; }
        public DateTime? Deleted { get; set; }
        
    }
}