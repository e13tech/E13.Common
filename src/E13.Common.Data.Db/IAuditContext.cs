using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db
{
    public interface IAuditContext<T>
    {
        T AuditUser { get; }
        string? Source { get; }
    }
}
