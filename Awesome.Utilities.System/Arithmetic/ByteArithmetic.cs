using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Arithmetic
{
    /// <summary>
    ///     Arithmetic for byte
    /// </summary>
    public class ByteArithmetic : IArithmetic<byte>
    {
        /// <summary>
        /// Adds the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Add(byte x, byte y)
        {
            return (byte)((ushort)x + (ushort)y);
        }

        /// <summary>
        /// Substracts the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Substract(byte x, byte y)
        {
            return (byte)((ushort)x - (ushort)y);
        }

        /// <summary>
        /// Multiplies the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Multiply(byte x, byte y)
        {
            return (byte)((ushort)x * (ushort)y);
        }

        /// <summary>
        /// Divides the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Divide(byte x, byte y)
        {
            return (byte)((ushort)x / (ushort)y);
        }

        /// <summary>
        /// Powers the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Power(byte x, byte y)
        {
            return (byte)Math.Pow(x, y);
        }

        /// <summary>
        /// Moduloes the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Modulo(byte x, byte y)
        {
            return (byte)((ushort)x % (ushort)y);
        }

        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterThan(byte x, byte y)
        {
            return x > y;
        }

        /// <summary>
        /// Greaters the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool GreaterOrEqualTo(byte x, byte y)
        {
            return x >= y;
        }

        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessThan(byte x, byte y)
        {
            return x < y;
        }

        /// <summary>
        /// Lesses the or equal to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool LessOrEqualTo(byte x, byte y)
        {
            return x <= y;
        }

        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(byte x, byte y)
        {
            return x == y;
        }

        /// <summary>
        /// Finds the maximum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Max(byte x, byte y)
        {
            return Math.Max(x, y);
        }

        /// <summary>
        /// Finds the minimum value between two numbers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public byte Min(byte x, byte y)
        {
            return Math.Min(x, y);
        }

        /// <summary>
        /// Returns the absolute value of the number
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public byte Abs(byte x)
        {
            return x;
        }

        /// <summary>
        /// Gets the zero value.
        /// </summary>
        public byte Zero { get { return 0; } }
    }
}
