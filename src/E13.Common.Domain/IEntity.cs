using System;

namespace E13.Common.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }

        string CreatedBy { get; set; }
        string CreatedSource { get; set; }
        DateTime Created { get; set; }

        string ModifiedBy { get; set; }
        string ModifiedSource { get; set; }
        DateTime Modified { get; set; }
    }
}
