using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Units.Distances.Imperial;
using System.Units.Distances.Metric;

namespace System.Units.Distances
{
    /// <summary>
    ///     A distance in any unit.
    /// </summary>
    public abstract class Distance
    {
        /// <summary>
        ///     The value of this distance
        /// </summary>
        public readonly decimal Value;

        /// <summary>
        /// Gets the symbol representing the distance
        /// </summary>
        public abstract string Abbreviation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Distance"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected Distance(decimal value)
        {
            this.Value = value;
        }

        /// <summary>
        ///     Converts the current distance to a distance of another type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ConvertTo<T>() where T : Distance
        {
            return (T)this.ConvertTo(typeof(T));
        }

        /// <summary>
        ///     Converts the current distance to a distance of another type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public abstract Distance ConvertTo(Type type);

        /// <summary>
        ///     Builds an instance of a distance by the name of it (refers to class name).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Distance BuildByName(string name, decimal value)
        {
            var type = Type.GetType(typeof(MetricDistance).Namespace + "." + name) ?? Type.GetType(typeof(ImperialDistance).Namespace + "." + name);
            if (type == null)
            {
                throw new NotSupportedException(string.Format(Properties.Strings.Distance_NameXIsNotAValidDistanceType, name));
            }
            return Distance.Build(type, value);
        }

        /// <summary>
        /// Builds an instance of a distance with the Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T Build<T>(decimal value) where T : Distance
        {
            return (T)Distance.Build(typeof(T), value);
        }

        internal static Distance Build(Type type, decimal value)
        {
            Validate.Is.NotNull(type, "type");
            if (!typeof(Distance).IsAssignableFrom(type))
            {
                throw new NotSupportedException(string.Format(Properties.Strings.Distance_TypeXIsNotADistance, type));
            }
            return (Distance)Activator.CreateInstance(type, value);
        }
    }
}
