using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
    /// <summary>
    ///     A list of items for a current page
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ResultPage<T> : ReadOnlyCollection<T>, IPageable
    {
        [DataMember(Name = "CurrentPage")]
        private int currentPage;
        /// <summary>
        ///     Gets the current page.
        /// </summary>
        public int CurrentPage { get { return this.currentPage; } }

        [DataMember(Name = "PerPage")]
        private int perPage;
        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int PerPage { get { return this.perPage; } }

        [DataMember(Name = "TotalNumberOfRecords")]
        private int totalNumberOfRecords;
        /// <summary>
        ///     Gets the total number of records.
        /// </summary>
        public int TotalNumberOfRecords { get { return this.totalNumberOfRecords; } }

        [DataMember(Name = "LastPage")]
        private int lastPage;
        /// <summary>
        ///     Gets the last page.
        /// </summary>
        public int LastPage { get { return this.lastPage; } }

        /// <summary>
        ///     The first page in a paged list.
        /// </summary>
        public const int ValueOfFirstPage = 1;

        [DataMember(Name = "NextPage")]
        private int? nextPage;
        /// <summary>
        /// Gets the next page, if there is one.
        /// </summary>
        public int? NextPage { get { return this.nextPage; } }

        [DataMember(Name = "PreviousPage")]
        private int? previousPage;
        /// <summary>
        /// Gets the previous page, if there is one.
        /// </summary>
        public int? PreviousPage { get { return this.previousPage; } }

        /// <summary>
        /// Gets an empty result page.
        /// </summary>
        public static ResultPage<T> Empty { get { return new ResultPage<T>(new List<T>(), 1, 1, 0); } }

        /// <summary>
        /// Gets how the current result page is ordered.
        /// </summary>
        [DataMember]
        public OrderParameter[] OrderedBy { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultPage&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="perPage">The per page.</param>
        /// <param name="totalNumberOfRecords">The total number of records.</param>
        /// <param name="orderedBy">The ordered by.</param>
        public ResultPage(IList<T> items, int currentPage, int perPage, int totalNumberOfRecords, OrderParameter[] orderedBy =  null)
            : base(items)
        {
            this.currentPage = currentPage;
            this.perPage = perPage;
            this.totalNumberOfRecords = totalNumberOfRecords;
            this.OrderedBy = orderedBy ?? new OrderParameter[0];

            this.lastPage = (int)Math.Ceiling((totalNumberOfRecords * 1.0d / perPage));
            this.nextPage = currentPage < this.lastPage ? currentPage + 1 : new int?();
            this.previousPage = currentPage > 1 ? currentPage - 1 : new int?();
        }

        /// <summary>
        /// Casts this instance to another type
        /// </summary>
        /// <typeparam name="TCast">The type of the cast.</typeparam>
        /// <returns></returns>
        public ResultPage<TCast> Cast<TCast>()
        {
            return new ResultPage<TCast>(this.Items.Cast<TCast>().ToList(), this.CurrentPage, this.PerPage, this.TotalNumberOfRecords);
        }
    }
}
