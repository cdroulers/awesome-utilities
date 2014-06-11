using System.Data;

namespace System.Collections.Generic
{
    /// <summary>
    ///     An interface for paging
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        ///     Gets the current page.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        int PerPage { get; }

        /// <summary>
        ///     Gets the total number of records.
        /// </summary>
        int TotalNumberOfRecords { get; }

        /// <summary>
        ///     Gets the last page.
        /// </summary>
        int LastPage { get; }

        /// <summary>
        /// Gets the next page, if there is one.
        /// </summary>
        int? NextPage { get; }

        /// <summary>
        /// Gets the previous page, if there is one.
        /// </summary>
        int? PreviousPage { get; }

        /// <summary>
        /// Gets how the current <see cref="IPageable"/> is ordered.
        /// </summary>
        OrderParameter[] OrderedBy { get; }
    }
}