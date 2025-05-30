﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Domain
{
    public interface ICreatable<T> : IEntity
    {
        T? CreatedBy { get; set; }
        string? CreatedSource { get; set; }
        DateTime? Created { get; set; }
    }
}
