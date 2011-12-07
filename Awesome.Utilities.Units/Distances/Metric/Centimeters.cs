using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Metric
{
    /// <summary>
    ///     Centimeters!
    /// </summary>
    public class Centimeters : MetricDistance
    {
        /// <summary>
        ///     Factor from meters.
        /// </summary>
        public const int Factor = -2;

        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "cm"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Centimeters"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Centimeters(decimal value)
            : base(value)
        {
        }
    }
}
