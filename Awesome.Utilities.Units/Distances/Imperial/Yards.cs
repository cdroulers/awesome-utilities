using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Imperial
{
    /// <summary>
    ///     Yards unit
    /// </summary>
    public class Yards : ImperialDistance
    {
        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation
        {
            get { return "yd"; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Yards"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Yards(decimal value)
            : base(value)
        {
        }
    }
}
