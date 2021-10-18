using System;
using System.Threading.Tasks;
using Earthquake.Data.USGS.GeoJSON;

namespace Earthquake.Data.USGS
{
    public interface IUsgsHttpClient
    {
        Task<GeoJson> FindByCoordinatesBetweenDateRangeAsync(double latitude, double longitude, DateTime startDate,
            DateTime endDate, double maxRadiusInKm);
    }
}
