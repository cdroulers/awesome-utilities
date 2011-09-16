using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace System.Geolocation
{
    [DebuggerDisplay("Coordinates {Longitude} - {Latitude}")]
    public struct Coordinates
    {
        public readonly double Longitude;
        public readonly double Latitude;

        public Coordinates(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public const double EarthRadius = 6371D;

        public double DistanceBetween(Coordinates other)
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
            return d;
        }

        public const double MetersPerDegreeLatitude = 2 * Math.PI * EarthRadius / 360;

        public BoundingBox BoundingBox(double radius)
        {
            var min = new Coordinates(
                this.Longitude - (radius / Math.Abs(Math.Cos(Deg2Rad(this.Latitude)) * MetersPerDegreeLatitude)),
                this.Latitude - (radius / MetersPerDegreeLatitude)
            );
            var max = new Coordinates(
                this.Longitude + (radius / Math.Abs(Math.Cos(Deg2Rad(this.Latitude)) * MetersPerDegreeLatitude)),
                this.Latitude + (radius / MetersPerDegreeLatitude)
            );
            return new BoundingBox(min, max);
        }

        private double Deg2Rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        public bool Equals(Coordinates other)
        {
            return other.Longitude.Equals(this.Longitude) && other.Latitude.Equals(this.Latitude);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(Coordinates)) return false;
            return Equals((Coordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Longitude.GetHashCode() * 397) ^ this.Latitude.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("Longitude: {0}, Latitude: {1}", Longitude, Latitude);
        }
    }
}
