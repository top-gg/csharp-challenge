using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Earthquake.Entities;
using Earthquake.Infrastructure;

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
            catch (FileNotFoundException fileNotFoundException)
            {
                throw new DataContextException(fileNotFoundException.Message, fileNotFoundException);
            }
            catch (Exception exception)
            {
                throw new DataContextException(exception.Message, exception);
            }
        }

        public IEnumerable<EarthquakeEntity> FindByCoordinatesBetweenDateRange(double latitude, double longitude,
            DateTime startDate, DateTime endDate, byte magnitudeMultiplier)
        {
            try
            {
                // CSVHelper converts UTC time to local,
                // we need to keep it for the performance reasons,
                // so we use ToLocalTime() for our date filters.
                return _earthquakes
                    .OrderByDescending(t => t.Time)
                    .Where(t => Haversine.CalculateDistanceInMiles(latitude, longitude, t.Latitude, t.Longitude) <=
                                t.Magnitude * magnitudeMultiplier &&
                                t.Time >= startDate.ToLocalTime() && t.Time <= endDate.ToLocalTime())
                    .Take(10);
            }
            catch (Exception exception)
            {
                throw new DataContextException("There was an exception with CSV Data Context", exception);
            }
        }
    }
}