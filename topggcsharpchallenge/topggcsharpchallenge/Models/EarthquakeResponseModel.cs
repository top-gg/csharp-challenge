using System;

namespace topggcsharpchallenge.Models
{
    public class EarthquakeResponseModel
    {
        public DateTime Time { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Depth { get; set; }
        public double Mag { get; set; }
        public string MagType { get; set; }
        public int Nst { get; set; }
        public int Gap { get; set; }
        public double Dmin { get; set; }
        public double Rms { get; set; }
        public string Net { get; set; }
        public string Id { get; set; }
        public DateTime Updated { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public double HorizontalError { get; set; }
        public double DepthError { get; set; }
        public double MagError { get; set; }
        public int MagNst { get; set; }
        public string Status { get; set; }
        public string LocationSource { get; set; }
        public string MagSource { get; set; }
    }
}
