using E13.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Tests.Sample
{
    public class TestCreatable : ICreatable
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public string? CreatedBy { get; set; } = "CreatedBy";
        public string? CreatedSource { get; set; } = "CreatedSource";
        public DateTime? Created { get; set; }
    }
}
