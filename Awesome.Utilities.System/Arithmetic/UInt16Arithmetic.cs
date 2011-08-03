using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Arithmetic
{
    /// <summary>
    ///     An arithmetic interface for ushort
    /// </summary>
    public struct UInt16Arithmetic : IArithmetic<ushort>
    {
        #region IArithmetic<ushort> Members
        /// <summary>
        /// Adds the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Add(ushort x, ushort y)
        {
            return (ushort)(x + y);
        }

        /// <summary>
        /// Substracts the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Substract(ushort x, ushort y)
        {
            return (ushort)(x - y);
        }

        /// <summary>
        /// Multiplies the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Multiply(ushort x, ushort y)
        {
            return (ushort)(x * y);
        }

        /// <summary>
        /// Divides the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Divide(ushort x, ushort y)
        {
            return (ushort)(x / y);
        }

        /// <summary>
        /// Powers the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Power(ushort x, ushort y)
        {
            return (ushort)Math.Pow(x, y);
        }

        /// <summary>
        /// Moduloes the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Modulo(ushort x, ushort y)
        {
            return (ushort)(x % y);
        }

        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterThan(ushort x, ushort y)
        {
            return x > y;
        }

        /// <summary>
        /// Greaters the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterOrEqualTo(ushort x, ushort y)
        {
            return x >= y;
        }

        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessThan(ushort x, ushort y)
        {
            return x < y;
        }

        /// <summary>
        /// Lesses the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessOrEqualTo(ushort x, ushort y)
        {
            return x <= y;
        }

        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(ushort x, ushort y)
        {
            return x == y;
        }

        /// <summary>
        /// Finds the maximum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Max(ushort x, ushort y)
        {
            return Math.Max(x, y);
        }

        /// <summary>
        /// Finds the minimum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public ushort Min(ushort x, ushort y)
        {
            return Math.Min(x, y);
        }

        /// <summary>
        /// Returns the absolute value of the number
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public ushort Abs(ushort x)
        {
            return x;
        }

        /// <summary>
        /// Gets the zero value.
        /// </summary>
        public ushort Zero { get { return 0; } }
        #endregion
    }
}
