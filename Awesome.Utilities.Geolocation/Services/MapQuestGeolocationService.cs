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
    public class MapQuestGeolocationService : GeolocationServiceBase
    {
        private static readonly Uri BaseAddressDefault = new Uri("http://www.mapquestapi.com/geocoding/v1");

        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestGeolocationService"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="baseAddress">The base address.</param>
        public MapQuestGeolocationService(string key, Uri baseAddress = null)
            : base(baseAddress ?? BaseAddressDefault)
        {
            this.key = key;
        }

        private UriBuilder GetBuilder(string path, NameValueCollection query)
        {
            var builder = new UriBuilder(this.BaseAddress);
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
        /// Gets all the address information for all results for a specific address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public override AddressInformation[] GetAllAddressInformation(string address)
        {
            var data = this.GetJson(address);

            var addresses = new List<AddressInformation>();
            foreach (var add in data.results[0].locations)
            {
                var components = new List<AddressInformationComponent>();

                components.Add(new AddressInformationComponent(add.street, add.street, new string[] { "street_number" }));
                components.Add(new AddressInformationComponent(add.postalCode, add.postalCode, new string[] { "postal_code" }));
                components.Add(new AddressInformationComponent(add.adminArea5, add.adminArea5, new string[] { add.adminArea5Type.ToLowerInvariant() }));
                components.Add(new AddressInformationComponent(add.adminArea4, add.adminArea4, new string[] { add.adminArea4Type.ToLowerInvariant() }));
                components.Add(new AddressInformationComponent(add.adminArea3, add.adminArea3, new string[] { add.adminArea3Type.ToLowerInvariant() }));
                components.Add(new AddressInformationComponent(add.adminArea1, add.adminArea1, new string[] { add.adminArea1Type.ToLowerInvariant() }));

                double longitude = (double)add.latLng.lng;
                double latitude = (double)add.latLng.lat;

                string type = null;

                if (!string.IsNullOrEmpty(add.street))
                {
                    type = "street_address";
                }
                else if (!string.IsNullOrEmpty(add.postalCode))
                {
                    type = "postal_code";
                }
                else
                {
                    type = add.adminArea5Type ?? add.add.adminArea4Type ?? add.adminArea3Type ?? add.adminArea1Type;
                }

                addresses.Add(new AddressInformation(components.ToArray(), new Coordinates(longitude, latitude), data.results[0].providedLocation.location, type));
            }

            return addresses.ToArray();
        }

        private dynamic GetJson(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }

            var values = new NameValueCollection();
            values["location"] = HttpUtility.UrlEncode(address);
            var builder = this.GetBuilder("address", values);

            var client = new WebClient();

            string response = client.DownloadString(builder.Uri);
            dynamic data = DynamicJson.Parse(response);
            this.CheckError(data, address);

            return data;
        }

        private void CheckError(dynamic data, string address)
        {
            if (data.info.statuscode != 0)
            {
                throw new GeolocationGenericException(string.Format(Properties.Strings.GenericException_MapQuest, data.info.statuscode, string.Join(", ", data.info.messages.ToArray())));
            }
            if (data.results.Count == 1 && data.results[0].locations.Count == 0)
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }
        }
    }
}
