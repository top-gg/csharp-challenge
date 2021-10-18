using System;

namespace Earthquake.DTO
{
    public class EarthquakeDto
    {
        public DateTime Time { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Magnitude { get; set; }
    }
}
