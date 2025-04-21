using E13.Common.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData data,
            InterceptionResult<int> result)
        {
            var auditContext = data.Context as IAuditContext ?? throw new Exception("Audit context is not set.");

            foreach (var entry in data.Context!.ChangeTracker.Entries<IDeletable>())
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
                    entry.Entity.DeletedBy = null;
                    entry.Entity.DeletedSource = null;
                }
            }
            return base.SavingChanges(data, result);
        }
    }

}
