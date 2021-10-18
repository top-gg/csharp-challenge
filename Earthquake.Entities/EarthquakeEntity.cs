using System;

namespace Earthquake.Entities
{
    public class EarthquakeEntity
    {
        public DateTime Time { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Magnitude { get; set; }
    }
}
