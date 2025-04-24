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
    public abstract class BaseDbContext<T> : DbContext, IAuditContext<T>
    {
        /// <summary>
        /// The user name used when the user name is null
        /// </summary>
        public const string UnknownUser = "*Unknown";

        protected ILogger Logger { get;}

        public required T AuditUser { get; set; }

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
            modelBuilder.HasQueryFiltersFor<IDeletable<T>>(e => e.Deleted != null);

            base.OnModelCreating(modelBuilder);
        }
    }
}
