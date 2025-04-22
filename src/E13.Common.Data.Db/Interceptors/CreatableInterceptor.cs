using E13.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Interceptors
{
    public sealed class CreatableInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var auditContext = eventData.Context as IAuditContext ?? throw new Exception("Audit context is not set.");

            HandleEventData(eventData, auditContext);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            var auditContext = eventData.Context as IAuditContext ?? throw new Exception("Audit context is not set.");

            HandleEventData(eventData, auditContext);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Handles the event data for the creatable entities.
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="auditContext"></param>
        private static void HandleEventData(DbContextEventData eventData, IAuditContext auditContext)
        {
            foreach (var entry in eventData.Context!.ChangeTracker.Entries<ICreatable>()
                         .Where(e => e.State == EntityState.Added))
            {
                entry.Entity.Created = DateTime.UtcNow;
                entry.Entity.CreatedBy = auditContext.AuditUser;
                entry.Entity.CreatedSource = auditContext.Source;
            }
        }
    }
}
