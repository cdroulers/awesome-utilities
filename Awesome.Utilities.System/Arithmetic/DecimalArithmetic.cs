using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Arithmetic
{
    /// <summary>
    ///     An arithmetic interface for decimal
    /// </summary>
    public struct DecimalArithmetic : IArithmetic<decimal>
    {
        #region IArithmetic<decimal> Members
        /// <summary>
        /// Adds the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Add(decimal x, decimal y)
        {
            return x + y;
        }

        /// <summary>
        /// Substracts the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Substract(decimal x, decimal y)
        {
            return x - y;
        }

        /// <summary>
        /// Multiplies the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Multiply(decimal x, decimal y)
        {
            return x * y;
        }

        /// <summary>
        /// Divides the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Divide(decimal x, decimal y)
        {
            return x / y;
        }

        /// <summary>
        /// Powers the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Power(decimal x, decimal y)
        {
            return (decimal)Math.Pow((double)x, (double)y);
        }

        /// <summary>
        /// Moduloes the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Modulo(decimal x, decimal y)
        {
            return x % y;
        }

        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterThan(decimal x, decimal y)
        {
            return x > y;
        }

        /// <summary>
        /// Greaters the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterOrEqualTo(decimal x, decimal y)
        {
            return x >= y;
        }

        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessThan(decimal x, decimal y)
        {
            return x < y;
        }

        /// <summary>
        /// Lesses the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessOrEqualTo(decimal x, decimal y)
        {
            return x <= y;
        }

        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(decimal x, decimal y)
        {
            return x == y;
        }

        /// <summary>
        /// Finds the maximum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Max(decimal x, decimal y)
        {
            return Math.Max(x, y);
        }

        /// <summary>
        /// Finds the minimum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public decimal Min(decimal x, decimal y)
        {
            return Math.Min(x, y);
        }

        /// <summary>
        /// Returns the absolute value of the number
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public decimal Abs(decimal x)
        {
            return Math.Abs(x);
        }

        /// <summary>
        /// Gets the zero value.
        /// </summary>
        public decimal Zero { get { return 0.0M; } }
        #endregion
    }
}
