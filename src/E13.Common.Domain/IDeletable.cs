﻿using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Domain
{
    public interface IDeletable<T> : IEntity
    {
        T? DeletedBy { get; set; }
        string? DeletedSource { get; set; }
        DateTime? Deleted { get; set; }

        bool IsDeleted() => Deleted != null;
    }
}
