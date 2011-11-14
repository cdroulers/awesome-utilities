using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Component of an address
    /// </summary>
    [DebuggerDisplay("AddressInformationComponent {ToString()}")]
    public struct AddressInformationComponent
    {
        /// <summary>
        ///     The long name
        /// </summary>
        public readonly string LongName;
        /// <summary>
        ///     The short name
        /// </summary>
        public readonly string ShortName;
        /// <summary>
        ///     The types
        /// </summary>
        public readonly string[] Types;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressInformationComponent"/> struct.
        /// </summary>
        /// <param name="longName">The long name.</param>
        /// <param name="shortName">The short name.</param>
        /// <param name="types">The types.</param>
        public AddressInformationComponent(string longName, string shortName, string[] types)
        {
            this.LongName = longName;
            this.ShortName = shortName;
            this.Types = types;
        }

        public override string ToString()
        {
            return string.Format("LongName: {0}, ShortName: {1}, Types: {2}", LongName, ShortName, string.Join(", ", Types));
        }

        /// <summary>
        /// REturns true if both are equal
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(AddressInformationComponent other)
        {
            return Equals(other.LongName, LongName) && Equals(other.ShortName, ShortName) && other.Types.SequenceEqual(this.Types);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(AddressInformationComponent)) return false;
            return Equals((AddressInformationComponent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (LongName != null ? LongName.GetHashCode() : 0);
                result = (result * 397) ^ (ShortName != null ? ShortName.GetHashCode() : 0);
                result = (result * 397) ^ (Types != null ? Types.GetHashCode() : 0);
                return result;
            }
        }
    }
}
