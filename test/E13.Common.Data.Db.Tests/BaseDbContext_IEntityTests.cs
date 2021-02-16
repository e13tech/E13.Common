using E13.Common.Data.Db.Tests.Sample;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace E13.Common.Data.Db.Tests
{
    public class BaseDbContext_IEntityTests
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

        [Test]
        public void InitialData_CreatedSource_AddTestData()
        {
            var arranged = Context.Entities.First();

            arranged.CreatedSource.Should().Be("E13.Common.Data.Db.Tests.Sample.TestDbContext.AddTestData");
        }

        [Test]
        public void InitialData_CreatedBy_Unknown()
        {
            var arranged = Context.Entities.First();

            arranged.CreatedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_UnknownUser_CreatedByUnknown()
        {
            var id = Guid.NewGuid();
            Context.Entities.Add(new TestEntity { Id = id });
            Context.SaveChanges();

            var arranged = Context.Entities.First(e => e.Id == id);

            arranged.CreatedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_NamedUser_CreatedByNamedUser()
        {
            var id = Guid.NewGuid();
            Context.Entities.Add(new TestEntity { Id = id });
            Context.SaveChanges(nameof(SaveChanges_NamedUser_CreatedByNamedUser));

            var arranged = Context.Entities.First(e => e.Id == id);

            arranged.CreatedBy.Should().Be(nameof(SaveChanges_NamedUser_CreatedByNamedUser));
        }

        [Test]
        public void InitialData_ModifiedBy_Unknown()
        {
            var arranged = Context.Entities.First();

            arranged.ModifiedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_UnspecifiedUser_ModifiedByUnknown()
        {
            var initial = Context.Entities.First();
            initial.Text.Should().BeNull();

            //act
            initial.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges();

            var assert = Context.Entities.First();
            assert.ModifiedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_UnspecifiedUser_ModifiedUpdates()
        {
            var initial = Context.Entities.First();
            var arranged = initial.Modified;

            //act
            initial.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges();

            var actual = Context.Entities.First();
            actual.Modified.Should().BeAfter(arranged);
        }

        [Test]
        public void SaveChanges_NamedUser_UpdatesModifiedBy()
        {
            var arranged = Context.Entities.First();
            arranged.Text.Should().BeNull();

            //act
            arranged.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges(nameof(SaveChanges_NamedUser_UpdatesModifiedBy));

            var assert = Context.Entities.First();
            assert.ModifiedBy.Should().Be(nameof(SaveChanges_NamedUser_UpdatesModifiedBy));
        }

        [Test]
        public void SaveChanges_NamedUser_ModifiedUpdates()
        {
            var initial = Context.Entities.First();
            var arranged = initial.Modified;

            //act
            initial.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges(nameof(SaveChanges_NamedUser_ModifiedUpdates));

            var actual = Context.Entities.First();
            actual.Modified.Should().BeAfter(arranged);
        }
    }
}