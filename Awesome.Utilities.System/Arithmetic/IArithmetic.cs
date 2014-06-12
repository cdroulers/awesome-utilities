using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Arithmetic
{
    /// <summary>
    /// An interface for numbers
    /// </summary>
    /// <typeparam name="T">The type of arithmetic.</typeparam>
    public interface IArithmetic<T>
    {
        /// <summary>
        /// Adds the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The added value.</returns>
        T Add(T x, T y);

        /// <summary>
        /// Substracts the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The substracted value.</returns>
        T Substract(T x, T y);

        /// <summary>
        /// Multiplies the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The multiplied value.</returns>
        T Multiply(T x, T y);

        /// <summary>
        /// Divides the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The divided value.</returns>
        T Divide(T x, T y);

        /// <summary>
        /// Powers the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The value powered.</returns>
        T Power(T x, T y);

        /// <summary>
        /// Modulo the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The value with modulo applied.</returns>
        T Modulo(T x, T y);

        /// <summary>
        /// Greater the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>True if greater than.</returns>
        bool GreaterThan(T x, T y);

        /// <summary>
        /// Greater the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>True if greater or equal to.</returns>
        bool GreaterOrEqualTo(T x, T y);

        /// <summary>
        /// Less the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>True if less than.</returns>
        bool LessThan(T x, T y);

        /// <summary>
        /// Less than or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>True if less or equal to.</returns>
        bool LessOrEqualTo(T x, T y);

        /// <summary>
        /// Returns true if the values are equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>True if equal to.</returns>
        bool Equals(T x, T y);

        /// <summary>
        /// Finds the maximum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The max between the two.</returns>
        T Max(T x, T y);

        /// <summary>
        /// Finds the minimum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The min between the two.</returns>
        T Min(T x, T y);

        /// <summary>
        /// Returns the absolute value of the number
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The absolute value.</returns>
        T Abs(T x);

        /// <summary>
        /// Gets the zero value for the Arithmetic
        /// </summary>
        /// <value>The zero.</value>
        T Zero { get; }
    }
}
