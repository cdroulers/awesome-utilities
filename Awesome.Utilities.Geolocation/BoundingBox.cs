using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Geolocation
{
    /// <summary>
    ///     A rectangle shape of coordinates.
    /// </summary>
    public struct BoundingBox
    {
        /// <summary>
        ///     Top left coordinates of the bounding box.
        /// </summary>
        public readonly Coordinates SouthWest;
        /// <summary>
        ///     Bottom right coordinates of the bounding box.
        /// </summary>
        public readonly Coordinates NorthEast;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
        /// </summary>
        /// <param name="southWest">The top left.</param>
        /// <param name="northEast">The bottom right.</param>
        public BoundingBox(Coordinates southWest, Coordinates northEast)
        {
            this.SouthWest = southWest;
            this.NorthEast = northEast;
        }

        /// <summary>
        /// Determines whether the specified coordinates are within the bounding box.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified coordinates]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Coordinates coordinates)
        {
            return (coordinates.Longitude >= this.SouthWest.Longitude &&
                coordinates.Longitude <= this.NorthEast.Longitude) &&
                (coordinates.Latitude >= this.SouthWest.Latitude &&
                coordinates.Latitude <= this.NorthEast.Latitude);
        }
    }
}
