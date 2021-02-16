using E13.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestEffectable : IEffectable
    {
        public Guid Id { get; set; }
        public string EffectiveBy { get; set; }
        public string EffectiveSource { get; set; }
        public DateTime Effective { get; set; }
    }
}
