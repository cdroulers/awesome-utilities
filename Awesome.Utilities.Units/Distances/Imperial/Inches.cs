using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Imperial
{
    /// <summary>
    ///     Inches unit
    /// </summary>
    public class Inches : ImperialDistance
    {
        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation
        {
            get { return "in"; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inches"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Inches(decimal value)
            : base(value)
        {
        }
    }
}
