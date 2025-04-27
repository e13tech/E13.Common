using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Domain
{
    public interface IOwnable<T> : IEntity
    {
        T? OwnedBy { get; set; }
        string? OwnedSource { get; set; }
        DateTime? Owned { get; set; }
    }
}
