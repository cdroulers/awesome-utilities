using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Units.Distances.Imperial
{
    /// <summary>
    ///     Factors for distance conversions
    /// </summary>
    internal static class ConversionFactors
    {
        public static readonly IDictionary<Type, IDictionary<Type, decimal>> Values = new Dictionary<Type, IDictionary<Type, decimal>>()
        {
            {
                typeof(Miles), new Dictionary<Type, decimal>()
                {
                    { typeof(Yards), 1760M },
                    { typeof(Feet), 5280M },
                    { typeof(Inches), 63360M },
                }
            },
            {
                typeof(Yards), new Dictionary<Type, decimal>()
                {
                    { typeof(Miles), 1M / 1760M },
                    { typeof(Feet), 3M },
                    { typeof(Inches), 36M },
                }
            },
            {
                typeof(Feet), new Dictionary<Type, decimal>()
                {
                    { typeof(Miles), 1M / 5820M },
                    { typeof(Yards), 1M / 3M },
                    { typeof(Inches), 12M },
                }
            },
            {
                typeof(Inches), new Dictionary<Type, decimal>()
                {
                    { typeof(Miles), 1M / 63360M },
                    { typeof(Yards), 1M / 36M },
                    { typeof(Feet), 1M / 12M },
                }
            },
        };
    }
}
