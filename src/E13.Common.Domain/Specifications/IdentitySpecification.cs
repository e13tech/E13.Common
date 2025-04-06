using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E13.Common.Domain.Specifications
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T?, bool>> ToExpression()
        {
            return x => true;
        }
    }
}
