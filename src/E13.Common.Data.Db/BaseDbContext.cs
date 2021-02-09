using E13.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;

namespace E13.Common.Data.Db
{
    public abstract class BaseDbContext : DbContext
    {
        /// <summary>
        /// The user name used when the user name is null
        /// </summary>
        private const string UnknownUser = "*Unknown";

        protected ILogger Logger { get;}
        protected string User { get; set; }
        protected BaseDbContext(ILogger logger, string user)
        {
            Logger = logger;
            User = user;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var caller = new StackFrame(1).GetMethod();
            TagEntries($"{caller.DeclaringType}.{caller.Name}");
            var result = base.SaveChanges();

            // saving after a reload effectively clears the change tracker affecting no records
            Reload();
            base.SaveChanges();
            
            return result;
        }

        public void Reload() => ChangeTracker.Entries()
            .Where(e => e.Entity != null).ToList()
            .ForEach(e => e.State = EntityState.Detached);

        private void TagEntries(string source)
        {
            var entries = ChangeTracker.Entries().Where(e => 
                e.Entity is IEntity && 
                (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            );

            Logger.LogInformation($"ChangeTracker Contains {entries.Count()}/{ChangeTracker.Entries().Count()} in an Added or Modified state.");

            foreach(var entry in entries)
            {
                Logger.LogDebug($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                var utcNow = DateTime.UtcNow;

                if (entry.Entity is IEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((IEntity)entry.Entity).Created = utcNow;
                        ((IEntity)entry.Entity).CreatedBy = User ?? UnknownUser;
                        ((IEntity)entry.Entity).CreatedSource = source;
                    }

                    ((IEntity)entry.Entity).Modified = utcNow;
                    ((IEntity)entry.Entity).ModifiedBy = User ?? UnknownUser;
                    ((IEntity)entry.Entity).ModifiedSource = source;

                    if (entry.State == EntityState.Deleted && entry.Entity is IDeletable)
                    {
                        // Implementing IDeletable implies soft deletes required
                        entry.State = EntityState.Modified;

                        ((IDeletable)entry.Entity).Deleted = utcNow;
                        ((IDeletable)entry.Entity).DeletedBy = User ?? UnknownUser;
                        ((IDeletable)entry.Entity).DeletedSource = source;
                    }
                }
            }
        }
    }
}
