using E13.Common.Data.Db.Tests.Sample;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace E13.Common.Data.Db.Tests
{
    public class BaseDbContextTests
    {
        private TestDbContext Context;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(o => o.UseInMemoryDatabase($"{Guid.NewGuid()}"));

            Context = services.BuildServiceProvider().GetService<TestDbContext>();
            Context.AddTestData();
        }

        /// <summary>
        /// InMemory Data should initialize to a single entry in every table
        /// </summary>
        [Test]
        public void InMemory_Baseline_OnePerTable()
        {
            Context.Entities.Count().Should().Be(1);
            Context.Deletable.Count().Should().Be(1);
            Context.Effectable.Count().Should().Be(1);
            Context.Ownable.Count().Should().Be(1);
        }

        /// <summary>
        /// InMemory Data should initialize with a non-empty guid for the Id
        /// </summary>
        [Test]
        public void InMemory_Baseline_EmptyGuids()
        {
            Context.Entities.All(e => e.Id == Guid.Empty).Should().BeFalse();
            Context.Deletable.All(e => e.Id == Guid.Empty).Should().BeFalse();
            Context.Effectable.All(e => e.Id == Guid.Empty).Should().BeFalse();
            Context.Ownable.All(e => e.Id == Guid.Empty).Should().BeFalse();
        }
    }
}