using E13.Common.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E13.Common.Data.Db.Interceptors
{
    public sealed class ModifiableInterceptor : SaveChangesInterceptor
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
            foreach (var entry in eventData.Context!.ChangeTracker.Entries<IModifiable>()
                         .Where(e => e.State is EntityState.Added or EntityState.Modified))
            {
                entry.Entity.Modified = DateTime.UtcNow;
                entry.Entity.ModifiedBy = auditContext.AuditUser;
                entry.Entity.ModifiedSource = auditContext.Source;
            }
        }
    }
}
