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
    public sealed class ModifiableInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData data,
            InterceptionResult<int> result)
        {
            var auditContext = data.Context as IAuditContext ?? throw new Exception("Audit context is not set.");

            foreach (var entry in data.Context!.ChangeTracker.Entries<IModifiable>()
                         .Where(e => e.State is EntityState.Added or EntityState.Modified))
            {
                entry.Entity.Modified = DateTime.UtcNow;
                entry.Entity.ModifiedBy = auditContext.AuditUser;
                entry.Entity.ModifiedSource = auditContext.Source;
            }
            return base.SavingChanges(data, result);
        }
    }
}
