using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     An interface for geolocation methods.
    /// </summary>
    public interface IGeolocationService
    {
        /// <summary>
        /// Gets the coordinates of the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Coordinates GetCoordinates(string address);

        /// <summary>
        /// Gets all the address information of an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        AddressInformation GetAddressInformation(string address);

        /// <summary>
        /// Gets all the address information for all results for a specific address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        AddressInformation[] GetAllAddressInformation(string address);
    }
}
