using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Data.Json;
using System.Collections.Specialized;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     MapQuest geolocation service implementation
    /// </summary>
    public class MapQuestGeolocationService : IGeolocationService
    {
        private static readonly Uri BaseAddressDefault = new Uri("http://www.mapquestapi.com/geocoding/v1");

        private readonly Uri baseAddress;
        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestGeolocationService"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="baseAddress">The base address.</param>
        public MapQuestGeolocationService(string key, Uri baseAddress = null)
        {
            this.key = key;
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
            query["key"] = this.key;
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
            var data = this.GetJson(address.RemoveDiacritics()); // MapQuest doesn't like the French, and probably not any other non-English language!

            double longitude = (double)data.results[0].locations[0].latLng.lng;
            double latitude = (double)data.results[0].locations[0].latLng.lat;

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

            string formatted = data.results[0].providedLocation.location;

            var components = new List<AddressInformationComponent>();

            components.Add(new AddressInformationComponent(data.results[0].locations[0].street, data.results[0].locations[0].street, new string[] { "street_number" }));
            components.Add(new AddressInformationComponent(data.results[0].locations[0].postalCode, data.results[0].locations[0].postalCode, new string[] { "postal_code" }));
            components.Add(new AddressInformationComponent(data.results[0].locations[0].adminArea5, data.results[0].locations[0].adminArea5, new string[] { data.results[0].locations[0].adminArea5Type.ToLowerInvariant() }));
            components.Add(new AddressInformationComponent(data.results[0].locations[0].adminArea4, data.results[0].locations[0].adminArea4, new string[] { data.results[0].locations[0].adminArea4Type.ToLowerInvariant() }));
            components.Add(new AddressInformationComponent(data.results[0].locations[0].adminArea3, data.results[0].locations[0].adminArea3, new string[] { data.results[0].locations[0].adminArea3Type.ToLowerInvariant() }));
            components.Add(new AddressInformationComponent(data.results[0].locations[0].adminArea1, data.results[0].locations[0].adminArea1, new string[] { data.results[0].locations[0].adminArea1Type.ToLowerInvariant() }));

            double longitude = (double)data.results[0].locations[0].latLng.lng;
            double latitude = (double)data.results[0].locations[0].latLng.lat;

            return new AddressInformation(components.ToArray(), new Coordinates(longitude, latitude), formatted);
        }

        private dynamic GetJson(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }

            var values = new NameValueCollection();
            values["location"] = address;
            var builder = this.GetBuilder("address", values);

            var client = new WebClient();

            string response = client.DownloadString(builder.Uri);
            dynamic data = DynamicJson.Parse(response);
            this.CheckError(data);

            if (data.results.Count == 1 && data.results[0].locations.Count == 0)
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }
            if (data.results.Count > 1 || data.results[0].locations.Count > 1)
            {
                throw new MultipleCoordinatesException(string.Format(Properties.Strings.MultipleCoordinatesException, address));
            }
            return data;
        }

        private void CheckError(dynamic data)
        {
            if (data.info.statuscode != 0)
            {
                throw new GeolocationGenericException(string.Format(Properties.Strings.GenericException_MapQuest, data.info.statuscode, string.Join(", ", data.info.messages.ToArray())));
            }
        }
    }
}
