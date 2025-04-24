using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestDbContext : BaseDbContext<string>
    {
        public DbSet<TestCreatable> Creatables { get; set; }
        public DbSet<TestModifiable> Modifiables { get; set; }
        public DbSet<TestDeletable> Deletable { get; set; }
        /// <summary>
        /// Isolated TestDeletable DbSet that will be used in tests
        /// designed to ensure that the normal SaveChanges(...) processes
        /// will clean up bad/invalid data caused by manual manipulation
        /// of the data
        /// </summary>
        public DbSet<TestInvalidDeletable> InvalidDeletable { get; set; }
        public DbSet<TestEffectable> Effectable { get; set; }
        public DbSet<TestOwnable> Ownable { get; set; }

        public TestDbContext(DbContextOptions opt)
            : base(opt, new NullLogger<TestDbContext>())
        { }

        public void AddTestData()
        {
            AuditUser = UnknownUser;
            Source = "E13.Common.Data.Db.Tests.Sample.TestDbContext.AddTestData";

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

            /** 
             * this separate DbSet is an example of a data scenario that
             * should be impossible if all database interactions go through 
             * the application.
             * 
             * This will look like bad/invalid data because it is
             */
            InvalidDeletable.Add(new TestInvalidDeletable
            {
                Id = Guid.Empty,
                Deleted = null,
                DeletedBy = "PreviousBy",
                DeletedSource = "PreviousSource"
            });
            // Because invalid data is being arranged the internal SaveChanges avoiding the TagEntries code must be used.
            SaveChanges();
        }
    }
}
