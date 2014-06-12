using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Units.Distances;
using System.Units.Distances.Metric;

namespace System.Geolocation
{
    /// <summary>
    ///     Earth coordinates.
    /// </summary>
    [DebuggerDisplay("Coordinates {ToString()}")]
    public struct Coordinates
    {
        /// <summary>
        ///     The longitude
        /// </summary>
        public readonly double Longitude;
        /// <summary>
        ///     The latitude
        /// </summary>
        public readonly double Latitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinates"/> struct.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public Coordinates(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        /// <summary>
        ///     Radius of Earth in kilometers.
        /// </summary>
        public const double EarthRadius = 6371D;

        /// <summary>
        /// Returns the approximate distance between the current coordinates and the specified ones.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Distance DistanceBetween(Coordinates other)
        {
            // Haversine formula: http://en.wikipedia.org/wiki/Haversine_formula

            double dLat = Deg2Rad(other.Latitude - this.Latitude);
            double dLon = Deg2Rad(other.Longitude - this.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(Deg2Rad(this.Latitude)) *
                       Math.Cos(Deg2Rad(other.Latitude)) *
                       Math.Sin(dLon / 2) *
                       Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = EarthRadius * c;
            return new Kilometers((decimal)d);
        }

        /// <summary>
        ///     Meters per degree of latitude.
        /// </summary>
        public const double MetersPerDegreeLatitude = 2 * Math.PI * EarthRadius / 360;

        /// <summary>
        ///     Creates a bounding box from this point extending on all four sides of {radius} kilometers
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <returns></returns>
        public BoundingBox BoundingBox(Distance radius)
        {
            double radiusDouble = (double)radius.ConvertTo<Kilometers>().Value;
            var min = new Coordinates(
                this.Longitude - (radiusDouble / Math.Abs(Math.Cos(Deg2Rad(this.Latitude)) * MetersPerDegreeLatitude)),
                this.Latitude - (radiusDouble / MetersPerDegreeLatitude)
            );
            var max = new Coordinates(
                this.Longitude + (radiusDouble / Math.Abs(Math.Cos(Deg2Rad(this.Latitude)) * MetersPerDegreeLatitude)),
                this.Latitude + (radiusDouble / MetersPerDegreeLatitude)
            );
            return new BoundingBox(min, max);
        }

        private double Deg2Rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        /// <summary>
        /// returns whether the specified coordinates are equal to the current ones.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Coordinates other)
        {
            return other.Longitude.Equals(this.Longitude) && other.Latitude.Equals(this.Latitude);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(Coordinates)) return false;
            return Equals((Coordinates)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Longitude.GetHashCode() * 397) ^ this.Latitude.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Latitude: {0}, Longitude: {1}", this.Latitude, this.Longitude);
        }
    }
}
