using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E13.Common.Core
{
    /// <summary>
    /// Represents the default implementation of the <see cref="PagedCollection{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the data to page</typeparam>
    public class PagedCollection<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the index from.
        /// </summary>
        /// <value>The index from.</value>
        public int IndexFrom { get; set; }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        public int TotalPages
        {
            get => (int)Math.Ceiling(TotalCount / (double)PageSize);
        }
        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        public bool HasPreviousPage
        {
            get => PageIndex - IndexFrom > 0;
        }

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        public bool HasNextPage
        {
            get => PageIndex - IndexFrom + 1 < TotalPages;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollection{T}" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="indexFrom">The index from.</param>
        public PagedCollection(IQueryable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");

            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = source.Count();

            var items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

            this.AddRange(items);
        }

        /// <summary>
        /// internal modifier to restrict usage of the default constructor only to
        /// to projects that have been explicitly exposed to
        /// </summary>
        internal PagedCollection()
        {

        }
    }
}
