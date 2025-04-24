using E13.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestOwnable : IOwnable<string>
    {
        public Guid Id { get; set; }
        public string OwnedBy { get; set; } = "OwnedBy";
        public string OwnedSource { get; set; } = "OwnedSource";
        public DateTime Owned { get; set; }
    }
}
