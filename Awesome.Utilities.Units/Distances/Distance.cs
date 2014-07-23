using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Units.Distances.Imperial;
using System.Units.Distances.Metric;

namespace System.Units.Distances
{
    /// <summary>
    ///     A distance in any unit.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
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
        /// <typeparam name="T">The type of Distance to convert to.</typeparam>
        /// <returns>A converted Distance</returns>
        public T ConvertTo<T>() where T : Distance
        {
            return (T)this.ConvertTo(typeof(T));
        }

        /// <summary>
        ///     Converts the current distance to a distance of another type.
        /// </summary>
        /// <param name="type">The type to convert to.</param>
        /// <returns>A converted Distance</returns>
        public abstract Distance ConvertTo(Type type);

        /// <summary>
        ///     Builds an instance of a distance by the name of it (refers to class name).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>A distance built according to the name.</returns>
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
        /// <typeparam name="T">The type of Distance to build.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A distance built according to the passed type.</returns>
        public static T Build<T>(decimal value) where T : Distance
        {
            return (T)Distance.Build(typeof(T), value);
        }

        /// <summary>
        /// Builds an instance of a distance with the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>A built distance.</returns>
        /// <exception cref="System.NotSupportedException">If the passed type is not a distance.</exception>
        public static Distance Build(Type type, decimal value)
        {
            Validate.Is.Not.Null(type, "type");
            if (!typeof(Distance).IsAssignableFrom(type))
            {
                throw new NotSupportedException(string.Format(Properties.Strings.Distance_TypeXIsNotADistance, type));
            }

            return (Distance)Activator.CreateInstance(type, value);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Distance: {0}{1}", this.Value, this.Abbreviation);
        }

        /// <summary>
        /// Returns a 
        /// <see cref="System.String" /> that represents this instance.
        /// Any default format for decimal will work
        /// "U" will add the abbreviation of the unit.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string format)
        {
            return this.Value.ToString(format.Replace("U", this.Abbreviation));
        }

        /// <summary>
        /// Adds the two distances together
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>Both distances added together.</returns>
        public Distance Add(Distance other)
        {
            return Distance.Build(this.GetType(), this.Value + other.ConvertTo(this.GetType()).Value);
        }

        /// <summary>
        /// Substracts the two distances together
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>Both distances substracted from one another.</returns>
        public Distance Substract(Distance other)
        {
            return Distance.Build(this.GetType(), this.Value - other.ConvertTo(this.GetType()).Value);
        }

        /// <summary>
        /// Multiplies the distance by a factor
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <returns>The distance multiplied by the factor.</returns>
        public Distance Multiply(decimal factor)
        {
            return Distance.Build(this.GetType(), this.Value * factor);
        }

        /// <summary>
        /// Divides the distance by a factor
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <returns>The distance divided by the factor.</returns>
        public Distance Divide(decimal factor)
        {
            return Distance.Build(this.GetType(), this.Value / factor);
        }

        /// <summary>
        /// Returns a value telling whether this distance is greater than another.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if it greater, otherwise false.</returns>
        public bool GreaterThan(Distance other)
        {
            return this.Value > other.ConvertTo(this.GetType()).Value;
        }

        /// <summary>
        /// Returns a value telling whether this distance is greater or equal to another.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if it greater or equal, otherwise false.</returns>
        public bool GreaterOrEqualTo(Distance other)
        {
            return this.Value >= other.ConvertTo(this.GetType()).Value;
        }

        /// <summary>
        /// Returns a value telling whether this distance is lesser than another.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if it lesser, otherwise false.</returns>
        public bool LesserThan(Distance other)
        {
            return this.Value < other.ConvertTo(this.GetType()).Value;
        }

        /// <summary>
        /// Returns a value telling whether this distance is lesser or equal to another.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if it lesser or equal, otherwise false.</returns>
        public bool LesserOrEqualTo(Distance other)
        {
            return this.Value <= other.ConvertTo(this.GetType()).Value;
        }

        /// <summary>
        /// Returns whether this distance is equal to another.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// True if the distances are equal.
        /// </returns>
        public bool Equals(Distance other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Value == other.ConvertTo(this.GetType()).Value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (!(obj is Distance))
            {
                return false;
            }

            return Equals((Distance)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int result = this.Abbreviation != null ? this.Abbreviation.GetHashCode() : 0;
            result = (result * 397) ^ this.Value.GetHashCode();
            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="d2">The d2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Distance operator +(Distance d1, Distance d2)
        {
            return d1.Add(d2);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="d2">The d2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Distance operator -(Distance d1, Distance d2)
        {
            return d1.Substract(d2);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Distance operator *(Distance d1, decimal factor)
        {
            return d1.Multiply(factor);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Distance operator /(Distance d1, decimal factor)
        {
            return d1.Divide(factor);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="d2">The d2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(Distance d1, Distance d2)
        {
            return d1.LesserThan(d2);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="d2">The d2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(Distance d1, Distance d2)
        {
            return d1.GreaterThan(d2);
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="d2">The d2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(Distance d1, Distance d2)
        {
            return d1.LesserOrEqualTo(d2);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="d1">The d1.</param>
        /// <param name="d2">The d2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(Distance d1, Distance d2)
        {
            return d1.GreaterOrEqualTo(d2);
        }
    }
}
