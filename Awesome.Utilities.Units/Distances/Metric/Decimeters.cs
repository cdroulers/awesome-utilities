using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Metric
{
    /// <summary>
    ///     Decimeters
    /// </summary>
    public class Decimeters : MetricDistance
    {
        /// <summary>
        ///     Factor from meters.
        /// </summary>
        public const int Factor = -1;

        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public override string Abbreviation { get { return "dm"; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decimeters"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Decimeters(decimal value)
            : base(value)
        {
        }
    }
}
