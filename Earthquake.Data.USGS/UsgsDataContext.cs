using System;
using System.Collections.Generic;
using System.Linq;
using Earthquake.Entities;
using Earthquake.Infrastructure;

namespace Earthquake.Data.USGS
{
    public class UsgsDataContext : IUsgsDataContext
    {
        private readonly IUsgsHttpClient _client;

        public UsgsDataContext(IUsgsHttpClient client)
        {
            _client = client;
        }

        public IEnumerable<EarthquakeEntity> FindByCoordinatesBetweenDateRange(double latitude, double longitude,
            DateTime startDate, DateTime endDate, byte magnitudeMultiplier)
        {
            try
            {
                // Since we cannot filter by magnitude here I have used max magnitude.
                // Calculated from max magnitude ever seen (9.5) * arbitrary magnitude 
                // multiplier in miles converted to KM (1.609344 mil-km constant multiplier)
                // Example: (950 miles ~ 15289 kms)
                var maxRadiusInKm = 9.5 * magnitudeMultiplier * 1.609344;

                var geoJsonResponse = _client
                    .FindByCoordinatesBetweenDateRangeAsync(latitude, longitude, startDate, endDate, maxRadiusInKm)
                    .ConfigureAwait(true)
                    .GetAwaiter().GetResult();

                return geoJsonResponse.Features
                    .Where(t => Haversine.CalculateDistanceInMiles(t.Geometry.Coordinates[1], t.Geometry.Coordinates[0],
                        latitude, longitude) <= t.Properties.Mag * magnitudeMultiplier)
                    .Select(t => new EarthquakeEntity
                    {
                        Longitude = t.Geometry.Coordinates[0],
                        Latitude = t.Geometry.Coordinates[1],
                        Time = DateTimeOffset.FromUnixTimeMilliseconds(t.Properties.Time).LocalDateTime,
                        Magnitude = t.Properties.Mag
                    })
                    .Take(10);
            }
            catch (Exception exception)
            {
                throw new DataContextException("There was an exception with USGS Data Context", exception);
            }
        }
    }
}