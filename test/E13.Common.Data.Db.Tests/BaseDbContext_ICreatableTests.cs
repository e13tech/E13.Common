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
    public class BaseDbContext_ICreatableTests
    {
        private TestDbContext? Context;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddScoped<CreatableInterceptor<string>>();
            services.AddScoped<ModifiableInterceptor<string>>();
            services.AddScoped<SoftDeleteInterceptor<string>>();
            services.AddDbContext<IAuditContext<string>, TestDbContext>((sp, o) =>
            {
                o.UseInMemoryDatabase($"{Guid.NewGuid()}");
                o.EnableSensitiveDataLogging();
                o.AddInterceptors(
                    sp.GetRequiredService<CreatableInterceptor<string>>(),
                    sp.GetRequiredService<ModifiableInterceptor<string>>(),
                    sp.GetRequiredService<SoftDeleteInterceptor<string>>());
            });

            Context = services.BuildServiceProvider().GetService<TestDbContext>();

            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.AddTestData();
        }
        [Test]
        public void InitialData_CreatedSource_AddTestData()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Creatables.First();

            arranged.CreatedSource.Should().Be("E13.Common.Data.Db.Tests.Sample.TestDbContext.AddTestData");
        }

        [Test]
        public void InitialData_CreatedBy_Unknown()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var arranged = Context.Creatables.First();

            arranged.CreatedBy.Should().Be(BaseDbContext<string>.UnknownUser);
        }

        [Test]
        public void SaveChanges_UnknownUser_CreatedByUnknown()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            var id = Guid.NewGuid();
            Context.Creatables.Add(new TestCreatable { Id = id });
            Context.SaveChanges();

            var arranged = Context.Creatables.First(e => e.Id == id);

            arranged.CreatedBy.Should().Be(BaseDbContext<string>.UnknownUser);
        }

        [Test]
        public void SaveChanges_NamedUser_CreatedByNamedUser()
        {
            if (Context == null)
                throw new Exception("TestDbContext did not Setup() successfully");

            Context.AuditUser = nameof(SaveChanges_NamedUser_CreatedByNamedUser);
            var id = Guid.NewGuid();
            Context.Creatables.Add(new TestCreatable { Id = id });
            Context.SaveChanges();

            var arranged = Context.Creatables.First(e => e.Id == id);

            arranged.CreatedBy.Should().Be(nameof(SaveChanges_NamedUser_CreatedByNamedUser));
        }
    }
}