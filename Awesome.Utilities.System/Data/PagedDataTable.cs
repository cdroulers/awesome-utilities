using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data
{
    /// <summary>
    ///     A data table that contains paging information
    /// </summary>
    public class PagedDataTable : DataTable, IPageable
    {
        /// <summary>
        /// Gets the current page.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Gets the per page.
        /// </summary>
        public int PerPage { get; private set; }

        /// <summary>
        /// Gets the total number of records.
        /// </summary>
        public int TotalNumberOfRecords { get; private set; }

        /// <summary>
        /// Gets the last page.
        /// </summary>
        public int LastPage
        {
            get { return (int)Math.Ceiling(this.TotalNumberOfRecords * 1.0d / this.PerPage); }
        }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        public int? NextPage
        {
            get { return this.CurrentPage < this.LastPage ? this.CurrentPage + 1 : new int?(); }
        }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        public int? PreviousPage
        {
            get { return this.CurrentPage > 1 ? this.CurrentPage - 1 : new int?(); }
        }

        /// <summary>
        /// Gets the items in the data table. i.e. The rows already cast as objects.
        /// </summary>
        public IEnumerable<DataColumn> AllColumns
        {
            get { return this.Columns.Cast<DataColumn>(); }
        }

        /// <summary>
        /// Gets the items in the data table. i.e. The rows already cast as objects.
        /// </summary>
        public IEnumerable<DataRow> Items
        {
            get { return this.Rows.Cast<DataRow>(); }
        }

        /// <summary>
        /// Gets how the current data table is ordered.
        /// </summary>
        public OrderParameter[] OrderedBy { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedDataTable"/> class.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="totalNumberOfRecords">The total number of records.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="tableNamespace">The table namespace.</param>
        /// <param name="orderedBy">The ordered by.</param>
        public PagedDataTable(int currentPage, int perPage, int totalNumberOfRecords, string tableName = null, string tableNamespace = null, OrderParameter[] orderedBy = null)
            : base(tableName, tableNamespace)
        {
            this.CurrentPage = currentPage;
            this.PerPage = perPage;
            this.TotalNumberOfRecords = totalNumberOfRecords;
            this.OrderedBy = orderedBy ?? new OrderParameter[0];
        }
    }
}
