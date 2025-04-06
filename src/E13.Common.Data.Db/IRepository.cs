using E13.Common.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace E13.Common.Data.Db
{
    /// <summary>
    /// Defines the interfaces for generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TContext, TEntity> 
        where TContext : DbContext
        where TEntity : class
    {
        /// <summary>

        /// <summary>
        /// Gets the <see cref="IPagedCollection{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="pageIndex">The index of page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>An <see cref="IPagedCollection{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        PagedCollection<TEntity> GetPagedList(Expression<Func<TEntity, bool>>? predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                         int pageIndex = 0,
                                         int pageSize = 20);

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <returns>An <see cref="IPagedCollection{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
        TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
        /// </summary>
        /// <param name="selector">The selector for projection.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <returns>An <see cref="IPagedCollection{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
        TResult? GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                           Expression<Func<TEntity, bool>>? predicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>>? predicate = null);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<TEntity> entities);
    }
}
