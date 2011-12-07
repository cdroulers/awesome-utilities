using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Units.Distances.Imperial;

namespace System.Units.Distances.Metric
{
    /// <summary>
    ///     Represents a metric distance.
    /// </summary>
    public abstract class MetricDistance : Distance
    {
        /// <summary>
        ///     How many yards in a meter.
        /// </summary>
        public const decimal MeterToYardRatio = 1.093613298M;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricDistance"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected MetricDistance(decimal value)
            : base(value)
        {
        }

        /// <summary>
        /// Converts the current distance to a distance of another type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public override Distance ConvertTo(Type type)
        {
            if (this.GetType() == type)
            {
                return Distance.Build(type, this.Value);
            }

            if (typeof(MetricDistance).IsAssignableFrom(type))
            {
                int first = (int)this.GetType().GetField("Factor").GetValue(null);
                int second = (int)type.GetField("Factor").GetValue(null);

                int factor = Math.Max(first, second) - Math.Min(first, second);
                if (second > first)
                {
                    factor *= -1;
                }

                return Distance.Build(type, this.Value * (decimal)Math.Pow(10, factor));
            }
            else
            {
                return Distance.Build<Yards>(this.ConvertTo<Meters>().Value * MetricDistance.MeterToYardRatio).ConvertTo(type);
            }
        }
    }
}
