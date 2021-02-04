using E13.Common.Core;

namespace System.Linq
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedCollection{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedCollection{T}"/> interface.</returns>
        public static PagedCollection<T> ToPagedCollection<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0) 
            => new PagedCollection<T>(source, pageIndex, pageSize, indexFrom);
    }
}
