using System;
using System.Arithmetic;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     A math class with more advanced methods than System.Math
    /// </summary>
    public static class AdvancedMath
    {
        #region GreatestCommonDivisor
        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static byte GreatestCommonDivisor(byte one, byte two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Byte);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static decimal GreatestCommonDivisor(decimal one, decimal two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Decimal);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static double GreatestCommonDivisor(double one, double two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Double);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static short GreatestCommonDivisor(short one, short two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Int16);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static int GreatestCommonDivisor(int one, int two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Int32);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static long GreatestCommonDivisor(long one, long two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Int64);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static sbyte GreatestCommonDivisor(sbyte one, sbyte two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.SByte);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static float GreatestCommonDivisor(float one, float two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.Single);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static ushort GreatestCommonDivisor(ushort one, ushort two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.UInt16);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static uint GreatestCommonDivisor(uint one, uint two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.UInt32);
        }

        /// <summary>
        /// Finds the greatest common divisor of two numbers.
        /// The greatest common divisor is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The greatest common divisor of the two numbers.</returns>
        public static ulong GreatestCommonDivisor(ulong one, ulong two)
        {
            return AdvancedMath.GreatestCommonDivisor(one, two, Arithmetics.UInt64);
        }

        private static T GreatestCommonDivisor<T>(T one, T two, IArithmetic<T> arithmetic)
        {
            T big = arithmetic.Max(arithmetic.Abs(one), arithmetic.Abs(two));
            T small = arithmetic.Min(arithmetic.Abs(one), arithmetic.Abs(two));
            T remainder = arithmetic.Modulo(big, small);
            return arithmetic.Equals(remainder, arithmetic.Zero) ? small : AdvancedMath.GreatestCommonDivisor(small, remainder, arithmetic);
        }
        #endregion

        #region LeastCommonMultiple
        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static byte LeastCommonMultiple(byte one, byte two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Byte);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static decimal LeastCommonMultiple(decimal one, decimal two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Decimal);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static double LeastCommonMultiple(double one, double two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Double);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static short LeastCommonMultiple(short one, short two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Int16);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static int LeastCommonMultiple(int one, int two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Int32);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static long LeastCommonMultiple(long one, long two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Int64);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static sbyte LeastCommonMultiple(sbyte one, sbyte two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.SByte);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static float LeastCommonMultiple(float one, float two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.Single);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static ushort LeastCommonMultiple(ushort one, ushort two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.UInt16);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static uint LeastCommonMultiple(uint one, uint two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.UInt32);
        }

        /// <summary>
        /// Finds the least common multiple of two numbers.
        /// The least common multiple is the largest positive integer that divides the numbers without a remainder.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The least common multiple of the two numbers.</returns>
        public static ulong LeastCommonMultiple(ulong one, ulong two)
        {
            return AdvancedMath.LeastCommonMultiple(one, two, Arithmetics.UInt64);
        }

        private static T LeastCommonMultiple<T>(T one, T two, IArithmetic<T> arithmetic)
        {
            return arithmetic.Divide(arithmetic.Abs(arithmetic.Multiply(one, two)), AdvancedMath.GreatestCommonDivisor(one, two, arithmetic));
        }
        #endregion
    }
}
