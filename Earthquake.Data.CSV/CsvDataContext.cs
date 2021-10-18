using Earthquake.Entities;
using Earthquake.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Earthquake.Data.CSV
{
    public class CsvDataContext : ICsvDataContext
    {
        private readonly IEnumerable<EarthquakeEntity> _earthquakes;

        public CsvDataContext(ICsvParser csvParser)
        {
            try
            {
                _earthquakes = csvParser.Read<EarthquakeEntity, EarthquakeMap>().ToList();
            }
            catch (FileNotFoundException)
            {
                //TODO: we need to log the exception or throw it
            }
            catch (Exception)
            {
                //TODO: we need to log the exception or throw it
            }
        }

        public IEnumerable<EarthquakeEntity> FindByCoordinatesBetweenDateRange(double latitude, double longitude, DateTime startDate, DateTime endDate, byte magnitudeMultiplier)
        {
            try
            {
                // CSVHelper converts UTC time to local,
                // we need to keep it for the performance reasons,
                // so we use ToLocalTime() for our date filters.
                return _earthquakes
                    .OrderByDescending(t => t.Time)
                    .Where(t => Haversine.CalculateDistanceInMiles(latitude, longitude, t.Latitude, t.Longitude) <= t.Magnitude * magnitudeMultiplier && 
                                t.Time.Date >= startDate.Date.ToLocalTime() && t.Time.Date <= endDate.Date.ToLocalTime())
                    .Take(10);
            }
            catch (Exception exception)
            {
                throw new DataContextException("There was an exception with CSV Data Context", exception);
            }

        }
    }
}
