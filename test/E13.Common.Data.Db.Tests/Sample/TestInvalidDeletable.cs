using E13.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestInvalidDeletable : IDeletable
    {
        public Guid Id { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeletedSource { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
