using E13.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Interceptors
{
    public sealed class CreatableInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData data,
            InterceptionResult<int> result)
        {
            var auditContext = data.Context as IAuditContext ?? throw new Exception("Audit context is not set.");

            foreach (var entry in data.Context!.ChangeTracker.Entries<ICreatable>()
                         .Where(e => e.State == EntityState.Added))
            {
                entry.Entity.Created = DateTime.UtcNow;
                entry.Entity.CreatedBy = auditContext.AuditUser;
                entry.Entity.CreatedSource = auditContext.Source;
            }
            return base.SavingChanges(data, result);
        }
    }
}
