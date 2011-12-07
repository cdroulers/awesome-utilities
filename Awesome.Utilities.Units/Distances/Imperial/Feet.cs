using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Imperial
{
    /// <summary>
    ///     Feet!
    /// </summary>
    public class Feet : ImperialDistance
    {
        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "ft"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feet"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Feet(decimal value)
            : base(value)
        {
        }
    }
}
