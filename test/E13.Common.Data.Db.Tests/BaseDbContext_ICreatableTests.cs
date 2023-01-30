using E13.Common.Data.Db.Tests.Sample;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace E13.Common.Data.Db.Tests
{
    public class BaseDbContext_ICreatableTests
    {
        private TestDbContext Context;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(o =>
            {
                o.UseInMemoryDatabase($"{Guid.NewGuid()}");
                o.EnableSensitiveDataLogging();
            });

            Context = services.BuildServiceProvider().GetService<TestDbContext>();
            Context.AddTestData();
        }
        [Test]
        public void InitialData_CreatedSource_AddTestData()
        {
            var arranged = Context.Creatables.First();

            arranged.CreatedSource.Should().Be("E13.Common.Data.Db.Tests.Sample.TestDbContext.AddTestData");
        }

        [Test]
        public void InitialData_CreatedBy_Unknown()
        {
            var arranged = Context.Creatables.First();

            arranged.CreatedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_UnknownUser_CreatedByUnknown()
        {
            var id = Guid.NewGuid();
            Context.Creatables.Add(new TestCreatable { Id = id });
            Context.SaveChanges();

            var arranged = Context.Creatables.First(e => e.Id == id);

            arranged.CreatedBy.Should().Be(BaseDbContext.UnknownUser);
        }

        [Test]
        public void SaveChanges_NamedUser_CreatedByNamedUser()
        {
            var id = Guid.NewGuid();
            Context.Creatables.Add(new TestCreatable { Id = id });
            Context.SaveChanges(nameof(SaveChanges_NamedUser_CreatedByNamedUser));

            var arranged = Context.Creatables.First(e => e.Id == id);

            arranged.CreatedBy.Should().Be(nameof(SaveChanges_NamedUser_CreatedByNamedUser));
        }
    }
}