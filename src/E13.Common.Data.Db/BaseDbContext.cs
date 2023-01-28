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

namespace E13.Common.Data.Db
{
    public abstract class BaseDbContext : DbContext
    {
        /// <summary>
        /// The user name used when the user name is null
        /// </summary>
        public const string UnknownUser = "*Unknown";

        protected ILogger Logger { get;}
        protected BaseDbContext(DbContextOptions options, ILogger logger)
            : base(options)
        {
            Logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public int SaveChanges(string user, string source = null)
        {
            if(source == null)
            {
                var caller = new StackFrame(1).GetMethod();
                source = $"{caller.DeclaringType}.{caller.Name}";
            }

            TagEntries(source, user);
            var result = base.SaveChanges();

            // saving after a reload effectively clears the change tracker affecting no records
            Reload();
            base.SaveChanges();

            return result;
        }

        public override int SaveChanges()
        {
            var caller = new StackFrame(1).GetMethod();

            return SaveChanges(UnknownUser, $"{caller.DeclaringType}.{caller.Name}");
        }

        public void Reload() => ChangeTracker.Entries()
            .Where(e => e.Entity != null).ToList()
            .ForEach(e => e.State = EntityState.Detached);

        private void TagEntries(string source, string user)
        {
            var e = ChangeTracker.Entries().ToList();

            var entries = ChangeTracker.Entries().Where(e => 
                e.Entity is IEntity && 
                (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            );

            Logger.LogInformation($"ChangeTracker Contains {entries.Count()}/{ChangeTracker.Entries().Count()} in an Added or Modified state.");

            foreach(var entry in entries)
            {
                Logger.LogDebug($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                var utcNow = DateTime.UtcNow;

                if (entry.State == EntityState.Added && entry.Entity is ICreatable creatable)
                {
                    creatable.Created = utcNow;
                    creatable.CreatedBy = user;
                    creatable.CreatedSource = source;
                }

                if (entry.Entity is IModifiable modifiable)
                {
                    modifiable.Modified = utcNow;
                    modifiable.ModifiedBy = user;
                    modifiable.ModifiedSource = source;
                }

                if (entry.Entity is IDeletable deletable)
                {
                    if(entry.State == EntityState.Deleted)
                    {
                        // Implementing IDeletable implies soft deletes required
                        entry.State = EntityState.Modified;

                        deletable.Deleted = utcNow;
                        deletable.DeletedBy = user;
                        deletable.DeletedSource = source;
                    } 
                    else if(deletable.Deleted != null || deletable.DeletedBy != null || deletable.DeletedSource != null)
                        // if any of the deleted values are not null then 
                    {
                        entry.State = EntityState.Modified;
                        // If state is not Deleted then ensure the IDeletable properties need to be nulled out
                        deletable.Deleted = null;
                        deletable.DeletedBy = null;
                        deletable.DeletedSource = null;
                    }
                    
                }
            }
        }
    }
}
