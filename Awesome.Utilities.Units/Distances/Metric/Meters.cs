using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Metric
{
    /// <summary>
    ///     Meters
    /// </summary>
    public class Meters : MetricDistance
    {
        /// <summary>
        ///     Factor from meters.
        /// </summary>
        public const int Factor = 0;

        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "m"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meters"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Meters(decimal value)
            : base(value)
        {
        }
    }
}
