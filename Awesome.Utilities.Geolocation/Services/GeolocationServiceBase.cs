using System;
using System.Collections.Generic;
using System.Data.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Base geolocation service implementation
    /// </summary>
    public abstract class GeolocationServiceBase : IGeolocationService
    {
        /// <summary>
        ///     The base address to build the URLs from.
        /// </summary>
        protected readonly Uri BaseAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocationServiceBase"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        protected GeolocationServiceBase(Uri baseAddress)
        {
            Validate.Is.Not.Null(baseAddress, "baseAddress");
            this.BaseAddress = baseAddress;
        }

        /// <summary>
        /// Gets the coordinates of the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public virtual Coordinates GetCoordinates(string address)
        {
            return this.GetAddressInformation(address).Coordinates;
        }

        /// <summary>
        /// Gets all the address information of an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public virtual AddressInformation GetAddressInformation(string address)
        {
            var addresses = this.GetAllAddressInformation(address);
            addresses = this.CheckMultipleResults(address, addresses);
            return addresses.First();
        }

        /// <summary>
        /// Filters the results.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        protected virtual AddressInformation[] FilterResults(AddressInformation[] addresses)
        {
            return addresses;
        }

        private AddressInformation[] CheckMultipleResults(string address, AddressInformation[] addresses)
        {
            var results = this.FilterResults(addresses);
            if (results.Length > 1)
            {
                throw new MultipleCoordinatesException(string.Format(Properties.Strings.MultipleCoordinatesException, address), addresses);
            }
            return results;
        }

        /// <summary>
        /// Gets all the address information for all results for a specific address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public abstract AddressInformation[] GetAllAddressInformation(string address);
    }
}
