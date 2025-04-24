using E13.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestModifiable : IModifiable<string>
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public string? ModifiedBy { get; set; }
        public string? ModifiedSource { get; set; }
        public DateTime? Modified { get; set; }
    }
}
