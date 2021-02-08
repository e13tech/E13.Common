using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Infra
{
    [Flags]
    public enum Persistence
    {
        None = 0,
        AzureSql = 1,
        AzureCosmos = 2
    }
}
