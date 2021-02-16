using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Domain
{
    public interface IExpirable
    {
        string ExpirationBy { get; set; }
        string ExpirationSource { get; set; }
        DateTime Expiration { get; set; }
    }
}
