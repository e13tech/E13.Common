using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace E13.Common.Domain.Specifications
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> BitwiseAnd(Specification<T> specification)
        {
            // Optimization to avoid combining with All which is unnecessary overhead
            if (specification == All)
                return this;
            if (this == All)
                return specification;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> BitwiseOr(Specification<T> specification)
        {
            // Optimization to avoid combining with All which is unnecessary overhead
            if (specification == All)
                return this;
            if (this == All)
                return specification;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> LogicalNot() => new NotSpecification<T>(this);
        
        public Specification<T> All => new IdentitySpecification<T>();

        #region Operators
        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.BitwiseAnd(right);
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return left.BitwiseOr(right);
        }

        public static Specification<T> operator !(Specification<T> not)
        {
            if (not == null)
                throw new ArgumentNullException(nameof(not));

            return not.LogicalNot();
        }
        #endregion
    }
}
