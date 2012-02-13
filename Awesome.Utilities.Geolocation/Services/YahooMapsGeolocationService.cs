using System;
using System.Collections.Generic;
using System.Data.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;

namespace System.Geolocation.Services
{
    /// <summary>
    ///     Yahoo maps geolocation service implementation
    ///     Documentation: http://developer.yahoo.com/geo/placefinder/
    /// </summary>
    public class YahooMapsGeolocationService : GeolocationServiceBase
    {
        private static readonly Uri BaseAddressDefault = new Uri("http://where.yahooapis.com/");

        /// <summary>
        /// Initializes a new instance of the <see cref="YahooMapsGeolocationService"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        public YahooMapsGeolocationService(Uri baseAddress = null)
            : base(baseAddress ?? BaseAddressDefault)
        {
        }

        private UriBuilder GetBuilder(string path, NameValueCollection query)
        {
            query["flags"] = "json";
            var builder = new UriBuilder(this.BaseAddress);
            if (!builder.Path.EndsWith("/"))
            {
                builder.Path += "/";
            }
            builder.Path += path;
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
            foreach (var add in data.ResultSet.Results)
            {
                var components = new List<AddressInformationComponent>();

                string street = null;
                if (add.street != null && add.street is string)
                {
                    street = add.street as string;
                }
                else if (add.street != null && add.street.stfull is string)
                {
                    street = add.street.stfull as string;
                }

                components.Add(new AddressInformationComponent((add.house + " " + street).Trim(), (add.house + " " + street).Trim(), new string[] { AddressPartsNames.StreetAddress }));
                components.Add(new AddressInformationComponent(add.uzip, add.uzip, new string[] { AddressPartsNames.PostalCode }));
                components.Add(new AddressInformationComponent(add.neighborhood, add.neighborhood, new string[] { AddressPartsNames.Neighborhood }));
                components.Add(new AddressInformationComponent(add.city, add.city, new string[] { AddressPartsNames.Locality }));
                components.Add(new AddressInformationComponent(add.county, add.countycode, new string[] { AddressPartsNames.AdministrativeAreaLevel2 }));
                components.Add(new AddressInformationComponent(add.state, add.statecode, new string[] { AddressPartsNames.AdministrativeAreaLevel1 }));
                components.Add(new AddressInformationComponent(add.country, add.countrycode, new string[] { AddressPartsNames.Country }));

                double longitude = double.Parse(add.longitude, CultureInfo.InvariantCulture);
                double latitude = double.Parse(add.latitude, CultureInfo.InvariantCulture);

                string formatted = ((string.IsNullOrWhiteSpace(add.line1) ? string.Empty : add.line1 + ", ") +
                    (string.IsNullOrWhiteSpace(add.line2) ? string.Empty : add.line2 + ", ") +
                    (string.IsNullOrWhiteSpace(add.line3) ? string.Empty : add.line3 + ", ") +
                    add.line4).Trim(',').Trim();

                string type = null;
                if (!string.IsNullOrWhiteSpace(street))
                {
                    type = AddressPartsNames.StreetAddress;
                }
                else if (!string.IsNullOrWhiteSpace(add.uzip))
                {
                    type = AddressPartsNames.PostalCode;
                }
                else
                {
                    type = !string.IsNullOrWhiteSpace(add.city) ? AddressPartsNames.Locality :
                        !string.IsNullOrWhiteSpace(add.county) ? AddressPartsNames.AdministrativeAreaLevel2 :
                        !string.IsNullOrWhiteSpace(add.state) ? AddressPartsNames.AdministrativeAreaLevel1 :
                        AddressPartsNames.Country;
                }

                addresses.Add(new AddressInformation(components.ToArray(), new Coordinates(longitude, latitude), formatted, type));
            }

            return addresses.ToArray();
        }

        private dynamic GetJson(string address)
        {
            var values = new NameValueCollection();
            values["q"] = HttpUtility.UrlEncode(address);
            var builder = this.GetBuilder("geocode", values);

            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string response = client.DownloadString(builder.Uri);
            var data = DynamicJson.Parse(response);
            this.CheckError(data, address);

            return data;
        }

        private void CheckError(dynamic data, string address)
        {
            if (data.ResultSet.Error == 100 && data.ResultSet.Found == 0)
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }
            if (data.ResultSet.Error != 0)
            {
                throw new GeolocationGenericException(string.Format(Properties.Strings.GenericException_YahooMaps, data.ResultSet.Error, data.ResultSet.ErrorMessage));
            }
            if (data.ResultSet.Found == 0)
            {
                throw new AddressNotFoundException(string.Format(Properties.Strings.NoResultsException, address));
            }
        }
    }
}
