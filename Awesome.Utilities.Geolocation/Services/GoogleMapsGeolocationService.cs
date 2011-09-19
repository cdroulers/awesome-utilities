using System;
using System.Collections.Generic;
using System.Data.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace System.Geolocation.Services
{
    public class GoogleMapsGeolocationService : IGeolocationService
    {
        private static readonly Uri BaseAddressDefault = new Uri("http://maps.googleapis.com/maps/api");

        private readonly Uri baseAddress;

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

        public Coordinates GetCoordinates(string address)
        {
            var values = new NameValueCollection();
            values["address"] = address;
            var builder = this.GetBuilder("geocode/json", values);

            var client = new WebClient();

            string response = client.DownloadString(builder.Uri);
            dynamic data = DynamicJson.Parse(response);

            if (data.results.Count == 0)
            {
                throw new ApplicationException(string.Format(Properties.Strings.NoResultsException, address));
            }
            if (data.results.Count > 1)
            {
                throw new MultipleCoordinatesException(string.Format(Properties.Strings.MultipleCoordinatesException, address));
            }

            double longitude = (double)data.results[0].geometry.location.lng;
            double latitude = (double)data.results[0].geometry.location.lat;

            return new Coordinates(longitude, latitude);
        }
    }
}
