using E13.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E13.Common.Data.Db.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Concept taken from https://stackoverflow.com/questions/47673524/ef-core-soft-delete-with-shadow-properties-and-query-filters/48744644#48744644
        /// and adopted
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="filterExpression"></param>
        public static void HasQueryFiltersFor<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> filterExpression)
        {
            var iDeletableType = typeof(TInterface);
            var implementationsOfIDeletable = modelBuilder.Model.GetEntityTypes()
                .Where(entityType => iDeletableType.IsAssignableFrom(entityType.ClrType))
                .ToList();

            /**
             * Per the article below, reminder that an Entity Type can only have a single HasQueryFilter(...) 
             * applied so if an implementing class adds another for the same Type only the last one will
             * be in effect
             * 
             * https://learn.microsoft.com/en-us/ef/core/querying/filters
             */
            foreach (var implementation in implementationsOfIDeletable)
            {
                modelBuilder
                    .Entity(implementation.ClrType)
                    .HasQueryFilter(ConvertFilterExpression(filterExpression, implementation.ClrType));
            }
        }

        private static LambdaExpression ConvertFilterExpression<TInterface>(
                            Expression<Func<TInterface, bool>> filterExpression,
                            Type entityType)
        {
            var newParam = Expression.Parameter(entityType);
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

            return Expression.Lambda(newBody, newParam);
        }
    }
}
