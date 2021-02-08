using E13.Common.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E13.Common.Domain.Tests.Specifications
{
    /// <summary>
    /// Simple Specification that always returns true
    /// </summary>
    public class TrueSpecification : Specification<object>
    {
        public override Expression<Func<object, bool>> ToExpression()
        {
            return b => true;
        }
    }
    /// <summary>
    /// Simple Specification that always returns true
    /// </summary>
    public class FalseSpecification : Specification<object>
    {
        public override Expression<Func<object, bool>> ToExpression()
        {
            return b => false;
        }
    }
}
