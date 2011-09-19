using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Arithmetic
{
    /// <summary>
    ///     An arithmetic interface for int
    /// </summary>
    public struct Int32Arithmetic : IArithmetic<int>
    {
        #region IArithmetic<int> Members
        /// <summary>
        /// Adds the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Add(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// Substracts the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Substract(int x, int y)
        {
            return x - y;
        }

        /// <summary>
        /// Multiplies the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Multiply(int x, int y)
        {
            return x * y;
        }

        /// <summary>
        /// Divides the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Divide(int x, int y)
        {
            return x / y;
        }

        /// <summary>
        /// Powers the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Power(int x, int y)
        {
            return (int)Math.Pow(x, y);
        }

        /// <summary>
        /// Moduloes the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Modulo(int x, int y)
        {
            return x % y;
        }

        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterThan(int x, int y)
        {
            return x > y;
        }

        /// <summary>
        /// Greaters the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterOrEqualTo(int x, int y)
        {
            return x >= y;
        }

        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessThan(int x, int y)
        {
            return x < y;
        }

        /// <summary>
        /// Lesses the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessOrEqualTo(int x, int y)
        {
            return x <= y;
        }

        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(int x, int y)
        {
            return x == y;
        }

        /// <summary>
        /// Finds the maximum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Max(int x, int y)
        {
            return Math.Max(x, y);
        }

        /// <summary>
        /// Finds the minimum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public int Min(int x, int y)
        {
            return Math.Min(x, y);
        }

        /// <summary>
        /// Returns the absolute value of the number
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public int Abs(int x)
        {
            return Math.Abs(x);
        }

        /// <summary>
        /// Gets the zero value.
        /// </summary>
        public int Zero { get { return 0; } }
        #endregion
    }
}
