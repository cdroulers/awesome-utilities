using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Units.Distances.Metric;

namespace System.Units.Distances.Imperial
{
    /// <summary>
    ///     Represents an Imperial system distance.
    /// </summary>
    public abstract class ImperialDistance : Distance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImperialDistance"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected ImperialDistance(decimal value)
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

            if (typeof(ImperialDistance).IsAssignableFrom(type))
            {
                if (!ConversionFactors.Values.ContainsKey(this.GetType()))
                {
                    throw new NotSupportedException(string.Format(Properties.Strings.ImperialDistance_TypeConversionNotSupported, this.ToString(), type.Name));
                }
                if (!ConversionFactors.Values.ContainsKey(type))
                {
                    throw new NotSupportedException(string.Format(Properties.Strings.ImperialDistance_TypeConversionNotSupported, this.ToString(), type.Name));
                }
                return (Distance)Activator.CreateInstance(type, this.Value * ConversionFactors.Values[this.GetType()][type]);
            }
            else
            {
                return Distance.Build<Meters>(this.ConvertTo<Yards>().Value / MetricDistance.MeterToYardRatio).ConvertTo(type);
            }
        }
    }
}
