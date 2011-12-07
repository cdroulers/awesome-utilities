using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Imperial
{
    /// <summary>
    ///     Miles!
    /// </summary>
    public class Miles : ImperialDistance
    {
        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "mi"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Miles"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Miles(decimal value)
            : base(value)
        {
        }
    }
}
