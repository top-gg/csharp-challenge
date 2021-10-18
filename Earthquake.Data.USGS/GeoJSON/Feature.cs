namespace Earthquake.Data.USGS.GeoJSON
{
    public class Feature
    {
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
    }
}