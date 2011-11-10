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
    }
}
