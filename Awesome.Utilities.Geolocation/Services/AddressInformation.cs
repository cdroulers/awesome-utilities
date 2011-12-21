using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Information about an address
    /// </summary>
    [DebuggerDisplay("AddressInformation {ToString()}")]
    public class AddressInformation
    {
        /// <summary>
        /// Gets the components of the address
        /// </summary>
        public AddressInformationComponent[] Components { get; private set; }

        /// <summary>
        /// Gets the coordinates of the address
        /// </summary>
        public Coordinates Coordinates { get; private set; }

        /// <summary>
        /// Gets the formatted address.
        /// </summary>
        public string FormattedAddress { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressInformation"/> class.
        /// </summary>
        /// <param name="components">The components.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="formattedAddress">The formatted address.</param>
        public AddressInformation(AddressInformationComponent[] components, Coordinates coordinates, string formattedAddress)
        {
            this.Components = components;
            this.Coordinates = coordinates;
            this.FormattedAddress = formattedAddress;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.FormattedAddress;
        }
    }
}
