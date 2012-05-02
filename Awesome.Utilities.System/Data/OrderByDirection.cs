using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data
{
    /// <summary>
    ///     A Generic order by enumeration.
    /// </summary>
    [DataContract]
    public enum OrderByDirection
    {
        /// <summary>
        ///     Ascending order
        /// </summary>
        [EnumMember]
        Ascending,
        /// <summary>
        ///     Descending order
        /// </summary>
        [EnumMember]
        Descending,
    }
}
