﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace E13.Common.Domain.Specifications
{
    internal sealed class OrSpecification<T> : Specification<T>
    {
        private Specification<T> Left { get; }
        private Specification<T> Right { get; }

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            Left = left;
            Right = right;
        }
        public override Expression<Func<T?, bool>> ToExpression()
        {
            var leftExpression = Left.ToExpression();
            var rightExpression = Right.ToExpression();

            var andExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T?, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
}
