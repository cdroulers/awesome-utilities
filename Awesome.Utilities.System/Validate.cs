using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     Helper to throw exceptions on arguments.
    /// </summary>
    public class Validate
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Validate"/> class.
        ///     protected so we can't initialize the class
        /// </summary>
        private Validate()
        {
        }

        public static readonly Validate Is = new Validate();

        /// <summary>
        /// Validates thats the value supplied is not null.
        /// </summary>
        /// <param name="toValidate">the value</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public void NotNull<T>(T toValidate, string name)
             where T : class
        {
            if (toValidate == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Validates thats the string supplied is not null or empty.
        /// </summary>
        /// <param name="toValidate">the string</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentNullOrEmptyException"></exception>
        public void NotNullOrEmpty(string toValidate, string name)
        {
            if (string.IsNullOrEmpty(toValidate))
            {
                throw new StringArgumentNullOrEmptyException(name, Properties.Strings.Validate_NullOrEmpty);
            }
        }

        /// <summary>
        /// Validates thats the string supplied is not null or white space.
        /// </summary>
        /// <param name="toValidate">the string</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentNullOrWhiteSpaceException"></exception>
        public void NotNullOrWhiteSpace(string toValidate, string name)
        {
            if (string.IsNullOrWhiteSpace(toValidate))
            {
                throw new StringArgumentNullOrWhiteSpaceException(name, Properties.Strings.Validate_NullOrWhitespace);
            }
        }

        /// <summary>
        /// Validates that the values are equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">To validate.</param>
        /// <param name="toCompare">To compare.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentException"></exception>
        public void EqualTo<T>(T toValidate, T toCompare, string name)
        {
            if (!object.Equals(toValidate, toCompare))
            {
                throw new ArgumentException(string.Format(Properties.Strings.Validate_EqualTo, toValidate, toCompare), name);
            }
        }

        /// <summary>
        /// Validates that the value supplied is higher than the comparison value supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public void HigherThan<T>(T toValidate, T toCompare, string name)
            where T : IComparable
        {
            if (toValidate.CompareTo(toCompare) <= 0)
            {
                throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_HigherThan, toCompare));
            }
        }

        /// <summary>
        /// Validates that the value supplied is higher or equal to the comparison value supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public void HigherThanOrEqualTo<T>(T toValidate, T toCompare, string name)
            where T : IComparable
        {
            if (toValidate.CompareTo(toCompare) < 0)
            {
                throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_HigherThanOrEqualTo, toCompare));
            }
        }

        /// <summary>
        /// Validates that the value supplied is lower than the comparison supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public void LowerThan<T>(T toValidate, T toCompare, string name)
            where T : IComparable
        {
            if (toValidate.CompareTo(toCompare) >= 0)
            {
                throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_LowerThan, toCompare));
            }
        }

        /// <summary>
        /// Validates that the value supplied is lower or equal to the comparison supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate"></param>
        /// <param name="toCompare"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public void LowerThanOrEqualTo<T>(T toValidate, T toCompare, string name)
            where T : IComparable
        {
            if (toValidate.CompareTo(toCompare) > 0)
            {
                throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_LowerThanOrEqualTo, toCompare));
            }
        }

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
        public void Between<T>(T toValidate, T lowerLimit, T higherLimit, string name, bool inclusive = true)
            where T : IComparable
        {
            if (inclusive)
            {
                if (toValidate.CompareTo(lowerLimit) < 0 || toValidate.CompareTo(higherLimit) > 0)
                {
                    throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_BetweenInclusive, lowerLimit, higherLimit));
                }
            }
            else
            {
                if (toValidate.CompareTo(lowerLimit) <= 0 || toValidate.CompareTo(higherLimit) >= 0)
                {
                    throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_Between, lowerLimit, higherLimit));
                }
            }
        }
    }
}
