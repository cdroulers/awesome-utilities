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
        public readonly Coordinates TopLeft;
        /// <summary>
        ///     Bottom right coordinates of the bounding box.
        /// </summary>
        public readonly Coordinates BottomRight;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
        /// </summary>
        /// <param name="topLeft">The top left.</param>
        /// <param name="bottomRight">The bottom right.</param>
        public BoundingBox(Coordinates topLeft, Coordinates bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
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
            return (coordinates.Longitude >= this.TopLeft.Longitude &&
                coordinates.Longitude <= this.BottomRight.Longitude) &&
                (coordinates.Latitude >= this.TopLeft.Latitude &&
                coordinates.Latitude <= this.BottomRight.Latitude);
        }
    }
}
