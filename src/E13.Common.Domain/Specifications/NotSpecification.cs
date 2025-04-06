using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace E13.Common.Domain.Specifications
{
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private Specification<T> Not { get; }

        public NotSpecification(Specification<T> not)
        {
            Not = not;
        }
        public override Expression<Func<T?, bool>> ToExpression()
        {
            var notExpression = Not.ToExpression();

            var unaryExpression = Expression.Not(notExpression.Body);

            return Expression.Lambda<Func<T?, bool>>(unaryExpression, notExpression.Parameters.Single());
        }
    }
}
