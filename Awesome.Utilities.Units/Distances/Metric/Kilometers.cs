using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Metric
{
    /// <summary>
    ///     Kilometers
    /// </summary>
    public class Kilometers : MetricDistance
    {
        /// <summary>
        ///     Factor from meters.
        /// </summary>
        public const int Factor = 3;

        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "km"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meters"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Kilometers(decimal value)
            : base(value)
        {
        }
    }
}
