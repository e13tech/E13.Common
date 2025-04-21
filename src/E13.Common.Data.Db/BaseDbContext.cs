using E13.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using E13.Common.Data.Db.Extensions;

namespace E13.Common.Data.Db
{
    public abstract class BaseDbContext : DbContext, IAuditContext
    {
        /// <summary>
        /// The user name used when the user name is null
        /// </summary>
        public const string UnknownUser = "*Unknown";

        protected ILogger Logger { get;}

        public string? AuditUser { get; set; }

        public string? Source { get; set; }

        protected BaseDbContext(DbContextOptions options, ILogger logger)
            : base(options)
        {
            Logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasQueryFiltersFor<IDeletable>(e => e.Deleted != null);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// This is used by E13.Common.Data.Db.Tests in order to allow TestDbContext.TestSeed() to seed
        /// data without going through the TagEntries.
        /// 
        /// This is necessary so that the unit tests can, via code, initialize the context with data that
        /// may normally be prevented by TagChanges().  An example being a scenario where a backend database
        /// is manually adjusted via raw SQL and puts entities into an invalid state.
        /// 
        /// The tests will be ensuring that subsequent calls automatically clean up bad entity states caused
        /// by manual data manipulation.
        /// </summary>
        /// <returns></returns>
        internal int SaveChanges_NoTagEntries()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges()
        {
            // 1 – Get caller outside EF
            var caller = new StackFrame(1).GetMethod();
            var source = $"{caller?.DeclaringType?.FullName}.{caller?.Name}";

            // 2 – Stash it in the scoped audit context (injected)
            Source = source ?? "Unknown";
            AuditUser = AuditUser ?? UnknownUser;

            // 3 – Proceed. Interceptors now have Source & User.
            return base.SaveChanges();
        }

    }
}
