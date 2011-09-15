using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Geolocation.Services
{
    public interface IGeolocationService
    {
        Coordinates GetCoordinates(string address);
    }
}
