using System;
using System.Collections.Generic;
using System.Data.Json;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Google maps geolocation service implementation
    ///     Documentation: http://code.google.com/apis/maps/documentation/geocoding
    /// </summary>
    public class GoogleMapsGeolocationService : GeolocationServiceBase
    {
        private static readonly Uri BaseAddressDefault = new Uri("http://maps.googleapis.com/maps/api");

        /// <summary>
        ///     When set to true, this parameter will make the service match results together, and only throw exceptions
        ///     when two results of the same type are returned.
        /// </summary>
        public readonly bool IgnoreCloseMatches;

        /// <summary>
        ///     The language to query Google Servers with.
        /// </summary>
        public readonly CultureInfo Language;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleMapsGeolocationService"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="ignoreCloseMatches">if set to <c>true</c> [ignore close matches].</param>
        /// <param name="language">The language for all queries.</param>
        public GoogleMapsGeolocationService(Uri baseAddress = null, bool ignoreCloseMatches = false, CultureInfo language = null)
            : base(baseAddress ?? BaseAddressDefault)
        {
            this.IgnoreCloseMatches = ignoreCloseMatches;
            this.Language = language;
        }

        private UriBuilder GetBuilder(string path, NameValueCollection query)
        {
            var builder = new UriBuilder(this.BaseAddress);
            if (!builder.Path.EndsWith("/"))
            {
                builder.Path += "/";
            }
            builder.Path += path;
            query["sensor"] = "false";
            if (this.Language != null)
            {
                query["language"] = this.Language.Name;
            }
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
            foreach (var add in data.results)
            {
                var components = new List<AddressInformationComponent>();

                foreach (var c in add.address_components)
                {
                    components.Add(new AddressInformationComponent(c.long_name, c.short_name, (c.types as Collections.ArrayList).ToArray().Select(t => t as string).ToArray()));
                }

                double longitude = (double)add.geometry.location.lng;
                double latitude = (double)add.geometry.location.lat;

                addresses.Add(new AddressInformation(components.ToArray(), new Coordinates(longitude, latitude), add.formatted_address, (add.types as Collections.ArrayList).ToArray().FirstOrDefault() as string));
            }

            return addresses.ToArray();
        }

        private dynamic GetJson(string address)
        {
            var values = new NameValueCollection();
            values["address"] = HttpUtility.UrlEncode(address);
            var builder = this.GetBuilder("geocode/json", values);

            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string response = client.DownloadString(builder.Uri);
            var data = DynamicJson.Parse(response);
            this.CheckError(data, address);

            return data;
        }

        private void CheckError(dynamic data, string address)
        {
            if (data.status != "OK")
            {
                if (data.status == "ZERO_RESULTS")
                {
                    throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
                }
                throw new GeolocationGenericException(string.Format(Properties.Strings.GenericException_GoogleMaps, data.status));
            }
        }

        /// <summary>
        /// Filters the results.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        protected override AddressInformation[] FilterResults(AddressInformation[] addresses)
        {
            var results = addresses.Where(s => !s.Components.Any(c => c.Types.Contains(AddressPartsNames.NaturalFeature, StringComparer.InvariantCultureIgnoreCase)));

            if (this.IgnoreCloseMatches)
            {
                if (results.Any(r => r.Type == null || !Priorities.ContainsKey(r.Type)))
                {
                    return addresses;
                }
                // Filter stuffs here.
                foreach (var address in results.ToList())
                {
                    // If there are more than one of the same type, then don't filter.
                    if (results.Count(r => Priorities[r.Type] == Priorities[address.Type]) > 1)
                    {
                        return results.ToArray();
                    }
                }

                results = results.OrderByDescending(r => Priorities[r.Type]).Take(1);
            }

            return results.ToArray();
        }

        private static readonly Dictionary<string, int> Priorities = new Dictionary<string, int>()
        {
            { AddressPartsNames.StreetAddress, 10 },
            { AddressPartsNames.Intersection, 20 },
            { AddressPartsNames.Route, 30 },
            { AddressPartsNames.SubPremise, 40 },
            { AddressPartsNames.University, 45 },
            { AddressPartsNames.Premise, 50 },
            { AddressPartsNames.PostalCode, 60 },
            { AddressPartsNames.NaturalFeature, 70 },
            { AddressPartsNames.Airport, 80 },
            { AddressPartsNames.ColloquialArea, 90 },
            { AddressPartsNames.Park, 100 },
            { AddressPartsNames.PointOfInterest, 110 },
            { AddressPartsNames.Neighborhood, 120 },
            { AddressPartsNames.SubLocality, 130 },
            { AddressPartsNames.Locality, 140 },
            { AddressPartsNames.AdministrativeAreaLevel1, 150 },
            { AddressPartsNames.AdministrativeAreaLevel2, 160 },
            { AddressPartsNames.AdministrativeAreaLevel3, 170 },
            { AddressPartsNames.Country, 180 },
            { AddressPartsNames.Political, 190 },
        };
    }
}
