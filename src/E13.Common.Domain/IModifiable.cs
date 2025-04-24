using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Domain
{
    public interface IModifiable<T> : IEntity
    {
        T ModifiedBy { get; set; }
        string? ModifiedSource { get; set; }
        DateTime? Modified { get; set; }
    }
}
