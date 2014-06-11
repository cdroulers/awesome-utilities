using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Arithmetic
{
    /// <summary>
    ///     A helper class to get Arithmetic classes
    /// </summary>
    public static class Arithmetics
    {
        /// <summary>
        /// Gets the instance of arithmetic for the specific type.
        /// </summary>
        /// <typeparam name="T">The type of arithmetic to get.</typeparam>
        /// <returns>An instance of <see cref="IArithmetic&lt;T&gt;"/> of the right type.</returns>
        public static IArithmetic<T> Get<T>()
        {
            return (IArithmetic<T>)Types[typeof(T)];
        }

        /// <summary>
        /// Gets the Byte arithmetic.
        /// </summary>
        /// <value>The Byte.</value>
        public static readonly IArithmetic<byte> Byte = new ByteArithmetic();

        /// <summary>
        /// Gets the Decimal arithmetic.
        /// </summary>
        /// <value>The Decimal.</value>
        public static readonly IArithmetic<decimal> Decimal = new DecimalArithmetic();

        /// <summary>
        /// Gets the Double arithmetic.
        /// </summary>
        /// <value>The Double.</value>
        public static readonly IArithmetic<double> Double = new DoubleArithmetic();

        /// <summary>
        /// Gets the Int16 arithmetic.
        /// </summary>
        /// <value>The Int16.</value>
        public static readonly IArithmetic<short> Int16 = new Int16Arithmetic();

        /// <summary>
        /// Gets the int32 arithmetic.
        /// </summary>
        /// <value>The int32.</value>
        public static readonly IArithmetic<int> Int32 = new Int32Arithmetic();

        /// <summary>
        /// Gets the Int64 arithmetic.
        /// </summary>
        /// <value>The Int64.</value>
        public static readonly IArithmetic<long> Int64 = new Int64Arithmetic();

        /// <summary>
        /// Gets the SByte arithmetic.
        /// </summary>
        /// <value>The SByte.</value>
        public static readonly IArithmetic<sbyte> SByte = new SByteArithmetic();

        /// <summary>
        /// Gets the Single arithmetic.
        /// </summary>
        /// <value>The Single.</value>
        public static readonly IArithmetic<float> Single = new SingleArithmetic();

        /// <summary>
        /// Gets the UInt16 arithmetic.
        /// </summary>
        /// <value>The UInt16.</value>
        public static readonly IArithmetic<ushort> UInt16 = new UInt16Arithmetic();

        /// <summary>
        /// Gets the UInt32 arithmetic.
        /// </summary>
        /// <value>The UInt32.</value>
        public static readonly IArithmetic<uint> UInt32 = new UInt32Arithmetic();

        /// <summary>
        /// Gets the UInt64 arithmetic.
        /// </summary>
        /// <value>The UInt64.</value>
        public static readonly IArithmetic<ulong> UInt64 = new UInt64Arithmetic();

        private static readonly Dictionary<Type, object> Types = new Dictionary<Type, object>()
        {
            { typeof(byte), Arithmetics.Byte },
            { typeof(decimal), Arithmetics.Decimal },
            { typeof(double), Arithmetics.Double },
            { typeof(short), Arithmetics.Int16 },
            { typeof(int), Arithmetics.Int32 },
            { typeof(long), Arithmetics.Int64 },
            { typeof(sbyte), Arithmetics.SByte },
            { typeof(float), Arithmetics.Single },
            { typeof(ushort), Arithmetics.UInt16 },
            { typeof(uint), Arithmetics.UInt32 },
            { typeof(ulong), Arithmetics.UInt64 },
        };
    }
}
