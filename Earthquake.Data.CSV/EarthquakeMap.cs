using CsvHelper.Configuration;
using Earthquake.Entities;

namespace Earthquake.Data.CSV
{
    public sealed class EarthquakeMap : ClassMap<EarthquakeEntity>
    {
        public EarthquakeMap()
        {
            Map(m => m.Time).Index(0).Name("time");
            Map(m => m.Latitude).Index(1).Name("latitude");
            Map(m => m.Longitude).Index(2).Name("longitude");
            Map(m => m.Magnitude).Index(4).Name("mag");
        }
    }
}
