using System;
using System.Collections.Generic;
using Earthquake.Entities;

namespace Earthquake.Data
{
    public interface IDataContext
    {
        IEnumerable<EarthquakeEntity> FindByCoordinatesBetweenDateRange(double latitude, double longitude, DateTime startDate, DateTime endDate, byte magnitudeMultiplier);
    }
}