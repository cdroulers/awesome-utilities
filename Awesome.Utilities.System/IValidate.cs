using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     Validation interface
    /// </summary>
    public interface IValidate
    {
        /// <summary>
        /// Gets a negateion of the current validation hypothesis.
        /// </summary>
        IValidate Not { get; }

        /// <summary>
        /// Validates thats the value supplied is not null.
        /// </summary>
        /// <param name="toValidate">the value</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        void Null<T>(T toValidate, string name)
            where T : class;

        /// <summary>
        /// Validates thats the string supplied is not null or empty.
        /// </summary>
        /// <param name="toValidate">the string</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentNullOrEmptyException"></exception>
        void NullOrEmpty(string toValidate, string name);

        /// <summary>
        /// Validates thats the string supplied is not null or white space.
        /// </summary>
        /// <param name="toValidate">the string</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentNullOrWhiteSpaceException"></exception>
        void NullOrWhiteSpace(string toValidate, string name);

        /// <summary>
        /// Validates that the values are equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">To validate.</param>
        /// <param name="toCompare">To compare.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentException"></exception>
        void EqualTo<T>(T toValidate, T toCompare, string name);

        /// <summary>
        /// Validates that the value supplied is higher than the comparison value supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        void HigherThan<T>(T toValidate, T toCompare, string name)
            where T : IComparable;

        /// <summary>
        /// Validates that the value supplied is higher or equal to the comparison value supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        void HigherThanOrEqualTo<T>(T toValidate, T toCompare, string name)
            where T : IComparable;

        /// <summary>
        /// Validates that the value supplied is lower than the comparison supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        void LowerThan<T>(T toValidate, T toCompare, string name)
            where T : IComparable;

        /// <summary>
        /// Validates that the value supplied is lower or equal to the comparison supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        void LowerThanOrEqualTo<T>(T toValidate, T toCompare, string name)
            where T : IComparable;

        /// <summary>
        /// Betweens the specified to validate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">To validate.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="higherLimit">The higher limit.</param>
        /// <param name="name">The name.</param>
        /// <param name="inclusive">if set to <c>true</c> [inclusive].</param>
        /// <returns></returns>
        void Between<T>(T toValidate, T lowerLimit, T higherLimit, string name, bool inclusive = true)
            where T : IComparable;

        /// <summary>
        /// Checks whether "toValidate" is contained in the "array"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">To validate.</param>
        /// <param name="array">The array.</param>
        /// <param name="name">The name.</param>
        void ContainedIn<T>(T toValidate, IEnumerable<T> array, string name);
    }
}
