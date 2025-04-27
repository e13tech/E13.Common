using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Domain
{
    public interface IExpirable<T> : IEntity
    {
        T? ExpirationBy { get; set; }
        string? ExpirationSource { get; set; }
        DateTime? Expiration { get; set; }
    }
}
