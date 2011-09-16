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
        public readonly Coordinates TopLeft;
        public readonly Coordinates BottomRight;

        public BoundingBox(Coordinates topLeft, Coordinates bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public bool Contains(Coordinates coordinates)
        {
            return (coordinates.Longitude >= this.TopLeft.Longitude &&
                coordinates.Longitude <= this.BottomRight.Longitude) &&
                (coordinates.Latitude >= this.TopLeft.Latitude &&
                coordinates.Latitude <= this.BottomRight.Latitude);
        }
    }
}
