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
    ///     Google maps geolocation service implementation
    /// </summary>
    public class GoogleMapsGeolocationService : IGeolocationService
    {
        private static readonly Uri BaseAddressDefault = new Uri("http://maps.googleapis.com/maps/api");

        private readonly Uri baseAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleMapsGeolocationService"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        public GoogleMapsGeolocationService(Uri baseAddress = null)
        {
            this.baseAddress = baseAddress ?? BaseAddressDefault;
        }

        private UriBuilder GetBuilder(string path, NameValueCollection query)
        {
            var builder = new UriBuilder(this.baseAddress);
            if (!builder.Path.EndsWith("/"))
            {
                builder.Path += "/";
            }
            builder.Path += path;
            query["sensor"] = "false";
            builder.Query += string.Join("&", Array.ConvertAll(query.AllKeys, key => string.Format("{0}={1}", key, query[key])));
            return builder;
        }

        /// <summary>
        /// Gets the coordinates of the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Coordinates GetCoordinates(string address)
        {
            var data = this.GetJson(address);

            double longitude = (double)data.results[0].geometry.location.lng;
            double latitude = (double)data.results[0].geometry.location.lat;

            return new Coordinates(longitude, latitude);
        }

        /// <summary>
        /// Gets all the address information of an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public AddressInformation GetAddressInformation(string address)
        {
            var data = this.GetJson(address);

            string formatted = data.results[0].formatted_address;

            var components = new List<AddressInformationComponent>();

            foreach (var c in data.results[0].address_components)
            {
                components.Add(new AddressInformationComponent(c.long_name, c.short_name, (c.types as Collections.ArrayList).ToArray().Select<object, string>(t => t as string).ToArray()));
            }

            return new AddressInformation(components.ToArray(), formatted);
        }

        private dynamic GetJson(string address)
        {
            var values = new NameValueCollection();
            values["address"] = address;
            var builder = this.GetBuilder("geocode/json", values);

            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string response = client.DownloadString(builder.Uri);
            var data = DynamicJson.Parse(response);

            if (data.results.Count == 0)
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }
            if (data.results.Count > 1)
            {
                throw new MultipleCoordinatesException(string.Format(Properties.Strings.MultipleCoordinatesException, address));
            }
            return data;
        }
    }
}
