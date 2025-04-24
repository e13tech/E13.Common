using E13.Common.Data.Db.Interceptors;
using E13.Common.Data.Db.Tests.Sample;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace E13.Common.Data.Db.Tests
{
    public class BaseDbContext_IDeletableTests
    {
        private TestDbContext? Context;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(o => {
                o.UseInMemoryDatabase($"{Guid.NewGuid()}");
                o.AddInterceptors(new CreatableInterceptor<string>());
                o.AddInterceptors(new ModifiableInterceptor<string>());
                o.AddInterceptors(new SoftDeleteInterceptor<string>());
            });

            Context = services.BuildServiceProvider().GetService<TestDbContext>();
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.AddTestData();
        }

        [Test]
        public void QueryFilter_ExcludesDeletedByDefault()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.Deletable.Count().Should().Be(0);
            Context.Deletable.Count(e => e.Deleted == null).Should().Be(0);

            Context.Deletable.IgnoreQueryFilters().Count().Should().Be(1);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.Deleted == null).Should().Be(1);
        }

        [Test]
        public void SaveChanges_Deleting_SetsDeleted()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.Deletable.IgnoreQueryFilters().Count().Should().Be(1);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.Deleted == null).Should().Be(1);
            var arranged = Context.Deletable.IgnoreQueryFilters().First();

            Context.Deletable.Remove(arranged);
            Context.SaveChanges();

            Context.Deletable.IgnoreQueryFilters().Count().Should().Be(1);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.Deleted == null).Should().Be(0);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.Deleted != null).Should().Be(1);
        }

        [Test]
        public void SaveChangesForUser_Deleting_SetsDeletedBy()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.AuditUser = nameof(SaveChangesForUser_Deleting_SetsDeletedBy);
            Context.Deletable.IgnoreQueryFilters().Count().Should().Be(1);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.DeletedBy == null).Should().Be(1);
            var arranged = Context.Deletable.IgnoreQueryFilters().First(f => f.Deleted == null);

            Context.Deletable.Remove(arranged);
            Context.SaveChanges();

            Context.Deletable.IgnoreQueryFilters().Count().Should().Be(1);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.DeletedBy == null).Should().Be(0);
            Context.Deletable.IgnoreQueryFilters().Count(e => e.DeletedBy == nameof(SaveChangesForUser_Deleting_SetsDeletedBy)).Should().Be(1);
        }

    }
}