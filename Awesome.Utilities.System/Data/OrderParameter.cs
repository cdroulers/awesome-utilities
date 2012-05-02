using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data
{
    /// <summary>
    ///     An order parameter for anything that requires sorting.
    /// </summary>
    [DataContract]
    public class OrderParameter
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        [DataMember]
        public string PropertyName { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        [DataMember]
        public OrderByDirection Direction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderParameter"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="direction">The direction.</param>
        public OrderParameter(string propertyName, OrderByDirection direction)
        {
            this.PropertyName = propertyName;
            this.Direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderParameter"/> class.
        /// </summary>
        public OrderParameter()
        {
        }
    }
}
