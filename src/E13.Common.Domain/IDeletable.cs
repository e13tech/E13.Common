using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Domain
{
    public interface IDeletable : IEntity
    {
        string DeletedBy { get; set; }
        string DeletedSource { get; set; }
        DateTime? Deleted { get; set; }
        public bool IsDeleted()
        { return Deleted == null; }
    }
}
