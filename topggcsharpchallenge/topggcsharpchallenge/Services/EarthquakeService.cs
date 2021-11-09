using System;
using System.Collections.Generic;
using System.Linq;

using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    class EarthquakeService : IEarthquakeService
    {
        private readonly IUsgsService usgsService;

        public EarthquakeService(IUsgsService usgsService)
        {
            this.usgsService = usgsService;
        }

        IList<EarthquakeResponseModel> IEarthquakeService.Get(int latitude, int longitude, DateTime startDate, DateTime endDate)
        {
            IList<EarthquakeResponseModel> earthquakeData = usgsService.getEarthquakeData();
            return earthquakeData
                .Where(x => startDate <= x.Time && x.Time <= endDate)
                .Where(x => CalculateDistanceInSphere(x.Latitude, x.Longitude, latitude, longitude) <= x.Mag * Constants.TRAVEL_DISTANCE_FACTOR)
                .Take(Constants.EARTHQUAKE_COUNT_LIMIT)
                .OrderByDescending(x => x.Time)
                .ToList();
        }

        private double CalculateDistanceInSphere(double lat1, double long1, double lat2, double long2, int sphereRadius = Constants.EARTH_RADIUS_MILES)
        {
            int degreesInACircle = 180;
            double lat1rad = lat1 * Math.PI / degreesInACircle;
            double lat2rad = lat2 * Math.PI / degreesInACircle;
            double deltaLat = (lat1 - lat2) * Math.PI / degreesInACircle;
            double deltaLong = (long1 - long2) * Math.PI / degreesInACircle;

            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Pow(Math.Sin(deltaLong / 2), 2) * Math.Cos(lat1rad) * Math.Cos(lat2rad);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            return sphereRadius * c;
        }
    }
}
