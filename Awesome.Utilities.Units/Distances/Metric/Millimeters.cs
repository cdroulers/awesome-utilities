using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Metric
{
    /// <summary>
    ///     Millimiters
    /// </summary>
    public class Millimeters : MetricDistance
    {
        /// <summary>
        ///     Factor from meters.
        /// </summary>
        public const int Factor = -3;

        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "mm"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Millimeters"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Millimeters(decimal value)
            : base(value)
        {
        }
    }
}
