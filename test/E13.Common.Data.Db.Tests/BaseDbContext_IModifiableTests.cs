using E13.Common.Data.Db.Tests.Sample;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace E13.Common.Data.Db.Tests
{
    public class BaseDbContext_IModifiableTests
    {
        private TestDbContext? Context;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(o => o.UseInMemoryDatabase($"{Guid.NewGuid()}"));

            Context = services.BuildServiceProvider().GetService<TestDbContext>();

            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.AddTestData();
        }

        [Test]
        public void InitialData_ModifiedBy_Null()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Modifiables.First();

            arranged.ModifiedBy.Should().NotBeNull();
        }

        [Test]
        public void InitialData_Modified_Null()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Modifiables.First();

            arranged.Modified.Should().NotBeNull();
        }

        [Test]
        public void InitialData_ModifiedSource_Null()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Modifiables.First();

            arranged.ModifiedSource.Should().NotBeNull();
        }

        [Test]
        public void SaveChanges_UnspecifiedUser_ModifiedByUnknown()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var initial = Context.Modifiables.First();
            initial.Text.Should().BeEmpty();

            //act
            initial.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges();

            var assert = Context.Modifiables.First();
            assert.ModifiedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_UnspecifiedUser_ModifiedUpdates()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var initial = Context.Modifiables.First();
            var arranged = initial.Modified!.Value;

            //act
            initial.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges();

            var actual = Context.Modifiables.First();
            actual.Modified.Should().BeAfter(arranged);
        }

        [Test]
        public void SaveChanges_NamedUser_UpdatesModifiedBy()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Modifiables.First();
            arranged.Text.Should().BeEmpty();

            //act
            arranged.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges(nameof(SaveChanges_NamedUser_UpdatesModifiedBy));

            var assert = Context.Modifiables.First();
            assert.ModifiedBy.Should().Be(nameof(SaveChanges_NamedUser_UpdatesModifiedBy));
        }

        [Test]
        public void SaveChanges_NamedUser_ModifiedUpdates()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Modifiables.First();

            //act
            arranged.Text = $"{Guid.NewGuid()}";
            Context.SaveChanges(nameof(SaveChanges_NamedUser_ModifiedUpdates));

            var actual = Context.Modifiables.First();
            actual.Modified.Should().NotBeNull();
        }
    }
}