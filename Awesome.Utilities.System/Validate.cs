using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     Helper to throw exceptions on arguments.
    /// </summary>
    public class Validate : IValidate
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Validate"/> class.
        ///     protected so we can't initialize the class
        /// </summary>
        private Validate()
        {
        }

        /// <summary>
        ///     Default instance of validate.
        /// </summary>
        public static readonly Validate Is = new Validate();

        private NotValidate not;

        /// <summary>
        /// Negates the current validation hypothesis.
        /// </summary>
        public IValidate Not { get { return this.not ?? (this.not = new NotValidate(this)); } }

        /// <summary>
        /// Validates thats the value supplied is not null.
        /// </summary>
        /// <param name="toValidate">the value</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public void Null<T>(T toValidate, string name)
             where T : class
        {
            if (toValidate != null)
            {
                throw new ArgumentNotNullException(name);
            }
        }

        /// <summary>
        /// Validates thats the string supplied is not null or empty.
        /// </summary>
        /// <param name="toValidate">the string</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentNullOrEmptyException"></exception>
        public void NullOrEmpty(string toValidate, string name)
        {
            if (!string.IsNullOrEmpty(toValidate))
            {
                throw new StringArgumentNotNullOrEmptyException(name, Properties.Strings.Validate_NotNullOrEmpty);
            }
        }

        /// <summary>
        /// Validates thats the string supplied is not null or white space.
        /// </summary>
        /// <param name="toValidate">the string</param>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentNullOrWhiteSpaceException"></exception>
        public void NullOrWhiteSpace(string toValidate, string name)
        {
            if (!string.IsNullOrWhiteSpace(toValidate))
            {
                throw new StringArgumentNotNullOrWhiteSpaceException(name, Properties.Strings.Validate_NotNullOrWhitespace);
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

        /// <summary>
        /// Checks whether "toValidate" is contained in the "array"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toValidate">To validate.</param>
        /// <param name="array">The array.</param>
        /// <param name="name">The name.</param>
        public void ContainedIn<T>(T toValidate, IEnumerable<T> array, string name)
        {
            if (!array.Contains(toValidate))
            {
                throw new ArgumentException(name, string.Format(Properties.Strings.Validate_ContainedIn, toValidate, string.Join(", ", array.Select(a => a.ToString()))));
            }
        }

        private class NotValidate : IValidate
        {
            private readonly Validate from;

            public NotValidate(Validate from)
            {
                this.from = from;
            }

            public IValidate Not { get { return this.from; } }

            public void Null<T>(T toValidate, string name)
                 where T : class
            {
                if (toValidate == null)
                {
                    throw new ArgumentNullException(name);
                }
            }

            public void NullOrEmpty(string toValidate, string name)
            {
                if (string.IsNullOrEmpty(toValidate))
                {
                    throw new StringArgumentNullOrEmptyException(name, Properties.Strings.Validate_NullOrEmpty);
                }
            }

            public void NullOrWhiteSpace(string toValidate, string name)
            {
                if (string.IsNullOrWhiteSpace(toValidate))
                {
                    throw new StringArgumentNullOrWhiteSpaceException(name, Properties.Strings.Validate_NullOrWhitespace);
                }
            }

            public void EqualTo<T>(T toValidate, T toCompare, string name)
            {
                if (object.Equals(toValidate, toCompare))
                {
                    throw new ArgumentException(string.Format(Properties.Strings.Validate_NotEqualTo, toValidate, toCompare), name);
                }
            }

            public void HigherThan<T>(T toValidate, T toCompare, string name)
                where T : IComparable
            {
                if (!(toValidate.CompareTo(toCompare) <= 0))
                {
                    throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_NotHigherThan, toCompare));
                }
            }

            public void HigherThanOrEqualTo<T>(T toValidate, T toCompare, string name)
                where T : IComparable
            {
                if (!(toValidate.CompareTo(toCompare) < 0))
                {
                    throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_NotHigherThanOrEqualTo, toCompare));
                }
            }

            public void LowerThan<T>(T toValidate, T toCompare, string name)
                where T : IComparable
            {
                if (!(toValidate.CompareTo(toCompare) >= 0))
                {
                    throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_NotLowerThan, toCompare));
                }
            }

            public void LowerThanOrEqualTo<T>(T toValidate, T toCompare, string name)
                where T : IComparable
            {
                if (!(toValidate.CompareTo(toCompare) > 0))
                {
                    throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_NotLowerThanOrEqualTo, toCompare));
                }
            }

            public void Between<T>(T toValidate, T lowerLimit, T higherLimit, string name, bool inclusive = true)
                where T : IComparable
            {
                if (inclusive)
                {
                    if (!(toValidate.CompareTo(lowerLimit) < 0 || toValidate.CompareTo(higherLimit) > 0))
                    {
                        throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_BetweenInclusive, lowerLimit, higherLimit));
                    }
                }
                else
                {
                    if (!(toValidate.CompareTo(lowerLimit) <= 0 || toValidate.CompareTo(higherLimit) >= 0))
                    {
                        throw new ArgumentOutOfRangeException(name, string.Format(Properties.Strings.Validate_Between, lowerLimit, higherLimit));
                    }
                }
            }

            public void ContainedIn<T>(T toValidate, IEnumerable<T> array, string name)
            {
                if (array.Contains(toValidate))
                {
                    throw new ArgumentException(name, string.Format(Properties.Strings.Validate_NotContainedIn, toValidate, string.Join(", ", array.Select(a => a.ToString()))));
                }
            }
        }
    }
}
