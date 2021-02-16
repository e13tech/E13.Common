using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Domain
{
    public interface IEffectable
    {
        string EffectiveBy { get; set; }
        string EffectiveSource { get; set; }
        DateTime Effective { get; set; }
    }
}
