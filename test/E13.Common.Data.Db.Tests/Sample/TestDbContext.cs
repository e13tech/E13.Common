using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestDbContext : BaseDbContext
    {
        public DbSet<TestCreatable> Creatables { get; set; }
        public DbSet<TestModifiable> Modifiables { get; set; }
        public DbSet<TestDeletable> Deletable { get; set; }
        public DbSet<TestEffectable> Effectable { get; set; }
        public DbSet<TestOwnable> Ownable { get; set; }

        public TestDbContext(DbContextOptions opt)
            : base(opt, new NullLogger<TestDbContext>())
        { }

        public void AddTestData()
        {
            Creatables.Add(new TestCreatable
            {
                Id = Guid.Empty
            });
            Modifiables.Add(new TestModifiable
            {
                Id = Guid.Empty
            });
            Deletable.Add(new TestDeletable
            {
                Id = Guid.Empty
            });
            Effectable.Add(new TestEffectable
            {
                Id = Guid.Empty
            });
            Ownable.Add(new TestOwnable
            {
                Id = Guid.Empty
            });
            SaveChanges();
        }
    }
}
