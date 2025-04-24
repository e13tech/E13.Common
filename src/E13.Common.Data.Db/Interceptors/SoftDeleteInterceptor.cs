using E13.Common.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace E13.Common.Data.Db.Interceptors
{
    public sealed class SoftDeleteInterceptor<T> : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var auditContext = eventData.Context as IAuditContext<T> ?? throw new Exception("Audit context is not set.");

            HandleEventData(eventData, auditContext);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var auditContext = eventData.Context as IAuditContext<T> ?? throw new Exception("Audit context is not set.");

            HandleEventData(eventData, auditContext);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Handles the event data for the creatable entities.
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="auditContext"></param>
        private static void HandleEventData(DbContextEventData eventData, IAuditContext<T> auditContext)
        {
            foreach (var entry in eventData.Context!.ChangeTracker.Entries<IDeletable<T>>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = DateTime.UtcNow;
                    entry.Entity.DeletedBy = auditContext.AuditUser;
                    entry.Entity.DeletedSource = auditContext.Source;
                }
                else if (entry.State == EntityState.Modified &&
                         (entry.Entity.Deleted != null ||
                          entry.Entity.DeletedBy != null ||
                          entry.Entity.DeletedSource != null))
                {
                    // “undelete” scenario
                    entry.Entity.Deleted = null;
                    entry.Entity.DeletedBy = default(T);
                    entry.Entity.DeletedSource = null;
                }
            }
        }
    }
}
